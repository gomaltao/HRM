using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Patterns
{
    public class ICollectionResult<T>
    {
        bool Success { get; }
        string Message { get; }
        IEnumerable<T> ReturnValue { get; }
        Exception Exception { get; }



    }
}
