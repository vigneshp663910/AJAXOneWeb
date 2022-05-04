<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DealerView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.DealerView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_DealerCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>


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
        <div class="dropdown btnactions" id="customerAction">
            <%--<asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />--%>
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbEditDealer" runat="server" OnClick="lbActions_Click">Edit Dealer</asp:LinkButton>
                <asp:LinkButton ID="lbtnInActivateDealer" runat="server" OnClick="lbActions_Click">In Activate Dealer</asp:LinkButton>
                <asp:LinkButton ID="lbtnActivateDealer" runat="server" OnClick="lbActions_Click">Activate Dealer</asp:LinkButton>
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
                    <label>Dealer Code : </label>
                    <asp:Label ID="lblDealerCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Team Lead : </label>
                    <asp:Label ID="lblTeamLead" runat="server" CssClass="label"></asp:Label>
                </div>
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
                    <label>Serivce Manager : </label>
                    <asp:Label ID="lblSerivceManager" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblDealerState" runat="server" CssClass="label"></asp:Label>
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
<asp1:TabContainer ID="tbpDealer" runat="server" ToolTip="Dealer" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="tpnlDealerOffice" runat="server" HeaderText="Dealer Office" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDealerOffice" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId">
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
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDealerOfficeDelete" runat="server" OnClick="lnkbtnDealerOfficeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
                                                <td>Dealer(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDealerEmployeeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerEmployeeArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDealerEmployeeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerEmployeeArrowRight_Click" /></td>
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
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:Label ID="lblDealerEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeID")%>' runat="server" Visible="false" />
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
                                <asp:TemplateField HeaderText="Contact Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emergency Contact No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmergencyContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "EmergencyContactNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" Text='<%# DataBinder.Eval(Container.DataItem, "Email")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server" />
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

                                <%--<asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbProductDelete" runat="server" OnClick="lbProductDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlDealerCustomer" runat="server" HeaderText="Dealer Customer">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvRelation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:Label ID="lblDealerCustomerMappingID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCustomerMappingID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDecisionMaker" Text='<%# DataBinder.Eval(Container.DataItem, "DecisionMaker")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Mobile")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Relation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelation" Text='<%# DataBinder.Eval(Container.DataItem, "Relation.Relation")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Birth Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" Text='<%# DataBinder.Eval(Container.DataItem, "DOB","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anniversary Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOAniversary" Text='<%# DataBinder.Eval(Container.DataItem, "DOAnniversary","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbRelationDelete" runat="server" OnClick="lbRelationDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel> 
</asp1:TabContainer>

<asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageCustomerEdit" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateCustomer" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateCustomer_Click" />
        </div>
    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlAddAttribute" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Attribute to Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAttribute" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Attribute Main</label>
                        <asp:DropDownList ID="ddlAttributeMain" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAttributeMain_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Attribute Sub</label>
                        <asp:DropDownList ID="ddlAttributeSub" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remark</label>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveMarketSegment" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveMarketSegment_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Attribute" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddAttribute" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="PapnlAddProduct" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Product</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageProduct" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Make</label>
                        <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control" OnSelectedIndexChanged="FillProduct" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Product Type</label>
                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" OnSelectedIndexChanged="FillProduct" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Product</label>
                        <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Quantity</label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remark</label>
                        <asp:TextBox ID="txtRemarkProduct" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveProduct" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveProduct_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Product" runat="server" TargetControlID="lnkMPE" PopupControlID="PapnlAddProduct" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddRelation" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Relation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageRelation" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset4" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Person Name</label>
                        <asp:TextBox ID="txtPersonName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Mobile</label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Birth Date</label>
                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Anniversary Date</label>
                        <asp:TextBox ID="txtAnniversaryDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Relation</label>
                        <asp:DropDownList ID="ddlRelation" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveRelation" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveRelation_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Relation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddRelation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlFleet" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Fleet</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageFleet" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer Name</label>
                        <asp:TextBox ID="txtFleet" runat="server" CssClass="form-control" MaxLength="40" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <div id="FleDivAuto" style="position: absolute; background-color: red; z-index: 1;">
                            <div id="FleDiv1" class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="FleDiv2"  class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="FleDiv3"  class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="FleDiv4"  class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="FleDiv5"  class="fieldset-borderAuto" style="display: none">
                            </div>
                        </div>
                    </div>
                    <div style="display: none">
                        <asp:TextBox ID="txtFleetID" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnFleed" runat="server" Text="Save" CssClass="btn Save" OnClick="btnFleed_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Fleed" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFleet" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlResponsibleEmp" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Responsible Employee</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblMessageResponsible" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">

            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Employee</label>
                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnResponsibleEmp" runat="server" Text="Save" CssClass="btn Save" OnClick="btnResponsibleEmp_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ResponsibleEmp" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlResponsibleEmp" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlDealer" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
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

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
