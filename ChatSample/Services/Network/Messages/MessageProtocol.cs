namespace ChatSample.Services.Network.Messages
{
    public static class MessageProtocol
    {
        #region Server Messages

        public const int SChatMessage = 101;

        #endregion

        #region Client Messages

        public const int CRegisterUser = 201;
        public const int CChatMessage = 202;

        #endregion
    }
}