using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PNotifications
    {
        public int NotificationId { get; set; }
        public string NotificationCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
        public string Subject { get; set; }
        public string AlertCode { get; set; }
    }
}
