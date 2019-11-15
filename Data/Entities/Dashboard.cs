using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Dashboard
    {
        [Required]
        [MaxLength(128)]
        public string DashboardName { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public string UserId { get; set; }
        public MonitorUser MonitorUserFK { get; set; }
        [Key]
        public Guid DashboardId { get; set; }
        [Required]
        public Guid DashboardSettingId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
