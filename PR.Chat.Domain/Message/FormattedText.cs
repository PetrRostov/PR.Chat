using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    public class FormattedText : IValueObject<FormattedText>
    {
        private string _text;
        public string Text
        {
            get { return _text; }
        }

        public bool SameValueAs(FormattedText other)
        {
            if (other == null)
                return false;

            return Text.Equals(other.Text);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;


            return SameValueAs((FormattedText) obj);
        }
    }
}