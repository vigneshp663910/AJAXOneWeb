<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerSalesConfiguration.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.DealerSalesConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageDealerSalesDistrict" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="fldDistrict" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label>Country</label>
                    <asp:DropDownList ID="ddlDSalesCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDSalesCountry_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Region</label>
                    <asp:DropDownList ID="ddlDSalesRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDSalesRegion_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>State</label>
                    <asp:DropDownList ID="ddlDSalesState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDSalesState_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>District</label>
                    <asp:DropDownList ID="ddlDSalesDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDSalesDistrict_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Dealer Code</label>
                    <asp:DropDownList ID="ddlDSalesDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearchDealerSalesDistrict" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchDealerSalesDistrict_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
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
                                        <asp:Label ID="lblRowCountDealerSalesDistrict" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerSalesDistrictArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerSalesDistrictArrowLeft_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerSalesDistrictArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerSalesDistrictArrowRight_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvDealerSalesDistrict" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnPageIndexChanging="gvDealerSalesDistrict_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGDSalesCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:DropDownList ID="ddlGDSalesCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDSalesCountry_SelectedIndexChanged"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtSalesCountry" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Region">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGDSalesRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.RegionID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>--%>
                                <%--<asp:DropDownList ID="ddlGDSalesRegion" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                <%--<asp:TextBox ID="txtSalesRegion" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGDSalesStateID" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:DropDownList ID="ddlGDSalesState" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtSalesState" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District">
                            <ItemTemplate>
                                <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:DropDownList ID="ddlGSalesDistrict" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtSalesDistrict" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Office">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOffice")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGDSalesOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOfficeID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlGDSalesOffice" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Code">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGDSalesDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlGDSalesDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDSalesDealer_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Engineer">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGDSalesEngineerUserID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.UserID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnDealerSalesDistrictEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDealerSalesDistrictEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="BtnUpdateDealerSalesDistrict" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnUpdateDealerSalesDistrict_Click" Width="70px" Height="33px" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>
