using System;

namespace Data.Importing.Infrastructure.Contexts
{
    public class TargetContext
    {
        public Type ParseTo { get; set; }
        public Type MapTo { get; private set; }
    }
}