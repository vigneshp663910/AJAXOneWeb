using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BDeviationProcess
    {
        public PApiResult GetDeviationProcess(int? DeviationProcessID, string FileName, string FileType, string Subject, bool IsActive, int? PageIndex, int? PageSize)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Deviation/GetDeviationProcess?DeviationProcessID=" + DeviationProcessID + "&FileName=" + FileName + "&FileType=" + FileType + "&Subject=" + Subject + "&IsActive=" + IsActive
                + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PAttachedFile GetAttachedFileDownload(string DocumentName)
        {
            string endPoint = "Deviation/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
    }
}
