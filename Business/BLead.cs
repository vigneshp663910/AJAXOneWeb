using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Business
{
    public class BLead
    {
        public List<PLeadCategory> GetLeadCategory(int? LeadCategoryID, string LeadCategory)
        {
            string endPoint = "Lead/GetLeadCategory?LeadCategoryID = " + LeadCategoryID + "LeadCategory = " + LeadCategory;
            return JsonConvert.DeserializeObject<List<PLeadCategory>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadProgressStatus> GetLeadProgressStatus(int? ProgressStatusID, string ProgressStatus)
        {
            string endPoint = "Lead/GetLeadProgressStatus?ProgressStatusID = " + ProgressStatusID + "ProgressStatus = " + ProgressStatus;
            return JsonConvert.DeserializeObject<List<PLeadProgressStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadQualification> GetLeadQualification(int? QualificationID, string Qualification)
        {
            string endPoint = "Lead/GetLeadQualification?QualificationID = " + QualificationID + "Qualification = " + Qualification;
            return JsonConvert.DeserializeObject<List<PLeadQualification>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadSource> GetLeadSource(int? SourceID, string Source)
        {
            string endPoint = "Lead/GetLeadSource?SourceID = " + SourceID + "Source = " + Source;
            return JsonConvert.DeserializeObject<List<PLeadSource>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadStatus> GetLeadStatus(int? StatusID, string Status)
        {
            string endPoint = "Lead/GetLeadStatus?StatusID = " + StatusID + "Status = " + Status;
            return JsonConvert.DeserializeObject<List<PLeadStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLeadType> GetLeadType(int? TypeID, string Type)
        {
            string endPoint = "Lead/GetLeadType?TypeID = " + TypeID + "Type = " + Type;
            return JsonConvert.DeserializeObject<List<PLeadType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

    }
}
