using System;
using System.Configuration;

namespace DataAccess
{
    /// <summary>
    /// This class contains a factory method to decide which data access class
    /// has to be instantiated (SQLServer or MySQL) based on the connection
    /// provider specified in web config.
    /// </summary>
    public class ProviderFactory
    {
        #region Class Variables
        private string providerType;
        public IDataAccess provider;
     //   public IDataAccessNP providerNP;
        private readonly string sqlProvider;
        private readonly string npgsqlProvider;
        #endregion

        #region Constructor
        public ProviderFactory()
        {
            providerType = Convert.ToString(ConfigurationManager.AppSettings["ConnectionProvider"]);
            sqlProvider = "SQLServer";
            npgsqlProvider = "NpgsqlServer";
        }
        #endregion

        #region Public Methods
        public IDataAccess GetProvider()
        // public IDataAccess GetProvider(Boolean isReport = false)
        {
            if (providerType.Equals(sqlProvider))
                provider = new SQLServer();

            //if (providerType.Equals(npgsqlProvider))
            //    providerNP = new NpgsqlServer();

            return provider;
        }
        #endregion
    }
}


