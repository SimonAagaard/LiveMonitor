using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Integration
    {
        public Guid IntegratonId { get; set; }
        public Guid UserId { get; set; }
        public Guid IntegrationSettingId { get; set; }
        public string Name { get; set; }
    }
}
