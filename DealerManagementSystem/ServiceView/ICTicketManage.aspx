<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketManage.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ICTicketManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS/YDMS_Scripts.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--   <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
        <ContentTemplate>
          <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="position: fixed; top: 35%; right: 46%" Width="100px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>

    <table id="txnHistory4:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead" style="height: 10px; background-color: #fbfbfb;"></div>
            </td>
        </tr>
    </table>

    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter : IC Ticket Manage </div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpand();">
                            <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" Text="Dealer Code" />
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer Code"></asp:Label>
                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Ticket "></asp:Label>
                                <asp:TextBox ID="txtICTicketNumber" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="IC Ticket Date From "></asp:Label>
                                <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Ticket Date To"></asp:Label>
                                <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label6" runat="server" Text="Status" />
                                <asp:DropDownList ID="ddlStatus" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label11" runat="server" CssClass="label" Text="Service Type" />
                                <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="TextBox" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" Text="Division" />
                                <asp:DropDownList ID="ddlDivision" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                &nbsp;
                                                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                <div id="divICTicketManage" runat="server">
                    <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <span id="txnHistory1:refreshDataGroup">
                                    <div class="boxHead">
                                        <div class="logheading">
                                            <div style="float: left">
                                                <table>
                                                    <tr>
                                                        <td>IC Ticket Manage</td>
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
                                    <div class="InputButtonRight-contain">
                                    </div>
                                    <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="ICTicketID" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging" OnRowDataBound="gvICTickets_RowDataBound1">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbICTicketID" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="62px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IC Ticket Date" HeaderStyle-Width="92px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer" HeaderStyle-Width="50px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Requested Date" HeaderStyle-Width="76px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model" HeaderStyle-Width="77px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Division" HeaderStyle-Width="77px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentModel.Division.DivisionCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Type" HeaderStyle-Width="79px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceType")%>' runat="server"></asp:Label><div style="display: none">
                                                            <asp:Label ID="lblServiceTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceTypeID")%>' runat="server"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Priority" HeaderStyle-Width="147px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "ServicePriority.ServicePriority")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Status" HeaderStyle-Width="147px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatus")%>' runat="server"></asp:Label><div style="display: none">
                                                            <asp:Label ID="lblServiceStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatusID")%>' runat="server"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Margin Warranty">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsMarginWarranty")%>' Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="150px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click">Edit </asp:LinkButton>&ensp;&ensp;
                                                            <asp:LinkButton ID="lbView" runat="server" OnClick="lbView_Click">View </asp:LinkButton>&ensp;&ensp;
                                                            <asp:LinkButton ID="lbReqDecline" runat="server" OnClick="lbReqDecline_Click">Req.Decline </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Button ID="btnDecline" runat="server" Text="Req.Decline" CssClass="InputButton" OnClick="btnDecline_Click" />
                                        <asp:Button ID="btnTechnicianAssign" runat="server" Text="Technician Assign" CssClass="InputButton" OnClick="btnTechnicianAssign_Click" />
                                        <asp:Button ID="btnServiceConfirmation" runat="server" Text="Service Confirmation" CssClass="InputButton" OnClick="btnServiceConfirmation_Click" />
                                        <asp:Button ID="btnNote" runat="server" Text="Note" CssClass="InputButton" OnClick="btnNote_Click" />
                                        <asp:Button ID="btnServiceCharge" runat="server" Text="Service Charge" CssClass="InputButton" OnClick="btnServiceCharge_Click" />
                                        <asp:Button ID="btnMaterialCharge" runat="server" Text="Material Charge" CssClass="InputButton" OnClick="btnMaterialCharge_Click" />
                                    </div>
                                </span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divDecline" runat="server" visible="false">
                    <table id="txnHistory1:panelGridid1" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Declain IC Ticket</div>
                                </div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel1">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body1">
                                            <table class="labeltxt fullWidth">

                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Reason For Declain"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtDeclineReason" runat="server" CssClass="input" TextMode="MultiLine" Width="700px" Height="300px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-btn excelBtn">
                                                            <div class="tbl-col-btn">
                                                                <asp:Button ID="btnSaveDecline" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnSaveDecline_Click" /><asp:Button ID="btnBackDecline" runat="server" Text="Back" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnBackDecline_Click" />
                                                            </div>
                                                            <div class="tbl-col-btn">
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
                </div>
            </div>
        </div>
    </div>

    <%-- </ContentTemplate>
       <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvICTickets" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
        $(document).ready(function () {
            var tablefixedWidthID = document.getElementById('tablefixedWidthID');
            var $width = $(window).width() - 28;
            //   alert($width);
            //    tablefixedWidthID.css("width", ($width + "px"));
            tablefixedWidthID.style.width = $width + "px";
            //  $('.tablefixedWidth').css("width", $width);
            // var $width


            var btnDecline = document.getElementById("<%= btnDecline.ClientID %>");
            var btnTechnicianAssign = document.getElementById("<%= btnTechnicianAssign.ClientID %>");
            var btnServiceConfirmation = document.getElementById("<%= btnServiceConfirmation.ClientID %>");
            var btnServiceConfirmation = document.getElementById("<%= btnServiceConfirmation.ClientID %>");
            var btnServiceCharge = document.getElementById("<%= btnServiceCharge.ClientID %>");
            var btnMaterialCharge = document.getElementById("<%= btnMaterialCharge.ClientID %>");
            var btnNote = document.getElementById("<%= btnNote.ClientID %>");
            btnDecline.style.display = 'none'
            btnTechnicianAssign.style.display = 'none';
            btnServiceConfirmation.style.display = 'none';
            btnServiceCharge.style.display = 'none';
            btnMaterialCharge.style.display = 'none';
            btnNote.style.display = 'none';
            parent = document.getElementById("<%= gvICTickets.ClientID %>");
            var items = parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].type == "radio") {
                    items[i].checked = false;
                }
            }
        });
        //$(window).on(scroll, function () {
        //    var $width = $(window).width() - 28;
        //    $('#tablefixedWidth').css("width", $width);
        //    // var $width
        //});

        function CheckOtherIsCheckedByGVID(rb) {
            var isChecked = rb.checked;
            var row = rb.parentNode.parentNode;

            var currentRdbID = rb.id;
            parent = document.getElementById("<%= gvICTickets.ClientID %>");
            var items = parent.getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {
                if (items[i].id != currentRdbID && items[i].type == "radio") {
                    if (items[i].checked) {
                        items[i].checked = false;
                    }
                }
            }
            var lblServiceStatusID = document.getElementById(rb.id.replace("rbICTicketID", "lblServiceStatusID"));
            var lblServiceTypeID = document.getElementById(rb.id.replace("rbICTicketID", "lblServiceTypeID"));

            var btnDecline = document.getElementById("<%= btnDecline.ClientID %>");
            var btnTechnicianAssign = document.getElementById("<%= btnTechnicianAssign.ClientID %>");
            var btnServiceConfirmation = document.getElementById("<%= btnServiceConfirmation.ClientID %>");
            var btnServiceConfirmation = document.getElementById("<%= btnServiceConfirmation.ClientID %>");
            var btnServiceCharge = document.getElementById("<%= btnServiceCharge.ClientID %>");
            var btnMaterialCharge = document.getElementById("<%= btnMaterialCharge.ClientID %>");
            var btnNote = document.getElementById("<%= btnNote.ClientID %>");

            btnDecline.style.display = 'none';
            btnTechnicianAssign.style.display = 'none';
            btnServiceConfirmation.style.display = 'none';
            btnServiceCharge.style.display = 'none';
            btnMaterialCharge.style.display = 'none';
            btnNote.style.display = 'none';

            if (lblServiceStatusID.innerHTML == '1') {
                btnDecline.style.display = 'inline'
                btnTechnicianAssign.style.display = 'inline'
                btnNote.style.display = 'inline'

            }
            else if (lblServiceStatusID.innerHTML == '2') {
                btnTechnicianAssign.style.display = 'inline'
                btnServiceConfirmation.style.display = 'inline'
                btnNote.style.display = 'inline'

            }
            else if ((lblServiceStatusID.innerHTML == '3') || (lblServiceStatusID.innerHTML == '4')) {

                btnTechnicianAssign.style.display = 'inline'
                btnServiceConfirmation.style.display = 'inline'
                btnNote.style.display = 'inline'


                if ((lblServiceTypeID.innerHTML == '1') || (lblServiceTypeID.innerHTML == '2') || (lblServiceTypeID.innerHTML == '6')) {
                    btnServiceCharge.style.display = 'inline'
                    btnMaterialCharge.style.display = 'inline'
                }
                else if ((lblServiceTypeID.innerHTML == '3') || (lblServiceTypeID.innerHTML == '4') || (lblServiceTypeID.innerHTML == '5')) {
                    btnServiceCharge.style.display = 'inline'
                }

            }
            else if (lblServiceStatusID.innerHTML == '5') {

            }

            //  var lblTicketSeverity = document.getElementById('MainContent_gvTickets_lblTicketSeverity_' + i);
        }
    </script>
</asp:Content>
