namespace PR.Chat.Application
{
    public interface IMapping<in TFrom, out TTo>
    {
        TTo Convert(TFrom from);
    }
}