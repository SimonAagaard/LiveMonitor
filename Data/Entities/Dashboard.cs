using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Dashboard
    {
        public string DashboardName { get; set; }
        public Guid UserId { get; set; }
        public Guid DashboardId { get; set; }
        public Guid DashboardSettingId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
