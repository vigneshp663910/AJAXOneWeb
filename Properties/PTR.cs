using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
   public class PTR
    {
       public int ID { get; set; }
       public int? TicketId { get; set; }
       public string TRNumber { get; set; }
       public string MailNote { get; set; }
       public PCategory Category { get; set; }
       public PSubCategory SubCategory { get; set; }
       public string Purpose { get; set; }
       public PUser CreatedBy { get; set; }
       public DateTime CreatedOn { get; set; }
       public PUser ApprovedBy { get; set; }
       public DateTime? ApprovedOn { get; set; }
       public int TRMovedBy { get; set; }
       public DateTime? TRMovedOn { get; set; }

       public PUser ChangeApprovedBy { get; set; }
       public DateTime? ChangeApprovedOn { get; set; }
       public PUser UATBy { get; set; }
       public DateTime? UATOn { get; set; }

       public string Status { get; set; }

       public PUser RequestedBy { get; set; }
       public DateTime? RequestedOn { get; set; }

       public PUser DevelopedBy { get; set; }
       public DateTime? DevelopedOn { get; set; }
    }
}
