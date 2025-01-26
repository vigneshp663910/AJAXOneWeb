<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="SalesAndServiceConfiguration.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.SalesAndServiceConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="fldDistrict" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <%--  <div class="col-md-2 col-sm-12">
                    <label>Country</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                </div>--%>
                <div class="col-md-2 col-sm-12">
                    <label>Region</label>
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>State</label>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>District</label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Sales Dealer</label>
                    <asp:DropDownList ID="ddlSalesDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Service Dealer</label>
                    <asp:DropDownList ID="ddlServiceDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Sales Retailer</label>
                    <asp:DropDownList ID="ddlSalesRetailer" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Service Retailer</label>
                    <asp:DropDownList ID="ddlServiceRetailer" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
            <div class="col-md-12 Report">
                <span id="txnHistory3:refreshDataGroup"></span>
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>District(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountDealerSales" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerSalesLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrow_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerSalesRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrow_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp1:TabContainer ID="tbp" runat="server" Font-Bold="True" Font-Size="Medium">
                    <asp1:TabPanel ID="tpnlSourceOfEnquiry" runat="server" HeaderText="Dealer Sales" Font-Bold="True">
                        <ContentTemplate>
                            <asp:GridView ID="gvDealerSales" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfRegion" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfState" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblGvDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfGvDistrictID" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblfDistrict" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Office">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDSalesOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOffice")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFGvSalesOffice" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlGvSalesOffice" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvSalesDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFGvSalesDealer" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlGvSalesDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlgvSalesDealer_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Sales Engineer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesDealerEngineer.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGvDealerSalesEngineer" runat="server" CssClass="form-control" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnDealerSalesEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDealerSalesEdit_Click"><i class="fa fa-fw fa-edit" style="font-size: 18px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BtnDealerSalesUpdate" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnDealerSalesUpdate_Click" Width="70px" Height="33px" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="Dealer Service" Font-Bold="True">
                        <ContentTemplate>
                            <asp:GridView ID="gvDealerService" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfRegion" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfState" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblGvDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfGvDistrictID" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblfDistrict" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Dealer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvServiceDealer" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceDealer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGvServiceDealer" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnDealerServiceEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDealerServiceEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BtnDealerServiceUpdate" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnDealerServiceUpdate_Click" Width="70px" Height="33px" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="Retailer Sales" Font-Bold="True">
                        <ContentTemplate>
                            <asp:GridView ID="gvRetailerSales" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfRegion" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfState" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblGvDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' Visible="false" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfGvDistrictID" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblfDistrict" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Retailer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvSalesRetailer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesRetailer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFGvSalesRetailer" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlGvSalesRetailer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlgvSalesRetailer_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Sales Engineer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesRetailerEngineer.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGvRetailerSalesEngineer" runat="server" CssClass="form-control" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnRetailerSalesEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnRetailerSalesEdit_Click"><i class="fa fa-fw fa-edit" style="font-size: 18px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BtnRetailerSalesUpdate" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnRetailerSalesUpdate_Click" Width="70px" Height="33px" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="TabPanel3" runat="server" HeaderText="Retailer Service" Font-Bold="True">
                        <ContentTemplate>
                            <asp:GridView ID="gvRetailerService" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfRegion" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfState" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblGvDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfGvDistrictID" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblfDistrict" runat="server" ForeColor="blue" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Retailer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvServiceDealer" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceRetailer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGvServiceRetailer" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnRetailerServiceEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnRetailerServiceEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BtnRetailerServiceUpdate" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnRetailerServiceUpdate_Click" Width="70px" Height="33px" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="TabPanel4" runat="server" HeaderText="List" Font-Bold="True">
                        <ContentTemplate>
                            <asp:GridView ID="gvList" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Office">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDSalesOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOffice")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Dealer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvSalesDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Sales Engineer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesDealerEngineer.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Dealer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvServiceDealer" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceDealer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Retailer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvSalesRetailer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesRetailer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Sales Engineer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesRetailerEngineer.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Retailer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGvServiceDealer" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceRetailer.DealerCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp1:TabPanel>
                </asp1:TabContainer>

            </div>
        </fieldset>
    </div>
</asp:Content>
