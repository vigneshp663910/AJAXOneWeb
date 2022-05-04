<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesCommissionClaimApprove.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SalesCommissionClaimApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">    
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "/Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "/Images/grid_expand.png";
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

    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Dealer</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Claim Number</label>
                        <asp:TextBox ID="txtClaimID" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Claim Date From</label>
                        <asp:TextBox ID="txtClaimDateF" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtClaimDateF" PopupButtonID="txtClaimDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtClaimDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                    </div>
                    <div class="col-md-2 text-left">
                        <label>Claim Date To</label>

                        <asp:TextBox ID="txtClaimDateT" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtClaimDateT" PopupButtonID="txtClaimDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtClaimDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                    </div>
                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:Label ID="lblStatus" runat="server" CssClass="label" Text="Status"></asp:Label>
                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Save" Width="100px" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                    </div>
                </div>
            </fieldset>
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Claim :</td>
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
                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="SalesCommissionClaimID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:collapseExpand('SalesCommissionClaimID-<%# Eval("SalesCommissionClaimID") %>');">
                                            <img id="imageInvoiceNumber-<%# Eval("SalesCommissionClaimID") %>" alt="Click to show/hide orders" border="0" src="/Images/grid_collapse.png" height="10" width="10" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesCommissionClaimID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Dt" HeaderStyle-Width="55px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimDate" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                                    <asp:TemplateField HeaderText="Cust. Code">
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
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="55px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr.1 By" HeaderStyle-Width="55px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnApproved1By" runat="server" Text="Approve" CssClass="btn Save" Width="80px" Height="30px" UseSubmitBehavior="true" Visible="false" OnClick="btnApproved1By_Click" />
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
                                        <asp:Button ID="btnApproved2By" runat="server" Text="Approve" CssClass="btn Save" Width="80px" Height="30px" UseSubmitBehavior="true" Visible="false" OnClick="btnApproved2By_Click" />
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
                                        <asp:Button ID="btnApproved3By" runat="server" Text="Approve" CssClass="btn Save" Width="80px" Height="30px" UseSubmitBehavior="true" Visible="false" OnClick="btnApproved3By_Click" />
                                        <asp:Label ID="lblApproved3By" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3By.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apr.3 On" HeaderStyle-Width="55px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblApproved3On" Text='<%# DataBinder.Eval(Container.DataItem, "Approved3On","{0:d}")%>' runat="server"></asp:Label>
                                        <tr>
                                            <td colspan="100%" style="padding-left: 96px">
                                                <div id="SalesCommissionClaimID-<%# Eval("SalesCommissionClaimID") %>" style="display: inline; position: relative;">

                                                    <table CssClass="table table-bordered table-condensed Grid">
                                                        <tr>
                                                            <td>
                                                                <label>SAC / HSN Code</label></td>
                                                            <td>
                                                                <label>Material</label></td>
                                                            <td>
                                                                <label>Qty</label></td>
                                                            <td>
                                                                <label>Amount</label></td>
                                                            <td>
                                                                <label>Tax</label></td>
                                                            <td>
                                                                <label>Apr. 1 Amt</label></td>
                                                            <td>
                                                                <label>Apr. 1 Remarks</label></td>
                                                            <td>
                                                                <label>Apr. 2 Amt</label></td>
                                                            <td>
                                                                <label>Apr. 2 Remarks</label></td>
                                                            <td>
                                                                <label>Apr. 3 Amt</label></td>
                                                            <td>
                                                                <label>Apr. 3 Remarks</label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblWarrantyInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimID")%>' runat="server" Visible="false" />
                                                                <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Material.HSN")%>' runat="server"></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Material.MaterialCode")%>' runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Qty")%>' runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Amount")%>' runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblBaseTax" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.BaseTax")%>' runat="server"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtApproved1Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Approved1Amount")%>' CssClass="form-control"  Enabled="false" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtApproved1Remarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Approved1Remarks")%>' CssClass="form-control"   Enabled="false" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtApproved2Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Approved2Amount")%>' CssClass="form-control"  Enabled="false" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtApproved2Remarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Approved2Remarks")%>' CssClass="form-control"   Enabled="false" /></td>

                                                            <td>
                                                                <asp:TextBox ID="txtApproved3Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Approved3Amount")%>' CssClass="form-control" Enabled="false" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtApproved3Remarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.Approved1Remarks")%>' CssClass="form-control"   Enabled="false" /></td>

                                                        </tr>
                                                    </table>

                                                    <%--  <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="150px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimItem.MaterialDesc")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                    <%--   <asp:TemplateField HeaderText="Delivery Number" HeaderStyle-Width="78px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                    <%--   <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="42px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOM")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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


                    </fieldset>
                </div>
            </div>
        </div>
    </div>



</asp:Content>



