<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationDistrict.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.LocationDistrict" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <fieldset class="fieldset-border" id="fldDistrict" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label>Dealer Code</label><%--<span class="Mandatory">*</span>--%>
                    <asp:DropDownList ID="ddlDDealer" runat="server" CssClass="form-control" ></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Country</label><%--<span class="Mandatory">*</span>--%>
                    <asp:DropDownList ID="ddlDCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDCountry_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>State</label><%--<span class="Mandatory">*</span>--%>
                    <asp:DropDownList ID="ddlDState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDState_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <div class="col-md-2 col-sm-12">
                    <label>District</label><%--<span class="Mandatory">*</span>--%>
                    <asp:TextBox ID="txtDistrict" runat="server" placeholder="District" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearchDistrict" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearchDistrict_Click"></asp:Button>
                    <%--<asp:Button ID="BtnAddDistrict" runat="server" CssClass="btn Save" Text="Add" OnClick="BtnAddDistrict_Click"></asp:Button>--%>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
            <div class="col-md-12 Report">

                <span id="txnHistory3:refreshDataGroup">
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
                                    <asp:Label ID="lblGDCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country.Country")%>' runat="server"></asp:Label>
                                    <%--Visible="false"--%>
                                    <asp:Label ID="lblGDCountryID" Text='<%# DataBinder.Eval(Container.DataItem, "Country.CountryID")%>' runat="server" Visible="false"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlGDCountry" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGDCountry_SelectedIndexChanged"></asp:DropDownList>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlGDCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDCountry_SelectedIndexChanged"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                    <%--Visible="false"--%>
                                    <asp:Label ID="lblGDStateID" Text='<%# DataBinder.Eval(Container.DataItem, "State.StateID")%>' runat="server" Visible="false"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlGDState" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlGDState" runat="server" CssClass="form-control"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Office">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDSalesOffice" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOffice")%>' runat="server"></asp:Label>
                                    <%--Visible="false"--%>
                                    <asp:Label ID="lblGDSalesOfficeID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesOffice.SalesOfficeID")%>' runat="server" Visible="false"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlGDState" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlGDSalesOffice" runat="server" CssClass="form-control"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label><%-- Visible="false"--%>
                                    <asp:Label ID="lblGDDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false"></asp:Label>
                                    <%--<asp:DropDownList ID="ddlGDDealer" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlGDDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDDealer_SelectedIndexChanged"></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemTemplate>
                                    <asp:Label ID="lblGDSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesDealerEngineer.ContactName")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblGDSalesEngineerUserID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesDealerEngineer.UserID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtGDDistrict" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' Enabled="false"></asp:TextBox>--%>
                                    <asp:Label ID="lblGDDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtGDDistrict" runat="server" CssClass="form-control" placeholder="District" MaxLength="40"></asp:TextBox>
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
