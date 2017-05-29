﻿using System;
using Kernel.Data;

namespace Data.Importing.Infrastructure
{
    public class ImportedEntry : IHasID<Guid>
    {
        public ImportedEntry(object value) : this(Guid.NewGuid(), value)
        {

        }
        public ImportedEntry(Guid id, object value)
        {
            this.ID = id;
            this.Value = value;
        }

        public Guid ID { get; set; }
        public object Value { get; private set; }
        public Type Type
        {
            get
            {
                return this.Value.GetType();
            }
        }
    }
}