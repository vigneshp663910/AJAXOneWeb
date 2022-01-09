using System;
using System.Collections.Generic;
using System.Web.Services;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Pre_Sales_DashboardVignesh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<Data> GetData()
        {
            List<Data> dataList = new List<Data>();

            dataList.Add(new Data("Katnadaka", 5));
            dataList.Add(new Data("Bangalore", 10));
            dataList.Add(new Data("KarnadakaWest", 7));
            return dataList;
        }
        public class Data
        {
            public string State = "";
            public int Count = 0;
            public Data(string Statename, int vale)
            {
                State = Statename;
                Count = vale;
            }
        }
    }
}