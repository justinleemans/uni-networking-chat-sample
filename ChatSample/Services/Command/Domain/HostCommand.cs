namespace ChatSample.Services.Command.Domain
{
    public class HostCommand : ICommand
    {
        public string[] Keys { get; } = { "host", "start" };
        public string Description => "Starts a new chat session";
        public string[] Arguments { get; } = {};

        private Action _callback;

        public HostCommand(Action callback)
        {
            _callback = callback;
        }

        public void Execute(params string[] args)
        {
            _callback();
        }
    }
}