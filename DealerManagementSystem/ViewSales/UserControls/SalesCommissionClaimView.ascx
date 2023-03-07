<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesCommissionClaimView.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesCommissionClaimView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadViewHeader.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<%@ Register Src="~/ViewPreSale/UserControls/SalesQuotationViewHeader.ascx" TagPrefix="UC" TagName="UC_SalesQuotationView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Quotation</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Claim Number : </label>
                    <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Claim Date : </label>
                    <asp:Label ID="lblClaimDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Invoice Number : </label>
                    <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Invoice Date : </label>
                    <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Requested By : </label>
                    <asp:Label ID="lblRequestedBy" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Approved1By : </label>
                    <asp:Label ID="lblApproved1By" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Approved1On : </label>
                    <asp:Label ID="lblApproved1On" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Approved2By : </label>
                    <asp:Label ID="lblApproved2By" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Approved2On : </label>
                    <asp:Label ID="lblApproved2On" runat="server"></asp:Label>
                </div>
                
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

<asp:TabContainer ID="tbpSaleQuotation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="10">
   
    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Quotation Material" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Remark</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Material : </label>
                                <asp:Label ID="lblMaterial" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Material Des : </label>
                                <asp:Label ID="lblMaterialDescription" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Qty : </label>
                                <asp:Label ID="lblQty" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Amount : </label>
                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Base Tax : </label>
                                <asp:Label ID="lblBaseTax" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved 1 Amount : </label>
                                <asp:Label ID="lblApproved1Amount" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved 2 Amount : </label>
                                <asp:Label ID="lblApproved2Amount" runat="server"></asp:Label>
                            </div>
                            
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Approved 1 Remarks : </label>
                                <asp:Label ID="lblApproved1Remarks" runat="server"></asp:Label>
                            </div>
                          <div class="col-md-12">
                                <label>Approved 2 Remarks : </label>
                                <asp:Label ID="lblApproved2Remarks" runat="server"></asp:Label>
                            </div>
                          
                        </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
     <asp:TabPanel ID="tpnlProduct" runat="server" HeaderText="Quotation">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_SalesQuotationView ID="UC_SalesQuotationView" runat="server"></UC:UC_SalesQuotationView>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabLead" runat="server" HeaderText="Lead">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_LeadView ID="UC_LeadView" runat="server"></UC:UC_LeadView>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
     <asp:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerViewSoldTo ID="CustomerViewSoldTo" runat="server"></UC:UC_CustomerViewSoldTo>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>
<script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
<script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
<link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
<script type="text/javascript">
    function InIEvent() { }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {

        prm.add_endRequest(function (sender, e) {
            $("#MainContent_UC_QuotationView_txtMaterial").autocomplete({
                source: function (request, response) {
                    
                    var param = { input: $('#MainContent_UC_QuotationView_txtMaterial').val() };
                    $.ajax({
                        url: "Quotation.aspx/SearchSMaterial",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });

        });
    };

    $(function () {

        $("#MainContent_UC_QuotationView_txtMaterial").autocomplete({

            source: function (request, response) {
                
                var param = { input: $('#MainContent_UC_QuotationView_txtMaterial').val() };
                $.ajax({
                    url: "Quotation.aspx/SearchSMaterial",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");  
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox  
        });
    });
</script>
