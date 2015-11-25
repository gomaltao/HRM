using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace HRM.Patterns
{
    public class CollectionResult<T> : ICollectionResult<T> 
    {
        private readonly bool _success;
        private readonly string _message;
        private readonly IEnumerable<T>  _returnValue;
        private readonly Exception _exception;

        public CollectionResult(bool success, IEnumerable<T> returnValue,  string message, Exception exception)
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
        public IEnumerable<T> ReturnValue
        {
            get { return _returnValue; }
        }

        public Exception Exception
        {
            get { return _exception; }

        }
        }
    }
