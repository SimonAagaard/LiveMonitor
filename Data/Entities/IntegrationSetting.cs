using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class IntegrationSetting
    {
        [Key]
        public Guid IntegrationSettingId { get; set; }
        [ForeignKey("IntegrationId")]
        public Guid IntegrationId { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string TenantID { get; set; }

    }
}
