using System;
using System.Collections.Generic;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models
{
    public class EntityDescriptorSettings : BaseModel
    {
        public EntityDescriptorSettings()
        {
            this.RoleDescriptors = new List<RoleDescriptorSettings>();
        }

        public string EntityId { get; set; }
        public DateTimeOffset ValidUntil { get; set; }
        public int CacheDuration { get; set; }
        public virtual OrganisationSettings Organisation { get; set; }
        public ICollection<RoleDescriptorSettings> RoleDescriptors { get; }
    }
}