using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class DashboardGroup : BaseEntity
    {
        public DashboardGroup()
        {
            //Default values
            GroupRefreshRate = 15;
        }
        [Key]
        public Guid DashboardGroupId { get; set; }
        [Required]
        [ForeignKey("DashboardId")]
        public Guid DashboardId { get; set; }
        public Dashboard Dashboard { get; set; }
        [Required]
        [Display(Name = "Refresh rate")]
        public int GroupRefreshRate { get; set; }
        [Required]
        public string DashboardGroupName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}