using System;

namespace PR.Chat.Infrastructure.Castle.Tests
{
    public class ClassA
    {
        private string _innerString;
        
        public ClassA(string innerString)
        {
            _innerString = innerString;
        }

        public ClassA()
        {
            
        }

        
        public virtual string GetInnerString()
        {
            return _innerString;
        }


        public virtual int MethodForIntercept1(string arg1, Guid arg2)
        {
            return 1;
        }

        public virtual string MethodForIntercept2(int i, string str)
        {
            return str + i.ToString();
        }
    }
}