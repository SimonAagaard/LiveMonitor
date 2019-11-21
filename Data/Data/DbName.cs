using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Data
{
   public class DbName
    {
        public string ConnectionName { get; set; }
        public DbName()
        {
            //Change value to the Db you wish to access
            ConnectionName = "LiveMonitorChristoffer";
        }
     
    }
}
