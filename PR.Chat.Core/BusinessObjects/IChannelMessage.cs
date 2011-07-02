namespace PR.Chat.Core.BusinessObjects
{
    public interface IChannelMessage : IMessage
    {
        IChannel Channel { get; }
    }
}