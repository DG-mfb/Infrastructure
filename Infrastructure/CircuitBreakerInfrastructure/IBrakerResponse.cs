using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public interface IBrakerResponse
    {
        object Result { get; }
        Type ResultType { get; }
        State BrakerState { get; }
        string ProjectedError { get; }
        Exception Exception { get; }
    }
}
