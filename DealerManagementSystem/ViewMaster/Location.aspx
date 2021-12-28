<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <asp1:TabContainer ID="tbpLocation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
                <asp1:TabPanel ID="tbpnlCountry" runat="server" HeaderText="Country" Font-Bold="True" ToolTip="List of Countries...">
                    <ContentTemplate>
                        <fieldset class="fieldset-border" id="fldCountry" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Country</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveCountry" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveCountry_Click"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtSCountry" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchCountry" runat="server" CssClass="btn Search" Text="Search" OnClick="BtnSearchCountry_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="RowID" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Country ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGCCountry" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageCEdit" runat="server" ImageUrl="~/Images/Edit.png" OnClick="ImageCEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' />
                                                <asp:ImageButton ID="ImageCUpdate" runat="server" ImageUrl="~/Images/Update.png" OnClick="ImageCUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                                <asp:ImageButton ID="ImageCDelete" runat="server" ImageUrl="~/Images/delete.png" OnClick="ImageCDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f2f2f2" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tbpnlRegion" runat="server" HeaderText="Region">
                    <ContentTemplate>
                        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Region</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlRCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>Region</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtRRegion" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveRegion" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveRegion_Click"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSRCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>Region</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtSRRegion" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchRegion" runat="server" CssClass="btn Search" Text="Search" OnClick="BtnSearchRegion_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvRegion" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" OnRowDataBound="gvRegion_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row ID" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGRCountry" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Region">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGRRegion" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Region")%>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageREdit" runat="server" ImageUrl="~/Images/Edit.png" OnClick="ImageREdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' />
                                                <asp:ImageButton ID="ImageRUpdate" runat="server" ImageUrl="~/Images/Update.png" OnClick="ImageRUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                                <asp:ImageButton ID="ImageRDelete" runat="server" ImageUrl="~/Images/delete.png" OnClick="ImageRDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f2f2f2" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tbpnlState" runat="server" HeaderText="State">
                    <ContentTemplate>
                        <fieldset class="fieldset-border" id="fldState" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">State</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>Region</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>State Code</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveState" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveState_Click"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSSCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>Region</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSSRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtSSState" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchState" runat="server" CssClass="btn Search" Text="Search" OnClick="BtnSearchState_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" OnRowDataBound="gvState_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row ID" ItemStyle-Width="100">
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
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGSCountry" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Region">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region.RegionID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGSRegion" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGRState" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="StateCode">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGRStateCode" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "StateCode")%>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageSEdit" runat="server" ImageUrl="~/Images/Edit.png" OnClick="ImageSEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' />
                                                <asp:ImageButton ID="ImageSUpdate" runat="server" ImageUrl="~/Images/Update.png" OnClick="ImageSUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                                <asp:ImageButton ID="ImageSDelete" runat="server" ImageUrl="~/Images/delete.png" OnClick="ImageSDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f2f2f2" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tbpnlDistrict" runat="server" HeaderText="District">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">District</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">                                
                                <div class="col-md-2 text-right">
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveDistrict" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveDistrict_Click"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSDCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSDState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">                                
                                <div class="col-md-2 text-right">
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtSDDistrict" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchDistrict" runat="server" CssClass="btn Search" Text="Search" OnClick="BtnSearchDistrict_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvDistrict" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" OnRowDataBound="gvDistrict_RowDataBound">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Row ID" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGDCountry" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGDState" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGDDistrict" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageDEdit" runat="server" ImageUrl="~/Images/Edit.png" OnClick="ImageDEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />
                                                <asp:ImageButton ID="ImageDUpdate" runat="server" ImageUrl="~/Images/Update.png" OnClick="ImageDUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                                <asp:ImageButton ID="ImageDDelete" runat="server" ImageUrl="~/Images/delete.png" OnClick="ImageDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f2f2f2" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
                <asp1:TabPanel ID="tbpnlCity" runat="server" HeaderText="City">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">City</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCityCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCityState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCityDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>City</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveCity" runat="server" CssClass="btn Save" Text="Save" OnClick="BtnSaveCity_Click"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCityCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCityState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCityDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>City</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtSCity" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchCity" runat="server" CssClass="btn Search" Text="Search" OnClick="BtnSearchCity_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found" OnRowDataBound="gvCity_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row ID" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGCityCountry" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGCityState" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.DistrictID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlGCityDistrict" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGCity" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Tehsil")%>' Enabled="false"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageCityEdit" runat="server" ImageUrl="~/Images/Edit.png" OnClick="ImageCityEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' />
                                                <asp:ImageButton ID="ImageCityUpdate" runat="server" ImageUrl="~/Images/Update.png" OnClick="ImageCityUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                                <asp:ImageButton ID="ImageCityDelete" runat="server" ImageUrl="~/Images/delete.png" OnClick="ImageCityDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TehsilID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f2f2f2" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp1:TabPanel>
            </asp1:TabContainer>
        </div>
    </div>
</asp:Content>
