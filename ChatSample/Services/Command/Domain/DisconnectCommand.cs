namespace ChatSample.Services.Command.Domain
{
    public class DisconnectCommand : ICommand
    {
        public string[] Keys { get; } = { "disconnect", "leave" };
        public string Description => "Disconnect form a chat session";
        public string[] Arguments { get; } = {};

        private Action _callback;
        
        public DisconnectCommand(Action callback)
        {
            _callback = callback;
        }

        public void Execute(params string[] args)
        {
            _callback();
        }
    }
}