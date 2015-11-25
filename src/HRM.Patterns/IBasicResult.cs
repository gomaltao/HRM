using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Patterns
{
public interface     IBasicResult
    {
        bool Success { get; }
        string Message { get; }
        Exception Exception { get; }
}
}
