namespace PR.Chat.Core.BusinessObjects
{
    public interface IBusinessObjectFactory
    {
        IAccount CreateAccount(string email);

        IChannel CreateChannel(string name, bool isHidden, bool isTemporary);

        IChannelMessage CreateChannelMessage(INick from, string text, IChannel channel);

        IPrivateMessage CreatePrivateMessage(INick from, string text, INick to);

        INick CreateNick(string name);
    }
}