<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesAndServiceConfiguration.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.SalesAndServiceConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="fldDistrict" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label>Country</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                </div>
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
                <asp:GridView ID="gvDealerSales" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" >
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSalesCountry" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                               </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSalesState" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District">
                            <ItemTemplate>
                                <asp:Label ID="lblGDsalesDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSalesDistrict" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Office">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOffice")%>' runat="server"></asp:Label>
                                   </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlGDSalesOffice" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Code">
                            <ItemTemplate>
                                <asp:Label ID="lblGvSalesDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label> 
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlGvSalesDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlgvSalesDealer_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Engineer">
                            <ItemTemplate>
                                <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlGvSalesEngineer" runat="server" CssClass="form-control" Visible="false" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnDealerSalesDistrictEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDealerSalesDistrictEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="BtnUpdateDealerSales" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnUpdateDealerSales_Click" Width="70px" Height="33px" Visible="false" />
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
