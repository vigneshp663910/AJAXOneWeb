<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketTechnicianAssign.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ICTicketTechnicianAssign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ICTicketButton.ascx" TagPrefix="UC" TagName="UC_ICTicket" %>
<%@ Register Src="~/UserControls/ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container IC_ticketManageInfo">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">

                <div class="rf-p-b " id="txnHistory:j_idt1289_body">

                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%" class="IC_basicInfo">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">IC Ticket Basic Information</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandBasicInformation();">
                                            <img id="imgBasicInformation" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlBasicInformation" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                            <table class="labeltxt fullWidth">
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Ticket "></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblICTicket" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Status"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Requested Date"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblRequestedDate" runat="server" CssClass="label" Text="10/10/2018"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label52" runat="server" CssClass="label" Text="Warranty"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label53" runat="server" CssClass="label" Text="Warranty Expiry"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="label" Text="10/10/2018"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <%--     <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label25" runat="server" CssClass="label" Text="Country"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="Label27" runat="server" CssClass="label" Text="India"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>--%>
                                                <%--   <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label29" runat="server" CssClass="label" Text="State"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="Label31" runat="server" CssClass="label" Text="GUJARAT"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label34" runat="server" CssClass="label" Text="District"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label38" runat="server" CssClass="label" Text="Contact Person"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label40" runat="server" CssClass="label" Text="Present Contact Number"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblPresentContactNumber" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label46" runat="server" CssClass="label" Text="Complaint Code"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="Label47" runat="server" CssClass="label" Text="ComplaintCode"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label42" runat="server" CssClass="label" Text="Complaint Description"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblComplaintDescription" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label44" runat="server" CssClass="label" Text="Information"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblInformation" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <%-- <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label48" runat="server" CssClass="label" Text="Reason For Closer"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="Label49" runat="server" CssClass="label" ></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>--%>

                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label50" runat="server" CssClass="label" Text="Old IC Ticket Number"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <%--   <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label104" runat="server" CssClass="label" Text="Location"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="TextBox6" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label106" runat="server" CssClass="label" Text="Delv Location"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="TextBox" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="Last HMR Value"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblLastHMRValue" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Last HMR Date"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblLastHMRDate" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label13" runat="server" CssClass="label" Text="Technician"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlTechnician" runat="server" CssClass="TextBox" />
                                                                <asp:Label ID="lblTechnician" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="new HMR Value"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblNewHMRValue" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="new HMR Date"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:Label ID="lblNewHMRDate" runat="server" CssClass="label"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </div>
                                    </div>

                                </asp:Panel>
                            </td>
                        </tr>
                    </table>


                    <table id="txnHistory1:panelGridid3" style="height: 100%; width: 100%" class="IC_materialInfo">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Technician Information</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandTechnicianInformation();">
                                            <img id="imgTechnicianInformation" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlTechnicianInformation" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel3">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body3">
                                            <asp:GridView ID="gvTechnician" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" OnRowDataBound="gvTechnician_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Technician">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="gvddlTechnician" runat="server" CssClass="TextBox" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technician Name">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbTechnicianAdd" runat="server" OnClick="lbTechnicianAdd_Click">Add</asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbTechnicianRemove" runat="server" OnClick="lbTechnicianRemove_Click">Remove</asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <UC:UC_ICTicket ID="ICTicketButton" runat="server"></UC:UC_ICTicket>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" BackColor="#1deaff" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table id="txnHistory2:panelGridid3" style="height: 100%; width: 100%" class="IC_materialInfo">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Technician Work Hours Information</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandTechnicianWorkHoursInformation();">
                                            <img id="imgTechnicianWorkHoursInformation" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlTechnicianWorkHoursInformation" runat="server">
                                    <div class="rf-p " id="txnHistory2:inputFiltersPanel3">
                                        <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body">
                                            <asp:GridView ID="gvTechnicianWorkDays" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" OnRowDataBound="gvTechnicianWorkDays_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Technician">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserName_ContactName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName_ContactName")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblServiceTechnicianWorkDateID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceTechnicianWorkDateID")%>' runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="gvddlTechnician" runat="server" CssClass="TextBox" OnSelectedIndexChanged="gvddlTechnician_SelectedIndexChanged" AutoPostBack="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Worked Day">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkedDay" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedDate","{0:d}")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtWorkedDate" runat="server" CssClass="TextBox" onkeyup="return removeText('MainContent_gvServiceCharges_txtServiceDate');"></asp:TextBox>
                                                            <asp:CalendarExtender ID="ceWorkedDate" runat="server" TargetControlID="txtWorkedDate" PopupButtonID="txtWorkedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtWorkedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Worked Hours">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtWorkedHours" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbWorkedDayRemove" runat="server" OnClick="lbWorkedDayRemove_Click">Remove</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbWorkedDayAdd" runat="server" OnClick="lbWorkedDayAdd_Click">Add</asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function collapseExpandBasicInformation(obj) {
            var gvObject = document.getElementById("MainContent_pnlBasicInformation");
            var imageID = document.getElementById("MainContent_imgBasicInformation");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
        function collapseExpandTechnicianInformation(obj) {
            var gvObject = document.getElementById("MainContent_pnlTechnicianInformation");
            var imageID = document.getElementById("MainContent_imgTechnicianInformation");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
        function collapseExpandTechnicianWorkHoursInformation(obj) {
            var gvObject = document.getElementById("MainContent_pnlTechnicianWorkHoursInformation");
            var imageID = document.getElementById("MainContent_imgTechnicianWorkHoursInformation");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
        //$(document).ready(function () {
        //    var tablefixedWidthID = document.getElementById('tablefixedWidthID');
        //    var $width = $(window).width() - 28;
        //    tablefixedWidthID.style.width = $width + "px";
        //});

    </script>
    <style>
        .footer {
            height: 15px;
            width: 100%;
        }

            .footer td {
                border: none;
            }

            .footer th {
                border: none;
            }
    </style>
    <style>
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006 height: 25px;
            ing: 20px;
            */ backgrou e;
            10px;
            / xtenderList;

        {
            om: do ted 1p cursor: pointer;
            color: aroon;
            utoExtenderHighlight;

        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        /*#divwidt
                 width: 150px !important;
        }

            #divwidth div {
                width: 150px !important;
            }*/
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_txtSearch").autocomplete('Search_CS.ashx');
        });
    </script>
</asp:Content>