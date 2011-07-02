namespace PR.Chat.Core.BusinessObjects
{
    public interface IPrivateMessage : IMessage
    {
        INick To { get; }
    }
}