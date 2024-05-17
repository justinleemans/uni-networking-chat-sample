namespace ChatSample.Commands
{
    public class ConnectCommand : ICommand
    {
        public string[] Keys { get; } = { "connect", "join" };
        public string Description => "Connect to a chat session";
        public string[] Arguments { get; } = { "<IPADDRESS>" };

        public void Execute(params string[] args)
        {
        }
    }
}