<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketEscalationOnBreakdown.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketEscalationOnBreakdown" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" Font-Bold="true" Font-Size="24px" />
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                </div>
                <div class="col-md-8 text-left">
                    <label class="modal-label">-</label>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                </div>
            </div>
        </fieldset>
    </div>
    <div runat="server" id="tblDashboard">
        <div class="col-md-12">

            <div class="col-md-6 col-sm-12" id="Div1" style="padding-top: 14px">
                <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
            </div>

            <div class="col-md-6 col-sm-12" id="Div2" style="padding-top: 20px">
                <asp:PlaceHolder ID="ph_usercontrols_2" runat="server"></asp:PlaceHolder>
            </div>

            <div class="col-md-6 col-sm-12" id="Div3" style="padding-top: 20px">
                <asp:PlaceHolder ID="ph_usercontrols_3" runat="server"></asp:PlaceHolder>
            </div>
            <div class="col-md-6 col-sm-12" id="Div4" style="padding-top: 14px">
                <asp:PlaceHolder ID="ph_usercontrols_4" runat="server"></asp:PlaceHolder>
            </div>
        </div>

    </div>



  <%--  <asp:Panel ID="pnlFilter" runat="server">

        <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
            <tr>
                <td>
                    <div class="boxHead">
                        <div class="logheading">Filter </div>
                        <div style="float: right; padding-top: 0px">
                            <a href="javascript:collapseExpand();">
                                <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                        </div>
                    </div>
                    <asp:Panel ID="pnlFilterContent" runat="server">
                        <div class="rf-p " id="txnHistory:inputFiltersPanel">
                            <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer"></asp:Label>
    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Date From "></asp:Label>
    <asp:Label ID="Label4" runat="server" CssClass="label" Text=" Date To"></asp:Label>
    <div class="ErrorRed" id="divError" runat="server" visible="false">
        <span id="errorMessage" runat="server"></span>
        <img alt="" src="images/error_red.gif" />
    </div>
    <div class="success" id="divSuccess" runat="server" visible="false">
        <span id="successMessage" runat="server"></span>
        <img alt="" src="Images/sucess_green.png" />
    </div>--%>
</asp:Content>
