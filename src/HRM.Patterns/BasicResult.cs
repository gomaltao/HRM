using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Patterns
{
    public class BasicResult : IBasicResult 
    {
        private readonly bool _success;
        private readonly string _message;
        private readonly Exception _exception;

        public BasicResult(bool success, string message, Exception exception)
        {
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

        public Exception Exception
        {
            get { return _exception; }
        }

    }
}
