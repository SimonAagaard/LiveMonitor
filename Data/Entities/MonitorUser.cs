using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    //Most properties needed for a user is provided by Identity, in this model class we only add properties to the existing ones
    public class MonitorUser : IdentityUser<Guid>, IEntity
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public List<Dashboard> Dashboards { get; set; }
        public List<Integration> Integrations { get; set; }
    }
}