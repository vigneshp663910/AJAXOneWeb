using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PTask_Insert
    {
        public int HeaderID { get; set; }
        public int CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public string Subject { get; set; }
        public Boolean Repeat { get; set; }
        public int TicketTypeID { get; set; }
        public int SeverityID { get; set; }
        public string Description { get; set; }
        public int? ActualCreater { get; set; }
        public string MobileNo { get; set; }
        public string ContactName { get; set; }
        public int? PriorityLevel { get; set; }
        public int? UATBy { get; set; }
        public List<PAttachedFile> AttchedFile { get; set; }
    }
    public class PTaskItem_Insert
    {
        public int HeaderID { get; set; }
        public int ItemID { get; set; }
        public int? SubCategoryID { get; set; }
        public int? SeverityID { get; set; }
        public int? AssignedTo { get; set; }
        public string AssignerRemark { get; set; }
        public Decimal? ActualDuration { get; set; }
        
        public string SupportType { get; set; }
        public List<PAttachedFile> AttchedFile { get; set; }

        public Decimal? Effort { get; set; }
        public int? ResolutionType   { get; set; }
        public string Resolution { get; set; }
    }
    public class PForum_Insert
    {
        public int HeaderID { get; set; }
        public string Message { get; set; }
        public PAttachedFile AttchedFile { get; set; }
    }
    public class PTicketHeader
    {
        public int HeaderID { get; set; }
        public PCategory Category { get; set; }
        public PSubCategory SubCategory { get; set; }
        public string Subject { get; set; }
        public Boolean Repeat { get; set; }
        public PSeverity Severity { get; set; }
        public PType Type { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public PStatus Status { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser ActualCreater { get; set; }
        public int age { get; set; }
        public DateTime? ClosedBy { get; set; }
        public DateTime? ClosedOn { get; set; }
        public PTicketItem TicketItem { get; set; }
        public List<PTicketItem> TicketItems { get; set; }
        public PTicketsApprovalDetails ApprovalDetail { get; set; }
        public List<PTicketsApprovalDetails> ApprovalDetails { get; set; }
        public string MobileNo { get; set; }
        public string ContactName { get; set; }
        public int? PriorityLevel { get; set; }
        public string WithInSLA1 { get; set; }
        //public PTR TR { get; set; }

        public PUser UATBy { get; set; }

        public DateTime? UATOn { get; set; }
        public string UATRemark { get; set; }
        public string SLA { get; set; }
    }
    public class PTicketItem
    {
        public int ItemID { get; set; }
        public int HeaderID { get; set; }
        public int ParentID { get; set; }
        public PStatus ItemStatus { get; set; }

        public PUser AssignedTo { get; set; }
        public PUser AssignedBy { get; set; }
        public DateTime AssignedOn { get; set; }
        public string AssignerRemark { get; set; }
        public Decimal? ActualDuration { get; set; }
        public Decimal? Effort { get; set; }
        public PResolutionType ResolutionType { get; set; }
        public string Resolution { get; set; }
        //public Boolean? NewTR { get; set; }
        //public string TRNumber { get; set; }
        //public Boolean? TRClosed { get; set; }
        //public Boolean? TRClosedOn { get; set; }
        public int age { get; set; }
        public string WithInSLA1 { get; set; }
        public string WithInSLA2 { get; set; }
        public Boolean? Approved { get; set; }

        public DateTime? InProgressOn { get; set; }
        public DateTime? ResolvedOn { get; set; }
        public Boolean InActive { get; set; }
    }
    public class PForum
    {
        public long ID { get; set; }
        //public int ItemID { get; set; }
        public int HeaderID { get; set; }
        // public int ParentID { get; set; }
        //public PStatus ItemStatus { get; set; }
        public PUser FromUser { get; set; }
        //  public PUser To { get; set; }
        public string Message  { get; set; }
        public DateTime CreatedOn { get; set; }
        public int  FileTypeID   { get; set; }
        public string FileType { get; set; }
    }
    public class PTicketsApprovalDetails
    {
        public int Id { get; set; }
        public int TicketID { get; set; }
        public PUser Approver { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public Boolean? IsAppoved { get; set; }
        public PUser RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public string RequestedRemark { get; set; }
        public string ApproverRemark { get; set; }
        public PUser RejectedBy { get; set; }
        public DateTime? RejectedOn { get; set; }
        public Boolean InActive { get; set; }
    }
}
