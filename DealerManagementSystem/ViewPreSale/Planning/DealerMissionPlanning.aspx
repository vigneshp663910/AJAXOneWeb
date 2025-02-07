<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerMissionPlanning.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Planning.DealerMissionPlanning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"  />
  
    <div class="col-md-12" id="divList" runat="server"> 
        <fieldset class="fieldset-border"  id="FldSearch" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Year</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Month</label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Product Type</label>
                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="BtnFUpload" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnFUpload_Click" />
                    <%--  <asp:Button ID="btnEdit" runat="server" CssClass="btn Save" Text="Edit" OnClick="btnEdit_Click" Width="150px" Visible="false"></asp:Button>--%>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border" id="FldUpload" runat="server" visible="false">
            <legend style="background: none; color: #007bff; font-size: 17px;">Upload File</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12" id="DivUpload" runat="server">
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </div>
                <div class="col-md-4 col-sm-12">
                    <asp:Button ID="BtnUpload" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnUpload_Click" Width="100px" />
                    <asp:Button ID="BtnFBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnFBack_Click" />
                </div>
            </div>
            <asp:GridView ID="GVUpload" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                EmptyDataText="No Data Found" AutoGenerateColumns="true" Width="100%">
                <AlternatingRowStyle BackColor="#ffffff" />
                <FooterStyle ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>
        </fieldset>
        <div class="col-md-12" id="DivReport" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                <div class="col-md-12 Report">

                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Planning(s):</td>

                                        <td>
                                            <asp:Label ID="lblRowCountV" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVTArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVTArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>


                    <asp:GridView ID="gvMissionPlanning" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnDataBound="OnDataBound" OnPageIndexChanging="gvVisitTarget_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Year" SortExpression="Year" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month" SortExpression="Month" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code" ItemStyle-Width="30px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblVisitTargetID" Text='<%# DataBinder.Eval(Container.DataItem, "VisitTargetID")%>' runat="server" Visible="false" />--%>
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Type" ItemStyle-Width="30px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblVisitTargetID" Text='<%# DataBinder.Eval(Container.DataItem, "VisitTargetID")%>' runat="server" Visible="false" />--%>
                                    <asp:Label ID="lblProductTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductTypeID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Billing Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillingPlan" Text='<%# DataBinder.Eval(Container.DataItem, "BillingPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtBillingPlan" Text='<%# DataBinder.Eval(Container.DataItem, "BillingPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Billing Revenue Plan" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillingRevenuePlan" Text='<%# DataBinder.Eval(Container.DataItem, "BillingRevenuePlan")%>' runat="server" />
                                    <asp:TextBox ID="txtBillingRevenuePlan" Text='<%# DataBinder.Eval(Container.DataItem, "BillingRevenuePlan")%>' runat="server" CssClass="form-control" TextMode="Number" Visible="false" onblur="Calculation(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Retail Plan" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRetailPlan" Text='<%# DataBinder.Eval(Container.DataItem, "RetailPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtRetailPlan" Text='<%# DataBinder.Eval(Container.DataItem, "RetailPlan")%>' runat="server" CssClass="form-control" TextMode="Number" Visible="false" onblur="Calculation(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lead Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblLeadPlan" Text='<%# DataBinder.Eval(Container.DataItem, "LeadPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtLeadPlan" Text='<%# DataBinder.Eval(Container.DataItem, "LeadPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lead Conversion Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblLeadConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "LeadConversionPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtLeadConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "LeadConversionPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quotation Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuotationPlan" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtQuotationPlan" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quotation Conversion Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuotationConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationConversionPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtQuotationConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationConversionPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parts Quotation Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartsQuotationPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsQuotationPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtPartsQuotationPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsQuotationPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parts Quotation Conversion Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartsQuotationConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsQuotationConversionPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtPartsQuotationConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsQuotationConversionPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parts Retail Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartsRetailPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsRetailPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtPartsRetailPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsRetailPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parts Billing Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartsBillingPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsBillingPlan")%>' runat="server" />
                                    <asp:TextBox ID="txtPartsBillingPlan" Text='<%# DataBinder.Eval(Container.DataItem, "PartsBillingPlan")%>'
                                        runat="server" CssClass="form-control" TextMode="Number" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnMissionPlanningEdit" runat="server" OnClick="btnEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                    <asp:Button ID="BtnUpdateMissionPlanning" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnUpdateMissionPlanning_Click" Width="70px" Height="33px" Visible="false" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnUpdateMissionPlanning_Click" Width="70px" Height="33px" Visible="false" />
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
            </fieldset>
        </div>
    </div>

</asp:Content>
