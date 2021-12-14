using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    static public class SSAP_CRM
    {
        //public SAP()
        //{
        //    try
        //    {
        //        RfcDestinationManager.RegisterDestinationConfiguration(new MyBackendConfig());
        //    }
        //    catch
        //    {

        //    }
        //}
        static public RfcDestination CRM_prd = null;
        static public RfcRepository CRM_repo = null;
        public static RfcDestination CRM_RfcDes()
        {
            if (CRM_prd == null)
            {
                try
                {
                    RfcDestinationManager.RegisterDestinationConfiguration(new CRM_MyBackendConfig());
                }
                catch
                { }
                CRM_prd = RfcDestinationManager.GetDestination(ConfigurationSettings.AppSettings["CRM_RFCDestination"]);
            }
            return CRM_prd;
        }


        public static RfcRepository CRM_RfcRep()
        {
            if (CRM_repo == null)
            {
                CRM_repo = SSAP_CRM.CRM_RfcDes().Repository;
            }
            return CRM_repo;
        }

    }

     class CRM_MyBackendConfig : IDestinationConfiguration
    {
        string CrmString = ConfigurationSettings.AppSettings["CRM_RFCDestination"];
        string EccString = ConfigurationSettings.AppSettings["RFCDestination"];
        public RfcConfigParameters GetParameters(String destinationName)
        {
            if (CrmString.Equals(destinationName))
             {
            RfcConfigParameters parms = new RfcConfigParameters();

            parms.Add(RfcConfigParameters.AppServerHost, ConfigurationSettings.AppSettings["CRM_AppServerHost"]);
            parms.Add(RfcConfigParameters.SystemNumber, ConfigurationSettings.AppSettings["CRM_SystemNumber"]);
            parms.Add(RfcConfigParameters.User, ConfigurationSettings.AppSettings["CRM_User"]);
            parms.Add(RfcConfigParameters.Password, ConfigurationSettings.AppSettings["CRM_Password"]);
            parms.Add(RfcConfigParameters.Client, ConfigurationSettings.AppSettings["CRM_Client"]);
            parms.Add(RfcConfigParameters.Language, ConfigurationSettings.AppSettings["Language"]);
            parms.Add(RfcConfigParameters.PoolSize, ConfigurationSettings.AppSettings["PoolSize"]);
            parms.Add(RfcConfigParameters.MaxPoolSize, ConfigurationSettings.AppSettings["MaxPoolSize"]);
            parms.Add(RfcConfigParameters.IdleTimeout, ConfigurationSettings.AppSettings["IdleTimeout"]);
            return parms;
            }
            if (EccString.Equals(destinationName))
            {
                RfcConfigParameters parms = new RfcConfigParameters();


                parms.Add(RfcConfigParameters.AppServerHost, ConfigurationSettings.AppSettings["AppServerHost"]);
                parms.Add(RfcConfigParameters.SystemNumber, ConfigurationSettings.AppSettings["SystemNumber"]);
                parms.Add(RfcConfigParameters.User, ConfigurationSettings.AppSettings["User"]);
                parms.Add(RfcConfigParameters.Password, ConfigurationSettings.AppSettings["Password"]);
                parms.Add(RfcConfigParameters.Client, ConfigurationSettings.AppSettings["Client"]);
                parms.Add(RfcConfigParameters.Language, ConfigurationSettings.AppSettings["Language"]);
                parms.Add(RfcConfigParameters.PoolSize, ConfigurationSettings.AppSettings["PoolSize"]);
                parms.Add(RfcConfigParameters.MaxPoolSize, ConfigurationSettings.AppSettings["MaxPoolSize"]);
                parms.Add(RfcConfigParameters.IdleTimeout, ConfigurationSettings.AppSettings["IdleTimeout"]);
                return parms;
            }
            else return null;
        }

        // The following two are not used in this example:
        public bool ChangeEventsSupported()
        {
            return false;
        }
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
    }

}