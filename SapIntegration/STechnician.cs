using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class STechnician
    {
        public List<PUser> getEmployeesFromSAP()
        {
            List<PUser> Employees = new List<PUser>();
            PUser Employee = null;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_LEAVE_APPLICATION");
            //  tagListBapi.SetValue("PERNR", "00000601");
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("ZEMPDATA");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                try
                {
                    tagTable.CurrentIndex = i;
                    Employee = new PUser();
                    Employee.Department = new PDMS_DealerDepartment();
                    Employee.Department.DealerDepartment = tagTable.CurrentRow.GetString("EMPLOYEE_DEPARTMENT");              
                    Employee.ContactName = tagTable.CurrentRow.GetString("EMPLOYEE_NAME");
                    Employee.Mail = tagTable.CurrentRow.GetString("EMAIL_ID");
                    Employee.UserName = tagTable.CurrentRow.GetString("EMPLOYEE_ID");
                    Employee.ContactNumber = tagTable.CurrentRow.GetString("TEL_NO");
                    Employee.ExternalReferenceID = tagTable.CurrentRow.GetString("DEALER_CODE");
 

                    Employees.Add(Employee);
                }
                catch (Exception ex)
                { }
            }

            return Employees;
        }
        public void ChangeDealerEmployeeName(string EmpID, string Name, string DEALER_CODE)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_CHANGE_DEALER_EMP_NAME");

            tagListBapi.SetValue("PERNR_001", EmpID.PadLeft(8, '0'));
            tagListBapi.SetValue("TIMR6_002", "X");

            tagListBapi.SetValue("CHOIC_003", "2");
            tagListBapi.SetValue("VORNA_007", Name);
            tagListBapi.SetValue("DEALER_CODE", DEALER_CODE);
            tagListBapi.Invoke(SAP.RfcDes());
        }
    }
}
