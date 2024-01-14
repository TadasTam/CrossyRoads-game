using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Services.Hub;
using CrossyRoad2D.Server.Services.Hub.Interfaces;

namespace CrossyRoad2D.Server.HubServiceProxies
{
    public class ConnectionServiceProxy : IConnectionService
    {
        private IConnectionService _baseConnectionService;
        private ILogger<ConnectionServiceProxy> _logger;
        private string _adminLoginKey;
        private IChatService _chatService;

        public ConnectionServiceProxy(
            IConnectionService connectionService,
            ILogger<ConnectionServiceProxy> logger,
            IConfiguration configuration,
            IChatService chatService)
        {
            _baseConnectionService = connectionService;
            _logger = logger;
            _adminLoginKey = configuration.GetValue<string>("AdminLoginKey");
            _chatService = chatService;
        }

        public void AddNewConnection(string connectionId)
        {
            _baseConnectionService.AddNewConnection(connectionId);
        }

        public void GiveAdmin(string connectionId, LoginMessageContent loginMessage)
        {
            if(_adminLoginKey != loginMessage.Password)
            {
                _chatService.MessageToConnection(connectionId, 
                    new ChatMessageToConnectionMessageContent() { 
                        Content = "/login [password]: Incorrect password" 
                    });

                _logger.LogInformation($"{connectionId} has tried to login as an admin");
                return;
            }

            _baseConnectionService.GiveAdmin(connectionId, loginMessage);

            _chatService.MessageToConnection(connectionId,
                    new ChatMessageToConnectionMessageContent()
                    {
                        Content = "/login [password]: You are now an admin"
                    });
            _logger.LogInformation($"Admin was given for connection {connectionId}");
        }

        public bool IsConnectionAdmin(string connectionId)
        {
            return _baseConnectionService.IsConnectionAdmin(connectionId);
        }

        public void RemoveConnection(string connectionId)
        {
            _baseConnectionService.RemoveConnection(connectionId);
        }

        public List<string> GetAllConnectionIds()
        {
            return _baseConnectionService.GetAllConnectionIds();
        }
    }
}
