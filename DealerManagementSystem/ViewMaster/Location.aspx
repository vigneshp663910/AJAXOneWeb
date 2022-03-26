<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
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
    <asp1:TabContainer ID="tbpLocation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
        <asp1:TabPanel ID="tbpnlCountry" runat="server" HeaderText="Country" Font-Bold="True" ToolTip="List of Countries...">
            <ContentTemplate>
                <fieldset class="fieldset-border" id="fldCountry" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Search Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Country</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="BtnSearchCountry" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchCountry_Click"></asp:Button>
                            <%--<asp:Button ID="BtnAddCountry" runat="server" CssClass="btn Save" Text="Add" OnClick="BtnAddCountry_Click"></asp:Button>--%>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" OnPageIndexChanging="gvCountry_PageIndexChanging">  <%--EmptyDataText="No Data Found"--%>
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Country ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Country" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGCCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="true" />
                                        <%--<asp:TextBox ID="txtGCCountry" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Visible="false"></asp:TextBox>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGCCountry" runat="server" placeholder="Country" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country Code" SortExpression="CountryCode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGCCountryCode" Text='<%# DataBinder.Eval(Container.DataItem, "CountryCode")%>' runat="server" />
                                                </ItemTemplate>
                                                <%--<FooterTemplate>
                                                    <asp:TextBox ID="txtProductMake" runat="server" placeholder="Make" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>--%>
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
                                                    <%--<asp:TextBox ID="txtGCCountryCurrency" runat="server" placeholder="Country Currency" CssClass="form-control"></asp:TextBox>--%>
                                                   <asp:DropDownList ID="ddlGCCountryCurrency" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Organization" SortExpression="SalesOrganization">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGCSalesOrganization" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOrganization")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%--<asp:TextBox ID="txtGCSalesOrganization" runat="server" placeholder="Sales Organization" CssClass="form-control"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlGCSalesOrganization" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageCEdit" runat="server" ImageUrl="~/Images/Edit1.png" OnClick="ImageCEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' />
                                        <asp:ImageButton ID="ImageCUpdate" runat="server" ImageUrl="~/Images/Save.png" OnClick="ImageCUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>--%>
                                        <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                       <%-- <asp:ImageButton ID="ImageCDelete" runat="server" ImageUrl="~/Images/delete1.png" OnClick="ImageCDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tbpnlRegion" runat="server" HeaderText="Region">
            <ContentTemplate>
                <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Search Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Country</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlRCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <%--<span class="Mandatory">*</span>--%>
                            <label>Region</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtRRegion" runat="server" CssClass="form-control" MaxLength="10"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="BtnSearchRegion" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchRegion_Click"></asp:Button>
                            <%--<asp:Button ID="BtnAddRegion" runat="server" CssClass="btn Save" Text="Add" OnClick="BtnAddRegion_Click"></asp:Button>--%>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
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
                                        <%--Visible="false"--%>
                                        <asp:Label ID="lblGRCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGRCountry" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGRCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtGRRegion" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Region")%>' Enabled="false"></asp:TextBox>--%>
                                        <asp:Label ID="lblGRRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblGRRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGRRegion" runat="server" placeholder="Region" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageREdit" runat="server" ImageUrl="~/Images/Edit1.png" OnClick="ImageREdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' />
                                        <asp:ImageButton ID="ImageRUpdate" runat="server" ImageUrl="~/Images/Save.png" OnClick="ImageRUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>--%>
                                        <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                        <%--<asp:ImageButton ID="ImageRDelete" runat="server" ImageUrl="~/Images/Delete1.png" OnClick="ImageRDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tbpnlState" runat="server" HeaderText="State">
            <ContentTemplate>
                <fieldset class="fieldset-border" id="fldState" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Search Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Country</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Region</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlSRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>State</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State Code</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="BtnSearchState" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchState_Click"></asp:Button>
                            <%--<asp:Button ID="BtnAddState" runat="server" CssClass="btn Save" Text="Add" OnClick="BtnAddState_Click"></asp:Button>--%>

                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnRowDataBound="gvState_RowDataBound" OnPageIndexChanging="gvState_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="State ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateID" Text='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGSCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblGSCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label><%--Visible="false"--%>
                                        <%--<asp:DropDownList ID="ddlGSCountry" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGSCountry_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGSCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGSCountry_SelectedIndexChanged"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGSRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region.Region")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblGSRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "Region.RegionID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGSRegion" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGSRegion" runat="server" placeholder ="Region" CssClass="form-control"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtGRState" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' Enabled="false"></asp:TextBox>--%>
                                        <asp:Label ID="lblGSState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGSState" runat="server" placeholder ="State" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StateCode">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtGRStateCode" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "StateCode")%>' Enabled="false"></asp:TextBox>--%>
                                        <asp:Label ID="lblGSStateCode" Text='<%# DataBinder.Eval(Container.DataItem, "StateCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGSStateCode" runat="server" placeholder="State Code" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageSEdit" runat="server" ImageUrl="~/Images/Edit1.png" OnClick="ImageSEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' />
                                        <asp:ImageButton ID="ImageSUpdate" runat="server" ImageUrl="~/Images/Save.png" OnClick="ImageSUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>--%>
                                        <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                        <%--<asp:ImageButton ID="ImageSDelete" runat="server" ImageUrl="~/Images/Delete1.png" OnClick="ImageSDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tbpnlDistrict" runat="server" HeaderText="District">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Search Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Country</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDCountry_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDState_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>DealerCode</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>District</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="BtnSearchDistrict" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchDistrict_Click"></asp:Button>
                            <%--<asp:Button ID="BtnAddDistrict" runat="server" CssClass="btn Save" Text="Add" OnClick="BtnAddDistrict_Click"></asp:Button>--%>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvDistrict" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnRowDataBound="gvDistrict_RowDataBound" OnPageIndexChanging="gvDistrict_PageIndexChanging">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGDCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label> <%--Visible="false"--%>
                                        <asp:Label ID="lblGDCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGDCountry" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGDCountry_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGDCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDCountry_SelectedIndexChanged"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGDState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label> <%--Visible="false"--%>
                                        <asp:Label ID="lblGDStateID" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGDState" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGDState" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DealerCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGDDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label><%-- Visible="false"--%>
                                        <asp:Label ID="lblGDDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGDDealer" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGDDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtGDDistrict" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' Enabled="false"></asp:TextBox>--%>
                                        <asp:Label ID="lblGDDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGDDistrict" runat="server" CssClass="form-control"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageDEdit" runat="server" ImageUrl="~/Images/Edit1.png" OnClick="ImageDEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />
                                        <asp:ImageButton ID="ImageDUpdate" runat="server" ImageUrl="~/Images/Save.png" OnClick="ImageDUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>--%>
                                        <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                        <%--<asp:ImageButton ID="ImageDDelete" runat="server" ImageUrl="~/Images/Delete1.png" OnClick="ImageDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnDistrictEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' OnClick="lnkBtnDistrictEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnDistrictDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' OnClick="lnkBtnDistrictDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="BtnAddOrUpdateDistrict" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateDistrict_Click" Width="70px" Height="33px" />
                                    </FooterTemplate>
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
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tbpnlCity" runat="server" HeaderText="City">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Search Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Country</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCityCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCityCountry_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCityState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCityState_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>District</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCityDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>City</label><%--<span class="Mandatory">*</span>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="BtnSearchCity" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchCity_Click"></asp:Button>
                            <%--<asp:Button ID="BtnAddCity" runat="server" CssClass="btn Save" Text="Add" OnClick="BtnAddCity_Click"></asp:Button>--%>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnRowDataBound="gvCity_RowDataBound" OnPageIndexChanging="gvCity_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGCityCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label> <%--Visible="false"--%>
                                        <asp:Label ID="lblGCityCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGCityCountry" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGCityCountry_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGCityCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGCityCountry_SelectedIndexChanged"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGCityState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>  <%--Visible="false"--%>
                                        <asp:Label ID="lblGCityStateID" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlGCityState" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGCityState_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGCityState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGCityState_SelectedIndexChanged"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGCityDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server"></asp:Label>  <%--Visible="false"--%>
                                        <asp:Label ID="lblGCityDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "District.DistrictID")%>' runat="server" Visible="false"></asp:Label> 
                                        <%--<asp:DropDownList ID="ddlGCityDistrict" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlGCityDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGCity" Text='<%# DataBinder.Eval(Container.DataItem, "Tehsil")%>' runat="server"></asp:Label>
                                        <%--<asp:Label ID="lblGCityID" Text='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' runat="server" Visible="false"></asp:Label>--%>
                                        <%--<asp:TextBox ID="txtGCity" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Tehsil")%>' Enabled="false" />--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGCity" runat="server" placeholder="City" CssClass="form-control"/>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageCityEdit" runat="server" ImageUrl="~/Images/Edit1.png" OnClick="ImageCityEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' />
                                        <asp:ImageButton ID="ImageCityUpdate" runat="server" ImageUrl="~/Images/Save.png" OnClick="ImageCityUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>--%>
                                        <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                        <%--<asp:ImageButton ID="ImageCityDelete" runat="server" ImageUrl="~/Images/Delete1.png" OnClick="ImageCityDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnCityEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' OnClick="lnkBtnCityEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnCityDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' OnClick="lnkBtnCityDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="BtnAddOrUpdateCity" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateCity_Click" Width="70px" Height="33px" />
                                    </FooterTemplate>
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
            </ContentTemplate>
        </asp1:TabPanel>
    </asp1:TabContainer>
    <script type="text/javascript">
        $(document).ready(function () {
            GridRowDisable('MainContent_tbpLocation_tbpnlCountry_gvCountry', 'MainContent_tbpLocation_tbpnlCountry_gvCountry_lblGCCountry_')
            GridRowDisable('MainContent_tbpLocation_tbpnlRegion_gvRegion', 'MainContent_tbpLocation_tbpnlRegion_gvRegion_lblGRRegion_')
            GridRowDisable('MainContent_tbpLocation_tbpnlState_gvState', 'MainContent_tbpLocation_tbpnlState_gvState_lblGSState_')
            GridRowDisable('MainContent_tbpLocation_tbpnlDistrict_gvDistrict', 'MainContent_tbpLocation_tbpnlDistrict_gvDistrict_lblGDDistrict_')
            GridRowDisable('MainContent_tbpLocation_tbpnlCity_gvCity', 'MainContent_tbpLocation_tbpnlCity_gvCity_lblGCity_')
            //var gvCountry = document.getElementById('MainContent_tbpLocation_tbpnlCountry_gvCountry');

            //if (gvCountry != null) {
            //    for (var i = 0; i < gvCountry.rows.length - 1; i++) {
            //        var lblGCCountry = document.getElementById('MainContent_tbpLocation_tbpnlCountry_gvCountry_lblGCCountry_' + i);
            //        if (lblGCCountry != null) {
            //            if (lblGCCountry.innerHTML == "") {
            //                lblGCCountry.parentNode.parentNode.style.display = "none";
            //            }
            //        }
            //    }
            //}
        });

        function GridRowDisable(gv,lbl)
        {
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


