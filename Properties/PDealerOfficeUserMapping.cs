using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PDealerOfficeUserMapping
    {
        public PUser User { get; set; }
        public Boolean IsActive { get; set; }
    }
}