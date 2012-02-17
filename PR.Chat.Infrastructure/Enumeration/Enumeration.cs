using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PR.Chat.Infrastructure.Enumeration
{
    public abstract class Enumeration : IComparable
    {
        private readonly string _displayName;
        private readonly string _value;

        protected Enumeration(string value)
        {
            _value = value;
            _displayName = value;
        }

        protected Enumeration(string value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public string Value
        {
            get { return _value; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        #region IComparable Members

        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }

        #endregion

        public override string ToString()
        {
            return DisplayName;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields
                .Select(info => info.GetValue(null))
                .OfType<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static T FromValue<T>(string value) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }
}