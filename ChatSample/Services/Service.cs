namespace ChatSample.Services
{
    public abstract class Service
    {
        protected Program Program { get; }

        protected Service(Program program)
        {
            Program = program;
        }
    }
}