<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="VisitReport.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.VisitReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <asp:TabContainer ID="tabConMaterial" runat="server" ToolTip="Material" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
        <asp:TabPanel ID="tabbPnlDivision" runat="server" HeaderText="Daily Visit Count Report" Font-Bold="True" ToolTip="Division...">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Dealer</label>
                                    <asp:DropDownList ID="ddlDealerDay" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerDay_SelectedIndexChanged" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Dealer Employee</label>
                                    <asp:DropDownList ID="ddlDealerEmployeeDay" runat="server" CssClass="form-control" />
                                </div>
                              
                                 <div class="col-md-2 col-sm-12">
                        <label class="modal-label">From Date</label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">To Date</label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSearchDay" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearchDay_Click"></asp:Button>
                                    <asp:Button ID="btnExportExcelDay" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelDay_Click" Width="100px" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <div class="boxHead">
                                        <div class="logheading">
                                            <div style="float: left">
                                                <table>
                                                    <tr>
                                                        <td>Visit Report(s):</td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" CssClass="label"></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVisitArrowLeft_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVisitArrowRight_Click" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvVisitReport_PageIndexChanging">

                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tabPnlModel" runat="server" HeaderText="Monthly Visit Count Report" Font-Bold="True" ToolTip="Model...">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Dealer</label>
                                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Dealer Employee</label>
                                    <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Year</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Month</label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                                </div>
                                <%-- <div class="col-md-2 col-sm-12">
                        <label class="modal-label">From Date</label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">To Date</label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>--%>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <div class="boxHead">
                                        <div class="logheading">
                                            <div style="float: left">
                                                <table>
                                                    <tr>
                                                        <td>Visit Report(s):</td>
                                                        <td>
                                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnVisitArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVisitArrowLeft_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnVisitArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVisitArrowRight_Click" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:GridView ID="gvVisitReport" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvVisitReport_PageIndexChanging">

                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>


</asp:Content>
