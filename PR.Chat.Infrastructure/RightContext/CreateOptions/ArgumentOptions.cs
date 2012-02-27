namespace PR.Chat.Infrastructure.RightContext
{
    public class ArgumentOptions
    {
        public int ArgumentPosition { get; set; }

        public int ParameterPosition { get; set; }

        public bool IsMethodOwner { get; set; }

        public bool IsExecutor { get; set; }
    }
}