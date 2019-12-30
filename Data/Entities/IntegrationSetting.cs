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
        [StringLength(36)]
        public string ClientId { get; set; }
        [StringLength(32)]
        public string ClientSecret { get; set; }
        [StringLength(36)]
        public string TenantId { get; set; }
        [StringLength(255)]
        public string ResourceId { get; set; }
        [StringLength(510)]
        public string ResourceUrl { get; set; }
        public bool IsActive { get; set; }
        public string MetricName { get; set; }
        public string Aggregation { get; set; }
        public string Interval { get; set; }
        [Range(-1440, -1)]
        public int MinutesOffset { get; set; }
        public List<DataSet> DataSets { get; set; }
        public List<BearerToken> BearerTokens { get; set; }
    }
}