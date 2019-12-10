using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Dashboard : BaseEntity
    {
        [Required]
        [Display(Name = "Dashboard name")]
        [MaxLength(128)]
        public string DashboardName { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public Guid UserId { get; set; }
        public MonitorUser MonitorUser { get; set; }
        [Key]
        public Guid DashboardId { get; set; }
        [Required]
        [ForeignKey("DashboardSettingId")]
        public Guid DashboardSettingId { get; set; }
        public DashboardSetting DashboardSetting { get; set; }
        public List<DashboardGroup> DashboardGroups { get; set; }
        [Required]
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
