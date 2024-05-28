using System.Net;
using ChatSample.Services.Network.Messages.Client;
using ChatSample.Services.Network.Messages.Server;
using JeeLee.UniNetworking;
using JeeLee.UniNetworking.Transports.Tcp;

namespace ChatSample.Services.Network
{
    public class NetworkService : Service
    {
        private const int Port = 7777;
        
        private readonly TcpServerTransport _serverTransport = new TcpServerTransport();
        private readonly TcpClientTransport _clientTransport = new TcpClientTransport();

        private readonly IServer _server;
        private readonly IClient _client;

        private readonly Dictionary<int, string> _usernames = new Dictionary<int, string>();

        public NetworkService(Program program) : base(program)
        {
            _server = new Server(_serverTransport);
            _client = new Client(_clientTransport);

            _server.ClientDisconnected += ServerClientDisconnected;
            
            _server.Subscribe<CRegisterUser>(ServerOnRegisterUser);
            _server.Subscribe<CChatMessage>(ServerOnChatMessage);
            
            _client.Subscribe<SChatMessage>(ClientOnChatMessage);
        }

        public void Tick()
        {
            _server.Tick();
            _client.Tick();
        }

        public void Host()
        {
            _serverTransport.MaxConnections = 10;
            _serverTransport.Port = Port;
            
            _server.Start();
            Connect("127.0.0.1");
        }

        public void Connect(string ipAddress)
        {
            _clientTransport.IpAddress = ipAddress;
            _clientTransport.Port = Port;

            if (!_client.Connect())
            {
                if (_server.IsRunning)
                {
                    _server.Stop();
                }

                Console.WriteLine("Could not connect to server");
                return;
            }

            var registerMessage = _client.GetMessage<CRegisterUser>();
            registerMessage.Username = Program.ChatService.Username;
            _client.SendMessage(registerMessage);
            
            Console.WriteLine("Connected");
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public void Exit()
        {
            _client.Disconnect();
            _server.Stop();

            _server.ClientDisconnected -= ServerClientDisconnected;
            
            _server.Unsubscribe<CRegisterUser>(ServerOnRegisterUser);
            _server.Unsubscribe<CChatMessage>(ServerOnChatMessage);
            
            _client.Unsubscribe<SChatMessage>(ClientOnChatMessage);
        }

        public void SendChatMessage(string message)
        {
            if (!_client.IsConnected)
            {
                return;
            }

            var chatMessage = _client.GetMessage<CChatMessage>();
            chatMessage.Message = message;
            _client.SendMessage(chatMessage);
        }

        #region Handlers

        private void ServerClientDisconnected(int connectionId)
        {
            _usernames.Remove(connectionId);
        }

        private void ServerOnRegisterUser(CRegisterUser message, int connectionId)
        {
            _usernames.TryAdd(connectionId, message.Username);
        }

        private void ServerOnChatMessage(CChatMessage message, int connectionId)
        {
            if (!_usernames.TryGetValue(connectionId, out var username))
            {
                return;
            }

            var chatMessage = _server.GetMessage<SChatMessage>();
            chatMessage.Message = $"[{username}] {message.Message}";
            _server.SendMessage(chatMessage);
        }

        private void ClientOnChatMessage(SChatMessage message)
        {
            Program.ChatService.SendChatMessage(message.Message);
        }

        #endregion
    }
}