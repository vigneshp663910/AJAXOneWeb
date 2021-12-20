<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimInvoiceCreate.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyClaimInvoiceCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        function ConfirmCreate() {
            var x = confirm("No changes will be allowed after saving");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
    <div class="container">

        <div class="col2">


            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="20px" />
            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Create Invoice </div>
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
                                            <td colspan="2">
                                                <h3 style="margin-block-start: 1px; margin-block-end: 1px;">CONSOLIDATION IS EFFECTIVE FROM</h3>
                                            </td>

                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><b>1.	NEPI CLAIM  & COMMISSIONING  :: RESTORED DATE >=01.09.2018. Manual settlement for RESTORED DATE < 01.09.2018</b></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><b>2.	WARRANTY CLAIM  ::  IC LOGIN DATE >=16.11.2018 .  Manual settlement for   IC LOGIN DATE < 16.11.2018</b></td>
                                        </tr>
                                    </table>
                                    <table class="labeltxt">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label></td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="Year"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="TextBox" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Approved Month"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="TextBox">
                                                    <asp:ListItem Value="01">Jan</asp:ListItem>
                                                    <asp:ListItem Value="02">Feb</asp:ListItem>
                                                    <asp:ListItem Value="03">Mar</asp:ListItem>
                                                    <asp:ListItem Value="04">Apr</asp:ListItem>
                                                    <asp:ListItem Value="05">May</asp:ListItem>
                                                    <asp:ListItem Value="06">Jun</asp:ListItem>
                                                    <asp:ListItem Value="07">Jul</asp:ListItem>
                                                    <asp:ListItem Value="08">Aug</asp:ListItem>
                                                    <asp:ListItem Value="09">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Month Range"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlMonthRange" runat="server" CssClass="TextBox">
                                                    <asp:ListItem Value="1">W1 (1st to 7th) </asp:ListItem>
                                                    <asp:ListItem Value="2">W2 (8th to 15th)</asp:ListItem>
                                                    <asp:ListItem Value="3">W3 (16th to 23rd) </asp:ListItem>
                                                    <asp:ListItem Value="4">W4 (24th to 31st)</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Invoice Type"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlInvoiceTypeID" runat="server" CssClass="TextBox">
                                                    <asp:ListItem Value="1">NEPI & Commission</asp:ListItem>
                                                    <asp:ListItem Value="2">Warranty Below 50K</asp:ListItem>
                                                     <asp:ListItem Value="5">Warranty Partial Below 50K </asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnPrint_Click" Visible="false" />--%>
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
                                                <td>Create Invoice</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 3px; padding-bottom: 21px;">
                                <asp:Label ID="lblInvNo" runat="server" CssClass="label" Text="" Font-Bold="true" Font-Size="20px"></asp:Label>
                                <asp:Button ID="btnGenerateInvoice" runat="server" Text="Save" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnGenerateInvoice_Click" Visible="false" BackColor="#1deaff"  OnClientClick="return ConfirmCreate();"  />
                            </div>

                            <div style="background-color: white">
                                <asp:GridView ID="gvICTickets" runat="server" Width="100%" CssClass="TableGrid" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="SL. No" HeaderText="SL. No" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Material" HeaderText="Material" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="HSN Code" HeaderText="SAC / HSN Code" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Approved Value" HeaderText="Approved Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Taxable" HeaderText="Taxable" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="SGST %" HeaderText="SGST %" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="SGST Value" HeaderText="SGST Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="CGST %" HeaderText="CGST %" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="CGST Value" HeaderText="CGST Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="IGST %" HeaderText="IGST %" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="IGST Value" HeaderText="IGST Value" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>

                                </asp:GridView>
                                <asp:GridView ID="gvWS" runat="server" Width="100%" CssClass="TableGrid" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="SLNo" HeaderText="SL. No" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Material" HeaderText="Material" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="HSN" HeaderText="SAC / HSN Code" ItemStyle-HorizontalAlign="Center" />
                                        <%--   <asp:BoundField DataField="UOM" HeaderText="UOM" ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="TaxableValue" HeaderText="Taxable Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="SGST %" HeaderText="SGST %" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="SGST Value" HeaderText="SGST Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="CGST %" HeaderText="CGST %" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="CGST Value" HeaderText="CGST Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="IGST %" HeaderText="IGST %" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="IGST Value" HeaderText="IGST Value" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>