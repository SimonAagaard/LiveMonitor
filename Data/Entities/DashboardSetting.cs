using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class DashboardSetting
    {
        public Guid DashboardSettingId { get; set; }
        public Guid DashboardId { get; set; }
        public Guid DashboardTypeId { get; set; }
        public int RefreshRate { get; set; }
        public string XLabel { get; set; }
        public string YLabel { get; set; }
    }
}
