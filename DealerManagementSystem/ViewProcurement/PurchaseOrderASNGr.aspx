<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderAsnGR.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.PurchaseOrderAsnGR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderASNGrView.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderASNGrView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged" />
                        </div>
                           <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Office</label>
                            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Gr Number</label>
                            <asp:TextBox ID="txtGrNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Gr Date From</label>
                            <asp:TextBox ID="txtGrDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtGrDateFrom" PopupButtonID="txtPoDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtGrDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Gr Date To</label>
                            <asp:TextBox ID="txtGrDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtGrDateTo" PopupButtonID="txtPoDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtGrDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                      <%--  <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Gr Status</label>
                            <asp:DropDownList ID="ddlGrStatus" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>--%>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">-</label>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="65px" />
                            <%--<asp:Button ID="btnCreatePO" runat="server" CssClass="btn Save" Text="Create PO" OnClick="btnCreatePO_Click" Width="150px"></asp:Button>
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />--%>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Gr Report</legend>
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
                        <asp:GridView ID="gvAsnGr" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20"
                            OnPageIndexChanging="gvAsnGr_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewPO" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewPO_Click" Width="75px" Height="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gr Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrID" Text='<%# DataBinder.Eval(Container.DataItem, "GrID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblGrNumber" Text='<%# DataBinder.Eval(Container.DataItem, "GrNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblGrDate" Text='<%# DataBinder.Eval(Container.DataItem, "GrDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asn Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblASNNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.AsnNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblAsnDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.AsnDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchaseOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.PurchaseOrderNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblPurchaseOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.PurchaseOrderDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.Dealer.DealerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Dealer Office">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.Location.OfficeName")%>' runat="server"></asp:Label>
                                       </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorCode" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.Vendor.DealerCode")%>' runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblVendorName" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.Vendor.DealerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <%--   <asp:TemplateField HeaderText="Gr Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.ProcurementStatus")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn" style="text-align: right">
                    <asp:Button ID="btnPurchaseOrderASNGrViewBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderASNGrViewBack_Click" />
                </div>
            </div>
            <UC:UC_PurchaseOrderASNGrView ID="UC_PurchaseOrderASNGrView" runat="server"></UC:UC_PurchaseOrderASNGrView>
        </div>
    </div>
</asp:Content>
