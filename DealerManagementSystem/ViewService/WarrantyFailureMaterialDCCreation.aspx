<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyFailureMaterialDCCreation.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyFailureMaterialDCCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
       
    </script>
    <div class="container">
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
        </script>
        <div class="col2">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table id="txnHistory3:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Failed Material Create </div>
                            <div style="float: right; padding-top: 0px">
                                <a href="javascript:collapseExpand();">
                                    <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                            </div>
                        </div>
                        <asp:Panel ID="pnlFilterContent" runat="server">
                            <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                    <table class="labeltxt">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged" AutoPostBack="true" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="DC Draft"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlDCTemplate" runat="server" CssClass="TextBox" Width="250px" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                            </td>
                                        </tr>
                                    </table>
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
                            <div class="logheading">Create Delivery Challan </div>
                            <div style="float: right; padding-top: 0px">
                                <a href="javascript:collapseExpand();">
                                    <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="rf-p " id="txnHistory1:inputFiltersPanel">
                                <div class="rf-p-b " id="txnHistory1:inputFiltersPanel_body">
                                    <table class="labeltxt">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Delivery To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDeliveryTo" runat="server" CssClass="label" Text="Doddaballapur"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Transporter Name"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtTransporterName" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Docket Details"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtDocketDetails" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Packing Details"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtPackingDetails" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnCreateDC" runat="server" Text="Create DC" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnCreateDC_Click" OnClientClick="return dateValidation();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <span id="txnHistory1:refreshDataGroup">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Delivery Challan List</td>
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

                            <div style="background-color: white">
                                <asp:GridView ID="gvDCTemplate" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="DCTemplateID" OnRowDataBound="gvDCTemplate_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvDCTemplate_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:collapseExpand('DCTemplateID-<%# Eval("DCTemplateID") %>');">
                                                    <img id="imageDCTemplateID-<%# Eval("DCTemplateID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="10" width="10" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DC Draft ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCTemplateID" Text='<%# DataBinder.Eval(Container.DataItem, "DCTemplateID")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DC Draft Remarks">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCTemplateName" Text='<%# DataBinder.Eval(Container.DataItem, "DCTemplateName" )%>' runat="server"></asp:Label>
                                                <tr>
                                                    <td colspan="100%" style="padding-left: 96px">
                                                        <div id="ICTicketID-<%# Eval("DCTemplateID") %>" style="display: inline; position: relative;">

                                                            <asp:GridView ID="gvDCItem" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="DCTemplateItemID"
                                                                CssClass="TableGrid">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWarrantyInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.WarrantyInvoiceItemID")%>' runat="server" Visible="false"></asp:Label>

                                                                            <asp:CheckBox ID="cbSelectedMaterial" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim" HeaderStyle-Width="62px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceNumber")%>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Claim Dt">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="75px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.ICTicketID")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IC Ticket Dt" HeaderStyle-Width="75px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Restore Dt" HeaderStyle-Width="75px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.RestoreDate","{0:d}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cust. Code">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.CustomerCode")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cust. Name">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.CustomerName")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="HMR">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHMR" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.HMR")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Margin Warranty">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.MarginWarranty")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.Model")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MC Serial No">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.MachineSerialNumber")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FSR No">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFSRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.FSRNumber")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TSIR No">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.TSIRNumber")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-Width="55px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.ClaimStatus")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="SAC / HSN Code">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.HSNCode")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Material" HeaderStyle-Width="78px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.Material")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="150px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.MaterialDesc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="40px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.Qty")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
                                                                <HeaderStyle BackColor="#BFE4FF" Font-Bold="True" ForeColor="Black" />
                                                                <RowStyle ForeColor="Black" BackColor="#bfe4ff" />
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
</asp:Content>