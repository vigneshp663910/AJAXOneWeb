<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DealerView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.DealerView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_DealerCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>


<script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
<script type="text/javascript">  
    function FleAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {

        var txtCustomerID = document.getElementById('MainContent_UC_DealerView_txtFleetID');
        txtCustomerID.value = CustomerID.innerText;
        var txtCustomer = document.getElementById('MainContent_UC_DealerView_txtFleet');
        txtCustomer.value = CustomerName.innerText;

        document.getElementById('FleDivAuto').style.display = "none";
    }
</script>
<script type="text/javascript"> 
    $(function () {
        $('#FleDiv1').click(function () {
            var CustomerID = document.getElementById('lblDealerID1')
            var CustomerName = document.getElementById('lblDealerName1')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv2').click(function () {
            var CustomerID = document.getElementById('lblDealerID2')
            var CustomerName = document.getElementById('lblDealerName2')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv3').click(function () {
            var CustomerID = document.getElementById('lblDealerID3')
            var CustomerName = document.getElementById('lblDealerName3')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv4').click(function () {
            var CustomerID = document.getElementById('lblDealerID4')
            var CustomerName = document.getElementById('lblDealerName4')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv5').click(function () {
            var CustomerID = document.getElementById('lblDealerID5')
            var CustomerName = document.getElementById('lblDealerName5')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });

</script>
<style>
    .fieldset-borderAuto {
        border: solid 1px #cacaca;
        margin: 1px 0;
        border-radius: 5px;
        padding: 10px;
        background-color: #b4b4b4;
    }

        .fieldset-borderAuto tr {
            /* background-color: #000084; */
            background-color: inherit;
            font-weight: bold;
            color: white;
        }

        .fieldset-borderAuto:hover {
            background-color: blue;
        }
</style>


<style type="text/css">
    .mycheckBig input {
        width: 25px;
        height: 25px;
    }
</style>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="dealerAction">
            <%--<asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />--%>
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <%-- <asp:LinkButton ID="lnkBtnAddDealerOffice" runat="server" OnClick="lnkBtnActions_Click">Add Dealer Office</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkBtnAddNotification" runat="server" OnClick="lnkBtnActions_Click">Add Dealer Notification</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Dealer</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer Code :</label>
                    <asp:Label ID="lblDealerCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblDealerState" runat="server" CssClass="label"></asp:Label>
                </div>
                <%--<div class="col-md-12">
                    <label>Team Lead : </label>
                    <asp:Label ID="lblTeamLead" runat="server" CssClass="label"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer Name : </label>
                    <asp:Label ID="lblDealerName" runat="server" CssClass="label"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Country : </label>
                    <asp:Label ID="lblDealerCountry" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Active : </label>
                    <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
                <%--<div class="col-md-12">
                    <label>Serivce Manager : </label>
                    <asp:Label ID="lblSerivceManager" runat="server" CssClass="label"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Region : </label>
                    <asp:Label ID="lblDealerRegion" runat="server" CssClass="label"></asp:Label>
                </div>
                <%-- <div class="col-md-12">
                    <label>Active : </label>
                    <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>--%>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpDealer" runat="server" ToolTip="Dealer" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="tpnlDealerOffice" runat="server" HeaderText="Dealer Office" Font-Bold="True" ToolTip="List of Dealer Office...">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Branch Office(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountDealerOffice" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerOfficeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerOfficeArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerOfficeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerOfficeArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDealerOffice" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealerOffice_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        <asp:Label ID="lblOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeCode" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sap Location Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSapLocationCode" Text='<%# DataBinder.Eval(Container.DataItem, "SapLocationCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address1")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDealerOfficeDelete" runat="server" OnClick="lnkbtnDealerOfficeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlDealerEmployee" runat="server" HeaderText="Dealer Employee">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Employee(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountDealerEmployee" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerEmployeeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerEmployeeArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerEmployeeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerEmployeeArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDealerEmployee" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealerEmployee_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                    <ItemTemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:Label ID="lblDealerEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDepartment.DealerDepartment")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDesignation.DealerDesignation")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Father Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFatherName" Text='<%# DataBinder.Eval(Container.DataItem, "FatherName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- <asp:TemplateField HeaderText="DOB">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" Text='<%# DataBinder.Eval(Container.DataItem, "DOB")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Contact Number" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNumber" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNumber")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emergency Contact No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmergencyContactNumber" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "EmergencyContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "EmergencyContactNumber")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Email")%>'><%# DataBinder.Eval(Container.DataItem, "Email")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aadhaar Card No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAadhaarCardNo" Text='<%# DataBinder.Eval(Container.DataItem, "AadhaarCardNo")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="PAN No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPANNo" Text='<%# DataBinder.Eval(Container.DataItem, "PANNo")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlDealerNotification" runat="server" HeaderText="Dealer Notification">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Dealer Notification(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountDealerNotification" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerNotificationArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerNotificationArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerNotificationArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerNotificationArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDealerNotification" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealerNotification_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                    <ItemTemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:Label ID="lblDealerNotificationID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerNotificationID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblModuleName" Text='<%# DataBinder.Eval(Container.DataItem, "Module.ModuleName")%>' runat="server" />
                                        <asp:Label ID="lblDealerNotificationModuleID" Text='<%# DataBinder.Eval(Container.DataItem, "Module.DealerNotificationModuleID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "User.ContactName")%>' runat="server" />
                                        <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Login ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "User.UserName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "User.Department.DealerDepartment")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "User.Designation.DealerDesignation")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNumber" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "User.ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "User.ContactNumber")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "User.Mail")%>'><%# DataBinder.Eval(Container.DataItem, "User.Mail")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SMS">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblIsSMS" Text='<%# DataBinder.Eval(Container.DataItem, "IsSMS")%>' runat="server" />--%>
                                        <asp:CheckBox ID="chkbxIsSMS" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSMS")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lblIsMail" Text='<%# DataBinder.Eval(Container.DataItem, "IsMail")%>' runat="server" />--%>
                                        <asp:CheckBox ID="chkbxIsMail" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsMail")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnNotificationDelete" runat="server" OnClick="lnkBtnNotificationDelete_Click"><i class="fa fa-fw fa-times" style="font-size: 18px"></i></asp:LinkButton>
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
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>



<asp:Panel ID="pnlDealer" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageDealerEdit" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_DealerCreate ID="UC_Dealer" runat="server"></UC:UC_DealerCreate>
        </div>
        <%--<div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateDealer" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateDealer_Click" />
        </div>--%>
    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Dealer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlDealer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddNotification" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Dealer Notifiaction</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnAddNotificationClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAddNotification" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label" runat="server" id="lblDealer" visible="false">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Module</label>
                        <asp:DropDownList ID="ddlDealerNotificationModule" runat="server" CssClass="form-control" />
                        <%--OnSelectedIndexChanged="ddlDealerNotificationModule_SelectedIndexChanged" AutoPostBack="true" --%>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Employee</label>
                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Send SMS</label>
                        <asp:CheckBox ID="cbSendSMS" runat="server" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Send Email</label>
                        <asp:CheckBox ID="cbSendEmail" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddNotification" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddNotification_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddNotification" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddNotification" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
