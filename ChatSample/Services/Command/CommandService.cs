using ChatSample.Services.Command.Domain;

namespace ChatSample.Services.Command
{
    public class CommandService : Service
    {
        private readonly IEnumerable<ICommand> _commands;

        public CommandService(Program program) : base(program)
        {
            _commands = ConstructCommands();
        }

        public bool TryRunCommand(string input)
        {
            string[] args = SanitizeInput(input.Split()).ToArray();
            if (args.Length <= 0 || !args[0].StartsWith("/"))
            {
                return false;
            }

            args[0] = args[0].Substring(1);
            var command = _commands.FirstOrDefault(command => command.Keys.Contains(args[0]));
            
            if (command == null)
            {
                return false;
            }

            command.Execute(args);

            return true;
        }

        private IEnumerable<string> SanitizeInput(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (i <= 0)
                {
                    yield return input[i].Trim().ToLower();
                    continue;
                }

                yield return input[i].Trim();
            }
        }

        private IEnumerable<ICommand> ConstructCommands()
        {
            yield return new HelpCommand(_commands);
            yield return new SetUsernameCommand(username => Program.ChatService.Username = username);
            yield return new ExitCommand(() => Program.Stop());
        }
    }
}