using System;

namespace XHHelper.XHExceptions
{
    public class FigureColorProcessingException: Exception
    {
        public string _message;
        public Exception _exception;
        public FigureColorProcessingException(string message)
        {
            _message = message;
        }
        public FigureColorProcessingException(string message, Exception exception)
        {
            _message = message;
            _exception = exception;
        }
    }
}
