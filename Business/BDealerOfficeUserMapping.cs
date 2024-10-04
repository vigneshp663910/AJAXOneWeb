using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BDealerOfficeUserMapping
    {
        private IDataAccess provider;
        public BDealerOfficeUserMapping()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PApiResult GetDealerOfficeUserMapping(int? DealerID, int? OfficeID, int? DepartmentID, int? DesignationID, bool? IsActive, bool? IsEnabled, int? PageIndex, int? PageSize)
        {
            string endPoint = "DealerOfficeUserMapping/GetDealerOfficeUserMapping?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID + "&IsActive=" + IsActive + "&IsEnabled=" + IsEnabled + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
