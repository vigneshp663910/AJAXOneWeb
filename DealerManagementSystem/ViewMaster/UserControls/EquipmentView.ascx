<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipmentView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.EquipmentView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerVerification.ascx" TagPrefix="UC" TagName="UC_CustomerVerification" %>

<script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
<script type="text/javascript">  
    function FleAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {

        var txtCustomerID = document.getElementById('MainContent_UC_CustomerView_txtFleetID');
        txtCustomerID.value = CustomerID.innerText;
        var txtCustomer = document.getElementById('MainContent_UC_CustomerView_txtFleet');
        txtCustomer.value = CustomerName.innerText;

        document.getElementById('FleDivAuto').style.display = "none";
    }
</script>
<script type="text/javascript"> 
    $(function () {
        $('#FleDiv1').click(function () {
            var CustomerID = document.getElementById('lblCustomerID1')
            var CustomerName = document.getElementById('lblCustomerName1')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv2').click(function () {
            var CustomerID = document.getElementById('lblCustomerID2')
            var CustomerName = document.getElementById('lblCustomerName2')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv3').click(function () {
            var CustomerID = document.getElementById('lblCustomerID3')
            var CustomerName = document.getElementById('lblCustomerName3')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv4').click(function () {
            var CustomerID = document.getElementById('lblCustomerID4')
            var CustomerName = document.getElementById('lblCustomerName4')
            FleAutoCustomer(CustomerID, CustomerName, "", "");
        });
    });
    $(function () {
        $('#FleDiv5').click(function () {
            var CustomerID = document.getElementById('lblCustomerID5')
            var CustomerName = document.getElementById('lblCustomerName5')
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
<asp:Panel ID="PnlCustomerView" runat="server" class="col-md-12">
    <div class="col-md-12">
        <div class="action-btn">
            <div class="" id="boxHere"></div>
            <div class="dropdown btnactions" id="customerAction">
                <%--<asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />--%>
                <div class="btn Approval">Actions</div>
                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                    <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbActions_Click">Edit Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbAddAttribute" runat="server" OnClick="lbActions_Click">Add Attribute</asp:LinkButton>
                    <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                    <asp:LinkButton ID="lbAddRelation" runat="server" OnClick="lbActions_Click">Add Relation</asp:LinkButton>
                    <asp:LinkButton ID="lbAddFleet" runat="server" OnClick="lbActions_Click">Add Fleet</asp:LinkButton>
                    <asp:LinkButton ID="lbAddResponsibleEmployee" runat="server" OnClick="lbActions_Click">Add Responsible Employee</asp:LinkButton>
                    <asp:LinkButton ID="lbtnVerifiedCustomer" runat="server" OnClick="lbActions_Click">Verified Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbtnInActivateCustomer" runat="server" OnClick="lbActions_Click">In Activate Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbtnActivateCustomer" runat="server" OnClick="lbActions_Click">Activate Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbtnSyncToSap" runat="server" OnClick="lbActions_Click">Sync to Sap</asp:LinkButton>
                    <asp:LinkButton ID="lbtnShipTo" runat="server" OnClick="lbActions_Click">Add ShipTo</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 field-margin-top">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Customer : </label>
                        <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Alternative Mobile : </label>
                        <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>GSTIN : </label>
                        <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address 1 : </label>
                        <asp:Label ID="lblAddress1" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address 2 : </label>
                        <asp:Label ID="lblAddress2" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address 3 : </label>
                        <asp:Label ID="lblAddress3" runat="server" CssClass="label"></asp:Label>
                    </div>
                     <div class="col-md-12">
                        <label>City : </label>
                        <asp:Label ID="lblCity" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Contact Person : </label>
                        <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Email : </label>
                        <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                    </div>
                     <div class="col-md-12">
                        <label>Mobile : </label>
                        <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>PAN : </label>
                        <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
                    </div> 
                    <div class="col-md-12">
                        <label>District : </label>
                        <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>State : </label>
                        <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                    </div>

                    <div class="col-md-12">
                        <label>PinCode : </label>
                        <asp:Label ID="lblPinCode" runat="server" CssClass="label"></asp:Label>
                    </div>

                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Last Visit Date : </label>
                        <asp:Label ID="lblLastVisitDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>OrderBlock : </label>
                        <asp:CheckBox ID="cbOrderBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>
                    <div class="col-md-12">
                        <label>Active : </label>
                        <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>
                    <div class="col-md-12">
                        <label>BillingBlock : </label>
                        <asp:CheckBox ID="cbBillingBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>
                   

                    <div class="col-md-12">
                        <label>Verified : </label>
                        <asp:CheckBox ID="cbVerified" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>
                    <div class="col-md-12">
                        <label>DeliveryBlock : </label>
                        <asp:CheckBox ID="cbDeliveryBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>

                </div>
            </div>
        </fieldset>



    </div>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Customer Info..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="2">
        <asp1:TabPanel ID="tpnlAttribute" runat="server" HeaderText="Attribute" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAttribute" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                            <asp:Label ID="lblCustomerAttributeID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerAttributeID")%>' runat="server" Visible="false" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attribute Main">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttributeMain" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeMain.AttributeMain")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attribute Sub">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttributeSub" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeSub.AttributeSub")%>' runat="server" />
                                            <asp:Label ID="lblAttributeSubID" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeSub.AttributeSubID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbMarketSegmentDelete" runat="server" OnClick="lbMarketSegmentDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
        <asp1:TabPanel ID="tpnlProducts" runat="server" HeaderText="Products">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:Label ID="lblCustomerProductID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerProductID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Product">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Product")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Product Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                            <asp:Label ID="lblProductTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductTypeID")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Brand Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Make.Make")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">

                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbProductDelete" runat="server" OnClick="lbProductDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
        <asp1:TabPanel ID="tpnlRelations" runat="server" HeaderText="Relations">
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
                                            <asp:Label ID="lblCustomerRelationID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerRelationID")%>' runat="server" Visible="false" />
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
                                            <asp:Label ID="lblMobile" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
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
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbRelationDelete" runat="server" OnClick="lbRelationDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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

</asp:Panel>

 


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
 

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
