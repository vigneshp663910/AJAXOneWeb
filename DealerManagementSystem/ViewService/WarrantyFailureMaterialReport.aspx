<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyFailureMaterialReport.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyFailureMaterialReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS/YDMS_Scripts.js"></script>--%>
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "../Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "../Images/grid_expand.png";
            }
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Code</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">IC Ticket</label>
                        <asp:TextBox ID="txtICServiceTicket" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">IC Ticket Date From</label>
                        <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">IC Ticket Date To</label>
                        <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Claim ID</label>
                        <asp:TextBox ID="txtClaimID" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Claim Date From</label>
                        <asp:TextBox ID="txtClaimDateF" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtClaimDateF" PopupButtonID="txtClaimDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtClaimDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Claim Date To</label>
                        <asp:TextBox ID="txtClaimDateT" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtClaimDateT" PopupButtonID="txtClaimDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtClaimDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">DC Date From</label>
                        <asp:TextBox ID="txtDCDateF" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDCDateF" PopupButtonID="txtDCDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDCDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">DC Date To</label>
                        <asp:TextBox ID="txtDCDateT" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtDCDateT" PopupButtonID="txtDCDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtDCDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">DC Number</label>
                        <asp:TextBox ID="txtDCNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Material Return Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Visible="false" Width="100px"/>
                        <asp:Button ID="Button1" runat="server" Text="Export to excel report" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="160px" />
                        <asp:Button ID="Button2" runat="server" Text="Export to excel For SAP" CssClass="btn Approval" UseSubmitBehavior="true" OnClick="btnExportExcelForSAP_Click" Width="160px" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Failed Material Report</legend>
                    <div class="col-md-12 Report">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
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
                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyInvoiceHeaderID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:collapseExpand('WarrantyInvoiceHeaderID-<%# Eval("WarrantyInvoiceHeaderID") %>');">
                                            <img id="imageWarrantyInvoiceHeaderID-<%# Eval("WarrantyInvoiceHeaderID") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_collapse.png" height="10" width="10" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim" HeaderStyle-Width="62px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWarrantyInvoiceHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceHeaderID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Dt">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="75px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket Dt" HeaderStyle-Width="75px" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Restore Dt" HeaderStyle-Width="75px" Visible="false">
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
                                <asp:TemplateField HeaderText="Margin Warranty" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "MarginWarranty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MC Serial No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FSR No" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFSRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "FSRNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr.1 By" HeaderStyle-Width="55px" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproved1By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1By.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr.1 On" HeaderStyle-Width="55px" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproved1On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1On","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr.2 By" HeaderStyle-Width="55px" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproved2By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2By.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr.2 On" HeaderStyle-Width="55px" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproved2On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2On","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachment" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPscID" Text='<%# DataBinder.Eval(Container.DataItem, "PscID")%>' runat="server" Visible="false" />
                                        <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemStyle BorderStyle="None" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" Text='<%# Eval("fileName") %>' OnClientClick='<%# Eval("Url") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Status" HeaderStyle-Width="55px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimStatus")%>' runat="server"></asp:Label>
                                        <tr>
                                            <td colspan="100%" style="padding-left: 96px">
                                                <div id="WarrantyInvoiceHeaderID-<%# Eval("WarrantyInvoiceHeaderID") %>" style="display: inline; position: relative;">
                                                    <asp:GridView ID="gvICTicketItems" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SAC / HSN Code" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Desc">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDesc")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category" HeaderStyle-Width="200px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="100px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOM")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="90px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Base+Tax" HeaderStyle-Width="55px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBaseTax" Text='<%# DataBinder.Eval(Container.DataItem, "BaseTax")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Failure Mat Remarks 1" HeaderStyle-Width="150px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialStatusRemarks1" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks1")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apr. 1 Amt" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproved1Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Amount")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apr. 1 Remarks" HeaderStyle-Width="55px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproved1Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Remarks")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Failure Mat Remarks 2" HeaderStyle-Width="150px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialStatusRemarks2" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks2")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apr 2 Amt" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproved2Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Amount")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apr. 2 Remarks" HeaderStyle-Width="55px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproved2Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Remarks")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Return Status">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialReturnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#ffffff" />
                                                        <FooterStyle ForeColor="White" />
                                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
