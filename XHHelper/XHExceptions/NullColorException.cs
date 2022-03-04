using System;

namespace XHHelper.XHExceptions
{
    public class NullColorException: Exception
    {
        public string _message;
        public NullColorException(string message)
        {
            _message= message;
        }
    }
}
