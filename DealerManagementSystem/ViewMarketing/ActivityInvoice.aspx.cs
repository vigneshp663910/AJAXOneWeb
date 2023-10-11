using Business;
using Properties; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ActivityInvoice : BasePage
    {
       // public override SubModule SubModuleName { get { return SubModule.ViewMarketing_ActivityInvoice; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        BDMS_Activity oActivity = new BDMS_Activity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                int AID = Convert.ToInt32(oActivity.Decrypt(Request.QueryString["AID"].ToString()));
                DataSet ds = oActivity.GetActivityInvoiceDetail(AID);
                if (ds.Tables.Count > 0)
                {
                    DataTable dtHdr = ds.Tables[0];
                    DataTable dtDetail = ds.Tables[1];
                    DataTable dtCharges = ds.Tables[2];
                    if (dtHdr.Rows.Count > 0)
                    {
                        DataRow dr = dtHdr.Rows[0];
                        imgQRCodeImg.Visible = false;
                        if (dr["IsEInvoice"] == DBNull.Value ? false : (Convert.ToBoolean(dr["IsEInvoice"].ToString())))
                        {
                            if ((Convert.ToBoolean(dr["IsEInvoice"].ToString())) && (Convert.ToDateTime(dr["EInvoiceDate"].ToString()) <= Convert.ToDateTime(dr["InvoiceDate"].ToString())))
                            {
                                if (string.IsNullOrEmpty(dr["IRN"].ToString()))
                                {
                                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getActivityInvoiceESigned(AID);
                                    if (!string.IsNullOrEmpty(EInvoiceSigned.Comments))
                                    {
                                        lblMessage.Text = EInvoiceSigned.Comments;
                                    }
                                    else
                                    {
                                        lblMessage.Text = "E Invoice Not generated.";
                                    }
                                    divPrint.Visible = false;
                                    return;
                                }
                                else
                                {
                                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getActivityInvoiceESigned(Convert.ToInt64(dr["AIH_PkHdrID"].ToString()));
                                    string Path = new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, dr["InvoiceNo"].ToString());

                                    byte[] imgdata = System.IO.File.ReadAllBytes(Path.Replace("file:///", ""));

                                    imgQRCodeImg.ImageUrl = "data:image;base64," + Convert.ToBase64String(imgdata);
                                    imgQRCodeImg.Visible = true;
                                    lblIRN.Text = "IRN : " + dr["IRN"].ToString();
                                    // imgQRCodeImg.ImageUrl = Path.Replace("file:///", "");
                                }
                            }
                        }
                        PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(dr["DealerCode"].ToString());
                        lblDealerCode.Text = "Authorized Dealer :   Code :" + dr["DealerCode"].ToString();
                        lblDealerNameHdr.Text = dr["DealerName"].ToString();
                        lblDealerName.Text = dr["DealerName"].ToString();
                        lblDealerAddress.Text = Customer.Address1 + " " + Customer.Address2 + "<br/>" + Customer.Address3 + " " + Customer.City + " " + Customer.State.State + "[" + Customer.State.StateCode + "]";
                        lblDealerPAN.Text = Customer.PAN;
                        lblDealerGSTIN.Text = Customer.GSTIN;

                        lblInvNo.Text = dr["InvoiceNo"].ToString();
                        lblInvDate.Text = dr["InvDate"].ToString();
                        lblActivity.Text = dr["Activity"].ToString();
                        lblControlNo.Text = dr["AP_ControlNo"].ToString();
                        lblLocation.Text = dr["AA_Location"].ToString();
                        lblPeriod.Text = dr["Period"].ToString();
                        lblRemarks.Text = dr["AA_Remarks"].ToString();
                        if (dtDetail.Rows.Count > 0)
                        {
                            DataRow drDetail = dtDetail.Rows[0];
                            lblActivityCode.Text = drDetail["MaterialCode"].ToString();
                            lblActivityDesc.Text = drDetail["MaterialDescription"].ToString();
                            lblSAC.Text = drDetail["AI_SAC"].ToString();
                            lblGST.Text = Convert.ToDouble("0" + drDetail["AI_GST"].ToString()).ToString("#");
                            lblTaxableValue.Text = drDetail["AIH_TaxableAmount"].ToString();
                            lblGSTValue.Text = drDetail["AIH_TaxAmount"].ToString();
                            lblTotalAmount.Text = drDetail["AIH_TotalAmount"].ToString();

                            lblTaxableValue_Total.Text = drDetail["AIH_TaxableAmount"].ToString();
                            lblTax_Total.Text = drDetail["AIH_TaxAmount"].ToString();
                            lblGrandTotal.Text = drDetail["AIH_TotalAmount"].ToString();
                        }
                        string sGSTHdr = "", sGSTValue = "";
                        foreach (DataRow drCharges in dtCharges.Rows)
                        {
                            sGSTHdr = sGSTHdr + (sGSTHdr == "" ? "" : "/") + drCharges["AIC_Charge"].ToString();
                            sGSTValue = sGSTValue + (sGSTValue == "" ? "" : "/") + drCharges["AIC_ChargeAmount"].ToString();
                        }
                        lblGSTHdr.Text = sGSTHdr;
                        lblGSTValue.Text = sGSTValue;
                        double dblAmount = Convert.ToDouble("0" + lblGrandTotal.Text);
                        int AmountPart1 = Convert.ToInt32("0" + dblAmount.ToString("#"));
                        int AmountPart2 = (dblAmount % 1) > 0 ? Convert.ToInt32("0" + (dblAmount % 1).ToString("#")) : 0;
                        lblAmountinWords.Text = (new BDMS_Fn()).NumbersToWords(AmountPart1) + " only";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "window.print()", true);
                    }
                }


            }
        }
    }
}