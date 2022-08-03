using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PAttachedFile
    {
        public long AttachedFileID { get; set; }
        public long ReferenceID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long TicketID { get; set; }
        public byte[] AttachedFile { get; set; } 
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }


}
