<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dealer.Master" CodeBehind="EnquiryIndiamart.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.EnquiryIndiamart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
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
    </style>

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
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 text-left">
                    <%--<asp:Label ID="Label7" runat="server" Text="Date From "></asp:Label>--%>
                    <label>Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    <%--<asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
                    <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>--%>
                </div>
                <div class="col-md-2 text-left">
                    <%--<asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>--%>
                    <label>Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    <%--<asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
                    <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>--%>
                </div>
                <div class="col-md-2 text-left">
                    <label>Status</label>
                    <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">Open</asp:ListItem>
                        <asp:ListItem Value="2">Close</asp:ListItem>
                        <asp:ListItem Value="5">Rejected</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnEnquiryIndiamart_Click" OnClientClick="return dateValidation();" />


                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="125px" />
                </div>
            </div>
        </fieldset>


    </div>
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
                                            <asp:Label ID="lblRowCountEnquiryIM" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnEnquiryIMArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnEnquiryIMArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnEnquiryIMArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnEnquiryIMArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvEnquiry" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvEnquiry_PageIndexChanging"
                        DataKeyNames="Sender Name,Sender Email,MOB,Company Name,Address,State,Country,Product Name,Date,City,Query ID">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div style="display:none">
                                        <asp:Label ID="lblEnquiryStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server" />
                                    </div>
                                    <div class="dropdown" id='<%# "divActions"+ Container.DataItemIndex%>'>
                                        <div class="btn Approval" style="height: 25px">Action</div>
                                        <div class="dropdown-content" style="font-size: small; margin-right: -75px">
                                            <asp:LinkButton ID="lnkBtnConvert" runat="server" OnClick="lbActions_Click">Convert to Enquiry</asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnReject" runat="server" OnClick="lbActions_Click">Reject</asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
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

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlAddEnquiry" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblAddEnquiryMessage" runat="server" CssClass="message" Visible="false" />
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Query ID</label>
                    <asp:Label ID="lblQueryIDAdd" runat="server" Text="" CssClass="message" />
                </div>
                <UC:UC_AddEnquiry ID="UC_AddEnquiry" runat="server"></UC:UC_AddEnquiry>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AddEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlRejectEnquiry" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueRejectEnquiry">Reject Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="btnRejectEnquiryClose" runat="server" Text="X" CssClass="PopupClose" />
            </a>
        </div>
        <asp:Label ID="lblRejectEnquiryMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="model-scroll">
                <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Query ID</label>
                            <asp:Label ID="lblQueryID" runat="server" Text="" CssClass="message" />
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Reason</label>
                            <asp:TextBox ID="txtRejectEnquiryReason" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnRejectEnquiry" runat="server" Text="Save" CssClass="btn Save" OnClick="btnRejectEnquiry_Click" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_RejectEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlRejectEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

</asp:Content>
