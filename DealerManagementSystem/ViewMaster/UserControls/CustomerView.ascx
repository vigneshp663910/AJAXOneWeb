<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>


<script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
<script type="text/javascript">  
    function FleAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {
        debugger
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
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <%--<asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />--%>
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbActions_Click">Edit Customer</asp:LinkButton>
                <%--   <asp:LinkButton ID="lbViewAditTrails" runat="server" OnClick="lbViewAditTrails_Click">View Adit Trails</asp:LinkButton>--%>
                <asp:LinkButton ID="lbAddMarketSegment" runat="server" OnClick="lbActions_Click">Add Attribute</asp:LinkButton>
                <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                <asp:LinkButton ID="lbAddRelation" runat="server" OnClick="lbActions_Click">Add Relation</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">Add Fleet</asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbActions_Click">Add Responsible Employee</asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbActions_Click">Add Fleet</asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbActions_Click">Add Responsible Employee</asp:LinkButton>

                <asp:LinkButton ID="lbtnVerifiedCustomer" runat="server" OnClick="lbActions_Click">Verified Customer</asp:LinkButton>
                <asp:LinkButton ID="lbtnInActivateCustomer" runat="server" OnClick="lbActions_Click">In Activate Customer</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Customer Name : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div> 
            <div class="col-md-4">
                <label>Contact Person : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Mobile : </label>
                <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Alternative Mobile : </label>
                <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Email : </label>
                <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Location : </label>
                <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
            </div>

            <div class="col-md-4">
                <label>GSTIN : </label>
                <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>PAN : </label>
                <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
            </div>

            <div class="col-md-4">
                <label>Verified : </label>
                <asp:CheckBox ID="cbVerified" runat="server" Enabled="false" />
            </div>

            <div class="col-md-4">
                <label>Active : </label>
                <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" />
            </div>

            <div class="col-md-4">
                <label>OrderBlock : </label>
                <asp:CheckBox ID="cbOrderBlock" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>DeliveryBlock : </label>
                <asp:CheckBox ID="cbDeliveryBlock" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>BillingBlock : </label>
                <asp:CheckBox ID="cbBillingBlock" runat="server" Enabled="false" />
            </div>



        </div>
    </fieldset>
</div>

<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="2">
    <asp1:TabPanel ID="tpnlAttribute" runat="server" HeaderText="Attribute" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Attribute</legend>
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
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlProducts" runat="server" HeaderText="Products">
        <ContentTemplate>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Products</legend>

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
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Product")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
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
                                        <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                               </div>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlRelations" runat="server" HeaderText="Relations">
        <ContentTemplate>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Relations</legend>
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
                                        <asp:Label ID="lblDOB" Text='<%# DataBinder.Eval(Container.DataItem, "DOB")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anniversary Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOAniversary" Text='<%# DataBinder.Eval(Container.DataItem, "DOAnniversary")%>' runat="server" />
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                               </div>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlEmployee" runat="server" HeaderText="EMP RESP">
        <ContentTemplate>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">EMP RESP</legend>

                    <div class="col-md-12 Report">
                         <div class="table-responsive">
                        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:Label ID="lblCustomerResponsibleEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerResponsibleEmployeeID")%>' runat="server" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.Name")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.ContactNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDecisionMaker" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.DealerEmployeeRole.Dealer.DealerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelation" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.DealerEmployeeRole.DealerDepartment.DealerDepartment")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOB" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.DealerEmployeeRole.DealerDesignation.DealerDesignation")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporting To ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOAniversary" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.DealerEmployeeRole.ReportingTo.Name")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbResponsibleDelete" runat="server" OnClick="lbResponsibleDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                               </div>

                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFleet" runat="server" HeaderText="Fleet">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Fleet</legend>
                <div class="col-md-12 Report">
                     <div class="table-responsive">
                    <asp:GridView ID="gvFleet" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    <asp:Label ID="lblCustomerFleetID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFleetID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.CustomerName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.ContactPerson")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.Mobile")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMail">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEMail" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.EMail")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbFleetDelete" runat="server" OnClick="lbFleetDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                           </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlLead" runat="server" HeaderText="Lead">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                <div class="col-md-12 Report">
                     <div class="table-responsive">
                    <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lead Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "LeadNumber")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lead Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "LeadDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qualification" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblQualification" Text='<%# DataBinder.Eval(Container.DataItem, "Qualification.Qualification")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source.Source")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Code" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <AlternatingRowStyle BackColor="#f2f2f2" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                           </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlVisit" runat="server" HeaderText="Visit">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Visit</legend>
                <div class="col-md-12 Report">
                     <div class="table-responsive">
                    <asp:GridView ID="gvColdVisit" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold Visit No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColdVisitNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitNumber")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold Visit Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblColdVisitDate" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "ActionType.ActionType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.ContactPerson")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMail">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEMail" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                           </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="Support Document">

        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Support Document</legend>
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fileUpload" runat="server" /></td>
                        <td>
                            <asp:Button ID="btnAddFile" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFile_Click" /></td>
                    </tr>
                </table>
                <div class="col-md-12 Report">
                     <div class="table-responsive">
                    <asp:GridView ID="gvSupportDocument" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server" />
                                    <asp:Label ID="lblAttachedFileID" Text='<%# DataBinder.Eval(Container.DataItem, "AttachedFileID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblFileType" Text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSupportDocumentDownload" runat="server" OnClick="lbSupportDocumentDownload_Click">Download </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSupportDocumentDelete" runat="server" OnClick="lbSupportDocumentDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                           </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>

<asp:Panel ID="pnlAddAttribute" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Attribute to Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblMessageAttribute" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label>Attribute Main</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlAttributeMain" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAttributeMain_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2">
                        <label>Attribute Sub</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlAttributeSub" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-4 text-right">
                        <label>Remark</label>
                    </div>
                    <div class="col-md-10">
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
    <asp:Label ID="lblMessageProduct" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                <div class="col-md-12">
                    <div class="col-md-2 text-right">
                        <label>Make</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Product Type</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Product</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Quantity</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Remark</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtRemarkProduct" runat="server" CssClass="form-control"></asp:TextBox>
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
    <asp:Label ID="lblMessageRelation" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset4" runat="server">
                <div class="col-md-12">
                    <div class="col-md-2 text-right">
                        <label>Person Name</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtPersonName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Mobile</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Birth Date</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Anniversary Date</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtAnniversaryDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Relation</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlRelation" runat="server" CssClass="form-control" />
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

<asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageCustomerEdit" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">

        <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateCustomer" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateCustomer_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlFleet" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Fleet</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblMessageFleet" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-2 text-right">
                        <label>Customer Name</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtFleet" runat="server" CssClass="form-control" MaxLength="40" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <div id="FleDivAuto" style="position: absolute; background-color: red; display: none; z-index: 1;">
                            <div id="FleDiv1" class="fieldset-border">
                            </div>
                            <div id="FleDiv2" class="fieldset-border">
                            </div>
                            <div id="FleDiv3" class="fieldset-border">
                            </div>
                            <div id="FleDiv4" class="fieldset-border">
                            </div>
                            <div id="FleDiv5" class="fieldset-border">
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
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12">

                    <div class="col-md-2 text-right">
                        <label>Dealer</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 text-right">
                        <label>Employee</label>
                    </div>
                    <div class="col-md-4">
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


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

