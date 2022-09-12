using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PHelp
    {
        public int DocumentAttachmentID { get; set; }
        public string Sno { get; set; }
        public string Description { get; set; }
        public string PDFAttachment { get; set; }
        public string PPSAttachment { get; set; }
        public string VideoLink { get; set; }
        public int OrderNo { get; set; }
        public Boolean IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
