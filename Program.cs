using ChatSample.Commands;
using JeeLee.UniNetworking;

namespace ChatSample
{
    public class Program
    { 
        private const int TicksPerSecond = 20;
        private const int SleepTime = 1000 / TicksPerSecond;

        private readonly IEnumerable<ICommand> _commands;

        private bool _isRunning;
        private string _username = "Guest";
        
        private static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
        }
        
        private Program()
        {
            _commands = ConstructCommands();
        }

        private void Start()
        {
            _isRunning = true;

            var update = new Thread(Update);
            update.Start();

            SendWelcomeMessage();

            while (_isRunning)
            {
                var input = Console.ReadLine();

                if (input == null || TryRunCommand(SanitizeInput(input.Split()).ToArray()))
                {
                    continue;
                }

                SendMessage(input);
            }
        }

        private void Update()
        {
            while (_isRunning)
            {
                Thread.Sleep(SleepTime);
            }
        }

        private void Stop()
        {
            _isRunning = false;
        }

        private void SetUsername(string username)
        {
            _username = username;
        }

        private void SendWelcomeMessage()
        {
            Console.WriteLine("UniNetworking chat sample app");
            Console.WriteLine("View this project on https://github.com/justinleemans/uni-networking-chat-sample");
            Console.WriteLine("View the UniNetworking library on https://github.com/justinleemans/uni-networking");
            Console.WriteLine("To run a command, start your input with '/' directly followed by the command you want to send");
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

        private bool TryRunCommand(params string[] args)
        {
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

        private void SendMessage(string message)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine($"[{_username}] {message}");
        }

        private IEnumerable<ICommand> ConstructCommands()
        {
            yield return new HelpCommand(_commands);
            yield return new SetUsernameCommand(SetUsername);
            yield return new ExitCommand(Stop);
        }
    }
}