using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketTSIRView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillTSIR();
        }
        protected void lnkDownloadR_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;
                GridView Parentgrid = (GridView)(gvRow.NamingContainer);

                long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

                PAttachedFile UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileForDownload(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }

        void FillTSIR()
        {
            string TSIRNumber = (string)Session["TSIRNumber"];
            List<PDMS_ICTicketTSIR> TSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIR(null, null, "", TSIRNumber, null, null, "", null, null, "", null, null, null, "", null);
            if (TSIRs.Count == 0)
            {
                return;
            }

            PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TSIRs[0].TsirID, null);
            txtServiceCharge.Text = TSIR.ServiceCharge.Material.MaterialCode + "" + TSIR.ServiceCharge.Material.MaterialDescription;
            txtNatureOfFailures.Text = TSIR.NatureOfFailures;
            txtProblemNoticedBy.Text = TSIR.ProblemNoticedBy;
            txtUnderWhatConditionFailureTaken.Text = TSIR.UnderWhatConditionFailureTaken;
            txtFailureDetails.Text = TSIR.FailureDetails;
            txtPointsChecked.Text = TSIR.PointsChecked;
            txtPossibleRootCauses.Text = TSIR.PossibleRootCauses;
            txtSpecificPointsNoticed.Text = TSIR.SpecificPointsNoticed;

            List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(null, TSIRs[0].TsirID, null);
            //gvAttachedFile.DataSource = UploadedFile;
            //gvAttachedFile.DataBind();

            gvMaterial.DataSource = new BDMS_Service().GetServiceMaterials(null, null, TSIR.TsirID, "", false, "");
            gvMaterial.DataBind();

        }
    }
}