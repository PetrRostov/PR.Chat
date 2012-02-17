namespace PR.Chat.Domain
{
    public interface IMessageSender
    {
        Message SendMessageTo(IMessageReceiver receiver);
    }
}