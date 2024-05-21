namespace ChatSample.Commands
{
    public interface ICommand
    {
        string[] Keys { get; }
        string Description { get; }
        string[] Arguments { get; }

        void Execute(params string[] args);
    }
}