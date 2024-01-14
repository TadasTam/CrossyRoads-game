using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CrossyRoad2D.Client.NetworkSync
{
    public class NetworkState
    {
        private static NetworkState _instance;
        public static NetworkState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetworkState();
                }
                return _instance;
            }
        }

        private HubConnection _hubConnection;
        private ConcurrentQueue<NetworkMessage> _networkMessagesJustReceived = new ConcurrentQueue<NetworkMessage>();
        private List<NetworkMessage> _networkMessagesForProcessing = new List<NetworkMessage>();

        private NetworkState()
        {

        }

        public void Connect()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5206/gamehub")
                .AddJsonProtocol()
                .Build();

            _hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _hubConnection.StartAsync();
            };

            foreach (var networkMessageName in Enum.GetNames(typeof(NetworkMessageKind)))
            {
                _hubConnection.On<object>(networkMessageName, (value) =>
                {
                    _networkMessagesJustReceived.Enqueue(new NetworkMessage()
                    {
                        Kind = (NetworkMessageKind)Enum.Parse(typeof(NetworkMessageKind), networkMessageName),
                        Content = (JsonElement)value
                    });
                });
            }

            _hubConnection.StartAsync();
        }

        public void UpdateBeforeProcessing()
        {
            while (_networkMessagesJustReceived.TryDequeue(out NetworkMessage request))
            {
                _networkMessagesForProcessing.Add(request);
            }
        }

        public void UpdateAfterProcessing()
        {
            _networkMessagesForProcessing.Clear();
        }

        public IEnumerable<T> GetMessagesOfKind<T>(NetworkMessageKind kind)
        {
            return _networkMessagesForProcessing
                .Where(networkMessage => networkMessage.Kind == kind)
                .Select(networkMessage =>
                {
                    T content = JsonSerializer.Deserialize<T>(networkMessage.Content.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                    return content;
                });
        }

        public void SendMessage<T>(NetworkMessageKind kind, T content)
        {
            _hubConnection.SendAsync(Enum.GetName(kind), content);
        }

        public void Disconnect()
        {
            _hubConnection.StopAsync();
        }
    }
}
