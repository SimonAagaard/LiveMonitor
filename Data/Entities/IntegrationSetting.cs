using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class IntegrationSetting
    {
        public Guid IntegrationSettingId { get; set; }
        public Guid IntegrationId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantID { get; set; }

    }
}
