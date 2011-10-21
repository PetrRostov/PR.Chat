using System;

namespace PR.Chat.Infrastructure
{
    public interface ILog
    {
        void Info(string format, params object[] args);

        void Debug(string format, params object[] args);

        void Error(string format, params object[] args);

        void Error(Exception e);
    }
}