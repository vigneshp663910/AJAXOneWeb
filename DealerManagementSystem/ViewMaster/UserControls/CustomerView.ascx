<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerVerification.ascx" TagPrefix="UC" TagName="UC_CustomerVerification" %>

<script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>


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
<asp:HiddenField ID="hdfCustomerID" runat="server" />
<asp:Panel ID="PnlCustomerView" runat="server" class="col-md-12">
    <div class="col-md-12">
        <div class="action-btn">
            <div class="" id="boxHere"></div>
            <div class="dropdown btnactions" id="customerAction">
                <div class="btn Approval">Actions</div>
                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                    <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbActions_Click">Edit Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbAddAttribute" runat="server" OnClick="lbActions_Click">Add Attribute</asp:LinkButton>
                    <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                    <asp:LinkButton ID="lbAddCustomerTeam" runat="server" OnClick="lbActions_Click">Add Customer Team</asp:LinkButton>
                    <asp:LinkButton ID="lbAddGroupOfCompanies" runat="server" OnClick="lbActions_Click">Add Group of Companies</asp:LinkButton>
                    <asp:LinkButton ID="lbAddResponsibleEmployee" runat="server" OnClick="lbActions_Click">Add Responsible Employee</asp:LinkButton>
                    <asp:LinkButton ID="lbtnVerifiedCustomer" runat="server" OnClick="lbActions_Click">Verified Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDeactivateCustomer" runat="server" OnClick="lbActions_Click">Deactivate Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbtnActivateCustomer" runat="server" OnClick="lbActions_Click">Activate Customer</asp:LinkButton>
                    <asp:LinkButton ID="lbtnSyncToSap" runat="server" OnClick="lbActions_Click">Sync to Sap</asp:LinkButton>
                    <asp:LinkButton ID="lbtnShipTo" runat="server" OnClick="lbActions_Click">Add ShipTo</asp:LinkButton>
                    <%--<asp:LinkButton ID="lbtnSyncToParts" runat="server" OnClick="lbActions_Click">Sync to Parts</asp:LinkButton> --%>
                    <asp:LinkButton ID="lbtnAddLeadAjax" runat="server" OnClick="lbActions_Click">Add Lead Ajax</asp:LinkButton>
                    <asp:LinkButton ID="lbtnUpdateGst" runat="server" OnClick="lbActions_Click">Update GST</asp:LinkButton>
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
                        <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Alternative Mobile : </label>
                        <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>GSTIN : </label>
                        <asp:Label ID="lblGSTIN" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address 1 : </label>
                        <asp:Label ID="lblAddress1" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address 2 : </label>
                        <asp:Label ID="lblAddress2" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address 3 : </label>
                        <asp:Label ID="lblAddress3" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>City : </label>
                        <asp:Label ID="lblCity" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Contact Person : </label>
                        <asp:Label ID="lblContactPerson" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Email : </label>
                        <asp:Label ID="lblEmail" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Mobile : </label>
                        <asp:Label ID="lblMobile" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>PAN : </label>
                        <asp:Label ID="lblPAN" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>District : </label>
                        <asp:Label ID="lblDistrict" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>State : </label>
                        <asp:Label ID="lblState" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>

                    <div class="col-md-12">
                        <label>PinCode : </label>
                        <asp:Label ID="lblPinCode" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Sales Type : </label>
                        <asp:Label ID="lblSalesType" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>

                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Last Visit Date : </label>
                        <asp:Label ID="lblLastVisitDate" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                   
                    <div class="col-md-12">
                        <label>Active : </label>
                        <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div> 
                    <div class="col-md-12">
                        <label>Verified : </label>
                        <asp:CheckBox ID="cbVerified" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>
                    <div class="col-md-12">
                        <label>OrderBlock : </label>
                        <asp:CheckBox ID="cbOrderBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                    </div>
                    <div class="col-md-12">
                        <label>BillingBlock : </label>
                        <asp:CheckBox ID="cbBillingBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
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
        <asp1:TabPanel ID="tpnlRelations" runat="server" HeaderText="Customer Team">
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
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "Designation.Designation")%>' runat="server" />
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
        <asp1:TabPanel ID="tpnlEmployee" runat="server" HeaderText="EMP RESP">
            <ContentTemplate>
                <div class="col-md-12">
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
                                            <asp:Label ID="lblMobile" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Employee.ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "Employee.ContactNumber")%></a>
                                            </asp:Label>
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
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpnlFleet" runat="server" HeaderText="Group of Companies">
            <ContentTemplate>
                <div class="col-md-12">
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
                                            <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.CustomerID")%>' runat="server" Visible="false" />
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
                                            <asp:Label ID="lblMobile" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Fleet.Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Fleet.Mobile")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.CustomerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGST" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.GSTIN")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Code" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "Fleet.State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Fleet.EMail")%>'><%# DataBinder.Eval(Container.DataItem, "Fleet.EMail")%></a>
                                            </asp:Label>
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
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpnlLead" runat="server" HeaderText="Lead">
            <ContentTemplate>
                <div class="col-md-12">
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
                                            <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "LeadDate","{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Category" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

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
                                    <%--  <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server" />
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
        <asp1:TabPanel ID="tpnlVisit" runat="server" HeaderText="Visit">
            <ContentTemplate>
                <div class="col-md-12">
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
                                            <asp:Label ID="lblColdVisitDate" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitDate","{0:d}")%>' runat="server" />
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
                                            <asp:Label ID="lblMobile" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%>'><%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Person Met">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPersonMetContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "PersonMet.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Person Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPersonMetDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "PersonMet.Designation.Designation")%>' runat="server" />
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
        <asp1:TabPanel ID="tpnlSupportDocument" runat="server" HeaderText="Support Document">
            <ContentTemplate>
                <div class="col-md-12">
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
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="Ship To Party" Height="350px">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 Report" style="height: 325px">
                        <div class="table-responsive">
                            <asp:GridView ID="gvShipTo" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                            <asp:Label ID="lblCustomerShipToID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerShipToID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Code" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EMail")%>'><%# DataBinder.Eval(Container.DataItem, "EMail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Address1">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address1")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address2">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address3">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tehsil">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTehsil" Text='<%# DataBinder.Eval(Container.DataItem, "Tehsil.Tehsil")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PinCode">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPinCode" Text='<%# DataBinder.Eval(Container.DataItem, "PinCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsActive" Text='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div class="dropdown btnactions">
                                                <div class="btn Approval">Actions</div>
                                                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                                    <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbActions_Click">Edit ShipTo</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnInActivateCustomer" runat="server" OnClick="lbActions_Click">In Activate ShipTo</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnActivateCustomer" runat="server" OnClick="lbActions_Click">Activate ShipTo</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnSyncToSap" runat="server" OnClick="lbActions_Click">ShipTo Sync to Sap</asp:LinkButton>
                                                </div>
                                            </div>
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
        <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="Equipment" Height="350px">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 Report" style="height: 325px">
                        <div class="table-responsive">
                            <asp:GridView ID="gvEquipment" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentModel.Model")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipment SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentSerialNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dispatched On">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchedOn" Text='<%# DataBinder.Eval(Container.DataItem, "DispatchedOn", "{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warranty ExpiryDate">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyExpiryDate", "{0:d}")%>' runat="server" />
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

