namespace PR.Chat.Core.BusinessObjects
{
    public interface INick : IEntity
    {
        string Name { get; }

        NickSettings Settings { get; }
    }
}