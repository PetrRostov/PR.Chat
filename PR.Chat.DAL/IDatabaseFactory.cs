namespace PR.Chat.DAL
{
    public interface IDatabaseFactory
    {
        IDatabase Create();
    }
}