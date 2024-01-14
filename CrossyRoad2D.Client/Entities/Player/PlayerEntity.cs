using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Singletons;
using System;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System.Windows.Controls;
using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Components;
using CrossyRoad2D.Client.Entities.Strategy;
using CrossyRoad2D.Client.Decorator;
using CrossyRoad2D.Client.Entities.Player;
using CrossyRoad2D.Client.Facade;
using CrossyRoad2D.Client.Entities.Player.State;
using CrossyRoad2D.Client.NetworkSync;

namespace CrossyRoad2D.Client.Entities
{
    public class PlayerEntity : Entity, INetworkSyncedEntity
    {
        public bool IsOfCurrentClient { get; init; }
        public double MinMoveActionIntervalSeconds { get; set; } = 0.3;
        public ServerPlayer ServerPlayer { get; set; } = new ServerPlayer();
        public bool CanWalkThroughObjects { get; set; } = false;
        public bool IsOnSandBlocks { get; set; } = false;
        public bool UpdatedServerPlayerThisFrame { get; set; } = false;
        public double LastMoveActionTime { get; private set; } = 0.0;
        public IPlayerRender FrogDecorated { get; set; }
        public IState State { get; set; }
        public MoveAlgorithm MoveAlgorithm { get; private set; }
        public bool SyncedToNetwork { get; set; }

        private FrogXamlComponent _frogXaml { get; set; } = new FrogXamlComponent();
        private UnaryAnimationComponent _frogScaleAnimation { get; init; } = new(
            animationDuration: 0.2,
            fastScaleTime: 0.05,
            initialValue: 1.0,
            maxValue: 1.3);
        private Score _score;

        private double _changeSpecialAlgorithmOn;
        private const double SPECIAL_ALGORITHM_DURATION = 10.0;

        public PlayerEntity(bool isOfCurrentClient, ServerPlayer serverPlayer, bool syncedToNetwork) : base(EntityType.Player)
        {
            RenderPriority = EntityRenderPriority.Players;
            ServerPlayer.Color = Color.White;
            IsOfCurrentClient = isOfCurrentClient;
            ServerPlayer = serverPlayer;
            State = new NormalState(this);
            _score = new Score(ServerPlayer.Position.Y);
            FrogDecorated = isOfCurrentClient ? DecorateClientPlayerEntity() : DecorateOtherPlayerEntity();
            SetMoveAlgo(new DefaultAlgorithm());
            _changeSpecialAlgorithmOn = 0.0;
            SyncedToNetwork = syncedToNetwork;
        }

        private IPlayerRender DecorateClientPlayerEntity()
        {
            FrogDecorated = _frogXaml;
            FrogDecorated = new PlayerRenderHighscoreDecorator(FrogDecorated, _score);

            if (ServerPlayer.IsBadCopy)
            {
                FrogDecorated = new PlayerRenderBadCopyDecorator(FrogDecorated);
            }
                
            FrogDecorated = new PlayerRenderHeartDecorator(FrogDecorated, this);
            return FrogDecorated;
        }

        private IPlayerRender DecorateOtherPlayerEntity()
        {
            FrogDecorated = _frogXaml;
            FrogDecorated = new PlayerRenderHighscoreDecorator(FrogDecorated, _score);
            FrogDecorated = new PlayerRenderNameDecorator(FrogDecorated, ServerPlayer.PlayerServerId);
            return FrogDecorated;
        }

        public override void Render(Canvas canvas)
        {
            _frogXaml.Color = ServerPlayer.Color;
            _frogXaml.Rectangle = new Rectangle(ServerPlayer.Position);
            _frogXaml.Scale = _frogScaleAnimation.GetAnimatedValue();
            FrogDecorated.Render(canvas);
        }

        public override void PrioritizedUpdate()
        {
            if(_changeSpecialAlgorithmOn > TimeState.Instance.TimeSecondsSinceStart)
            {
                MoveAlgorithm = new DefaultAlgorithm();
            }

            State.CheckCollisionsWithLethalCollidables();
            State.UpdatePlayerState();
        }

