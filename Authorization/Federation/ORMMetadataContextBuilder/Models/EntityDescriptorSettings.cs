using System;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models
{
    public class EntityDescriptorSettings : BaseModel
    {
        public string EntityId { get; set; }
        public DateTimeOffset ValidUntil { get; set; }
        public int CacheDuration { get; set; }
        public virtual OrganisationSettings Organisation { get; set; }
    }
}