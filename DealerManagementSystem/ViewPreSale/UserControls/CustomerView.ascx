<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.CustomerView" %>
<%@ Register Src="~/ViewPreSale/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>



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
                            <asp:LinkButton ID="lbEditCustomer" runat="server" OnClick="lbEditCustomer_Click">Edit Customer</asp:LinkButton>
                         <%--   <asp:LinkButton ID="lbViewAditTrails" runat="server" OnClick="lbViewAditTrails_Click">View Adit Trails</asp:LinkButton>--%>
                            <asp:LinkButton ID="lbAddMarketSegment" runat="server" OnClick="lbAddMarketSegment_Click">Add Market Segment</asp:LinkButton>
                            <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbAddProduct_Click">Add Product</asp:LinkButton>
                            <asp:LinkButton ID="lbAddRelation" runat="server" OnClick="lbAddRelation_Click">Add Relation</asp:LinkButton> 
                        </div>
                    </div>
                </div>
            </div>
          <%--  <div class="text-right">
                <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" Width="70px" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem>Action</asp:ListItem>
                    <asp:ListItem>Edit Customer</asp:ListItem>
                    <asp:ListItem>View Adit Trails</asp:ListItem>
                    <asp:ListItem>Add Market Segment</asp:ListItem>
                    <asp:ListItem>Add Products</asp:ListItem>
                    <asp:ListItem>Add Relations</asp:ListItem>
                </asp:DropDownList>
            </div>--%>
        </div>
    </fieldset>
</div>
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Market Segment</legend>
      
        <div class="col-md-12 Report">
            <asp:GridView ID="gvMarketSegment" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                <Columns>
                    <asp:TemplateField HeaderText="Product Name">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerMarketSegmentID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerMarketSegmentID")%>' runat="server" Visible="false" />
                            <asp:Label ID="lblMarketSegment" Text='<%# DataBinder.Eval(Container.DataItem, "MarketSegment.MarketSegment")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbMarketSegmentDelete" runat="server" OnClick="lbMarketSegmentDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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

<asp:Panel ID="pnlAddMarketSegment" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Market Segment to Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label>Market Segment</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlMarketSegment" runat="server" CssClass="form-control" />
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
<ajaxToolkit:ModalPopupExtender ID="MPE_MarketSegment" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddMarketSegment" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="PapnlAddProduct" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Customer Product</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
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


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

