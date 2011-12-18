namespace PR.Chat.Application
{
    public interface IMappingProvider
    {
        IMapping<TFrom, TTo> GetMapping<TFrom, TTo>();
    }
}