﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class DataSet : BaseEntity
    {
        [Key]
        public Guid DataSetId { get; set; }
        [Required]
        [ForeignKey("IntegrationSettingId")]
        public Guid IntegrationSettingId { get;  set; }
        public IntegrationSetting IntegrationSetting { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        //Assumes our x-axis will always measure in time
        [Required]
        public DateTime XValue { get; set; }
        [Required]
        public int YValue { get; set; }
    }
}
