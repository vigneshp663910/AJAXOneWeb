<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketBasicInformation.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketBasicInformation" %>
<table id="txnHistory1:panelGridid" style="height: 100%; width: 100%" class="IC_basicInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">IC Ticket Basic Information</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandBasicInformation();">
                        <img id="imgBasicInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlBasicInformation" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">

                        <table class="labeltxt fullWidth">
                            <tr>
                                <td>
                                    <table class="labeltxt fullWidth">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Ticket "></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblICTicket" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Requested Date"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblRequestedDate" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label34" runat="server" CssClass="label" Text="District"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label42" runat="server" CssClass="label" Text="Complaint Description"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblComplaintDescription" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Status"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label44" runat="server" CssClass="label" Text="Information"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblInformation" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                                <td>
                                    <table class="labeltxt fullWidth">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label13" runat="server" CssClass="label" Text="Dealer"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Customer Category"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblCustomerCategory" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label38" runat="server" CssClass="label" Text="Contact Person Name & No"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label50" runat="server" CssClass="label" Text="Old IC Ticket Number"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label52" runat="server" CssClass="label" Text="Warranty"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label19" runat="server" CssClass="label" Text="Is Margin Warranty"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">

                                                        <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />

                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                                <td>
                                    <table class="labeltxt fullWidth">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Equipment"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblEquipment" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label18" runat="server" CssClass="label" Text="Model"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label53" runat="server" CssClass="label" Text="Warranty Expiry"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Last HMR Date & Value"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblLastHMRValue" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Refurbished Expiry"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="AMC Expiry"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table class="labeltxt fullWidth">
                        </table>
                        <%--    <table class="labeltxt fullWidth">
                            <tr>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Ticket "></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblICTicket" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label13" runat="server" CssClass="label" Text="Dealer"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label4" runat="server" CssClass="label" Text="Equipment"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblEquipment" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Requested Date"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblRequestedDate" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label18" runat="server" CssClass="label" Text="Model"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>                           
                            <tr>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label34" runat="server" CssClass="label" Text="District"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label38" runat="server" CssClass="label" Text="Contact Person Name & No"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label53" runat="server" CssClass="label" Text="Warranty Expiry"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label42" runat="server" CssClass="label" Text="Complaint Description"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblComplaintDescription" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label50" runat="server" CssClass="label" Text="Old IC Ticket Number"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Last HMR Date & Value"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblLastHMRValue" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Status"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label52" runat="server" CssClass="label" Text="Warranty"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
                                        </div>
                                    </div>
                                </td>
                                 <td>
                                    <div>
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Refurbished Expiry"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label44" runat="server" CssClass="label" Text="Information"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblInformation" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-col">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label19" runat="server" CssClass="label" Text="Is Margin Warranty"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">

                                            <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />

                                        </div>
                                    </div>
                                </td>
                                  <td>
                                    <div>
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="AMC Expiry"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>--%>
                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function collapseExpandBasicInformation(obj) {
        var gvObject = document.getElementById("MainContent_UC_BasicInformation_pnlBasicInformation");
        var imageID = document.getElementById("MainContent_UC_BasicInformation_imgBasicInformation");

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

