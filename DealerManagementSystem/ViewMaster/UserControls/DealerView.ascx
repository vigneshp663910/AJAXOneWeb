<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DealerView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.DealerView" %>
<%@ Register Src="~/ViewMaster/UserControls/DealerCreate.ascx" TagPrefix="UC" TagName="UC_DealerCreate" %>
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
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px; overflow-x: auto; max-height: 300px">
                <asp:LinkButton ID="lnkBtnEditDealer" runat="server" OnClick="lnkBtnActions_Click">Edit Dealer</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnAddBranchOffice" runat="server" OnClick="lnkBtnActions_Click">Add Branch Office</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnAddNotification" runat="server" OnClick="lnkBtnActions_Click">Add Dealer Notification</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnEditBank" runat="server" OnClick="lnkBtnActions_Click">Edit Bank Details</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnEditDealerResponsibleUser" runat="server" OnClick="lnkBtnActions_Click">Edit Dealer Responsible User</asp:LinkButton>
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
                    <asp:Label ID="lblDealerCode" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblDealerState" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>IFSC Code : </label>
                    <asp:Label ID="lblIFSCCode" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <%--<div class="col-md-12">
                    <label>Team Lead : </label>
                    <asp:Label ID="lblTeamLead" runat="server" CssClass="label"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer Name : </label>
                    <asp:Label ID="lblDealerName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Country : </label>
                    <asp:Label ID="lblDealerCountry" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Bank : </label>
                    <asp:Label ID="lblDealerBank" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Account Number : </label>
                    <asp:Label ID="lblAccountNo" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <%--<div class="col-md-12">
                    <label>Serivce Manager : </label>
                    <asp:Label ID="lblSerivceManager" runat="server" CssClass="label"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Region : </label>
                    <asp:Label ID="lblDealerRegion" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Branch : </label>
                    <asp:Label ID="lblDealerBankBranch" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Active : </label>
                    <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
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
                                <asp:TemplateField HeaderText="Sl No" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        <asp:Label ID="lblOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sap Location Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSapLocationCode" Text='<%# DataBinder.Eval(Container.DataItem, "SapLocationCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeCode" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeCode")%>' runat="server" />
                                        <asp:Label ID="lblOfficeCodeID" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficeAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address1")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficeAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficeAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficeCity" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficeState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficeDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pincode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerOfficePincode" Text='<%# DataBinder.Eval(Container.DataItem, "Pincode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Head Office">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbxIsHeadOffice" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsHeadOffice")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkbtnDealerOfficeDelete" runat="server" OnClick="lnkbtnDealerOfficeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnItemAction_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnItemAction_Click" OnClientClick="return ConfirmDealerOfficeDelete();"><i class="fa fa-fw fa-times" style="font-size:18px" ></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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
    <asp1:TabPanel ID="tpnlDealerEmployee" runat="server" HeaderText="Dealer Employee" ToolTip="List of Dealer Employee...">
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
                                <asp:TemplateField HeaderText="Sl No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
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
                                <%--<asp:TemplateField HeaderText="Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
    <asp1:TabPanel ID="tpnlDealerNotification" runat="server" HeaderText="Dealer Notification" ToolTip="List of Dealer Notification...">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12" id="divDealerNotification" runat="server" visible="false">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-left">
                                <label class="modal-label">Dealer</label>
                                <asp:DropDownList ID="ddlDealerDN" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label class="modal-label">Department</label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label class="modal-label">Designation</label>
                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label class="modal-label">Module</label>
                                <asp:DropDownList ID="ddlDealerNotificationModuleG" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                        <div class="col-md-12 Report">
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
                                            <asp:TemplateField HeaderText="Sl No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
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
                                            <asp:TemplateField HeaderText="Contact">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactNumber" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "User.ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "User.ContactNumber")%></a>
                                                    </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblMail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "User.Mail")%>'><%# DataBinder.Eval(Container.DataItem, "User.Mail")%></a>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerNotificationID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
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
                                                    <asp:LinkButton ID="lnkBtnNotificationDelete" runat="server" OnClick="lnkBtnNotificationDelete_Click" OnClientClick="return ConfirmNotificationDelete();"><i class="fa fa-fw fa-times" style="font-size: 18px"></i></asp:LinkButton>
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
                    </fieldset>
                </div>
            </div>

        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlDealerResponsibleUser" runat="server" HeaderText="Dealer Responsible User" Font-Bold="True" ToolTip="List of Dealer Responsible Users...">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Dealer Responsible User(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountDealerResponsibleUser" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerResponsibleUserArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerResponsibleUserArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerResponsibleUserArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerResponsibleUserArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDealerResponsibleUser" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealerResponsibleUser_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDepartment.DealerDepartment")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDesignation.DealerDesignation")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNumber" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNumber")%></a>
                                        </asp:Label>
                                        <br />
                                        <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Email")%>'><%# DataBinder.Eval(Container.DataItem, "Email")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
