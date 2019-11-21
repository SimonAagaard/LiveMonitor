using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class BearerToken
    {
        public string AccessToken { get; set; }
        public MonitorUser MonitorUser { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}