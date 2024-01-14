using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Models;
using CrossyRoad2D.Server.Services.Hub.Interfaces;

namespace CrossyRoad2D.Server.Services.Hub
{
    public class ConnectionService : IConnectionService
    {
        private List<Connection> _connections = new();

        public void AddNewConnection(string connectionId)
        {
            _connections.Add(new()
            {
                Id = connectionId
            });
        }

        public void RemoveConnection(string connectionId)
        {
            _connections.RemoveAll(connection => connection.Id == connectionId);
        }

        public bool IsConnectionAdmin(string connectionId)
        {
            return _connections.FirstOrDefault(connection => connection.Id == connectionId)?.IsAdmin ?? false;
        }

        public void GiveAdmin(string connectionId, LoginMessageContent loginMessage)
        {
            var connection = _connections.FirstOrDefault(connection => connection.Id == connectionId);

            if (connection != null)
            {
                connection.IsAdmin = true;
            }
        }

        public List<string> GetAllConnectionIds()
        {
            return _connections.Select(x => x.Id).Distinct().ToList();
        }
    }
}
