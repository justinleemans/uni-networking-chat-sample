namespace ChatSample.Commands
{
    public class SetUsernameCommand : ICommand
    {
        public string[] Keys { get; } = { "name", "username" };
        public string Description => "Set your chat username, default = Guest";
        public string[] Arguments { get; } = { "<NAME>" };

        private Action<string> _callback;
        
        public SetUsernameCommand(Action<string> callback)
        {
            _callback = callback;
        }

        public void Execute(params string[] args)
        {
            if (args.Length < 2 || args[1] == string.Empty)
            {
                Console.WriteLine("No username supplied");
            }

            _callback(args[1]);
            Console.WriteLine("Username set");
        }
    }
}