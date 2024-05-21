using JeeLee.UniNetworking.Messages;
using JeeLee.UniNetworking.Messages.Attributes;
using JeeLee.UniNetworking.Messages.Payloads;

namespace ChatSample.Services.Network.Messages.Client
{
    [Message(MessageProtocol.CChatMessage)]
    public class CChatMessage : Message
    {
        public string Message { get; set; }

        protected override void OnClear()
        {
            Message = default;
        }

        protected override void OnSerialize(IWriteablePayload payload)
        {
            payload.WriteString(Message);
        }

        protected override void OnDeserialize(IReadablePayload payload)
        {
            Message = payload.ReadString();
        }
    }
}