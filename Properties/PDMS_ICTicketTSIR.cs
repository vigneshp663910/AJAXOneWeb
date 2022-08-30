using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_ICTicketTSIR
    {
        public long TsirID { get; set; }
        public string TsirNumber { get; set; }
        public DateTime TsirDate { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public PDMS_ServiceCharge ServiceCharge { get; set; }
        public PDMS_Material Material { get; set; }
        public List<PDMS_ServiceMaterial> SMaterials { get; set; }
        // public string FailureRepeats { get; set; }
        public string NatureOfFailures { get; set; }
        public string ProblemNoticedBy { get; set; }
        public string UnderWhatConditionFailureTaken { get; set; }
        public string FailureDetails { get; set; }
        public string PointsChecked { get; set; }
        public string PossibleRootCauses { get; set; }
        public string SpecificPointsNoticed { get; set; }
        // public string SERecommendedParts { get; set; }

        public string QualityComments { get; set; }
        public PUser QualityCommentsBy { get; set; }
        public DateTime? QualityCommentsOn { get; set; }

        public string ServiceComments { get; set; }
        public PUser ServiceCommentsBy { get; set; }
        public DateTime? ServiceCommentsOn { get; set; }

        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public PDMS_ICTicketTSIRStatus Status { get; set; }
        public string StatusRemarks { get; set; }
        public string PartsInvoiceNumber { get; set; }

        public List<PDMS_ICTicketTSIRMessage> TSIRMessageS { get; set; }
        public PDMS_ICTicketTSIRMessage TSIRMessage { get; set; }

        public Decimal? Sales1ApproveAmount   { get; set; }
        public Decimal? Sales2ApproveAmount { get; set; }
    }
    [Serializable]
    public class PDMS_ICTicketTSIR_API
    {
        public long TsirID { get; set; }
        public long ICTicketID { get; set; }
        public long ServiceChargeID { get; set; }
        public string NatureOfFailures { get; set; }
        public string ProblemNoticedBy { get; set; }
        public string UnderWhatConditionFailureTaken { get; set; }
        public string FailureDetails { get; set; }
        public string PointsChecked { get; set; }
        public string PossibleRootCauses { get; set; }
        public string SpecificPointsNoticed { get; set; }

        public string QualityComments { get; set; } 
        public DateTime? QualityCommentsOn { get; set; }

        public string ServiceComments { get; set; } 
        public DateTime? ServiceCommentsOn { get; set; }

        public string StatusRemarks { get; set; }
        public string PartsInvoiceNumber { get; set; }
    }
    [Serializable]
    public class PDMS_ICTicketTSIRStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class PDMS_ICTicketTSIRMessage
    {
        public long TSIRMessageID { get; set; }
        public long TsirID { get; set; }
        public string TSIRMessage { get; set; }
        public Boolean DisplayToDealer { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    [Serializable]
    public class PDMS_TSIRAttachedFile
    {
        public long AttachedFileID { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public PDMS_ICTicketTSIR TSIR { get; set; }
        public long FsrID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public PDMS_FSRAttachedName FSRAttachedName { get; set; }
    }

    [Serializable]
    public class PDMS_TSIRAttachedFile__M
    {
        public long AttachedFileID { get; set; }
        public long ICTicketID { get; set; }
        public long TsirID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public PDMS_FSRAttachedName FSRAttachedName { get; set; }
    }

}
