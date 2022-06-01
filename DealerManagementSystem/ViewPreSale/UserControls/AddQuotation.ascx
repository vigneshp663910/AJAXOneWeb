<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddQuotation.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddQuotation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <script>
            $(document).ready(function () {
                $("#DivTechnician").click(function () {
                    $("#pnlTechnicianInformation").toggle(function () {
                        $(this).animate({ height: '150px', });
                    });
                });
            });
        </script>

        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Quotation Type</label>
                    <asp:DropDownList ID="ddlQuotationType" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12" style="display: none">
                    <label class="modal-label">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
                </div>
                <%--  <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Rejection Reason</label>
                    <asp:DropDownList ID="ddlRejectionReason" runat="server" CssClass="form-control" />
                </div>--%>

                <%--   <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer </label>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>

                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtCustomerName" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
                </div>--%>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Valid From</label>
                    <asp:TextBox ID="txtValidFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Valid To</label>
                    <asp:TextBox ID="txtValidTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Pricing Date</label>
                    <asp:TextBox ID="txtPricingDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Price Group</label>
                    <asp:DropDownList ID="ddlPriceGroup" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Requested DeliveryDate</label>
                    <asp:TextBox ID="txtRequestedDeliveryDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Life Time Tax %</label>
                    <asp:TextBox ID="txtLifeTimeTax" runat="server" CssClass="form-control" BorderColor="Silver"  ></asp:TextBox>
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">User Status</label>
                    <asp:DropDownList ID="ddlUserStatus" runat="server" CssClass="form-control" />
                </div>

                <%--   <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Visit Date</label>
                    <asp:TextBox ID="txtVisitDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div> --%>

                <div class="col-md-6 col-sm-12" style="display: none">
                    <label class="modal-label">User Status Remarks</label>
                    <asp:DropDownList ID="ddlUserStatusRemarks" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Ship To Party</label>
                    <asp:DropDownList ID="ddlShipParty" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Commission Agent</label>
                    <asp:CheckBox ID="cbCommissionAgent" runat="server" />
                </div>
            </div>
        </fieldset>




        <%--        
Employee resonsible
contact person--%>

        <%-- <fieldset class="fieldset-border" id="Fieldset3" runat="server">
            <div class="col-md-12">

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Sold To Party</label>
                    <asp:TextBox ID="txtSoldToParty" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSoldToParty" WatermarkText="Sold To Party" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Bill To Party</label>
                    <asp:TextBox ID="txtBillToParty" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtBillToParty" WatermarkText="Bill To Party" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Ship To Party</label>
                    <asp:TextBox ID="txtShipToParty" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtShipToParty" WatermarkText="Ship To Party" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Payer</label>
                    <asp:TextBox ID="txtPayer" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtPayer" WatermarkText="Payer" WatermarkCssClass="WatermarkCssClass" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Dealer Office</label>
                    <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                </div>

            </div>
        </fieldset>--%>



        <%--<div class="container IC_ticketManageInfo">
    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">
               

                <div id="divSO" runat="server">

                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpandCallInformation();">
                            <img id="imgCallInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                
                    <table id="txnHistory4:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Material</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandMaterialCharges();">
                                            <img id="imgMaterialCharges" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div> 
        </div>
    </div>--%>
    </ContentTemplate>
</asp:UpdatePanel>
