using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PDeviationProcess
    {
        public long DeviationProcessID { get; set; }
        public PUser RequestedBy { get; set; }
        public DateTime? RequestedOn { get; set; }
        public PUser ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public string Subject { get; set; }
        public string Remarks { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
    public class PDeviationProcess_Insert
    {
        public long DeviationProcessID { get; set; }
        public int RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public string Subject { get; set; }
        public string Remarks { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
    }
}