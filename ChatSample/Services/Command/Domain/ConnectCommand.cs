namespace ChatSample.Services.Command.Domain
{
    public class ConnectCommand : ICommand
    {
        public string[] Keys { get; } = { "connect", "join" };
        public string Description => "Connect to a chat session";
        public string[] Arguments { get; } = { "<IPADDRESS>" };

        private Action<string> _callback;
        
        public ConnectCommand(Action<string> callback)
        {
            _callback = callback;
        }

        public void Execute(params string[] args)
        {
            if (args.Length < 2 || args[1] == string.Empty)
            {
                Console.WriteLine("No ip address supplied");
                return;
            }

            _callback(args[1]);
        }
    }
}