namespace ChatSample.Commands
{
    public class HostCommand : ICommand
    {
        public string[] Keys { get; } = { "host", "start" };
        public string Description => "Starts a new chat session";
        public string[] Arguments { get; } = {};

        public void Execute(params string[] args)
        {
        }
    }
}