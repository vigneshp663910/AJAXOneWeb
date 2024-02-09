
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Business
{
    public class FillDropDownt
    {
        void DataBindDDL(DropDownList ddl)
        {
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void Category(DropDownList ddl, int? CategoryID, string Category)
        {
            ddl.DataTextField = "Category";
            ddl.DataValueField = "CategoryID";
            ddl.DataSource = JsonConvert.DeserializeObject<List<PCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketCategory(CategoryID, Category).Data));
            DataBindDDL(ddl);
        }
        public void SubCategory(DropDownList ddl, int? SubCategoryID, string SubCategory, int? CategoryId)
        {
            ddl.DataTextField = "SubCategory";
            ddl.DataValueField = "SubCategoryID";            
            ddl.DataSource = JsonConvert.DeserializeObject<List<PSubCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketSubCategory(SubCategoryID, SubCategory, CategoryId).Data));
            DataBindDDL(ddl);
        }
        public void Severity(DropDownList ddl, int? SeverityID, string Severity)
        {
            ddl.DataTextField = "Severity";
            ddl.DataValueField = "SeverityID";
            ddl.DataSource = JsonConvert.DeserializeObject<List<PSeverity>>(JsonConvert.SerializeObject(new BTickets().getTicketSeverity(SeverityID, Severity).Data));
            DataBindDDL(ddl);
        }

        public void Type(DropDownList ddl, int? TypeID, string Type)
        {
            ddl.DataTextField = "Type";
            ddl.DataValueField = "TypeID";
            ddl.DataSource = JsonConvert.DeserializeObject<List<PType>>(JsonConvert.SerializeObject(new BTickets().getTicketType(TypeID, Type).Data));
            DataBindDDL(ddl);
        }

        public void Status(DropDownList ddl, int? StatusID, string Status)
        {
            ddl.DataTextField = "Status";
            ddl.DataValueField = "StatusID";
            ddl.DataSource = JsonConvert.DeserializeObject<List<PStatus>>(JsonConvert.SerializeObject(new BTickets().getTicketStatus(StatusID, Status).Data));
            DataBindDDL(ddl);
        }
        public void ResolutionType(DropDownList ddl, int? ResolutionTypeID, string ResolutionType)
        {
            ddl.DataTextField = "ResolutionType";
            ddl.DataValueField = "ResolutionTypeID";            
            ddl.DataSource = JsonConvert.DeserializeObject<List<PResolutionType>>(JsonConvert.SerializeObject(new BTickets().getTicketResolutionType(ResolutionTypeID, ResolutionType).Data));
            DataBindDDL(ddl);
        }

        public void EmployeeJohn(DropDownList ddl, int? EID, int? EmpID, string EmployeeUserID, string EmployeeName, string Department)
        {
            ddl.DataTextField = "EmployeeUserID";
            ddl.DataValueField = "EID";
            ddl.DataSource = new BEmployees().GetEmployeeListJohn(EID, EmpID, EmployeeUserID, EmployeeName, Department, null);
            DataBindDDL(ddl);
        }
        public void Department(DropDownList ddl)
        {

            ddl.DataTextField = "DepartmentName";
            ddl.DataValueField = "DepartmentID";
           // ddl.DataSource = new BEmployee().GetAllDepartment();
            DataBindDDL(ddl);
        }       
    }
}