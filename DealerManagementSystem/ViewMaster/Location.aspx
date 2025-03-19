<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/LocationDistrict.ascx" TagPrefix="UC" TagName="UC_LocationDistrict" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 120px;
            height: 50px;
            font: 20px;
        }

        .ajax__tab_xp .ajax__tab_header {
            background-position: bottom;
            background-repeat: repeat-x;
            font-family: verdana,tahoma,helvetica;
            font-size: 12px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <asp1:TabContainer ID="tbpLocation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
            <asp1:TabPanel ID="tbpnlCountry" runat="server" HeaderText="Country" Font-Bold="True" ToolTip="">
                <ContentTemplate>
                    <div class="col-md-12">
                        <fieldset class="fieldset-border">
                            <fieldset class="fieldset-border" id="fldCountry" runat="server">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label>Country</label>
                                        <asp:TextBox ID="txtCountry" runat="server" placeholder="Country" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="BtnSearchCountry" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchCountry_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">

                                    <span id="txnHistory2x:refreshDataGroup">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Countries:</td>

                                                            <td>
                                                                <asp:Label ID="lblRowCountN" runat="server" CssClass="label"></asp:Label></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnCountryArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnCountryArrowLeft_Click" /></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnCountryArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnCountryArrowRight_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" OnPageIndexChanging="gvCountry_PageIndexChanging">
                                            <%--EmptyDataText="No Data Found"--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country" SortExpression="Country">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGCCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGCCountry" runat="server" placeholder="Country" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Code" SortExpression="CountryCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGCCountryCode" Text='<%# DataBinder.Eval(Container.DataItem, "CountryCode")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGCCountryCode" runat="server" placeholder="Country Code" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country Currency" SortExpression="Country Currency">
                                                    <ItemStyle VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGCCountryCurrency" Text='<%# DataBinder.Eval(Container.DataItem, "Currency.Currency")%>' runat="server" />
                                                        <asp:Label ID="lblGCCountryCurrencyID" Text='<%# DataBinder.Eval(Container.DataItem, "Currency.CurrencyID")%>' runat="server" Visible="false" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlGCCountryCurrency" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales Organization" SortExpression="SalesOrganization">
                                                    <ItemStyle VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGCSalesOrganization" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOrganization")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlGCSalesOrganization" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select Sales Organization</asp:ListItem>
                                                            <asp:ListItem Value="AJE">AJE</asp:ListItem>
                                                            <asp:ListItem Value="AJF">AJF</asp:ListItem>
                                                            <asp:ListItem Value="AJI">AJI</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBtnCountryEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' OnClick="lnkBtnCountryEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkBtnCountryDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' OnClick="lnkBtnCountryDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="BtnAddOrUpdateCountry" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateCountry_Click" Width="70px" Height="33px" />
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
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tbpnlRegion" runat="server" HeaderText="Region">
                <ContentTemplate>
                    <div class="col-md-12">
                        <fieldset class="fieldset-border">
                            <fieldset class="fieldset-border" id="fldRegion" runat="server">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label>Country</label><%--<span class="Mandatory">*</span>--%>
                                        <asp:DropDownList ID="ddlRCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label>Region</label>
                                        <asp:DropDownList ID="ddlRRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="BtnSearchRegion" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchRegion_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">

                                    <span id="txnHistory2y:refreshDataGroup">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Region(s):</td>

                                                            <td>
                                                                <asp:Label ID="lblRowCountR" runat="server" CssClass="label"></asp:Label></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnRegionArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnRegionArrowLeft_Click" /></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnRegionArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnRegionArrowRight_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvRegion" runat="server" AutoGenerateColumns="false" PageSize="10" ShowFooter="true" AllowPaging="true" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnRowDataBound="gvRegion_RowDataBound" OnPageIndexChanging="gvRegion_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblGRCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlGRCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Region">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblGRRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGRRegion" runat="server" placeholder="Region" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBtnRegionEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' OnClick="lnkBtnRegionEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkBtnRegionDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' OnClick="lnkBtnRegionDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="BtnAddOrUpdateRegion" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateRegion_Click" Width="70px" Height="33px" />
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
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tbpnlState" runat="server" HeaderText="State">
                <ContentTemplate>
                    <div class="col-md-12">
                        <fieldset class="fieldset-border">
                            <fieldset class="fieldset-border" id="fldState" runat="server">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label>Country</label>

                                        <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label>Region</label>

                                        <asp:DropDownList ID="ddlSRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label>State</label>
                                        <asp:TextBox ID="txtState" runat="server" placeholder="State" CssClass="form-control" />
                                    </div>
                                    <div style="display: none">
                                        <div class="col-md-2 text-right">
                                            <label>State Code</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="BtnSearchState" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchState_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">


                                    <span id="txnHistory2:refreshDataGroup">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>State(s):</td>
                                                            <td>
                                                                <asp:Label ID="lblRowCountS" runat="server" CssClass="label"></asp:Label></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnStateArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnStateArrowLeft_Click" /></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnStateArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnStateArrowRight_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnRowDataBound="gvState_RowDataBound" OnPageIndexChanging="gvState_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Country">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGSCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblGSCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label><%--Visible="false"--%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlGSCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGSCountry_SelectedIndexChanged"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Region">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGSRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region.Region")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblGSRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "Region.RegionID")%>' runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlGSRegion" runat="server" placeholder="Region" CssClass="form-control"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGSState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGSState" runat="server" placeholder="State" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="StateCode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGSStateCode" Text='<%# DataBinder.Eval(Container.DataItem, "StateCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGSStateCode" runat="server" placeholder="State Code" CssClass="form-control"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBtnStateEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' OnClick="lnkBtnStateEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkBtnStateDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' OnClick="lnkBtnStateDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="BtnAddOrUpdateState" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateState_Click" Width="70px" Height="33px" />
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
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tbpnlDistrict" runat="server" HeaderText="District">
                <ContentTemplate>
                    <UC:UC_LocationDistrict ID="UC_DealerView" runat="server"></UC:UC_LocationDistrict>
                </ContentTemplate>
            </asp1:TabPanel>
        </asp1:TabContainer>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            GridRowDisable('MainContent_tbpLocation_tbpnlCountry_gvCountry', 'MainContent_tbpLocation_tbpnlCountry_gvCountry_lblGCCountry_')
            GridRowDisable('MainContent_tbpLocation_tbpnlRegion_gvRegion', 'MainContent_tbpLocation_tbpnlRegion_gvRegion_lblGRRegion_')
            GridRowDisable('MainContent_tbpLocation_tbpnlState_gvState', 'MainContent_tbpLocation_tbpnlState_gvState_lblGSState_')
            GridRowDisable('MainContent_tbpLocation_tbpnlDistrict_gvDistrict', 'MainContent_tbpLocation_tbpnlDistrict_gvDistrict_lblGDDistrict_')
            GridRowDisable('MainContent_tbpLocation_tbpnlCity_gvCity', 'MainContent_tbpLocation_tbpnlCity_gvCity_lblGCity_')

        });

        function GridRowDisable(gv, lbl) {
            var gvCountry = document.getElementById(gv);
            if (gvCountry != null) {
                for (var i = 0; i < gvCountry.rows.length - 1; i++) {
                    var lblGCCountry = document.getElementById(lbl + i);
                    if (lblGCCountry != null) {
                        if (lblGCCountry.innerHTML == "") {
                            lblGCCountry.parentNode.parentNode.style.display = "none";
                        }
                    }
                }
            }
        }
    </script>
</asp:Content>


