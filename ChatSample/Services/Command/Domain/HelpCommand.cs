namespace ChatSample.Services.Command.Domain
{
    public class HelpCommand : ICommand
    {
        public string[] Keys { get; } = { "help" };
        public string Description => "Shows a list of all available commands";
        public string[] Arguments { get; } = {};

        private readonly IEnumerable<ICommand> _commands;

        public HelpCommand(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public void Execute(params string[] args)
        {
            foreach (var command in _commands)
            {
                if (command.Arguments.Any())
                {
                    Console.WriteLine($"[{string.Join('|', command.Keys)}] {command.Description}, arguments {string.Join(' ', command.Arguments)}");
                }
                else
                {
                    Console.WriteLine($"[{string.Join('|', command.Keys)}] {command.Description}");
                }
            }
        }
    }
}