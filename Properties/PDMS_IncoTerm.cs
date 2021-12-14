using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    public class PDMS_IncoTerm
    {
         public int IncoTermID { get; set; }
         public string IncoTerm { get; set; }
         public string Description { get; set; }
         public string IncoTerm_Description
         {
             get
             {
                 return IncoTerm + " " + Description;
             }
         }
    }
}
