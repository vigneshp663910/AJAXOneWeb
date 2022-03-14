<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesQuotationView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.SalesQuotationView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddQuotation.ascx" TagPrefix="UC" TagName="UC_Quotation" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddFollowUp.ascx" TagPrefix="UC" TagName="UC_FollowUp" %>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnEditQuotation" runat="server" OnClick="lbActions_Click">Edit Quotation Basic Info</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditFinancier" runat="server" OnClick="lbActions_Click">Edit Financier Info</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddProduct" runat="server" OnClick="lbActions_Click">Add Product</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddCompetitor" runat="server" OnClick="lbActions_Click">Add Competitor</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddQuotationNote" runat="server" OnClick="lbActions_Click">Add Quotation Note</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFollowUp" runat="server" OnClick="lbActions_Click">Add Follow-up</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddEffort" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddExpense" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>
                <asp:LinkButton ID="lbtnGenerateQuotation" runat="server" OnClick="lbActions_Click">Generate Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbtnPrintPDF" runat="server" OnClick="lbActions_Click">Print PDF</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Quotation</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Ref Quotation No : </label>
                    <asp:Label ID="lblRefQuotationNo" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ref Quotation Date : </label>
                    <asp:Label ID="lblRefQuotationDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Number : </label>
                    <asp:Label ID="lblQuotationNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Date : </label>
                    <asp:Label ID="lblQuotationDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Quotation Type : </label>
                    <asp:Label ID="lblQuotationType" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Status : </label>
                    <asp:Label ID="lblQuotationStatus" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Valid From : </label>
                    <asp:Label ID="lblValidFrom" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Valid To : </label>
                    <asp:Label ID="lblValidTo" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Pricing Date : </label>
                    <asp:Label ID="lblPricingDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Price Group : </label>
                    <asp:Label ID="lblPriceGroup" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>User Status : </label>
                    <asp:Label ID="lblUserStatus" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

<asp:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp:TabPanel ID="tpnlFinancier" runat="server" HeaderText="Financier" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Bank Name : </label>
                        <asp:Label ID="lblBankName" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>IncoTerms : </label>
                        <asp:Label ID="lblIncoTerms" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Payment Terms : </label>
                        <asp:Label ID="lblPaymentTerms" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>DoNumber : </label>
                        <asp:Label ID="lblDoNumber" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Do Date : </label>
                        <asp:Label ID="lblDoDate" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Advance Amount : </label>
                        <asp:Label ID="lblAdvanceAmount" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Financier Amount : </label>
                        <asp:Label ID="lblFinancierAmount" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlProduct" runat="server" HeaderText="Product">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlantCode" Text='<%# DataBinder.Eval(Container.DataItem, "Plant.PlantCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBaseUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Material.BaseUnit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Price">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--    <asp:TemplateField HeaderText="Tax %">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxPersent" Text='<%# DataBinder.Eval(Container.DataItem, "TaxPersent")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxvalue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="NetValue">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetValue" Text='<%# DataBinder.Eval(Container.DataItem, "NetValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabCompetitor" runat="server" HeaderText="Competitor">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvCompetitor" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Make">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Make.Make")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblSalesQuotationCompetitorID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotationCompetitorID")%>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Product")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblCompetitorRemove" runat="server" OnClick="lblCompetitorRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TaQuotationNote" runat="server" HeaderText="Quotation Note">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvNote" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Make">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Note.Note")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblSalesQuotationCompetitorID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesQuotationNoteID")%>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remark">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblNoteRemove" runat="server" OnClick="lblNoteRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabLead" runat="server" HeaderText="Lead">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Lead Number : </label>
                        <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Visit Date : </label>
                        <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Dealer : </label>
                        <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Remarks : </label>
                        <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Customer : </label>
                        <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Contact Person : </label>
                        <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Mobile : </label>
                        <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Email : </label>
                        <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Address : </label>
                        <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Importance : </label>
                        <asp:Label ID="lblImportance" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Status : </label>
                        <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Ship to party">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Contact Person : </label>
                        <asp:Label ID="Label1" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Mobile : </label>
                        <asp:Label ID="Label2" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Email : </label>
                        <asp:Label ID="Label3" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Address : </label>
                        <asp:Label ID="Label4" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Importance : </label>
                        <asp:Label ID="Label5" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Status : </label>
                        <asp:Label ID="Label6" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlFollowUp" runat="server" HeaderText="Follow Up">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Engineer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Follow Up Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFollowUpDate" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Follow Up Note">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUpNote")%>' runat="server" />
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
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlEffort" runat="server" HeaderText="Effort">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Start Time" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortStartTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort End Time" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortEndTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlExpense" runat="server" HeaderText="Expense">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>

