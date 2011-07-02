using System;

namespace PR.Chat.Core.BusinessObjects
{
    [Serializable]
    public partial class NickSettings : IPropertyChangedNotifier
    {
        private Color _color;
        public Color Color { 
            get
            {
                return _color;
            }
            set
            {
                if (Equals(_color, value)) return;

                _color = value;
                OnPropertyChanged("Color");
            }
        }

        private FontSize _fontSize = FontSize.Medium;
        public FontSize FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if (Equals(_fontSize, value)) return;

                _fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }

        [NonSerialized]
        public static readonly NickSettings Default = new NickSettings {
            Color       = new Color {Red = 0, Green = 0, Blue = 0},
            FontSize    = FontSize.Medium
        };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgument(propertyName));
        }

        public event EventHandler<PropertyChangedEventArgument> PropertyChanged;
    }
}