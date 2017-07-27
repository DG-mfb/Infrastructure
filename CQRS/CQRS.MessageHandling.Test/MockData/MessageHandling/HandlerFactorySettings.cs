using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kernel.CQRS.MessageHandling;

namespace CQRS.MessageHandling.Test.MockData.MessageHandling
{
    internal class HandlerFactorySettingsMock : IHandlerFactorySettings
    {
        public IEnumerable<Assembly> LimitAssembliesTo
        {
            get
            {
                yield return this.GetType().Assembly;
            }
        }
    }
}
