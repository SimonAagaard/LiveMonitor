using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Integration : BaseEntity
    {
        [Key]
        public Guid IntegrationId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public MonitorUser MonitorUser { get; set; }
        public Guid IntegrationSettingId { get; set; }
        public string IntegrationName { get; set; }
        public IntegrationSetting IntegrationSetting { get; set; }
        public DashboardSetting DashboardSetting { get; set; }
    }
}