        public void SetPosition(Position position)
        {
            LastMoveActionTime = TimeState.Instance.TimeSecondsSinceStart;
            UpdatedServerPlayerThisFrame = true;
            _frogScaleAnimation.StartAnimation();
            UpdateRotation(ServerPlayer.Position, position);
            ServerPlayer.Position = position;
            _score.Set(ServerPlayer.Position.Y);
            FacadeUtils.Instance.ConsumeItems(position, this);
        }

        public string GetPlayerServerId()
        {
            return ServerPlayer.PlayerServerId;
        }

        public int GetPlayerEntityCopyId()
        {
            return ServerPlayer.PlayerCopyId;
        }

        public bool Move(double offsetX, double offsetY, bool ignoreInterval = false)
        {
            return MoveAlgorithm.TemplateMethod(offsetX, offsetY, this, ignoreInterval);
        }

        private void UpdateRotation(Position oldPosition, Position newPosition)
        {
            var xStepSign = Math.Sign(newPosition.X - oldPosition.X);
            var yStepSign = Math.Sign(newPosition.Y - oldPosition.Y);

            _frogXaml.Rotation = $"X:{xStepSign} Y:{yStepSign}" switch
            {
                "X:0 Y:1" => 0.0,
                "X:1 Y:0" => 90.0,
                "X:0 Y:-1" => 180.0,
                "X:-1 Y:0" => 270.0,
                _ => _frogXaml.Rotation
            };
        }

        public void PerformCloneDeep()
        {
            FacadeUtils.Instance.ClonePlayer(CloneDeep());
        }

        public void PerformCloneShallow()
        {
            FacadeUtils.Instance.ClonePlayer(CloneShallow());
        }

        public override PlayerEntity CloneDeep()
        {
            int nextPlayerId = FacadeUtils.Instance.GetNextCloneId(ServerPlayer.PlayerServerId);
            ServerPlayer serverPlayer = CloneServerPlayer(nextPlayerId, false);
            PlayerEntity copy = new PlayerEntity(IsOfCurrentClient, serverPlayer, syncedToNetwork: false);
            return copy;
        }

        public override PlayerEntity CloneShallow()
        {
            PlayerEntity copy = (PlayerEntity)MemberwiseClone();
            int nextPlayerId = FacadeUtils.Instance.GetNextCloneId(ServerPlayer.PlayerServerId);
            copy.ServerPlayer = CloneServerPlayer(nextPlayerId, true);
            copy._frogXaml = new FrogXamlComponent();
            copy.FrogDecorated = copy.DecorateClientPlayerEntity();
            copy.IncrementShallowCopyId();
            copy.SyncedToNetwork = false;
            return copy;
        }

        private ServerPlayer CloneServerPlayer(int playerId, bool isBadCopy)
        {
            Random rand = new Random();
            int x = rand.Next((int)Math.Floor(ServerPlayer.Position.X - 1), (int)Math.Floor(ServerPlayer.Position.X + 2));
            int y = rand.Next((int)Math.Floor(ServerPlayer.Position.Y - 3), (int)Math.Floor(ServerPlayer.Position.Y + 4));
            if (x == ServerPlayer.Position.X && y == ServerPlayer.Position.Y) x += 2;
            return new ServerPlayer { PlayerServerId = ServerPlayer.PlayerServerId, Color = ServerPlayer.Color, Position = new Position(x, y), PlayerCopyId = playerId, IsBadCopy = isBadCopy };
        }

        public void SetMoveAlgo(MoveAlgorithm newAlgorithm)
        {
            if (MoveAlgorithm is ISpecialAlgorithm)
            {
                Console.WriteLine("Cannot change algorithm while a special algorithm is active.");
                return;
            }

            MoveAlgorithm = newAlgorithm;

            if (newAlgorithm is ISpecialAlgorithm)
            {
                string label = newAlgorithm is PotionAlgorithm ? "Walls" : "Speed";
                FrogDecorated = new PlayerRenderSpellDecorator(FrogDecorated, label, DateTime.Now.AddSeconds(10));

                _changeSpecialAlgorithmOn = TimeState.Instance.TimeSecondsSinceStart + SPECIAL_ALGORITHM_DURATION;
            }
        }

        public void Accept(INetworkSyncedEntityVisitor visitor)
        {
            visitor.VisitPlayer(this);
        }
    }
}
