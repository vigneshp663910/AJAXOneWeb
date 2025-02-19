using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
    public class f_Return_Product_Type_Image
    {
        public string as_ProductType;
        string ls_imgFile;

        public string GetProductTypeImage()
        {
            if (as_ProductType == "SP") { ls_imgFile = "~/Images/SpareParts.png"; }
            else if (as_ProductType == "CM") { ls_imgFile = "~/Images/Argo-1000.jpg"; }
            else if (as_ProductType == "CP") { ls_imgFile = "~/Images/ASP-7011.jpg"; }
            else if (as_ProductType == "BP") { ls_imgFile = "~/Images/CRB-20.jpg"; }
            else if (as_ProductType == "BM") { ls_imgFile = "~/Images/Boom-Pump.jpg"; }
            else if (as_ProductType == "TM") { ls_imgFile = "~/Images/Transit-Mixer.jpg"; }
            else if (as_ProductType == "DP") { ls_imgFile = "~/Images/Dumper.jpg"; }
            else if (as_ProductType == "SB") { ls_imgFile = "~/Images/SPBP.png"; }
            else if (as_ProductType == "PS") { ls_imgFile = "~/Images/Paver.png"; }          
            return ls_imgFile ;
        }
    }
}
