using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PMachineOperator
    {
        public long MachineOperatorDetailsID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public PMachineOperatorAttachedFile Photo { get; set; }
        public DateTime? DOB { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber1 { get; set; }
        public string Email { get; set; }
        public PDMS_EqucationalQualification EqucationalQualification { get; set; }
        public decimal? TotalExperience { get; set; }
        public string Address { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string AadhaarCardNo { get; set; }
        public PMachineOperatorAttachedFile AdhaarCardCopyFrontSide { get; set; }
        public PMachineOperatorAttachedFile AdhaarCardCopyBackSide { get; set; }
        public string PANNo { get; set; }
        public PMachineOperatorAttachedFile PANCardCopy { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public PMachineOperatorAttachedFile ChequeCopy { get; set; }
        public string DLNumber { get; set; }
        public DateTime? DLIssueDate { get; set; }
        public string DLIssueingOffice { get; set; }
        public DateTime? DLExpiryDate { get; set; }
        public string DLFor { get; set; }
        public PMachineOperatorAttachedFile DLFrontSide { get; set; }
        public PMachineOperatorAttachedFile DLBackSide { get; set; }
        public Boolean IsAjaxHPApproved { get; set; }
        public PUser CreatedBy { get; set; }
        public string EmergencyContactNumber { get; set; }
        public PDMS_BloodGroup BloodGroup { get; set; }
        public Boolean IsActive { get; set; }
        public Int32 UserID { get; set; }
        public List<PMachineOperatorProductTypes> ProductTypes { get; set; }
    }
    [Serializable]
    public class PMachineOperatorAttachedFile
    {
        public long AttachedFileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int32 UserID { get; set; }
    }
    [Serializable]
    public class PMachineOperatorProductTypes
    {
        public long MachineOperatorDetailsID { get; set; }
        public PProductType ProductType { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PMachineOperator_Insert
    {
        public long MachineOperatorDetailsID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber1 { get; set; }
        public string Email { get; set; }
        public decimal? TotalExperience { get; set; }
        public string Address { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string AadhaarCardNo { get; set; }
        public string PANNo { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string DLNumber { get; set; }
        public DateTime? DLIssueDate { get; set; }
        public string DLIssueingOffice { get; set; }
        public DateTime? DLExpiryDate { get; set; }
        public string DLFor { get; set; }
        public string EmergencyContactNumber { get; set; }
        public Int32 UserID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int TehsilID { get; set; }
        public int EqucationalQualificationID { get; set; }
        public int BloodGroupID { get; set; }
        public PMachineOperatorAttachedFile_Insert Photo { get; set; }
        public PMachineOperatorAttachedFile_Insert AdhaarCardCopyFrontSide { get; set; }
        public PMachineOperatorAttachedFile_Insert AdhaarCardCopyBackSide { get; set; }
        public PMachineOperatorAttachedFile_Insert PANCardCopy { get; set; }
        public PMachineOperatorAttachedFile_Insert ChequeCopy { get; set; }
        public PMachineOperatorAttachedFile_Insert DLFrontSide { get; set; }
        public PMachineOperatorAttachedFile_Insert DLBackSide { get; set; }
        public List<PMachineOperatorProductTypes_Insert> ProductTypes { get; set; }
    }
    [Serializable]
    public class PMachineOperatorAttachedFile_Insert
    {
        public long AttachedFileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public Int32 UserID { get; set; }
    }
    [Serializable]
    public class PMachineOperatorProductTypes_Insert
    {
        public int ProductTypeID { get; set; }
        public Boolean IsActive { get; set; }
    }
}
