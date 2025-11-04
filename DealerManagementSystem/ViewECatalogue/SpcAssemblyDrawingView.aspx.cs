using Business;
using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcAssemblyDrawingView : System.Web.UI.Page
    {
        public PSpcAssembly Assembly
        {
            get
            {
                if (ViewState["SPAssemblyImageView_PSpAssemblyImage"] == null)
                {
                    ViewState["SPAssemblyImageView_PSpAssemblyImage"] = new PSpcAssembly();
                }
                return (PSpcAssembly)ViewState["SPAssemblyImageView_PSpAssemblyImage"];
            }
            set
            {
                ViewState["SPAssemblyImageView_PSpAssemblyImage"] = value;
            }
        }
        public List<PSpcAssemblyPartsCoOrdinate> PartsCoOrdinate
        {
            get
            {
                if (ViewState["SPAssemblyImageView_PartsCoOrdinate"] == null)
                {
                    ViewState["SPAssemblyImageView_PartsCoOrdinate"] = new List<PSpcAssemblyPartsCoOrdinate>();
                }
                return (List<PSpcAssemblyPartsCoOrdinate>)ViewState["SPAssemblyImageView_PartsCoOrdinate"];
            }
            set
            {
                ViewState["SPAssemblyImageView_PartsCoOrdinate"] = value;
            }
        }

        public int xyUpdate
        {
            get
            {
                if (ViewState["SPAssemblyImageView_xyUpdate"] == null)
                {
                    ViewState["SPAssemblyImageView_xyUpdate"] = 0;
                }
                return (int)ViewState["SPAssemblyImageView_xyUpdate"];
            }
            set
            {
                ViewState["SPAssemblyImageView_xyUpdate"] = value;
            }
        }
        public int xyBulkUpdate
        {
            get
            {
                if (ViewState["SPAssemblyImageView_xyBulkUpdate"] == null)
                {
                    ViewState["SPAssemblyImageView_xyBulkUpdate"] = 0;
                }
                return (int)ViewState["SPAssemblyImageView_xyBulkUpdate"];
            }
            set
            {
                ViewState["SPAssemblyImageView_xyBulkUpdate"] = value;
            }
        }

        public List<PSpcAssemblyPartsCoOrdinate> PartsListUpload
        {
            get
            {
                if (ViewState["SPAssemblyImageView_PartsListUpload"] == null)
                {
                    ViewState["SPAssemblyImageView_PartsListUpload"] = new List<PSpcAssemblyPartsCoOrdinate>();
                }
                return (List<PSpcAssemblyPartsCoOrdinate>)ViewState["SPAssemblyImageView_PartsListUpload"];
            }
            set
            {
                ViewState["SPAssemblyImageView_PartsListUpload"] = value;
            }
        }

        public List<PSpcAssemblyPartsCart_insert> PartsCart
        {
            get
            {
                if (ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"] == null)
                {
                    ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"] = new List<PSpcAssemblyPartsCart_insert>();
                }
                return (List<PSpcAssemblyPartsCart_insert>)ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"];
            }
            set
            {
                ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["SpcAssemblyID"]))
                {
                    UC_SpcAssemblyDView.Clear();
                    UC_SpcAssemblyDView.fillParts(Convert.ToInt32(Request.QueryString["SpcAssemblyID"]));
                }
            }
            
        }
    }
}