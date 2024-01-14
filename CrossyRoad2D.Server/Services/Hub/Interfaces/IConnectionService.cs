using CrossyRoad2D.Common.MessageContents;

namespace CrossyRoad2D.Server.Services.Hub.Interfaces
{
    public interface IConnectionService
    {
        void AddNewConnection(string connectionId);
        void GiveAdmin(string connectionId, LoginMessageContent loginMessage);
        bool IsConnectionAdmin(string connectionId);
        void RemoveConnection(string connectionId);
        List<string> GetAllConnectionIds();
    }
}