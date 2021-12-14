using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
   public class PApplicationSetting
    {
        public Int16 AppSettingsID { get; set; }
        public String SettingsName { get; set; }
        public String SettingsValue { get; set; }
        public bool SettingsBoolValue { get; set; }
        public String SettingsDescription { get; set; }
    }
}
