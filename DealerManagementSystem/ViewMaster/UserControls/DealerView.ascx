<%@ control language="C#" autoeventwireup="true" codebehind="DealerView.ascx.cs" inherits="DealerManagementSystem.ViewMaster.UserControls.DealerView" %>
<%@ register src="~/ViewMaster/UserControls/CustomerCreate.ascx" tagprefix="UC" tagname="UC_DealerCreate" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp1" %>


<script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
<script type="text/javascript">  
    function FleAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {
        debugger
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
                <asp:linkbutton id="lnkBtnAddNotification" runat="server" onclick="lnkBtnActions_Click">Add Dealer Notification</asp:linkbutton>
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
                    <asp:label id="lblDealerCode" runat="server" cssclass="label"></asp:label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:label id="lblEmail" runat="server" cssclass="label"></asp:label>
                </div>
                <div class="col-md-12">
                    <label>Active : </label>
                    <asp:checkbox id="cbIsActive" runat="server" enabled="false" cssclass="mycheckBig" />
                </div>
                <%--<div class="col-md-12">
                    <label>Team Lead : </label>
                    <asp:Label ID="lblTeamLead" runat="server" CssClass="label"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer Name : </label>
                    <asp:label id="lblDealerName" runat="server" cssclass="label"></asp:label>
                </div>

                <div class="col-md-12">
                    <label>Country : </label>
                    <asp:label id="lblDealerCountry" runat="server" cssclass="label"></asp:label>
                </div>
                <%--<div class="col-md-12">
                    <label>Serivce Manager : </label>
                    <asp:Label ID="lblSerivceManager" runat="server" CssClass="label"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:label id="lblMobile" runat="server" cssclass="label"></asp:label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:label id="lblDealerState" runat="server" cssclass="label"></asp:label>
                </div>
                <%-- <div class="col-md-12">
                    <label>Active : </label>
                    <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>--%>
            </div>
        </div>
    </fieldset>
</div>
<asp:label id="lblMessage" runat="server" text="" cssclass="message" visible="false" />
<asp1:Tabcontainer id="tbpDealer" runat="server" tooltip="Dealer" font-bold="True" font-size="Medium" activetabindex="1">
    <asp1:tabpanel id="tpnlDealerOffice" runat="server" headertext="Dealer Office" font-bold="True" tooltip="List of Dealer Office...">
        <contenttemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Branch Office(s):</td>

                                    <td>
                                        <asp:label id="lblRowCountDealerOffice" runat="server" cssclass="label"></asp:label>
                                    </td>
                                    <td>
                                        <asp:imagebutton id="ibtnDealerOfficeArrowLeft" runat="server" imageurl="~/Images/ArrowLeft.png" width="15px" onclick="ibtnDealerOfficeArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:imagebutton id="ibtnDealerOfficeArrowRight" runat="server" imageurl="~/Images/ArrowRight.png" width="15px" onclick="ibtnDealerOfficeArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:gridview id="gvDealerOffice" runat="server" autogeneratecolumns="False" width="100%" cssclass="table table-bordered table-condensed Grid" emptydatatext="No Data Found"
                            pagesize="10" allowpaging="true" onpageindexchanging="gvDealerOffice_PageIndexChanging">
                            <columns>
                                <asp:templatefield headertext="RId" itemstyle-width="25px" itemstyle-horizontalalign="Center">
                                    <itemtemplate>
                                        <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        <asp:label id="lblOfficeID" text='<%# DataBinder.Eval(Container.DataItem, "OfficeID")%>' runat="server" visible="false" />
                                    </itemtemplate>
                                    <itemstyle horizontalalign="Center" />
                                </asp:templatefield>
                                <asp:templatefield headertext="Office Code">
                                    <itemtemplate>
                                        <asp:label id="lblOfficeCode" text='<%# DataBinder.Eval(Container.DataItem, "OfficeCode")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Office Name">
                                    <itemtemplate>
                                        <asp:label id="lblOfficeName" text='<%# DataBinder.Eval(Container.DataItem, "OfficeName")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Sap Location Code">
                                    <itemtemplate>
                                        <asp:label id="lblSapLocationCode" text='<%# DataBinder.Eval(Container.DataItem, "SapLocationCode")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <%--<asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDealerOfficeDelete" runat="server" OnClick="lnkbtnDealerOfficeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                            </columns>
                            <alternatingrowstyle backcolor="White" />
                            <footerstyle forecolor="White" />
                            <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                            <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#FBFCFD" forecolor="Black" horizontalalign="Left" />
                        </asp:gridview>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp1:tabpanel>
    <asp1:tabpanel id="tpnlDealerEmployee" runat="server" headertext="Dealer Employee">
        <contenttemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Employee(s):</td>

                                    <td>
                                        <asp:label id="lblRowCountDealerEmployee" runat="server" cssclass="label"></asp:label>
                                    </td>
                                    <td>
                                        <asp:imagebutton id="ibtnDealerEmployeeArrowLeft" runat="server" imageurl="~/Images/ArrowLeft.png" width="15px" onclick="ibtnDealerEmployeeArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:imagebutton id="ibtnDealerEmployeeArrowRight" runat="server" imageurl="~/Images/ArrowRight.png" width="15px" onclick="ibtnDealerEmployeeArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:gridview id="gvDealerEmployee" runat="server" autogeneratecolumns="false" width="100%" cssclass="table table-bordered table-condensed Grid" emptydatatext="No Data Found"
                            pagesize="10" allowpaging="true" onpageindexchanging="gvDealerEmployee_PageIndexChanging">
                            <columns>
                                <asp:templatefield headertext="RId" itemstyle-horizontalalign="Center" itemstyle-width="25px">
                                    <itemtemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:label id="lblDealerEmployeeID" text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeID")%>' runat="server" visible="false" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Name" itemstyle-horizontalalign="Left">
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                    <itemtemplate>
                                        <asp:label id="lblName" text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Department">
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                    <itemtemplate>
                                        <asp:label id="lblDepartment" text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDepartment.DealerDepartment")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Designation">
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                    <itemtemplate>
                                        <asp:label id="lblDesignation" text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDesignation.DealerDesignation")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Name">
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                    <itemtemplate>
                                        <asp:label id="lblName" text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
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
                                <asp:templatefield headertext="Contact Number" itemstyle-horizontalalign="Right">
                                    <itemtemplate>
                                        <asp:label id="lblContactNumber" text='<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Emergency Contact No.">
                                    <itemtemplate>
                                        <asp:label id="lblEmergencyContactNumber" text='<%# DataBinder.Eval(Container.DataItem, "EmergencyContactNumber")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Email">
                                    <itemtemplate>
                                        <asp:label id="lblEmail" text='<%# DataBinder.Eval(Container.DataItem, "Email")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="State">
                                    <itemtemplate>
                                        <asp:label id="lblState" text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="District">
                                    <itemtemplate>
                                        <asp:label id="lblDistrict" text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Location">
                                    <itemtemplate>
                                        <asp:label id="lblLocation" text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Aadhaar Card No.">
                                    <itemtemplate>
                                        <asp:label id="lblAadhaarCardNo" text='<%# DataBinder.Eval(Container.DataItem, "AadhaarCardNo")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <%-- <asp:TemplateField HeaderText="PAN No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPANNo" Text='<%# DataBinder.Eval(Container.DataItem, "PANNo")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </columns>
                            <alternatingrowstyle backcolor="#ffffff" />
                            <footerstyle forecolor="White" />
                            <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                            <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                        </asp:gridview>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp1:tabpanel>
    <asp1:tabpanel id="tpnlDealerNotification" runat="server" headertext="Dealer Notification">
        <contenttemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Dealer Notification(s):</td>

                                    <td>
                                        <asp:label id="lblRowCountDealerNotification" runat="server" cssclass="label"></asp:label>
                                    </td>
                                    <td>
                                        <asp:imagebutton id="ibtnDealerNotificationArrowLeft" runat="server" imageurl="~/Images/ArrowLeft.png" width="15px" onclick="ibtnDealerNotificationArrowLeft_Click" />
                                    </td>
                                    <td>
                                        <asp:imagebutton id="ibtnDealerNotificationArrowRight" runat="server" imageurl="~/Images/ArrowRight.png" width="15px" onclick="ibtnDealerNotificationArrowRight_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:gridview id="gvDealerNotification" runat="server" autogeneratecolumns="false" width="100%" cssclass="table table-bordered table-condensed Grid" emptydatatext="No Data Found"
                            pagesize="10" allowpaging="true" onpageindexchanging="gvDealerNotification_PageIndexChanging">
                            <columns>
                                <asp:templatefield headertext="RId" itemstyle-horizontalalign="Center" itemstyle-width="25px">
                                    <itemtemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:label id="lblDealerNotificationID" text='<%# DataBinder.Eval(Container.DataItem, "DealerNotificationID")%>' runat="server" visible="false" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Module Name" itemstyle-horizontalalign="Left">
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                    <itemtemplate>
                                        <asp:label id="lblModuleName" text='<%# DataBinder.Eval(Container.DataItem, "Module.ModuleName")%>' runat="server" />
                                        <asp:label id="lblDealerNotificationModuleID" text='<%# DataBinder.Eval(Container.DataItem, "Module.DealerNotificationModuleID")%>' runat="server" visible="false" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Name">
                                    <itemtemplate>
                                        <asp:label id="lblContactName" text='<%# DataBinder.Eval(Container.DataItem, "User.ContactName")%>' runat="server" />
                                        <asp:label id="lblUserID" text='<%# DataBinder.Eval(Container.DataItem, "User.UserID")%>' runat="server" visible="false" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Login ID">
                                    <itemtemplate>
                                        <asp:label id="lblUserName" text='<%# DataBinder.Eval(Container.DataItem, "User.UserName")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Department">
                                    <itemtemplate>
                                        <asp:label id="lblDealerDepartment" text='<%# DataBinder.Eval(Container.DataItem, "User.Department.DealerDepartment")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Designation">
                                    <itemtemplate>
                                        <asp:label id="lblDealerDesignation" text='<%# DataBinder.Eval(Container.DataItem, "User.Designation.DealerDesignation")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Contact Number">
                                    <itemtemplate>
                                        <asp:label id="lblContactNumber" text='<%# DataBinder.Eval(Container.DataItem, "User.ContactNumber")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Email ID">
                                    <itemtemplate>
                                        <asp:label id="lblMail" text='<%# DataBinder.Eval(Container.DataItem, "User.Mail")%>' runat="server" />
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="SMS">
                                    <itemtemplate>
                                        <%--<asp:Label ID="lblIsSMS" Text='<%# DataBinder.Eval(Container.DataItem, "IsSMS")%>' runat="server" />--%>
                                        <asp:checkbox id="chkbxIsSMS" runat="server" checked='<%# DataBinder.Eval(Container.DataItem, "IsSMS")%>' enabled="false"></asp:checkbox>
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Mail">
                                    <itemtemplate>
                                        <%-- <asp:Label ID="lblIsMail" Text='<%# DataBinder.Eval(Container.DataItem, "IsMail")%>' runat="server" />--%>
                                        <asp:checkbox id="chkbxIsMail" runat="server" checked='<%# DataBinder.Eval(Container.DataItem, "IsMail")%>' enabled="false"></asp:checkbox>
                                    </itemtemplate>
                                </asp:templatefield>
                                <asp:templatefield headertext="Action" headerstyle-width="50px" itemstyle-horizontalalign="Center">
                                    <itemtemplate>
                                        <asp:linkbutton id="lnkBtnNotificationDelete" runat="server" onclick="lnkBtnNotificationDelete_Click"><i class="fa fa-fw fa-times" style="font-size: 18px"></i></asp:linkbutton>
                                    </itemtemplate>
                                </asp:templatefield>
                            </columns>
                            <alternatingrowstyle backcolor="#ffffff" />
                            <footerstyle forecolor="White" />
                            <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                            <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                        </asp:gridview>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp1:tabpanel>
</asp1:Tabcontainer>



<asp:panel id="pnlDealer" runat="server" cssclass="Popup" style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:button id="Button1" runat="server" text="X" cssclass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:label id="lblMessageDealerEdit" runat="server" text="" cssclass="message" visible="false" />
            <uc:uc_dealercreate id="UC_Dealer" runat="server"></uc:uc_dealercreate>
        </div>
        <%--<div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateDealer" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateDealer_Click" />
        </div>--%>
    </div>
</asp:panel>

<ajaxtoolkit:modalpopupextender id="MPE_Dealer" runat="server" targetcontrolid="lnkMPE" popupcontrolid="pnlDealer" backgroundcssclass="modalBackground" cancelcontrolid="btnCancel" />

<asp:panel id="pnlAddNotification" runat="server" cssclass="Popup" style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Dealer Notifiaction</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:button id="btnAddNotificationClose" runat="server" text="X" cssclass="PopupClose" />
        </a>
    </div>
    <asp:label id="lblMessageAddNotification" runat="server" text="" cssclass="message" visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">

            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label" runat="server" id="lblDealer" visible="false">Dealer</label>
                        <asp:dropdownlist id="ddlDealer" runat="server" cssclass="form-control" visible="false"></asp:dropdownlist>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Module</label>
                        <asp:dropdownlist id="ddlDealerNotificationModule" runat="server" cssclass="form-control" />
                        <%--OnSelectedIndexChanged="ddlDealerNotificationModule_SelectedIndexChanged" AutoPostBack="true" --%>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Employee</label>
                        <asp:dropdownlist id="ddlEmployee" runat="server" cssclass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Send SMS</label>
                        <asp:checkbox id="cbSendSMS" runat="server" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Send Email</label>
                        <asp:checkbox id="cbSendEmail" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:button id="btnAddNotification" runat="server" text="Save" cssclass="btn Save" onclick="btnAddNotification_Click" />
        </div>
    </div>
</asp:panel>
<ajaxtoolkit:modalpopupextender id="MPE_AddNotification" runat="server" targetcontrolid="lnkMPE" popupcontrolid="pnlAddNotification" backgroundcssclass="modalBackground" cancelcontrolid="btnCancel" />


<div style="display: none">
    <asp:linkbutton id="lnkMPE" runat="server">MPE</asp:linkbutton><asp:button id="btnCancel" runat="server" text="Cancel" />
</div>
