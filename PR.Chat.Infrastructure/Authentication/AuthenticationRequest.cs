﻿using System.Collections.Generic;

namespace PR.Chat.Core.Authentication
{
    public class AuthenticationRequest
    {
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();

        public IDictionary<string, object> Properties { get { return _properties; }}
    }
}