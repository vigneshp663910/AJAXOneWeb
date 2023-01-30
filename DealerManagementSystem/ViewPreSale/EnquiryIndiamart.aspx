<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dealer.Master" CodeBehind="EnquiryIndiamart.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.EnquiryIndiamart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<%@ Register Src="~/ViewPreSale/UserControls/EnquiryIndiamartView.ascx" TagPrefix="UC" TagName="UC_ViewEquiryIndiamart" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <%-- <style>
        .portlet.box.green {
            border: 1px solid #5cd1db;
            border-top: 0;
        }

            .portlet.box.green > .portlet-title {
                background-color: #32c5d2;
            }

                .portlet.box.green > .portlet-title > .caption {
                    color: #fff;
                }

        .pull-right {
            float: right !important;
        }

        .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all).btn-sm {
            font-size: 11px;
            padding: 6px 18px 6px 18px;
        }

        .btn.yellow:not(.btn-outline) {
            color: #fff;
            background-color: #c49f47;
            border-color: #c49f47;
        }

        .form-group {
            margin-bottom: 5px;
        }

        b, optgroup, strong {
            font-weight: 700;
        }
    </style>--%>

    <script type="text/javascript">
        function ConfirmCancel() {
            var x = confirm("Are you sure you want to cancel?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var gvFollowUp = document.getElementById('MainContent_gvEnquiry');
            if (gvFollowUp != null) {
                for (var i = 0; i < gvFollowUp.rows.length - 1; i++) {
                    var lblFollowUpStatusID = document.getElementById('MainContent_gvEnquiry_lblEnquiryStatus_' + i);
                    var divActions = document.getElementById('divActions' + i);
                    if (lblFollowUpStatusID.innerHTML != "Open") {
                        divActions.style.display = "none";
                    }
                }
            }
        });
    </script>

    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Date From</label>
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Date To</label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control" DataTextField="PreSaleStatus" DataValueField="PreSaleStatusID"/>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" DataTextField="LeadSource" DataValueField="LeadSourceID" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnEnquiryIndiamart_Click" OnClientClick="return dateValidation();" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="125px" />
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Enquiry Indiamart:</td>
                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <asp:GridView ID="gvEnquiry" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvEnquiry_PageIndexChanging"
                                DataKeyNames="Sender Name,Sender Email,MOB,Company Name,Address,State,Country,Product Name,Date,City,Query ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EnquiryIndiamartID")%>' CssClass="btn Back" Width="75px" Height="25px" OnClick="BtnView_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Query Id" DataField="Query ID" />
                                    <asp:BoundField HeaderText="Query Type" DataField="Query Type" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                    <asp:BoundField HeaderText="Sender Name" DataField="Sender Name" />
                                    <asp:BoundField HeaderText="Sender Email" DataField="Sender Email" />
                                    <asp:BoundField HeaderText="Lead Source" DataField="LeadSource" />
                                    <asp:BoundField HeaderText="Mobile" DataField="MOB" />
                                    <asp:BoundField HeaderText="Company Name" DataField="Company Name" />
                                    <asp:BoundField HeaderText="Address" DataField="Address" />
                                    <asp:BoundField HeaderText="City" DataField="City" />
                                    <asp:BoundField HeaderText="State" DataField="State" />
                                    <asp:BoundField HeaderText="Country" DataField="Country" />
                                    <asp:BoundField HeaderText="Product Name" DataField="Product Name" />
                                    <asp:BoundField HeaderText="Message" DataField="Message" />
                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                    <asp:BoundField HeaderText="Receiver Mobile" DataField="Receiver Mob" />
                                    <asp:BoundField HeaderText="Email Alternative" DataField="Email Alt" />
                                    <asp:BoundField HeaderText="Mobile Alternative" DataField="Mobile Alt" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_ViewEquiryIndiamart ID="UC_ViewEquiryIndiamart" runat="server"></UC:UC_ViewEquiryIndiamart>
        </div>
        <div style="display: none">
            <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
    </div>
</asp:Content>
