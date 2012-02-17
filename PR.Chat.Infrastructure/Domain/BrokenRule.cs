namespace PR.Chat.Infrastructure
{
    public class BrokenRule<T>
    {
        public ISpecification<T> Specification { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}