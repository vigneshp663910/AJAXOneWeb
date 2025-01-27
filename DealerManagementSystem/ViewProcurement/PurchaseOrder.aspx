<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.PurchaseOrder" %>

<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderView.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderView" %>
<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderCreate.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .Popup {
            transition: initial;
        }
    </style>
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
    </script>
    <style>
        .Back {
            float: right;
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged"/>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Office</label>
                        <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">PO Number</label>
                        <asp:TextBox ID="txtPoNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">PO Date From</label>
                        <asp:TextBox ID="txtPoDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPoDateFrom" PopupButtonID="txtPoDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPoDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">PO Date To</label>
                        <asp:TextBox ID="txtPoDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPoDateTo" PopupButtonID="txtPoDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPoDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>                    
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Order Type</label>
                        <asp:DropDownList ID="ddlPurchaseOrderType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Division</label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">PO Status</label>
                        <asp:DropDownList ID="ddlPOStatus" runat="server" CssClass="form-control"> 
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-8">
                        <label class="modal-label">Action</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                        
                        <asp:Button ID="btnCreatePO" runat="server" CssClass="btn Save" Text="Create PO" OnClick="btnCreatePO_Click" Width="120px"></asp:Button>
                       <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />--%>
                        
                        <asp:Button ID="btnExportExcelDetails" runat="server" Text="Export PO Details" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelDetails_Click" Width="150px" />
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
                                                <td>Purchase Order(s):</td>
                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" ToolTip="Excel Download..." />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20"
                                EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="<i class='fa fa-eye fa-1x' aria-hidden='true'></i>" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%--<asp:Button ID="btnViewPO" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewPO_Click" Width="75px" Height="25px" />--%>
                                             <asp:ImageButton ID="btnViewPO" ImageUrl="~/Images/Preview.png" runat="server" ToolTip="View..." Height="20px" Width="20px" ImageAlign="Middle"  OnClick="btnViewPO_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblPurchaseOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblPurchaseOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receiving Location">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location.OfficeName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorCode" Text='<%# DataBinder.Eval(Container.DataItem, "Vendor.DealerCode")%>' runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblVendorName" Text='<%# DataBinder.Eval(Container.DataItem, "Vendor.DealerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Order Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOType" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderType.PurchaseOrderType")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblf_division" Text='<%# DataBinder.Eval(Container.DataItem, "Division.DivisionCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:TemplateField HeaderText="Curr" HeaderStyle-Width="70px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblf_currency" Text='<%# DataBinder.Eval(Container.DataItem, "Currency")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--   <asp:TemplateField HeaderText="Vendor Code" HeaderStyle-Width="76px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblf_bill_to" Text='<%# DataBinder.Eval(Container.DataItem, "BillTo")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="PO Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderStatus.ProcurementStatus")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sale Order Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderNumber")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Value" >
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "Created.ContactName")%>' runat="server"></asp:Label>
                                           
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
        </div>
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton">
                    <asp:Button ID="btnPurchaseOrderViewBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderViewBack_Click" />
                </div>
            </div>
            <UC:UC_PurchaseOrderView ID="UC_PurchaseOrderView" runat="server"></UC:UC_PurchaseOrderView>
        </div>
        <div class="col-md-12" id="divPurchaseOrderCreate" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere1"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnPurchaseOrderCreateBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderCreateBack_Click" />
                </div>
            </div>
            <UC:UC_PurchaseOrderCreate ID="UC_PurchaseOrderCreate" runat="server"></UC:UC_PurchaseOrderCreate>
        </div>
    </div>
</asp:Content>
