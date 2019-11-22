using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class BearerToken : BaseEntity
    {
        [Key]
        public Guid BearerTokenId { get; set; }
        public string AccessToken { get; set; }
        [ForeignKey("IntegrationSettingId")]
        [Required]
        public Guid IntegrationSettingId { get; set; }
        public IntegrationSetting IntegrationSetting { get; set; }
        public DateTime DateCreated { get; set; }
    }
}