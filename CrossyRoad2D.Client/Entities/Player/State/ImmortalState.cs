using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Player.State
{
    public class ImmortalState : IState
    {
        private PlayerEntity _player { get; set; }
        private double _runOutOn;

        const double IMMORTAL_STATE_DURATION = 2.0;

        public ImmortalState(PlayerEntity player)
        {
            _player = player;
            _runOutOn = TimeState.Instance.TimeSecondsSinceStart + IMMORTAL_STATE_DURATION;
        }

        public void ConsumeHeart()
        {
            _player.State = new ExtraLifeState(_player);
        }

        public void CheckCollisionsWithLethalCollidables()
        {
            
        }

        public void UpdatePlayerState()
        {
            if (_runOutOn < TimeState.Instance.TimeSecondsSinceStart)
            {
                _player.State = new NormalState(_player);
            }
        }

        public string GetTimeRemainingString()
        {
            return $"Transitional: {(int)Math.Ceiling(_runOutOn - TimeState.Instance.TimeSecondsSinceStart)}";
        }
    }
}
