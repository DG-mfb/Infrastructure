using Kernel.Messaging.Response;

namespace Data.Importing.Infrastructure
{
    public class StageResult : AbstractResponse
    {
        public object Result { get; private set; }

        public bool IsCompleted { get; }

        public StageResult(object result)
        {
            this.Result = result;
        }
    }
}