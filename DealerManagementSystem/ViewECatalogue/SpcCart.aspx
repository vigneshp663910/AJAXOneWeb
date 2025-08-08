<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcCart.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcCart" %>

<%@ Register Src="~/ViewECatalogue/UserControls/SpcCartView.ascx" TagPrefix="UC" TagName="UC_SpcCartView" %>
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
            <fieldset id="fsCriteria" class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
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
                        <label class="modal-label">PO Number</label>
                        <asp:TextBox ID="txtPoNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Date From</label>
                        <asp:TextBox ID="txtPoDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPoDateFrom" PopupButtonID="txtPoDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPoDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Date To</label>
                        <asp:TextBox ID="txtPoDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPoDateTo" PopupButtonID="txtPoDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPoDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Division</label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Model</label>
                        <asp:DropDownList ID="ddlPurchaseOrderType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-8">
                        <label class="modal-label">Action</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                        <asp:Button ID="btnExportExcelDetails" runat="server" Text="Export PO Details" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="150px" />
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
                                                <td>PO(s):</td>
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
                                    <div style="float: right; overflow: auto;">
                                        <%--<div style="float :left">
                                             
                                        </div>--%>
                                        <div style="float: right">
                                            <img id="fs" alt="" src="../Images/NormalScreen.png" onclick="ScreenControl(2)" width="23" height="23" style="display: none;" />
                                            <img id="rs" alt="" src="../Images/FullScreen.jpg" onclick="ScreenControl(1)" width="23" height="23" style="display: block;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15"
                                EmptyDataText="No Data Found"
                                OnRowDataBound="gvICTickets_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:collapseExpand('CartOrderNo-<%# Eval("CartOrderNo") %>');">
                                                <img id="imageCartOrderNo-<%# Eval("CartOrderNo") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_collapse.png" height="10" width="10" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White" HeaderStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="15px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="<i class='fa fa-eye fa-1x' aria-hidden='true'></i>" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnViewPO" ImageUrl="~/Images/Preview.png" runat="server" ToolTip="View..." Height="20px" Width="20px" ImageAlign="Middle" OnClick="btnViewPO_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cart Order No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblspcCartID" Text='<%# DataBinder.Eval(Container.DataItem, "spcCartID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblCartOrderNo" Text='<%# DataBinder.Eval(Container.DataItem, "CartOrderNo")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblCartOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "CartOrderDate","{0:d}")%>' runat="server"></asp:Label>
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

                                    <asp:TemplateField HeaderText="Assembly Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorCode" Text='<%# DataBinder.Eval(Container.DataItem, "Assembly.AssemblyCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assembly Description">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOType" Text='<%# DataBinder.Eval(Container.DataItem, "Assembly.AssemblyDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivisionCode" Text='<%# DataBinder.Eval(Container.DataItem, "Assembly.Model.Division.DivisionCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Assembly.Model.Model")%>' runat="server"></asp:Label>
                                               <tr>
                                            <td colspan="100%" style="padding-left: 96px">
                                                <div id="CartOrderNo-<%# Eval("CartOrderNo") %>" style="display: inline; position: relative;">
                                                    <asp:GridView ID="gvCartItems" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Material">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblspcCartItemID" Text='<%# DataBinder.Eval(Container.DataItem, "spcCartItemID")%>' runat="server" Visible="false" />
                                                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SpcMaterial.Material")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                            <asp:TemplateField HeaderText="Material Description" HeaderStyle-Width="40px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "SpcMaterial.MaterialDescription","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="42px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartQty" Text='<%# DataBinder.Eval(Container.DataItem, "PartQty")%>' runat="server"></asp:Label>
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
                                            </td>
                                        </tr>
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
            <UC:UC_SpcCartView ID="UC_SpcCartView" runat="server"></UC:UC_SpcCartView>
        </div>
    </div>
</asp:Content>
