<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ApprovalForDeviatedCliamReques.aspx.cs" Inherits="DealerManagementSystem.ViewService.ApprovalForDeviatedCliamReques" %>

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
                        <div class="logheading">Deviated ICTicket Request</div>
                        <div style="float: right; padding-top: 0px">
                            <%--  <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>--%>
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
                                    <asp:Label ID="Label3" runat="server" Text="Claim Number"></asp:Label>
                                    <asp:TextBox ID="txtClaimNumber" runat="server"></asp:TextBox>
                                </div>
                            
                                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                    <asp:Button ID="btnRequest" runat="server" Text="Request" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnRequest_Click" OnClientClick="return dateValidation();" />
                                   
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table> 
       <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter : Deviated ICTicket Report</div>
                    <div style="float: right; padding-top: 0px">
                        <%--  <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>--%>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerF" runat="server" Width="250px" />
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblPlant" runat="server" Text="Claim Number"></asp:Label>
                                <asp:TextBox ID="txtClaimNumberF" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" Text="Requested Date From"></asp:Label>
                                <asp:TextBox ID="txtRequestedDateFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRequestedDateFrom" PopupButtonID="txtRequestedDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtRequestedDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" Text="Requested Date To"></asp:Label>
                                <asp:TextBox ID="txtRequestedDateTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRequestedDateTo" PopupButtonID="txtRequestedDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRequestedDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>

                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                 <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
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
                                                    <td>Deviated ICTicket Report</td>
                                                    <td>
                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                      <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyInvoiceHeaderID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvICTickets_PageIndexChanging">
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
                                                    <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TSIR No" Visible="false">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Approve"  >
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeviatedIsApproved" Text='<%# DataBinder.Eval(Container.DataItem, "DeviatedIsApproved")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Status" HeaderStyle-Width="55px">--%>
                                            <asp:TemplateField HeaderText="Reject">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimStatus")%>' runat="server"></asp:Label>--%>
                                                     
                                                    <asp:Label ID="lblDeviatedIsRejected" Text='<%# DataBinder.Eval(Container.DataItem, "DeviatedIsRejected")%>' runat="server"></asp:Label>
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
                                                                        <asp:TemplateField HeaderText="Material"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Material Desc"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDesc")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delivery Number"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Category" >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="UOM"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOM")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Base+Tax"  >
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBaseTax" Text='<%# DataBinder.Eval(Container.DataItem, "BaseTax")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                                                      
                                                                        <asp:TemplateField HeaderText="TSIR No">
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
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
        </div>
    </div>
</asp:Content>