</asp1:TabContainer>

<asp:Panel ID="pnlEditDealer" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueEditDealer">Edit Dealer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageEditDealer" runat="server" Text="" CssClass="message" Visible="false" />
            <%--<UC:UC_DealerCreate ID="UC_Dealer" runat="server"></UC:UC_DealerCreate>--%>
            <fieldset class="fieldset-border" id="Fieldset6" runat="server">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Dealer</legend>
                            <div class="col-md-12">
                                <label class="modal-label">Active</label>
                                <asp:CheckBox ID="cbIsActiveDealer" runat="server" Enabled="false" CssClass="mycheckBig" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Dealer Code</label>
                                <asp:TextBox ID="txtDealerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" Visible="false" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Dealer Name</label>
                                <asp:TextBox ID="txtDealerName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Dealer Short Name</label>
                                <asp:TextBox ID="txtDealerShortName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">GSTIN</label>
                                <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">PAN</label>
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Phone</label>
                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">Country</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">State</label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Office Code</label>
                                <asp:TextBox ID="txtOfficeCodeE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Dealer Type</label>
                                <asp:DropDownList ID="ddlDealerType" runat="server" CssClass="form-control" DataTextField="DealerType" DataValueField="DealerTypeID" />
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-4">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">E Invoice</legend>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">E Invoice</label>
                                <asp:CheckBox ID="cbEInvAPI" runat="server" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Service Paid E Invoice</label>
                                <asp:CheckBox ID="cbServicePaidEInvoice" runat="server" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label>E Invoice Date</label>
                                <asp:TextBox ID="txtEInvoiceDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp1:CalendarExtender ID="cxEInvoiceDate" runat="server" TargetControlID="txtEInvoiceDate" PopupButtonID="txtEInvoiceDate" Format="dd/MM/yyyy" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">API Username</label>
                                <asp:TextBox ID="txtApiUserName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">API Password</label>
                                <asp:TextBox ID="txtApiPassword" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnUpdateDealer" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateDealer_Click" OnClientClick="return ConfirmDealerChanges();" />
                    </div>
                </div>
            </fieldset>
        </div>
        <%--<div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateDealer" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateDealer_Click" />
        </div>--%>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditDealer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditDealer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEditDealerAddress" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueEditDealerAddress">Edit Dealer Address</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageEditDealerAddress" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset4" runat="server">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Address</legend>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">Address 1</label>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">Address 2</label>
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                            </div>
                            <%--<div class="col-md-12 col-sm-12">
                                <label class="modal-label">Address 3</label>
                                <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                            </div>--%>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">District</label>
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">City</label>
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">PinCode</label>
                                <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Contact Person</label>
                                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-4">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">E Invoice</legend>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">E Invoice</label>
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Service Paid E Invoice</label>
                                <asp:CheckBox ID="CheckBox3" runat="server" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label>E Invoice Date</label>
                                <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEInvoiceDate" PopupButtonID="txtEInvoiceDate" Format="dd/MM/yyyy" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">API Username</label>
                                <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">API Password</label>
                                <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnUpdateDealerAddress" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateDealerAddress_Click" OnClientClick="return ConfirmDealerAddressChanges();" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditDealerAddress" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditDealerAddress" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlAddBranchOffice" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueAddBranchOffice" runat="server">Add Branch Office</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAddBranchOffice" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                <div class="col-md-12">
                    <div class="col-md-4 col-sm-12">
                        <label class="modal-label">Is Head Office</label>
                        <asp:CheckBox ID="cbIsHeadOffice" runat="server" BorderColor="Silver" />
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <label class="modal-label">SAP Location Code</label>
                        <asp:TextBox ID="txtSapLocationCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Office Code</label>
                        <asp:TextBox ID="txtOfficeCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Office Name</label>
                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address 1</label>
                        <asp:TextBox ID="txtDealerOfficeAddress1" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtDealerOfficeAddress1" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address 2</label>
                        <asp:TextBox ID="txtDealerOfficeAddress2" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtDealerOfficeAddress2" WatermarkText="Address 2" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address 3</label>
                        <asp:TextBox ID="txtDealerOfficeAddress3" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtDealerOfficeAddress3" WatermarkText="Address 3" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">City</label>
                        <asp:TextBox ID="txtDealerOfficeCity" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtDealerOfficeCity" WatermarkText="City" WatermarkCssClass="WatermarkCssClass" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlDealerOfficeState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlDealerOfficeState_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">District</label>
                        <asp:DropDownList ID="ddlDealerOfficeDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Pincode</label>
                        <asp:TextBox ID="txtDealerOfficePincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="6"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtDealerOfficePincode" WatermarkText="Pincode" WatermarkCssClass="WatermarkCssClass" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">GSTIN</label>
                        <asp:TextBox ID="txtDealerOfficeGSTIN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDealerOfficeGSTIN" WatermarkText="GSTIN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">PAN</label>
                        <asp:TextBox ID="txtDealerOfficePAN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDealerOfficePAN" WatermarkText="PAN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Mobile</label>
                        <asp:TextBox ID="txtDealerOfficeMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDealerOfficeMobile" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Email</label>
                        <asp:TextBox ID="txtDealerOfficeEmail" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Email" MaxLength="40"></asp:TextBox>
                        <%-- <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID ="txtDealerOfficeEmail" WatermarkText="Email"  />--%>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnAddUpdateBranchOffice" runat="server" Text="Save" CssClass="btn Save" OnClientClick="return ConfirmDealerOfficeChanges();" OnClick="btnAddUpdateBranchOffice_Click" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddBranchOffice" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddBranchOffice" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddNotification" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueAddNotification">Add Dealer Notifiaction</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
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
            <asp:Button ID="btnAddNotification" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddNotification_Click" OnClientClick="return ConfirmNotificationAdd();" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddNotification" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddNotification" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEditBank" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueEditBank">Edit Bank Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnEditBankClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageEditBank" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Bank</label>
                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp:Label ID="lblDealerBankID" runat="server" Visible="false" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Branch</label>
                        <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">IFSC Code</label>
                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Account Number</label>
                        <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnEditBank" runat="server" Text="Save" CssClass="btn Save" OnClick="btnEditBank_Click" OnClientClick="return ConfirmDealerBankChanges();" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditBank" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditBank" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEditDealerResponsibleUser" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueEditDealerResponsibleUser">Edit Dealer Responsible User</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnEditDealerResponsibleUserClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblEditDealerResponsibleUserMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label" runat="server" id="lblDealerResposnibleUserType">Dealer Resposible User Type</label>
                        <asp:DropDownList ID="ddlDealerResposibleUserType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealerResposibleUserType_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Select Dealer Responsible User Type" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Team Lead" Value="TL"></asp:ListItem>
                            <asp:ListItem Text="Service Manager" Value="SM"></asp:ListItem>
                            <asp:ListItem Text="Sales Responsible User" Value="Sales Responsible User"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label" runat="server" id="lblDealerResponsibleUser">Dealer Resposible User</label>
                        <asp:DropDownList ID="ddlDealerResponsibleUser" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateDealerResposibleUser" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateDealerResposibleUser_Click" OnClientClick="return ConfirmDealerResposibleUserChanges();" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditDealerResposibleUser" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditDealerResponsibleUser" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

<script type="text/javascript">
    function ConfirmDealerChanges() {
        var x = confirm("Are you sure you want to confirm saving the Dealer Details Changes?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDealerAddressChanges() {
        var x = confirm("Are you sure you want to confirm saving the Dealer Address Changes?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDealerOfficeChanges() {

        var cbIsHeadOffice = document.getElementById('MainContent_UC_DealerView_cbIsHeadOffice');
        if (cbIsHeadOffice.checked) {
            var x = confirm("Are you sure you want to make this location the Head Office?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        else {
            var x = confirm("Are you sure you want to confirm saving your changes?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    }
    function ConfirmDealerOfficeDelete() {
        var x = confirm("Are you sure you want to delete Dealer Branch Office?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmNotificationAdd() {
        var x = confirm("Are you sure you want to add the Notification?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmNotificationDelete() {
        var x = confirm("Are you sure you want to delete the Notification?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDealerBankChanges() {
        var x = confirm("Are you sure you want to confirm saving the Bank Details Changes?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDealerResposibleUserChanges() {
        var x = confirm("Are you sure you want to confirm saving your Dealer Resposible User Changes?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>
