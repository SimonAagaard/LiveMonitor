using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class DataSet
    {
        [Key]
        public Guid DataSetId { get; set; }
        [ForeignKey("IntegrationSettingId")]
        public Guid IntegrationSettingId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        //Assumes our x-axis will always measure in time
        public DateTime XValue { get; set; }
        public int YValue { get; set; }
    }
}
