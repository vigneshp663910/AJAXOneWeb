using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    public class PDMS_Financier
    {
         public int FinancierID { get; set; }
         public string FinancierCode { get; set; }
         public string FinancierName { get; set; }
         public string FinancierName_Code
         {
             get
             {
                 return FinancierName + " - " + FinancierCode;
             }             
         }
    }
}
