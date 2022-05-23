<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderInvoiceReport.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.PurchaseOrderInvoiceReport" %>
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
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table id="txnHistory4:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead" style="height: 10px; background-color: #fbfbfb;"></div>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter</div>
                    <div style="float: right; padding-top: 0px">                       
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" Text="Vendor Code"></asp:Label>
                                <asp:TextBox ID="txtCustomerCode" runat="server"></asp:TextBox>
                            </div>

                              <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" Text="Invoice Number"></asp:Label>
                                <asp:TextBox ID="txtInvoiceNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Invoice Date From"></asp:Label>
                                <asp:TextBox ID="txtInvoiceDateF" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInvoiceDateF" PopupButtonID="txtInvoiceDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtInvoiceDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="Invoice Date To"></asp:Label>
                                <asp:TextBox ID="txtInvoiceDateT" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvoiceDateT" PopupButtonID="txtInvoiceDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtInvoiceDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>


                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" Text="PO Number"></asp:Label>
                                <asp:TextBox ID="txtPurchaseOrderNo" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="PO Date From"></asp:Label>
                                <asp:TextBox ID="txtPurchaseOrderDateF" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPurchaseOrderDateF" PopupButtonID="txtPurchaseOrderDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtPurchaseOrderDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Invoice Date To"></asp:Label>
                                <asp:TextBox ID="txtPurchaseOrderDateT" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtPurchaseOrderDateT" PopupButtonID="txtPurchaseOrderDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtPurchaseOrderDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label12" runat="server" CssClass="label" Text="Material"></asp:Label>
                                <asp:TextBox ID="txtMaterial" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                           <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" Text="PO Type"></asp:Label>
                                <asp:DropDownList ID="ddlPurchaseOrderType" runat="server" >
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Stock Order</asp:ListItem>
                                    <asp:ListItem Value="2">Emergency Order</asp:ListItem>
                                    <asp:ListItem Value="3">Auto PO Order</asp:ListItem>
                                    <asp:ListItem Value="4">Warranty Order</asp:ListItem>
                                    <asp:ListItem Value="5">Machine Order</asp:ListItem>
                                    <asp:ListItem Value="6">Intra-Dealer Order</asp:ListItem>
                                </asp:DropDownList>
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
                <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                    <tr>
                        <td>
                            <span id="txnHistory1:refreshDataGroup">
                                <div class="boxHead">
                                    <div class="logheading">
                                        <div style="float: left">
                                            <table>
                                                <tr>
                                                    <td>PO Invoice Report</td>
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

                                    <asp:GridView ID="gvICTickets" runat="server"   CssClass="TableGrid" Width="2700px" AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                      
                                    </asp:GridView>
                                </div>
                                <div style="background-color: white">
                                    <asp:GridView ID="gvDM" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="gvDM_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL. No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceID")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.GrossAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Header Count" Visible="false">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeaderCount" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderCount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Count">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.ItemCount")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="background-color: white">
                                    <asp:GridView ID="gvDCM" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="gvDCM_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL. No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceID")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.GrossAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Header Count" Visible="false">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeaderCount" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderCount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Count">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.ItemCount")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </span>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
</asp:Content>