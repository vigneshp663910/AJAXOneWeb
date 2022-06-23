using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    class BProject
    {
        private IDataAccess provider;
        public BProject()
        {
            provider = new ProviderFactory().GetProvider();
        }
    }
}
