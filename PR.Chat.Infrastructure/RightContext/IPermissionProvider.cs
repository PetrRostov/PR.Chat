namespace PR.Chat.Infrastructure.RightContext
{
    public interface IPermissionProvider
    {
        IPermission GetPermission(string name);
    }
}