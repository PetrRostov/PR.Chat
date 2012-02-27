using System;
using System.Diagnostics;

namespace PR.Chat.Infrastructure
{
    public static class Log
    {
        private static ILog GetLog()
        {
            return IoC.Resolve<ILog>();
        }

        [DebuggerStepThrough]
        public static void Info(string format, params object[] args)
        {
            Require.NotNullOrEmpty(format, "format");
            GetLog().Info(format, args);
        }

        [DebuggerStepThrough]
        public static void Debug(string format, params object[] args)
        {
            Require.NotNullOrEmpty(format, "format");
            GetLog().Debug(format, args);
        }

        [DebuggerStepThrough]
        public static void Error(string format, params object[] args)
        {
            Require.NotNullOrEmpty(format, "format");
            GetLog().Error(format, args);
        }

        [DebuggerStepThrough]
        public static void Error(Exception e)
        {
            Require.NotNull(e, "e");
            GetLog().Error(e);
        }
    }
}