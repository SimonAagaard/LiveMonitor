using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class IntegrationSetting : BaseEntity
    {
        [Key]
        public Guid IntegrationSettingId { get; set; }
        [Required]
        [ForeignKey("IntegrationId")]
        public Guid IntegrationId { get; set; }
        public Integration Integration { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string TenantID { get; set; }
        public List<DataSet> DataSets { get; set; }
        public List<BearerToken> BearerTokens { get; set; }
    }
}