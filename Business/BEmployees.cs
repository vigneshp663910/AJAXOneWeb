using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    
     public class BEmployees : System.Web.UI.Page
    {
        private IDataAccess provider;
        public BEmployees()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PEmployee> GetEmployeeListJohn(int? EID, int? EmpID, string EmployeeUserID, string EmployeeName, string Department)
        {
            List<PEmployee> Employees = new List<PEmployee>();
            PEmployee Employee = null;
            DbParameter EIDParam;
            DbParameter EmpIDParam;
            DbParameter EmployeeUserIDParam;
            DbParameter EmployeeNameParam;
            DbParameter DepartmentParam;

            if (EID != null)
                EIDParam = provider.CreateParameter("EID", EID, DbType.Int32);
            else
                EIDParam = provider.CreateParameter("EID", DBNull.Value, DbType.Int32);

            if (EmpID != null)
                EmpIDParam = provider.CreateParameter("EmpID", EmpID, DbType.Int32);
            else
                EmpIDParam = provider.CreateParameter("EmpID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(EmployeeUserID))
                EmployeeUserIDParam = provider.CreateParameter("EmployeeUserID", EmployeeUserID, DbType.String);
            else
                EmployeeUserIDParam = provider.CreateParameter("EmployeeUserID", DBNull.Value, DbType.String);

            if (!string.IsNullOrEmpty(EmployeeName))
                EmployeeNameParam = provider.CreateParameter("EmployeeName", EmployeeName, DbType.String);
            else
                EmployeeNameParam = provider.CreateParameter("EmployeeName", DBNull.Value, DbType.String);

            if (!string.IsNullOrEmpty(Department))
                DepartmentParam = provider.CreateParameter("Department", Department, DbType.String);
            else
                DepartmentParam = provider.CreateParameter("Department", DBNull.Value, DbType.String);

            DbParameter[] EmployeeParams = new DbParameter[5] { EIDParam,EmpIDParam, EmployeeUserIDParam, EmployeeNameParam, DepartmentParam };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("GetEmployeeList", EmployeeParams))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow EmployeeRow in EmployeeDataSet.Tables[0].Rows)
                        {
                            Employee = new PEmployee();
                            Employee.EID = Convert.ToInt32(EmployeeRow["EID"]);
                            Employee.EmpId = Convert.ToInt32(EmployeeRow["EmpId"]);
                            Employee.EmployeeUserID = Convert.ToString(EmployeeRow["EmployeeUserID"]);
                            Employee.EmployeeName = Convert.ToString(EmployeeRow["EmployeeName"]);
                            Employee.Department = new PDepartment();
                            Employee.Department.DepartmentName = Convert.ToString(EmployeeRow["Department"]);
                            Employee.Mail1 = Convert.ToString(EmployeeRow["Mail"]);
                            Employee.IsActive = Convert.ToBoolean(Convert.ToString(EmployeeRow["IsActive"]));
                            Employee.ReportingTo = new PEmployee()
                            {
                                EID = Convert.ToInt32(EmployeeRow["ReportingToEID"]),
                                EmpId = Convert.ToInt32(EmployeeRow["ReportingToEmpId"]),
                                EmployeeName = Convert.ToString(EmployeeRow["ReportingToEmployeeName"])
                            };
                            Employee.DmsEmp = new PDMS_DealerEmployee();
                            Employee.DmsEmp.LoginUserName = Convert.ToString(EmployeeRow["DMSUserName"]);
                            Employee.DmsEmp.DealerEmployeeRole = new PDMS_DealerEmployeeRole();
                            Employee.DmsEmp.DealerEmployeeRole.DealerDepartment = new PDMS_DealerDepartment();
                            Employee.DmsEmp.DealerEmployeeRole.DealerDepartment.DealerDepartment = Convert.ToString(EmployeeRow["DealerDepartment"]);

                            Employee.DmsEmp.DealerEmployeeRole.DealerDesignation = new PDMS_DealerDesignation();
                            Employee.DmsEmp.DealerEmployeeRole.DealerDesignation.DealerDesignation = Convert.ToString(EmployeeRow["DealerDesignation"]);

                            Employee.DmsEmp.DealerEmployeeRole.ReportingTo = new PDMS_DealerEmployee();
                            Employee.DmsEmp.DealerEmployeeRole.ReportingTo.Name = Convert.ToString(EmployeeRow["DmsReportingToName"]);

                           //   Employee.DOJ = Convert.ToDateTime(EmployeeRow["DOJ"]);
                            Employees.Add(Employee);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Employees;
        }


        public void CheckPermitionJohn(int SubModuleMasterID)
        {
            string userId;

            userId = User.Identity.Name.Split('\\')[1];
          //  userId = "abhishek.r";
            if (Session["UserId"] != null)
            {
                userId = new BEmployees().GetEmployeeListJohn(null, Convert.ToInt32(Session["UserId"]), "", "", "")[0].EmployeeUserID;
                PSession.User = null;
            }
            if ((PSession.User == null) || (PSession.Emp == null))
            {

                if (SubModuleMasterID == 0)
                {
                    PSession.Emp = new BEmployees().GetEmployeeListJohn(null, null, userId, "", "")[0];
                    PUser user = new BUser().GetUserDetails(userId);

                    if (user.UserName == null)
                    {
                        user.ContactName = PSession.Emp.EmployeeName;
                        user.UserName = userId;
                        user.PassWord = "YWJjIzEyMyQ=";
                        user.UserTypeID = (short)PSession.Emp.UserTypeID;
                        user.ExternalReferenceID = PSession.Emp.EmpId.ToString(); 
                        user.IsFirstTimeLogin = false;
                        user.IsLocked = false;
                        user.IsEnabled = true;
                        user.PasswordExpiryDate = DateTime.Now.AddMonths(3);
                        user.CreatedBy = 1;
                        user.UpdatedBy = 1;
                        user.CreatedOn = DateTime.Now;
                        user.UpdatedOn = DateTime.Now;
                        user.Mail = PSession.Emp.Mail1;
                        new BUser().InsertOrUpdateUser(user);
                        user = new BUser().GetUserDetails(userId);
                    }
                    PSession.User = user;
                }
            }
        }
        public Boolean UserCheck()
        {
            if ((User.Identity.Name.Split('\\')[1].ToLower() == "john.peter"))
            {
                return true;
            }
            return false;
        }


        public List<PUser> InsertOrUpdateTechnicianUserFromSAP()
        {
            List<PUser> Employees = new STechnician().getEmployeesFromSAP();
            List<PUser> DealerEmployee = new List<PUser>();
            foreach (PUser E in Employees)
            {
                // if ((Convert.ToInt64(E.UserName) > 900000) && (!string.IsNullOrEmpty( E.ExternalReferenceID)))
                if ((Convert.ToInt64(E.UserName) > 900000) && (E.ExternalReferenceID.Split('-').Count() == 2))
                {
                    if (E.ExternalReferenceID.Split('-')[1].ToUpper() != "SERVICE")
                    {
                        continue;
                    }
                    DbParameter ContactNameP = provider.CreateParameter("ContactName", E.ContactName, DbType.String);
                    DbParameter UserNameP = provider.CreateParameter("UserName", Convert.ToInt64(E.UserName), DbType.Int64);
                    DbParameter DealerCodeP = provider.CreateParameter("DealerCode", E.ExternalReferenceID.Split('-')[0], DbType.String);
                 //   DbParameter IsTechnicianP = provider.CreateParameter("IsTechnician", E.ExternalReferenceID, DbType.String);
                    DbParameter MailP = provider.CreateParameter("Mail", E.Mail, DbType.String);

                    DbParameter[] Params = new DbParameter[4] { ContactNameP, UserNameP, DealerCodeP, MailP };
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateTechnicianUser", Params);
                            scope.Complete();
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessage("BEmployees", "InsertOrUpdateTechnicianUserFromSAP", sqlEx);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessage("BEmployees", " InsertOrUpdateTechnicianUserFromSAP", ex);
                        throw;
                    }
                }
            }
            return DealerEmployee;
        }
        public List<PDealerEmployee> getDealerEmployeefromSAP()
        {
            List<PUser> Employees = new STechnician().getEmployeesFromSAP();
            List<PDealerEmployee> DealerEmployee = new List<PDealerEmployee>();
            foreach (PUser E in Employees)
            {
                if (Convert.ToInt32(E.UserName) > 900000)
                {
                    DealerEmployee.Add(new PDealerEmployee()
                    {
                        Dealer = new PDMS_Dealer() { DealerName = E.Department.DepartmentName, DealerCode = E.ExternalReferenceID.Split('-')[0] },
                        EmpId = Convert.ToInt32(E.UserName),
                        Department = E.Department.DepartmentName,
                        EmployeeName = E.ContactName,
                        EmployeeType = E.ExternalReferenceID.Split('-').Count() == 2 ? E.ExternalReferenceID.Split('-')[1] : ""
                    });
                }
            }
            return DealerEmployee;
        }
        public void ChangeDealerEmployeeName(string EmpID, string Name, string DEALER_CODE)
        {
            new STechnician().ChangeDealerEmployeeName(EmpID, Name, DEALER_CODE);
        }
        public DataTable GetDepartment(int? DepartmentId, string DepartmentName)
        {

            DbParameter DepartmentIdParam;
            DbParameter DepartmentNameParam;

            if (DepartmentId != null)

                DepartmentIdParam = provider.CreateParameter("DepartmentId", DepartmentId, DbType.Int32);
            else
                DepartmentIdParam = provider.CreateParameter("DepartmentId", DBNull.Value, DbType.Int32);
            if (!string.IsNullOrEmpty(DepartmentName))
                DepartmentNameParam = provider.CreateParameter("DepartmentName", DepartmentName, DbType.String);
            else
                DepartmentNameParam = provider.CreateParameter("DepartmentName", DBNull.Value, DbType.String);


            DbParameter[] Params = new DbParameter[2] { DepartmentIdParam, DepartmentNameParam };
            try
            {
                using (DataSet DataSet = provider.Select("GetAjaxDepartment", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
