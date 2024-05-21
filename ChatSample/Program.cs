using ChatSample.Services.Chat;
using ChatSample.Services.Command;

namespace ChatSample
{
    public class Program
    { 
        private const int TicksPerSecond = 20;
        private const int SleepTime = 1000 / TicksPerSecond;

        public CommandService CommandService { get; }
        public ChatService ChatService { get; }

        public bool IsRunning { get; set; }
        
        private static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
        }
        
        private Program()
        {
            CommandService = new CommandService(this);
            ChatService = new ChatService(this);
        }

        private void Start()
        {
            IsRunning = true;

            var update = new Thread(Update);
            update.Start();

            ChatService.SendWelcomeMessage();

            while (IsRunning)
            {
                var input = Console.ReadLine();

                if (input == null || CommandService.TryRunCommand(input))
                {
                    continue;
                }
            }
        }

        private void Update()
        {
            while (IsRunning)
            {
                Thread.Sleep(SleepTime);
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}