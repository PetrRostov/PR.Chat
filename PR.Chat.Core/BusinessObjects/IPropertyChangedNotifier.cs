using System;

namespace PR.Chat.Core.BusinessObjects
{
    public interface IPropertyChangedNotifier
    {
        event EventHandler<PropertyChangedEventArgument> PropertyChanged;
    }

    public class PropertyChangedEventArgument : EventArgs
    {
        public PropertyChangedEventArgument(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}