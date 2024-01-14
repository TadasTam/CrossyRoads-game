using CrossyRoad2D.Common.MessageContents;

namespace CrossyRoad2D.Server.Services.Hub.Interfaces
{
    public interface IPlayerService
    {
        void AddNewPlayer(string connectionId);
        void ClonePlayer(string connectionId, PlayerCloneMessageContent cloneMessage);
        void PlayerCopyDeath(string connectionId, PlayerCopyDeathMessageContent playerCopyDeathMessage);
        void RemovePlayer(string connectionId);
        void UpdatePlayer(string connectionId, PlayerUpdateMessageContent updateMessage);
        void KillPlayer(string connectionId);
        void Reset(string connectionId);
        void Speed(string connectionId);
    }
}