using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class MonitorUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
    }
}
