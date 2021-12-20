<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimApprovalLevel1.aspx.cs" Inherits="DealerManagementSystem.ServiceView.WarrantyClaimApprovalLevel1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS/YDMS_Scripts.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
        $(document).ready(function () {
            var tablefixedWidthID = document.getElementById('tablefixedWidthID');
            var $width = $(window).width() - 28;
            //   alert($width);
            //    tablefixedWidthID.css("width", ($width + "px"));
            tablefixedWidthID.style.width = $width + "px";



            //  $('.tablefixedWidth').css("width", $width);
            // var $width

        });
    </script>
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
                    <div class="logheading">Filter : Claim Approval</div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Claim Number"></asp:Label>
                                <asp:TextBox ID="txtClaimID" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Claim Date From"></asp:Label>
                                <asp:TextBox ID="txtClaimDateF" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtClaimDateF" PopupButtonID="txtClaimDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtClaimDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Claim Date To"></asp:Label>

                                <asp:TextBox ID="txtClaimDateT" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtClaimDateT" PopupButtonID="txtClaimDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtClaimDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Service Ticket"></asp:Label>
                                <asp:TextBox ID="txtICServiceTicket" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="IC Login Date From"></asp:Label>
                                <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Login Date To"></asp:Label>
                                <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="TSIR Number"></asp:Label>
                                <asp:TextBox ID="txtTSIRNumber" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                             <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Division"></asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <%-- <asp:Label ID="Label2" runat="server" CssClass="label" Text="Status"></asp:Label> --%>
                                <asp:Label ID="lblStatus" runat="server" CssClass="label" Text="Status"></asp:Label>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                &nbsp;
                              <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />

                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <div class="col2">
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
        <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
            <tr>
                <td>
                    <span id="txnHistory1:refreshDataGroup">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Claim Approval</td>
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

                        <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                            <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="InvoiceNumber" OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:collapseExpand('InvoiceNumber-<%# Eval("InvoiceNumber") %>');">
                                                <img id="imageInvoiceNumber-<%# Eval("InvoiceNumber") %>" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="10" width="10" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarrantyInvoiceHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceHeaderID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Dt" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Claim Type" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceType" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ServiceType.ServiceType")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="75px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketID")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IC Ticket Dt" HeaderStyle-Width="75px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Restore Dt" HeaderStyle-Width="75px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "RestoreDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust. Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust. Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HMR">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblHMR" Text='<%# DataBinder.Eval(Container.DataItem, "HMR")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Margin Warranty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "MarginWarranty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MC Serial No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--   <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server"></asp:Label>--%>
                                            <asp:LinkButton ID="lnkMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' OnClick="lnkMachineSerialNumber_Click" runat="server"></asp:LinkButton>
                                            <asp:Label ID="Label9" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TSIR No" Visible="false">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimStatus")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr.1 By" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnApproved1By" runat="server" Text="Approve" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnApproved1By_Click" />
                                            <asp:Label ID="lblApproved1By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1By.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr.1 On" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved1On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1On","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr.2 By" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnApproved2By" runat="server" Text="Approve" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnApproved2By_Click" />
                                            <asp:Label ID="lblApproved2By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2By.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr.2 On" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved2On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2On","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Apr.3 By" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnApproved3By" runat="server" Text="Approve" CssClass="InputButton" UseSubmitBehavior="true" Visible="false" OnClick="btnApproved3By_Click" />
                                            <asp:Label ID="lblApproved3By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3By.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr.3 On" HeaderStyle-Width="55px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved3On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3On","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Attachment" HeaderStyle-Width="240px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPscID" Text='<%# DataBinder.Eval(Container.DataItem, "PscID")%>' runat="server" Visible="false" />

                                            <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="false" BorderStyle="None">
                                                <Columns>
                                                    <asp:TemplateField
                                                        HeaderImageUrl="~/Images/AttachmentLogo.png">
                                                        <ItemStyle BorderStyle="None" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" Text='<%# Eval("fileName") %>' OnClientClick='<%# Eval("Url") %>' runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Images/grid_expand.png">
                                                        <ItemStyle BorderStyle="None" />
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="gvFileAttachedAF" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle BorderStyle="None" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click">
                                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                            </asp:LinkButton>
                                                            <asp:Label ID="lblAttachedFileID" Text='<%# DataBinder.Eval(Container.DataItem, "AttachedFileID")%>' runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="gvFileAttachedFSR" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle BorderStyle="None" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkFSRDownload" runat="server" OnClick="lnkFSRDownload_Click">
                                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                            </asp:LinkButton>
                                                            <asp:Label ID="lblAttachedFileID" Text='<%# DataBinder.Eval(Container.DataItem, "AttachedFileID")%>' runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="gvFileAttachedTSIR" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle BorderStyle="None" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTSIRDownload" runat="server" OnClick="lnkTSIRDownload_Click">
                                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                            </asp:LinkButton>
                                                            <asp:Label ID="lblAttachedFileID" Text='<%# DataBinder.Eval(Container.DataItem, "AttachedFileID")%>' runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                            <tr>
                                                <td colspan="100%" style="padding-left: 96px">
                                                    <div id="InvoiceNumber-<%# Eval("InvoiceNumber") %>" style="display: inline; position: relative;">
                                                        <asp:GridView ID="gvICTicketItems" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                                            <Columns>


                                                                <asp:TemplateField HeaderText="SAC / HSN Code">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWarrantyInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceItemID")%>' runat="server" Visible="false" />

                                                                        <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material" HeaderStyle-Width="78px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="150px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDesc")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delivery Number" HeaderStyle-Width="78px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category" HeaderStyle-Width="100px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="40px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="42px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOM")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Base+Tax" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBaseTax" Text='<%# DataBinder.Eval(Container.DataItem, "BaseTax")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="Material Status" HeaderStyle-Width="108px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialStatus" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatus")%>' runat="server" ></asp:Label>
                                                                            <asp:DropDownList ID="ddlMaterialStatus" runat="server" CssClass="TextBox" Visible="false" Width="100px">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                <asp:ListItem Value="Scrap">Scrap</asp:ListItem>
                                                                                <asp:ListItem Value="Return">Return</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Failure Mat Remarks 1" HeaderStyle-Width="150px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialStatusRemarks1" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks1")%>' runat="server"></asp:Label>
                                                                        <asp:DropDownList ID="ddlMaterialStatusRemarks1" runat="server" CssClass="TextBox" Visible="false" Width="100px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Apr. 1 Amt" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <%--  <asp:Label ID="lblApproved1Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Amount")%>' runat="server"></asp:Label>--%>
                                                                        <asp:TextBox ID="txtApproved1Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Amount")%>' CssClass="input" Width="70px" Enabled="false" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apr. 1 Remarks" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApproved1Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Remarks")%>' runat="server"></asp:Label>
                                                                        <asp:DropDownList ID="ddlApproved1Remarks" runat="server" CssClass="TextBox" Visible="false" Width="100px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Failure Mat Remarks 2" HeaderStyle-Width="150px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialStatusRemarks2" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks2")%>' runat="server"></asp:Label>
                                                                        <asp:DropDownList ID="ddlMaterialStatusRemarks2" runat="server" CssClass="TextBox" Visible="false" Width="100px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apr 2 Amt" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <%-- <asp:Label ID="lblApproved2Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Amount")%>' runat="server"></asp:Label>--%>
                                                                        <asp:TextBox ID="txtApproved2Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Amount")%>' CssClass="input" Width="70px" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apr. 2 Remarks" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApproved2Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Remarks")%>' runat="server"></asp:Label>
                                                                        <asp:DropDownList ID="ddlApproved2Remarks" runat="server" CssClass="TextBox" Visible="false" Width="100px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%--                                                                  <asp:TemplateField HeaderText="Failure Mat Remarks 3" HeaderStyle-Width="150px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialStatusRemarks3" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks3")%>' runat="server"></asp:Label>
                                                                        <asp:DropDownList ID="ddlMaterialStatusRemarks3" runat="server" CssClass="TextBox" Visible="false" Width="100px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Apr 3 Amt" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtApproved3Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3Amount")%>' CssClass="input" Width="70px" Enabled="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apr. 3 Remarks" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApproved3Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3Remarks")%>' runat="server"></asp:Label>
                                                                        <asp:DropDownList ID="ddlApproved3Remarks" runat="server" CssClass="TextBox" Visible="false" Width="100px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Material Return Status" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWarrantyMaterialReturnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus")%>' runat="server"></asp:Label>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TSIR No">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkTSIR" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' OnClick="lnkTSIR_Click" runat="server"></asp:LinkButton>
                                                                        <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
                                <HeaderStyle BackColor="#BFE4FF" Font-Bold="True" ForeColor="Black" />
                                <RowStyle ForeColor="Black" BackColor="#bfe4ff" />
                            </asp:GridView>
                        </div>
                    </span>
                </td>
            </tr>
        </table>
    </div>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="lnkDummy"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div runat="server" id="tblDashboard" class="container">
            <div id="Div1" runat="server" style="max-height: 500px; overflow: auto;">
                <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <asp:Button ID="btnClose" runat="server" Text="Close" />
    </asp:Panel>


    <asp:LinkButton ID="lnkDummyTSIR" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="mpTSIR" runat="server" PopupControlID="pnlTSIR" TargetControlID="lnkDummyTSIR" CancelControlID="btnCloseTSIR" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlTSIR" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div runat="server" id="Div2" class="container">
            <div id="Div3" runat="server" style="max-height: 500px; overflow: auto;">
                <asp:PlaceHolder ID="ph_usercontrols_2" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <asp:Button ID="btnCloseTSIR" runat="server" Text="Close" />
    </asp:Panel>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 98%;
            /*height: 140px;*/
        }
    </style>
</asp:Content>

