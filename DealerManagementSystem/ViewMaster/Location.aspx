<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Location" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <asp1:TabContainer ID="tbpLocation" runat="server">
                <asp1:TabPanel ID="tbpnlCountry" runat="server" HeaderText="Country">
                    <ContentTemplate>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchCountry" runat="server" CssClass="btn Search" Text="Search"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Country ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "CountryID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' runat="server"></asp:Label>
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
                        <fieldset class="fieldset-border" id="fldState" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 17px;">State</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>Country</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>State Code</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveState" runat="server" CssClass="btn Save" Text="Save"></asp:Button>
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
                                <%--<div class="col-md-2 text-right">
                                <label>State</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSSState" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>--%>
                                <div class="col-md-2 text-right">
                                    <label>Region</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSSRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchState" runat="server" CssClass="btn Search" Text="Search" OnClick="BtnSearchState_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="State ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateID" Text='<%# DataBinder.Eval(Container.DataItem, "StateID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Region">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "StateCode")%>' runat="server"></asp:Label>
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
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
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
                                    <label>State</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSDState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSDDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <asp:TemplateField HeaderText="District ID" Visible="false">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" />
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
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/Images/Edit.png" OnClick="ImageEdit_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>'/>
                                                <asp:ImageButton ID="ImageUpdate" runat="server" ImageUrl="~/Images/Update.png" OnClick="ImageUpdate_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' Visible="false"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Button ID="btnDDelete" runat="server" Font-Size="11px" Text="Delete" CssClass="btn btn-danger btn-sm" OnClick="btnDDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' />--%>
                                                <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/Images/delete.png" OnClick="ImageDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>'/>
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
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>City</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSaveCity" runat="server" CssClass="btn Save" Text="Save"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-right">
                                    <label>District</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>City</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSCCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSearchCity" runat="server" CssClass="btn Search" Text="Search"></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="City ID">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCityID" Text='<%# DataBinder.Eval(Container.DataItem, "CityID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCity" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>' runat="server"></asp:Label>
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
