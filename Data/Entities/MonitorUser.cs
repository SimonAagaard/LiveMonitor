using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    //Most properties needed for a user is provided by Identity, in this model class we only add properties to the existing ones
    public class MonitorUser : IdentityUser<Guid>
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public List<Dashboard> Dashboards { get; set; }
        public List<Integration> Integrations { get; set; }
    }
}