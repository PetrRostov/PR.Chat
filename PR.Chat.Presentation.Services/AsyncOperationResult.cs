using System;
using System.Threading;

namespace PR.Chat.Presentation.Services
{
    public class AsyncOperationResult : IAsyncResult
    {
        public string Value { get; set; }

        public bool IsCompleted
        {
            get; set;
        }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public object AsyncState { get; set; }

        public bool CompletedSynchronously
        {
            get { return false; }
        }
    }
}