using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class DashboardType : BaseEntity
    {
        public DashboardType()
        {

        }
        [Key]
        public Guid DashboardTypeId { get; set; }
       
        //Might need to be refactored down the line, could be made a list instead
        public Type DashboardTypeValue{ get; set; }
        public enum Type
        {
            LineChart,
            AreaChart,
        }
    }
}
