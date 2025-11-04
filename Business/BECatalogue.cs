using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace Business
{
    public class BECatalogue
    {
        public PApiResult InsertorUpdateSpcProductGroup(int? SpcProductGroupID, int DivisionID, string PGCode, string PGDescription, string PGSCode, string Remarks, Boolean IsActive)
        {
            string endPoint = "ECatalogue/InsertorUpdateSpcProductGroup?SpcProductGroupID=" + SpcProductGroupID + "&DivisionID=" + DivisionID + "&PGCode=" + PGCode
                + "&PGDescription=" + PGDescription + "&PGSCode=" + PGSCode + "&Remarks=" + Remarks + "&IsActive=" + IsActive;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PSpcProductGroup> GetSpcProductGroup(int? SpcProductGroupID, string PGCode, Boolean? IsActive)
        {
            string endPoint = "ECatalogue/GetSpcProductGroup?SpcProductGroupID=" + SpcProductGroupID + "&PGCode=" + PGCode + "&IsActive=" + IsActive;
            return JsonConvert.DeserializeObject<List<PSpcProductGroup>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public PApiResult InsertorUpdateSpcMaterialGroup(int? SpcMaterialGroupID, string MaterialGroup, string Description1, string Description2, string MType, Boolean IsActive)
        {
            string endPoint = "ECatalogue/InsertorUpdateSpcMaterialGroup?SpcMaterialGroupID=" + SpcMaterialGroupID + "&MaterialGroup=" + MaterialGroup
                + "&Description1=" + Description1 + "&Description2=" + Description2 + "&MType=" + MType + "&IsActive=" + IsActive;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult GetSpcMaterialGroup(int? SpcMaterialGroupID, string MaterialGroup, Boolean? Isactive, int Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcMaterialGroup?SpcMaterialGroupID=" + SpcMaterialGroupID + "&MaterialGroup=" + MaterialGroup + "&Isactive=" + Isactive
                + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult InsertorUpdateSpcModel(PSpcModel_Insert Model)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/InsertorUpdateSpcModel", Model));
        }

        public List<PSpcModel> GetSpcModel(int? SpcModelID, int? SpcProductGroupID, string ModelCode, Boolean? Isactive, Boolean? IsPublish, int? PageIndex = null, int? PageSize = null)
        {
            PApiResult Result = new BECatalogue().GetSpcModelWithResult(SpcModelID, SpcProductGroupID, ModelCode, Isactive, IsPublish, 0, PageIndex, PageSize);
            return JsonConvert.DeserializeObject<List<PSpcModel>>(JsonConvert.SerializeObject(Result.Data));
        }
        public PSpcMaterialGroup GetSpcModelFromSap(string ModelCode)
        {
            string endPoint = "ECatalogue/GetSpcModelFromSap?ModelCode=" + ModelCode;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Result.Status == PApplication.Failure)
            {
                throw new Exception(Result.Message);
            }
            return JsonConvert.DeserializeObject<PSpcMaterialGroup>(JsonConvert.SerializeObject(Result.Data));
        }
        public PApiResult GetSpcModelWithResult(int? SpcModelID, int? SpcProductGroupID, string ModelCode, Boolean? Isactive, Boolean? IsPublish, int Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcModel?SpcModelID=" + SpcModelID + "&SpcProductGroupID=" + SpcProductGroupID + "&ModelCode=" + ModelCode + "&Isactive=" + Isactive + "&IsPublish=" + IsPublish
                + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSpcAssembly(int? SpcProductGroupID, int? ModelID, int? SpcAssemblyID, string AssemblyCode, Boolean? Isactive, int Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcAssembly?SpcProductGroupID=" + SpcProductGroupID + "&ModelID=" + ModelID + "&SpcAssemblyID=" + SpcAssemblyID
                + "&AssemblyCode=" + AssemblyCode + "&Isactive=" + Isactive + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetAssemblyPartsCoOrdinate(int? ModelID, int? SpcAssemblyID)
        {
            string endPoint = "ECatalogue/GetSpcAssemblyPartsCoOrdinate?ModelID=" + ModelID + "&SpcAssemblyID=" + SpcAssemblyID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateSpcAssemblyPartsCoOrdinate(List<PSpcAssemblyPartsCoOrdinate_Insert> CoOrdinate)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/UpdateSpcAssemblyPartsCoOrdinate", CoOrdinate));
        }
        public PApiResult UpdateSpcAssemblyPartsDelete(long SpcAssemblyPartsCoOrdinateID)
        {
            string endPoint = "ECatalogue/UpdateSpcAssemblyPartsDelete?SpcAssemblyPartsCoOrdinateID=" + SpcAssemblyPartsCoOrdinateID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult InsertSpcAssemblyPartsFromExcel(List<PSpcAssemblyPartsCoOrdinate_Insert> PartsList)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/InsertSpcAssemblyPartsFromExcel", PartsList));
        }

        public PApiResult UploadSpcFile(PAttachedFile_Azure File)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/UploadSpcFile", File));
        }
        public PApiResult InsertorUpdateSpcAssembly(PSpcAssembly_Insert Assembly)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/InsertorUpdateSpcAssembly", Assembly));
        }

        public PApiResult InsertorUpdateSpcCart(PspcCart_Insert spcCart)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/InsertorUpdateSpcCart", spcCart));
        }
        public void DowloadSpcFile(string FileName)
        {
            PAttachedFile Files = null;
            try
            {
                string endPoint = "ECatalogue/DowloadSpcFile?FileName=" + FileName;
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                Files = JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(Result.Data));
                if (Files != null)
                {
                    if (Files.AttachedFile == null || Files.AttachedFile.Length == 0)
                    {
                    }
                    else
                    {
                        string rootPath = HttpContext.Current.Server.MapPath("~/ECat/Files/");  // Root directory
                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);
                        }

                        // Full path for the image
                        string fullPath = Path.Combine(rootPath, FileName); // Example: "image.jpg"

                        // Write the byte array to file
                        File.WriteAllBytes(fullPath, Files.AttachedFile);
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "GetServiceInvoiceFile", ex);
                throw;
            }
        }

        public PApiResult GetSpcCart(long? spcCartID, int? DealerID, int? OfficeID, string CartOrderNo, string DateFrom, string DateTo, int? SpcProductGroupID, int? ModelID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcCart?spcCartID=" + spcCartID + "&DealerID=" + DealerID + "&OfficeID=" + OfficeID
                + "&CartOrderNo=" + CartOrderNo + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&SpcProductGroupID=" + SpcProductGroupID + "&ModelID=" + ModelID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult InsertSpcCartTemp(List<PSpcCartTemp_Insert> spcCartTemp)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ECatalogue/InsertSpcCartTemp", spcCartTemp));
        }
        public PApiResult GetSpcCartTemp()
        {
            string endPoint = "ECatalogue/GetSpcCartTemp";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public void FillDivision(DropDownList ddlDivision, string Select = "Select Division")
        {
            List<PDMS_Division> Division = new BDMS_Master().GetDivision(null, null);
            Division.RemoveAll(e1 => e1.DivisionID == 13);
            Division.RemoveAll(e1 => e1.DivisionID == 15);
            Division.RemoveAll(e1 => e1.DivisionID == 18);

            foreach (PDMS_Division D in Division)
            {
                D.DivisionDescription = D.DivisionCode + " - " + D.DivisionDescription;
            }
            new DDLBind(ddlDivision, Division, "DivisionDescription", "DivisionID", true, Select);
        }
        public PApiResult UpdateSpcCartTempDelete(int SpcMaterialID)
        {
            string endPoint = "ECatalogue/UpdateSpcCartTempDelete?SpcMaterialID=" + SpcMaterialID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult UpdateSpcMaterialGroupDelete(int SpcMaterialGroupID)
        {
            string endPoint = "ECatalogue/UpdateSpcMaterialGroupDelete?SpcMaterialGroupID=" + SpcMaterialGroupID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateSpcProductGroupDelete(int SpcProductGroupID)
        {
            string endPoint = "ECatalogue/UpdateSpcProductGroupDelete?SpcProductGroupID=" + SpcProductGroupID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateSpcModelDelete(int SpcModelID)
        {
            string endPoint = "ECatalogue/UpdateSpcModelDelete?SpcModelID=" + SpcModelID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateSpcAssemblyDelete(int SpcAssemblyID)
        {
            string endPoint = "ECatalogue/UpdateSpcAssemblyDelete?SpcAssemblyID=" + SpcAssemblyID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult GetSpcMaterial(string Material, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcMaterial?Material=" + Material + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetAssemblyPartsReport(int? SpcProductGroupID, int? SpcModelID, int? SpcAssemblyID, string Material, int Excel, int? PageIndex = null, int? PageSize = null)
        {
            
            string endPoint = "ECatalogue/GetAssemblyPartsReport?SpcProductGroupID=" + SpcProductGroupID + "&SpcModelID=" + SpcModelID 
                + "&SpcAssemblyID=" + SpcAssemblyID + "&Material=" + Material + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

        }
    }
}
