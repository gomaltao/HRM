using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace HRM.Patterns
{
    public class APIResult<T> : IResult<T> 
    {
        private readonly bool _success;
        private readonly string _message;
        private readonly T _returnValue;
        private readonly Exception _exception;

        public APIResult(T returnValue, bool success, string message, Exception exception)
        {
            _returnValue = returnValue;
            _success = success;
            _message = message;
            _exception = exception;
        }

        public bool Success
        {
            get { return _success; }
        }

        public string Message
        {
            get { return _message; }
        }
        public T ReturnValue
        {
            get { return _returnValue; }
        }

        public Exception Exception
        {
            get { return _exception; }

        }
        }
    }
