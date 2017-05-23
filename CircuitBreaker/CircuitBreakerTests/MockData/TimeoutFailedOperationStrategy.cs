﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreakerTests.MockData
{
    public class TimeoutFailedOperationStrategy : FailedOperationStrategy
    {
        public override void Apply(FailedOperationContext context, Action<FailedOperationContext> next)
        {
            next(context);
        }
    }
}
