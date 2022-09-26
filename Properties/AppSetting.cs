using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public  class PApplicationSettings
    {
        public int SettingID { get; set; }
        public string Name { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; } 
    }
    static public class PApplication
    {
        static public string Success { get { return ("Success"); } }
        static public string Failure { get { return ("Failed"); } }
    }
}
