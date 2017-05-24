using Kernel.Messaging.Response;

namespace Data.Importing.Infrastructure
{
    public class StageResult : AbstractResponse
    {
        public object Result { get; private set; }

        public bool IsCompleted { get; }

        public bool IsResultValid { get; private set; }

        public StageResult(object result)
        {
            this.Result = result;
        }

        public void Validated()
        {
            this.IsResultValid = true;
        }
    }
}