using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public class BECatalogue
    {
        public List<PDMS_Model> GetSpcModel(int? DivisionID, int? ModelID, string ModelCode)
        {
            string endPoint = "ECatalogue/GetSpcModel?DivisionID=" + DivisionID + "&ModelID=" + ModelID + "&ModelCode=" + ModelCode;
            return JsonConvert.DeserializeObject<List<PDMS_Model>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetSpcAssembly(int? DivisionID,int? ModelID, int? SpcAssemblyID,string AssemblyCode, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcAssembly?DivisionID=" + DivisionID + "&ModelID=" + ModelID + "&SpcAssemblyID=" + SpcAssemblyID 
                + "&AssemblyCode=" + AssemblyCode + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
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
        public PApiResult InsertorUpdateSpcAssembly(int? SpcAssemblyID, int ModelID, string AssemblyCode, string AssemblyDescription, string AssemblyType, string Remarks)
        {
            string endPoint = "ECatalogue/InsertorUpdateSpcAssembly?SpcAssemblyID=" + SpcAssemblyID + "&ModelID=" + ModelID + "&AssemblyCode=" + AssemblyCode
                + "&AssemblyDescription=" + AssemblyDescription + "&AssemblyType=" + AssemblyType + "&Remarks=" + Remarks;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
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

                if (Files.AttachedFile == null || Files.AttachedFile.Length == 0)
                {
                }
                else
                {
                    string rootPath = HttpContext.Current.Server.MapPath("~/ECat/Files/");  // Root directory
                    if(!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    // Full path for the image
                    string fullPath = Path.Combine(rootPath, FileName); // Example: "image.jpg"

                    // Write the byte array to file
                    File.WriteAllBytes(fullPath, Files.AttachedFile);
                } 
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "GetServiceInvoiceFile", ex);
                throw;
            }
        }

        public PApiResult GetSpcCart(long? spcCartID, int? DealerID, int? OfficeID, string CartOrderNo, string DateFrom, string DateTo,int? DivisionID,int? ModelID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ECatalogue/GetSpcCart?spcCartID=" + spcCartID + "&DealerID=" + DealerID + "&OfficeID=" + OfficeID
                + "&CartOrderNo=" + CartOrderNo + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DivisionID=" + DivisionID + "&ModelID=" + ModelID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
      }
    }
}
