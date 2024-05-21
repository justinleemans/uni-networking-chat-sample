using JeeLee.UniNetworking.Messages;
using JeeLee.UniNetworking.Messages.Attributes;
using JeeLee.UniNetworking.Messages.Payloads;

namespace ChatSample.Services.Network.Messages.Client
{
    [Message(MessageProtocol.CRegisterUser)]
    public class CRegisterUser : Message
    {
        public string Username { get; set; }

        protected override void OnClear()
        {
            Username = default;
        }

        protected override void OnSerialize(IWriteablePayload payload)
        {
            payload.WriteString(Username);
        }

        protected override void OnDeserialize(IReadablePayload payload)
        {
            Username = payload.ReadString();
        }
    }
}