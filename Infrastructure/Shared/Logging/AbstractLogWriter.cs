using Kernel.Logging;
namespace Shared.Logging
{
    public abstract class AbstractLogWriter : ILogWriter
    {
        public abstract void WriteExeption(object o);
    }
}
