<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReturn.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.PurchaseOrderReturn" %>

<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderReturnView.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderReturnView" %>
<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderReturnCreate.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderReturnCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessagePoReturn" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Office</label>
                            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">PO Return Number</label>
                            <asp:TextBox ID="txtPoReturnNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">PO Return Date From</label>
                            <asp:TextBox ID="txtPoReturnDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPoReturnDateFrom" PopupButtonID="txtPoReturnDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPoReturnDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">PO Return Date To</label>
                            <asp:TextBox ID="txtPoReturnDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPoReturnDateTo" PopupButtonID="txtPoReturnDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPOReturnDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">PO Return Status</label>
                            <asp:DropDownList ID="ddlPoReturnStatus" runat="server" CssClass="form-control"> 
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="65px" />
                            <asp:Button ID="btnCreatePoReturn" runat="server" CssClass="btn Save" Text="Create PO Return" OnClick="btnCreatePoReturn_Click" Width="150px"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Purchase Return(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCountPoReturn" runat="server" CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeftPoReturn" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftPoReturn_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRightPoReturn" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightPoReturn_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvPoReturn" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewPoReturn" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewPoReturn_Click" Width="75px" Height="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="Purchase Return Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseOrderReturnID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblPurchaseOrderNumberReturn" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblPurchaseOrderReturnDate" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnDate","{0:d}")%>' runat="server"></asp:Label>
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
                                <asp:TemplateField HeaderText="Location">
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
                                <asp:TemplateField HeaderText="Purchase Return Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOReturnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnStatus.ProcurementStatus")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
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
        <div class="col-md-12" id="divPoReturnDetailsView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="PoReturnViewboxHere"></div>
                <div class="back-buttton" id="PoReturnViewBackBtn" style="text-align: right">
                    <asp:Button ID="btnPurchaseOrderReturnViewBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderReturnViewBack_Click" />
                </div>
            </div>
            <UC:UC_PurchaseOrderReturnView ID="UC_PurchaseOrderReturnView" runat="server"></UC:UC_PurchaseOrderReturnView>
        </div>
        <div class="col-md-12" id="divPurchaseOrderReturnCreate" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="PoReturnCreateboxHere"></div>
                <div class="back-buttton" id="PoReturnCreateBackBtn" style="text-align: right">
                    <asp:Button ID="btnPurchaseOrderReturnCreateBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderReturnCreateBack_Click" />
                </div>
            </div>
            <asp:Label ID="lblMessagePoReturnCreate" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <%--<legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>--%>
                    <UC:UC_PurchaseOrderReturnCreate ID="UC_PurchaseOrderReturnCreate" runat="server"></UC:UC_PurchaseOrderReturnCreate>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Search" OnClick="btnSave_Click" Width="150px" />
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