<asp:Panel ID="pnlFinancier" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Quotation</span><a href="#" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageFinancier" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Financier</label>
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Incoterms</label>
                    <asp:DropDownList ID="ddlIncoterms" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Payment Terms</label>
                    <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">DO Number</label>
                    <asp:TextBox ID="txtDoNumber" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDoNumber" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Do Date</label>
                    <asp:TextBox ID="txtDoDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Advance Amount</label>
                    <asp:TextBox ID="txtAdvanceAmount" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtAdvanceAmount" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Financier Amount</label>
                    <asp:TextBox ID="txtFinancierAmount" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>

            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnFinancier" runat="server" CssClass="btn Save" Text="Save" OnClick="btnFinancier_Click"></asp:Button>
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Financier" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFinancier" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlProduct" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Product</span><a href="#" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageProduct" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Plant</label>
                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Material</label>
                    <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>

                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Qty</label>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Discount</label>
                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnProductSave" runat="server" CssClass="btn Save" Text="Save" OnClick="btnProductSave_Click"></asp:Button>
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Product" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlProduct" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlCompetitor" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Competitor</span><a href="#" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageCompetitor" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset5" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Competitor</label>
                    <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Competitor Product Type</label>
                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Competitor Products</label>
                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Remark</label>
                    <asp:TextBox ID="txtCompetitorRemark" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnCompetitorSave" runat="server" CssClass="btn Save" Text="Save" OnClick="btnCompetitorSave_Click"></asp:Button>
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Competitor" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCompetitor" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pntNote" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Note</span><a href="#" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageNote" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset3" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Note</label>
                    <asp:DropDownList ID="ddlNote" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Remark</label>
                    <asp:TextBox ID="txtNoteRemark" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnNoteRemark" runat="server" CssClass="btn Save" Text="Save" OnClick="btnNoteRemark_Click"></asp:Button>
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Note" runat="server" TargetControlID="lnkMPE" PopupControlID="pntNote" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlQuotation" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Quotation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblMessageQuotation" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">

            <UC:UC_Quotation ID="UC_Quotation" runat="server"></UC:UC_Quotation>
        </div>
        <asp:Button ID="BtnSaveQuotation" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveQuotation_Click"></asp:Button>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Quotation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlQuotation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlFollowUp" runat="server" CssClass="Popup" Style="display: none; height: 400px">
    <div class="PopupHeader clearfix"><span id="PopupDialogue">Pre -Sales FollowUp</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" /></a></div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageFollowUp" runat="server" Text="" CssClass="message" Visible="false" />
        <UC:UC_FollowUp ID="UC_FollowUp" runat="server"></UC:UC_FollowUp>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveFollowUp" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveFollowUp_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_FollowUp" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFollowUp" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageEffort" runat="server" Text="" CssClass="message" Visible="false" />
        <UC:UC_Effort ID="UC_Effort" runat="server"></UC:UC_Effort>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveEffort" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveEffort_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cold Visit Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button8" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageExpense" runat="server" Text="" CssClass="message" Visible="false" />
        <UC:UC_Expense ID="UC_Expense" runat="server"></UC:UC_Expense>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveExpense" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveExpense_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Expense" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlExpense" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<%--<fieldset class="fieldset-border" id="Fieldset4" runat="server">
    <div class="col-md-12">
        <asp:Panel ID="pnlAllowM" runat="server" Enabled="true">
            <asp:Label ID="Label1" runat="server" Text="" CssClass="label" Width="100%" />
            <asp:Panel ID="pnlMaterial" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="QuotationItemID">
                            <Columns>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Plant.PlantCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Price">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBasicPrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasicPrice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax %">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxPersent" Text='<%# DataBinder.Eval(Container.DataItem, "TaxPersent")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxvalue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NetValue">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetValue" Text='<%# DataBinder.Eval(Container.DataItem, "NetValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMaterialRemove" runat="server" CommandArgument="QuotationItemID" OnClick="lblMaterialRemove_Click">Cancel</asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                       
                                        <asp:LinkButton ID="lblMaterialAdd" runat="server" OnClick="lblMaterialAdd_Click" AutoPostBack="false">Add</asp:LinkButton>
                                     
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</fieldset>--%>

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

 <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
<script type="text/javascript">
    function InIEvent() { }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
       
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_UC_QuotationView_txtMaterial").autocomplete({
                source: function (request, response) {
                    debugger;
                    var param = { input: $('#MainContent_UC_QuotationView_txtMaterial').val() };
                    $.ajax({
                        url: "Quotation.aspx/SearchSMaterial",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });

        });
    };

    $(function () {

        $("#MainContent_UC_QuotationView_txtMaterial").autocomplete({
           
            source: function (request, response) {
                debugger;
                var param = { input: $('#MainContent_UC_QuotationView_txtMaterial').val() };
                $.ajax({
                    url: "Quotation.aspx/SearchSMaterial",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");  
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox  
        });
    });


     
</script>

