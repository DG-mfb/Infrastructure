using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Cryptography.CertificateManagement
{
    public class CertificateStore<TStore>
    {
        public TStore Store { get; }

        public CertificateStore(TStore store)
        {
            this.Store = store;
        }
    }
}