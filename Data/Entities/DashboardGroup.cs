using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class DashboardGroup : BaseEntity
    {
        public DashboardGroup()
        {
            //Default values
            GroupRefreshRate = 15;
        }
        [Key]
        public Guid DashboardGroupId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public Guid UserId { get; set; }
        public MonitorUser MonitorUser { get; set; }
        public List<Dashboard> Dashboards { get; set; }

        [Required]
        [Display(Name = "Refresh rate")]
        public int GroupRefreshRate { get; set; }
        [Required]
        public string DashboardGroupName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}