<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>




<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
        <div class="col-md-12 View">
            <div class="col-md-6">
                <label>Customer : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Contact Person : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Mobile : </label>
                <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Alternative Mobile : </label>
                <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Email : </label>
                <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Location : </label>
                <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>GSTIN : </label>
                <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>PAN : </label>
                <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <div style="float: right;">
                    <div class="dropdown">
                        <asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />
                        <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                            <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbActions_Click">Edit Customer</asp:LinkButton>
                            <%--   <asp:LinkButton ID="lbViewAditTrails" runat="server" OnClick="lbViewAditTrails_Click">View Adit Trails</asp:LinkButton>--%>
                            <asp:LinkButton ID="lbAddMarketSegment" runat="server" OnClick="lbActions_Click">Add Attribute</asp:LinkButton>
                            <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                            <asp:LinkButton ID="lbAddRelation" runat="server" OnClick="lbActions_Click">Add Relation</asp:LinkButton>
                          <%--  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">Add Fleet</asp:LinkButton>--%>
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbActions_Click">Add Responsible Employee</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </fieldset>
</div>

<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlAttribute" runat="server" HeaderText="Attribute" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Attribute</legend>
                    <div class="col-md-12 Report">
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
                        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <asp:Label ID="lblCustomerrProductID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerrProductID")%>' runat="server" Visible="false" />
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
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlEmployee" runat="server" HeaderText="Employee">
        <ContentTemplate>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Employee</legend>

                    <div class="col-md-12 Report">
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
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFleet" runat="server" HeaderText="Fleet" Visible="false">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Fleet</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvFleet" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
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
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>

<asp:Panel ID="pnlAddAttribute" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Market Segment to Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
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
                    <div class="col-md-2 text-right">
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

<asp:Panel ID="pnlFleed" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Fleed</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-2 text-right">
                        <label>Customer Name</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" MaxLength="40" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <div id="UCdivAuto" style="position: absolute; background-color: red; display: none; z-index: 1;">
                            <div id="UCdiv1" class="fieldset-border">
                            </div>
                            <div id="UCdiv2" class="fieldset-border">
                            </div>
                            <div id="UCdiv3" class="fieldset-border">
                            </div>
                            <div id="UCdiv4" class="fieldset-border">
                            </div>
                            <div id="UCdiv5" class="fieldset-border">
                            </div>
                        </div>
                    </div>
                    <div style="display: none">
                        <asp:TextBox ID="txtCustomerID" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnFleed" runat="server" Text="Save" CssClass="btn Save" OnClick="btnFleed_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Fleed" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFleed" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlResponsibleEmp" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Responsible Employee</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="Label2" runat="server" Text="" CssClass="message" Visible="false" />
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

