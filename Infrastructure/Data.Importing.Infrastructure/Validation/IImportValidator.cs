using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Validation;

namespace Data.Importing.Infrastructure.Validation
{
    public interface IImportValidator : IValidator
    {
        void Validate(ValidationContext context, ImportState state);
    }
}