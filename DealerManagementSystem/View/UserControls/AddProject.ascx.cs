using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.View.UserControls
{
    public partial class AddProject : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {

        }

        void Clear()
        {


        }
        public PProject Read()
        {
            PProject OM = new PProject();

            return OM;
        }
        public string Validation()
        {
            string Message = "";

            return Message;
        }
    }
}