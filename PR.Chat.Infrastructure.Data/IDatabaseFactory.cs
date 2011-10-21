namespace PR.Chat.Infrastructure.Data
{
    public interface IDatabaseFactory
    {
        IDatabase Create();
    }
}