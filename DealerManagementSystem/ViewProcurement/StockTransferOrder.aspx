<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="StockTransferOrder.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.StockTransferOrder" %>

<%@ Register Src="~/ViewProcurement/UserControls/StockTransferOrderView.ascx" TagPrefix="UC" TagName="UC_StockTransferOrderView" %>
<%@ Register Src="~/ViewProcurement/UserControls/StockTransferOrderCreate.ascx" TagPrefix="UC" TagName="UC_StockTransferOrderCreate" %>
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
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Code</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged"/>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Office</label>
                        <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                    </div>  
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">STO Number</label>
                        <asp:TextBox ID="txtPoNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">STO Date From</label>
                        <asp:TextBox ID="txtPoDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPoDateFrom" PopupButtonID="txtPoDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPoDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">STO Date To</label>
                        <asp:TextBox ID="txtPoDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPoDateTo" PopupButtonID="txtPoDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPoDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">STO Status</label>
                        <asp:DropDownList ID="ddlPOStatus" runat="server" CssClass="form-control"> 
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="65px" />
                        <asp:Button ID="btnCreatePO" runat="server" CssClass="btn Save" Text="Create STO" OnClick="btnCreatePO_Click" Width="150px"></asp:Button>
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                        <asp:Button ID="btnExportExcelDetails" runat="server" Text="Export STO Details" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelDetails_Click" Width="150px" />
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
                                                <td>Stock Transfer Order(s):</td>
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
                            <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" 
                                EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewPO" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewPO_Click" Width="75px" Height="25px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STO">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockTransferOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "StockTransferOrderID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblStockTransferOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "StockTransferOrderNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblStockTransferOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "StockTransferOrderDate","{0:d}")%>' runat="server"></asp:Label>
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
                                    <asp:TemplateField HeaderText="Destination Location">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDestinationOffice" Text='<%# DataBinder.Eval(Container.DataItem, "DestinationOffice.OfficeName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Source Dealer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSourceDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SourceDealer.DealerCode")%>' runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSourceDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "SourceDealer.DealerName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Source Location">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSourceOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SourceOffice.OfficeName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Gross Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrossValue" Text='<%# DataBinder.Eval(Container.DataItem, "GrossValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created By">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
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
            <UC:UC_StockTransferOrderView ID="UC_StockTransferOrderView" runat="server"></UC:UC_StockTransferOrderView>
        </div>
        <div class="col-md-12" id="divPurchaseOrderCreate" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere1"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnPurchaseOrderCreateBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderCreateBack_Click" />
                </div>
            </div>
            <UC:UC_StockTransferOrderCreate ID="UC_StockTransferOrderCreate" runat="server"></UC:UC_StockTransferOrderCreate>
        </div>
    </div>
</asp:Content>

