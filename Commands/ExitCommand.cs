namespace ChatSample.Commands
{
    public class ExitCommand : ICommand
    {
        public string[] Keys { get; } = { "exit", "close" };
        public string Description => "Closes the application";
        public string[] Arguments { get; } = {};

        private Action _callback;

        public ExitCommand(Action callback)
        {
            _callback = callback;
        }

        public void Execute(params string[] args) => _callback();
    }
}