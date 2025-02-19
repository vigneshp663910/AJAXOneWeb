<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerServiceConfiguration.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.DealerServiceConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageDealerServiceDistrict" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <%--<asp1:TabContainer ID="tbpDealerServiceDistrict" runat="server" ToolTip="Dealer Service District..." Font-Bold="True" Font-Size="Medium">--%>
        <%--<asp1:TabPanel ID="tbpnlDealerServiceDistrict" runat="server" HeaderText="State">--%>
        <%--<ContentTemplate>--%>
        <div class="col-md-12" id="divList" runat="server">
            <%--<fieldset class="fieldset-border">--%>
            <fieldset class="fieldset-border" id="fldDealerServiceDistrict" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label>Country</label>
                        <asp:DropDownList ID="ddlDServiceCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDServiceCountry_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Region</label>
                        <asp:DropDownList ID="ddlDServiceRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDServiceRegion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>State</label>
                        <asp:DropDownList ID="ddlDServiceState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDServiceState_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>District</label>
                        <asp:DropDownList ID="ddlDServiceDistrict" runat="server" CssClass="form-control" AutoPostBack="true"  ></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Dealer Code</label>
                        <asp:DropDownList ID="ddlDServiceDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearchDealerServiceDistrict" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchDealerServiceDistrict_Click"></asp:Button>
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
                                            <asp:Label ID="lblRowCountDealerServiceDistrict" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnDealerServiceDistrictArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerServiceDistrictArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnDealerServiceDistrictArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerServiceDistrictArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvDealerServiceDistrict" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnPageIndexChanging="gvDealerServiceDistrict_PageIndexChanging"  >
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDServiceCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblGDServiceCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtServiceCountry" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDServiceRegion" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.Region")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblGDServiceRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "State.Region.RegionID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtServiceRegion" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDServiceState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblGDServiceStateID" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtServiceState" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDServiceDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblGDServiceDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtServiceDistrict" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDServiceDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblGDServiceDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlGDServiceDealer" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnDealerServiceDistrictEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDealerServiceDistrictEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="BtnUpdateDealerServiceDistrict" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnUpdateDealerServiceDistrict_Click" Width="70px" Height="33px" Visible="false" />
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
    </div> 
</asp:Content>