<asp:Panel ID="PnlCustomerVerification" runat="server" class="col-md-12" Visible="false">

    <div class="col-md-12" runat="server" id="Div2">
        <UC:UC_CustomerVerification ID="UC_CustomerVerification" runat="server"></UC:UC_CustomerVerification>
        <div class="col-md-12 text-center">
        </div>
    </div>
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
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Designation</label>
                        <asp:DropDownList ID="ddlCustomerEmployeeDesignation" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine" />
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
        <span id="PopupDialogue">Add Group of Companies</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
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
                        <asp:TextBox ID="txtFleet" runat="server" CssClass="form-control" MaxLength="40" BorderColor="Silver" onKeyUp="GetCustomerFleetAuto()" AutoCompleteType="Disabled"></asp:TextBox>

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
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageResponsible" runat="server" Text="" CssClass="message" Visible="false" />
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


<asp:Panel ID="pnlShipTo" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer ShipTo</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageShipTo" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset6" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Contact Person</label>
                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="35"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtContactPerson" WatermarkText="Contact Person" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Mobile</label>
                        <asp:TextBox ID="txtMobileShipTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtMobile" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Email" MaxLength="40"></asp:TextBox>
                        <%-- <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID ="txtEmail" WatermarkText="Email"  />--%>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address 1</label>
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtAddress1" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address 2</label>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtAddress2" WatermarkText="Address 2" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address 3</label>
                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtAddress3" WatermarkText="Address 3" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Tehsil</label>
                        <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" DataTextField="Tehsil" DataValueField="TehsilID" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">PinCode</label>
                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtPincode" WatermarkText="Pincode" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">City</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtCity" WatermarkText="City" WatermarkCssClass="WatermarkCssClass" />
                    </div>

                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnShipTo" runat="server" Text="Save" CssClass="btn Save" OnClick="btnShipTo_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ShipTo" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlShipTo" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />





