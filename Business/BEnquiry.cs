using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public class BEnquiry
    {
        private IDataAccess provider;
        public BEnquiry()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public Boolean InsertOrUpdateEnquiry()
        {

            return true;
        }
    }
}
