using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Integration
    {
        [Key]
        public Guid IntegrationId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public MonitorUser MonitorUser { get; set; }
        public Guid IntegrationSettingId { get; set; }
        public string IntegrationName { get; set; }
        public IntegrationSetting IntegrationSetting { get; set; }
    }
}
