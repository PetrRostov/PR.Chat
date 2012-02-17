namespace PR.Chat.Domain
{
    public interface IRoomBuilder
    {
        Room BuildRoom(string name, string description, bool isTemporary);
    }
}