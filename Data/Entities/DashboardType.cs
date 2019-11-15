using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class DashboardType : BaseEntity
    {
        [Key]
        public Guid DashboardTypeId { get; set; }
       
        public Type DashboardName { get; set; }
        public enum Type
        {
            TreeDiagram,
            VennDiagram,
            Flowchart
        }
    }
}
