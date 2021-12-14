using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
namespace Business
{
   public class BDMS_ModeOfPayment
    {
          private IDataAccess provider;
          public BDMS_ModeOfPayment()
         {
             try
             {
                 provider = new ProviderFactory().GetProvider();
             }
             catch (Exception e1)
             {
                 new FileLogger().LogMessageService("BDMS_ModeOfPayment", "provider : " + e1.Message, null);
             }
         }
          public List<PDMS_ModeOfPayment> GetModeOfPayment(int? ModeOfPaymentID, string ModeOfPayment)
          {
              List<PDMS_ModeOfPayment> ModeOfPayments = new List<PDMS_ModeOfPayment>();
              try
              {
                  DbParameter ModeOfPaymentP;
                  DbParameter ModeOfPaymentIDP = provider.CreateParameter("ModeOfPaymentID", ModeOfPaymentID, DbType.Int32);
                  if (!string.IsNullOrEmpty(ModeOfPayment))
                      ModeOfPaymentP = provider.CreateParameter("ModeOfPayment", ModeOfPayment, DbType.String);
                  else
                      ModeOfPaymentP = provider.CreateParameter("ModeOfPayment", null, DbType.String);

                  DbParameter[] Params = new DbParameter[2] { ModeOfPaymentIDP, ModeOfPaymentP };
                  using (DataSet DataSet = provider.Select("ZDMS_GetModeOfPayment", Params))
                  {
                      if (DataSet != null)
                      {
                          foreach (DataRow dr in DataSet.Tables[0].Rows)
                          {
                              ModeOfPayments.Add(new PDMS_ModeOfPayment()
                              {
                                  ModeOfPaymentID = Convert.ToInt32(dr["ModeOfPaymentID"]),
                                  ModeOfPayment = Convert.ToString(dr["ModeOfPayment"])
                              });
                          }
                      }
                  }
              }
              catch (SqlException sqlEx)
              { }
              catch (Exception ex)
              { }
              return ModeOfPayments;
          }
    }
}
