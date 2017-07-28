using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.CQRS.Transport
{
    public interface ITransportManager
    {
        Task Initialise();
        Task Start();
        Task Stop();
    }
}