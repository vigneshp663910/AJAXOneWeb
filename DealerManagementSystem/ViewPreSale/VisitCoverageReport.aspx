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
                            <label class="modal-label">Lead Date From</label>
                            <asp:TextBox ID="txtLeadDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Lead Date To</label>
                            <asp:TextBox ID="txtLeadDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Visit Date From</label>
                            <asp:TextBox ID="txtVisitDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Visit Date To</label>
                            <asp:TextBox ID="txtVisitDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <%--<asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />--%>
                        </div>
                    </div>
                </fieldset>
            </div>
            <asp1:TabContainer ID="tbpCust" runat="server" Font-Bold="True" Font-Size="Medium">
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
                                                            <td>Over All Report(s):</td>
                                                            <td><asp:Button ID="btnExcelOverAll" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelOverAll_Click" Width="120px" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvAll" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">

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
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Region</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Region Report(s):</td>
                                                            <td><asp:Button ID="btnExcelRegion" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelRegion_Click" Width="120px" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvRegion" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">

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
                                    <legend style="background: none; color: #007bff; font-size: 17px;">State</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>State Report(s):</td>
                                                            <td>
                                                                <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                                                            </td>
                                                            <td><asp:Button ID="btnExcelState" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelState_Click" Width="120px" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="gvState" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="15" AllowPaging="true" OnPageIndexChanging="gvState_PageIndexChanging">

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
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer </legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Dealer Report(s):</td>
                                                            <td>
                                                                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                                                            </td>
                                                            <td><asp:Button ID="btnExcelDealer" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelDealer_Click" Width="120px" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvDealer" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealer_PageIndexChanging">

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
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Engineer</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Engineer Report(s):</td>
                                                            <td>
                                                                <asp:Label ID="lblEngineer" runat="server" CssClass="label"></asp:Label>
                                                            </td>
                                                            <td><asp:Button ID="btnExcelEngineer" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelEngineer_Click" Width="120px" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvEngg" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvEngg_PageIndexChanging">

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
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Details</legend>
                                    <div class="col-md-12 Report">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Details Report(s):</td>
                                                            <td>
                                                                <asp:Label ID="lblDetails" runat="server" CssClass="label"></asp:Label>
                                                            </td>
                                                            <td><asp:Button ID="btnExcelDetails" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelDetails_Click" Width="120px" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="gvDetails" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                             
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
