using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class DashboardType
    {
        public Guid DashboardTypeId { get; set; }
        public Enum Name { get; set; }
    }
}
