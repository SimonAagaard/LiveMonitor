using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class DataSet
    {
        //Assumes our x-axis will always measure in time
        public DateTime XValue { get; set; }
        public int YValue { get; set; }
        public Guid IntegrationSettingId { get; set; }
        public Guid DataSetId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
