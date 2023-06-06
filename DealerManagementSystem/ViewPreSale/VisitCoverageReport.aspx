<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="VisitCoverageReport.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.VisitCoverageReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Country</label>
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Region</label>
                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <asp1:TabContainer ID="tbpCust" runat="server"  Font-Bold="True" Font-Size="Medium">
                <asp1:TabPanel ID="tpnlSalesEngineer" runat="server" HeaderText="Over All" Font-Bold="True" ToolTip="">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="col-md-12 Report">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">List OverAll</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Pre Sales Report(s):</td>
                                                            <td>
                                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnLeadArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeadArrowLeft_Click" /></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnLeadArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnLeadArrowRight_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvAll" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

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
                <asp1:TabPanel ID="TabVisit" runat="server" HeaderText="Region" Font-Bold="True" ToolTip="">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="col-md-12 Report">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Region List</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Pre Sales Report(s):</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvRegion" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

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
                <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="State" Font-Bold="True" ToolTip="">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="col-md-12 Report">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">State List</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Pre Sales Report(s):</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvState" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

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
                <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="Dealer" Font-Bold="True" ToolTip="">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="col-md-12 Report">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer List </legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Pre Sales Report(s):</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvDealer" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

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
                <asp1:TabPanel ID="TabPanel3" runat="server" HeaderText="Sales Engineer" Font-Bold="True" ToolTip="">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="col-md-12 Report">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">List Engg</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Pre Sales Report(s):</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvEngg" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

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
                <asp1:TabPanel ID="TabPanel4" runat="server" HeaderText="Details" Font-Bold="True" ToolTip="">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="col-md-12 Report">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">List Engg</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Pre Sales Report(s):</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvDetails" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">

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
            </asp1:TabContainer>
        </div>
    </div>
</asp:Content>
