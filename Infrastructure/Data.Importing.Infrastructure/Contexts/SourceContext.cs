using System;
using Kernel.Data.DataRepository;

namespace Data.Importing.Infrastructure.Contexts
{
    public class SourceContext
    {
        private Lazy<IReadOnlyRepository<ImportedEntry, Guid>> _source;
        public IReadOnlyRepository<ImportedEntry, Guid> Source
        {
            get
            {
                return this._source.Value;
            }
        }

        public SourceContext(Func<IReadOnlyRepository<ImportedEntry, Guid>> source)
        {
            this._source = new Lazy<IReadOnlyRepository<ImportedEntry, Guid>>(source);
        }
    }
}