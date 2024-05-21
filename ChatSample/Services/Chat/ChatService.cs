namespace ChatSample.Services.Chat
{
    public class ChatService : Service
    {
        public string Username { get; set; } = "Guest";

        public ChatService(Program program) : base(program)
        {
        }

        public void SendWelcomeMessage()
        {
            Console.WriteLine("UniNetworking chat sample app");
            Console.WriteLine("View this project on https://github.com/justinleemans/uni-networking-chat-sample");
            Console.WriteLine("View the UniNetworking library on https://github.com/justinleemans/uni-networking");
            Console.WriteLine("To run a command, start your input with '/' directly followed by the command you want to send");
        }

        public void SendChatMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}