<asp:Panel ID="pnlLeadAjax" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Create Ajax Lead</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button10" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageLeadAjax" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="fldCountry" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label>Product Type</label>
                        <asp:DropDownList ID="ddlProductTypeLead" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>Project</label>
                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>Expected Date of Sale</label>
                        <asp:TextBox ID="txtExpectedDateOfSale" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="cxExpectedDateOfSale" runat="server" TargetControlID="txtExpectedDateOfSale" PopupButtonID="txtExpectedDateOfSale" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender15" runat="server" TargetControlID="txtExpectedDateOfSale" WatermarkText="DD/MM/YYYY" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>Application</label>
                        <asp:DropDownList ID="ddlApplication" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label>Customer Feedback</label>
                        <asp:TextBox ID="txtCustomerFeedback" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label>Remarks</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnLeadAjaxSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnLeadAjaxSave_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_LeadAjax" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLeadAjax" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlUpdateGst" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Update GST</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button8" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageUpdateGst" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset7" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Update GST</legend>
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <b>Customer Code : </b><asp:Label ID="lblCustomerV" runat="server" Text=""></asp:Label>                        
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">GSTIN<samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtGSTIN" WatermarkText="GSTIN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">PAN<samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtPAN" WatermarkText="PAN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateGst" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateGst_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_UpdateGst" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlUpdateGst" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>


<script type="text/javascript"> 
    function GetCustomerFleetAuto() {
        $("#MainContent_UC_CustomerView_hdfCustomerID").val('');
        var param = { Cust: $('#MainContent_UC_CustomerView_txtFleet').val() }
        var Customers = [];
        if ($('#MainContent_UC_CustomerView_txtFleet').val().trim().length >= 3) {
            $.ajax({
                url: "Customer.aspx/GetCustomer1",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                data: JSON.stringify(param),
                dataType: 'JSON',
                success: function (data) {
                    var DataList = JSON.parse(data.d);
                    for (i = 0; i < DataList.length; i++) {
                        Customers[i] = {
                            value: DataList[i].CustomerName,
                            value1: DataList[i].CustomerID,
                            CustomerType: DataList[i].CustomerType,
                            ContactPerson: DataList[i].ContactPerson,
                            Mobile: DataList[i].Mobile
                        };
                    }
                    $('#MainContent_UC_CustomerView_txtFleet').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_CustomerView_hdfCustomerID").val(u.item.value1);
                            document.getElementById('divCustomerViewID').style.display = "block";
                            document.getElementById('divCustomerCreateID').style.display = "none";

                            //$("#MainContent_UC_Customer_hdfCustomerName").val(u.item.value);
                            //$("#MainContent_UC_Customer_hdfContactPerson").val(u.item.ContactPerson);
                            //$("#MainContent_UC_Customer_hdfMobile").val(u.item.Mobile);

                            //document.getElementById('lblCustomerName').innerText = u.item.value;
                            //document.getElementById('lblContactPerson').innerText = u.item.ContactPerson;
                            //document.getElementById('lblMobile').innerText = u.item.Mobile;


                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_CustomerView_txtFleet').width() + 48,
                            });
                            $(this).autocomplete("widget").scrollTop(0);
                        }
                    }).focus(function (e) {
                        $(this).autocomplete("search");
                    }).click(function () {
                        $(this).autocomplete("search");
                    }).data('ui-autocomplete')._renderItem = function (ul, item) {

                        var inner_html = FormatAutocompleteList(item);
                        return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                            .data('item.autocomplete', item)
                            .append(inner_html)
                            .appendTo(ul);
                    };

                }
            });
        }
        else {
            $('#MainContent_UC_Customer_txtCustomerName').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }
</script>
