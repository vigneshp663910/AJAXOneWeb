using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    public class PIncoTerms
    {
         public int IncoTermsID { get; set; }
         public string IncoTerms { get; set; }
         public string Description { get; set; }
         public string IncoTerm_Description
         {
             get
             {
                 return IncoTerms + " " + Description;
             }
         }
    }
}
