using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class DashboardSetting : BaseEntity
    {
        public DashboardSetting()
        {
            //Default values
            XLabel = "Choose label";
            YLabel = "Choose label";
            RefreshRate = 15;
        }
        [Key]
        public Guid DashboardSettingId { get; set; }
        [Required]
        [ForeignKey("DashboardId")]
        public Guid DashboardId { get; set; }
        public Dashboard Dashboard { get; set; }
        [Required]
        public Guid DashboardTypeId { get; set; }
        [Required]
        [Display(Name = "Refresh rate")]
        public int RefreshRate { get; set; }
        [Required]
        [Display(Name = "X-axis")]
        [MaxLength(128)]
        public string XLabel { get; set; }
        [Required]
        [Display(Name = "Y-axis")]
        [MaxLength(128)]
        public string YLabel { get; set; }
    }
}