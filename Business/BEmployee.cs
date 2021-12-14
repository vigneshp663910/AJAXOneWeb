using DataAccess;
using Properties;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    public class BEmployeeOld : System.Web.UI.Page
    {
        private IDataAccess provider;
        public BEmployeeOld()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //public List<PEmployee> getEmployee()
        //{
        //    List<PEmployee> EmployeeList = new List<PEmployee>();
        //    PEmployee pEmployee;

        //    //DbParameter TicketSeverityIDParam;
        //    //DbParameter TicketSeverityParam;

        //    //if (TicketSeverityID != null)
        //    //    TicketSeverityIDParam = provider.CreateParameter("TicketSeverityID", TicketSeverityID, DbType.Int32);
        //    //else
        //    //    TicketSeverityIDParam = provider.CreateParameter("TicketSeverityID", DBNull.Value, DbType.Int32);

        //    //if (!string.IsNullOrEmpty(TicketSeverity))
        //    //    TicketSeverityParam = provider.CreateParameter("TicketSeverity", TicketSeverity, DbType.String);
        //    //else
        //    //    TicketSeverityParam = provider.CreateParameter("TicketSeverity", DBNull.Value, DbType.String);



        //    //  DbParameter[] TicketResolutionTypeParams = new DbParameter[2] { TicketSeverityIDParam, TicketSeverityParam };

        //    try
        //    {
        //        using (DataSet DS = provider.Select("getEmployee"))
        //        {
        //            if (DS != null)
        //            {
        //                foreach (DataRow DR in DS.Tables[0].Rows)
        //                {

        //                    pEmployee = new PEmployee
        //                    {
        //                        Department = new PDepartment { DepartmentID = Convert.ToInt32(DR["DepartmentID"]), DepartmentName = Convert.ToString(DR["DepartmentName"]) },
        //                        EID = Convert.ToInt32(DR["EID"]),
        //                        EmployeeName = Convert.ToString(DR["EmployeeName"]),
        //                        EmployeeUserID = Convert.ToString(DR["EmployeeUserID"]),
        //                        Mail = Convert.ToString(DR["Mail"]),
        //                        ReportingTo = DR["ReportingTo"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["ReportingTo"]),
        //                        UserTypeID = Convert.ToInt32(DR["UserTypeID"]),
        //                        IsActive =  Convert.ToBoolean(DR["IsActive"])
        //                    };
        //                    EmployeeList.Add(pEmployee);
        //                }
        //            }
        //        }
        //        // This call is for track the status and loged into the trace logeer

        //    }
        //    catch (SqlException sqlEx)
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return EmployeeList;
        //}

        public Boolean ValidateUserIdJohn(string UserID)
        {
           
            if (Session["NewUser"] != null)
            {
                Session["NewUser"] = UserID;
                return true;
            }
          //  List<PEmployee> pList = new BEmployees().GetEmployeeList(null, null, UserID, "", "").Where(a => a.EmployeeUserID.ToUpper() == UserID.ToUpper()).Select(a => a).ToList();

            List<PEmployee> pList = new BEmployees().GetEmployeeListJohn(null, null, UserID, "", "");
            if (pList.Count == 0)
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://Ajax-fiori.com");
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                String Name = UserID + "@Ajax-fiori.com";
                Dsearch.Filter = "(mail=" + Name + ")";
                SearchResult sresult = Dsearch.FindOne();
                DirectoryEntry dsresult = sresult.GetDirectoryEntry();

                PEmployee p = new PEmployee();
                p.EmployeeUserID = UserID;
                try
                {
                    //for (int i = 0; i < dsresult.Properties.Count; i++)
                    //{
                    //p.EmployeeName = dsresult.Properties["cn"][0].ToString();
                    p.EmployeeName = dsresult.Properties["displayName"][0].ToString();
                    p.Mail1 = dsresult.Properties["mail"][0].ToString();
                    p.EmployeeUserID = p.Mail1.Split('@')[0].ToString();
                    p.Department = new PDepartment { DepartmentName = dsresult.Properties["department"][0].ToString() };


                    insertEMP(p);

                    // }
                }
                catch
                {
                }


               // pList = new BEmployee().getEmployee().Where(a => a.EmployeeUserID.ToUpper() == UserID.ToUpper()).Select(a => a).ToList();
                pList = new BEmployees().GetEmployeeListJohn(null, null, UserID, "", "");
                if (pList.Count == 0)
                {
                    Session["NewUser"] = null;
                    return false;
                }
            }
            Session["NewUser"] = UserID;
            return true;
        }
        public Boolean ValidateUserId1(string UserID)
        {
          
            DirectoryEntry entry = new DirectoryEntry("LDAP://Ajax-fiori.com");
            DirectorySearcher Dsearch = new DirectorySearcher(entry);
            String Name = "";
             String DepartmentName = "";
            int CountALL = 0;
            int CountName = 0;
            int CountDB = 0;
            PEmployee p = new PEmployee();
            //   p.EmployeeUserID = UserID;
            try
            {
                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    CountALL = CountALL + 1;
                    Name = GetProperty(sResultSet, "displayName");// GetProperty(sResultSet, "givenName") + GetProperty(sResultSet, "sn");
                    DepartmentName = GetProperty(sResultSet, "department");
                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(DepartmentName))
                    {
                        CountName = CountName + 1;
                         p.EmployeeName = Name;
                         p.Mail1 = GetProperty(sResultSet, "mail");
                         p.EmployeeUserID = p.Mail1.Split('@')[0].ToString();
                         p.Department = new PDepartment { DepartmentName = GetProperty(sResultSet, "department") };

                        // List<PEmployee> pList = new BEmployee().getEmployee().Where(a => a.EmployeeUserID.ToUpper() == p.EmployeeUserID.ToUpper()).Select(a => a).ToList();
                         List<PEmployee> pList = new BEmployees().GetEmployeeListJohn(null, null, UserID, "", "");
                        if (pList.Count == 0)
                         {
                             CountDB = CountDB + 1;
                             insertEMP(p);
                         }
                     }
                  
                }
            }
            catch
            {
            }


            //pList = new BEmployee().getEmployee().Where(a => a.EmployeeUserID.ToUpper() == UserID.ToUpper()).Select(a => a).ToList();
            //if (pList.Count == 0)
            //{
            //    Session["NewUser"] = null;
            //    return false;             
            //}
            //  }
            Session["NewUser"] = UserID;
            return true;
        }
        static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        public void insertEMP(PEmployee p)
        {
            DbParameter EmployeeUserIDParam = provider.CreateParameter("EmployeeUserID", p.EmployeeUserID, DbType.String);
            DbParameter EmployeeNameParam = provider.CreateParameter("EmployeeName", p.EmployeeName, DbType.String);
            DbParameter MailParam = provider.CreateParameter("Mail", p.Mail1, DbType.String);
            DbParameter DepartmentNameParam = provider.CreateParameter("DepartmentName", p.Department.DepartmentName, DbType.String);

            DbParameter[] EmployeeTypeParams = new DbParameter[4] { EmployeeUserIDParam, EmployeeNameParam, MailParam, DepartmentNameParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("insertEmployee", EmployeeTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        public DataTable GetAllDepartment()
        {
            DataTable dt = new DataTable();
            try
            {
                using (DataSet DS = provider.Select("GetAllDepartment"))
                {
                    if (DS != null)
                    {
                        dt = DS.Tables[0];
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return dt;
        }
    }
}
