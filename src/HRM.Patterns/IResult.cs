using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace HRM.Patterns
{
    public interface  IResult<T> 
    {

        bool Success { get; }
string Message { get; }
        T ReturnValue { get; }
        Exception Exception { get; }
    }
}
        