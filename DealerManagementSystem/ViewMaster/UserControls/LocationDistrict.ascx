<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationDistrict.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.LocationDistrict" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <fieldset class="fieldset-border" id="fldDistrict" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12"> 
                <div class="col-md-2 col-sm-12">
                    <label>Country</label>
                    <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDCountry_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Region</label>
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>State</label>
                    <asp:DropDownList ID="ddlDState" runat="server" CssClass="form-control" ></asp:DropDownList>
                </div>

                <div class="col-md-2 col-sm-12">
                    <label>District</label>
                    <asp:TextBox ID="txtDistrict" runat="server" placeholder="District" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearchDistrict" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchDistrict_Click"></asp:Button> 
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
            <div class="col-md-12 Report">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>District(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountD" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDistrictArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDistrictArrowLeft_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDistrictArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDistrictArrowRight_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvDistrict" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="true" EmptyDataText="No Data Found" OnPageIndexChanging="gvDistrict_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblGvCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGvCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblfGvCountry" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlfGvCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDCountry_SelectedIndexChanged" ></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate>
                                <asp:Label ID="lblGvState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGvStateID" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblfGvState" runat="server" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlfGvState" runat="server" CssClass="form-control"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District">
                            <ItemTemplate>
                                <asp:Label ID="lblGvDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtfGvDistrict" runat="server" CssClass="form-control" placeholder="District" MaxLength="40"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Office">
                            <ItemTemplate>
                                <asp:Label ID="lblGvSalesOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOffice")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGvSalesOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOfficeID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlfGvSalesOffice" runat="server" CssClass="form-control"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hilly">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbGvHilly" runat="server" CssClass="form-control" Checked='<%# DataBinder.Eval(Container.DataItem, "Hilly")%>' Visible="false"></asp:CheckBox>
                               
                                <asp:Label ID="lblGvHilly" Text='<%# DataBinder.Eval(Container.DataItem, "Hilly")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:CheckBox ID="cbfGvHilly" runat="server" CssClass="form-control"></asp:CheckBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnDistrictEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDistrictEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                <asp:LinkButton ID="lnkBtnDistrictDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' OnClick="lnkBtnDistrictDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="BtnAddOrUpdateDistrict" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateDistrict_Click" Width="70px" Height="33px" />
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
