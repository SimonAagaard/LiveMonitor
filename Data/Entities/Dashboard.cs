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
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
