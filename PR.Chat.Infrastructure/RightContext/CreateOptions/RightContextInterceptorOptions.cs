using System.Collections.Generic;

namespace PR.Chat.Infrastructure.RightContext
{
    public class RightContextInterceptorOptions
    {
        public RuleHolderOptions RuleHolderOptions { get; set; }

        public string Permission { get; set; }

        public IEnumerable<ArgumentOptions> Arguments { get; set; }
    }
}