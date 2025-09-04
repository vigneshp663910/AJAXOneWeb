<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ClaimReport.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ClaimReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlDealerSearch">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlActivityType">Activity Type</label>
                        <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                            <asp:ListItem Text="Field Activity" Value="FA"></asp:ListItem>
                            <asp:ListItem Text="Invoice Activity" Value="IA"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtFromDate">From Date</label>
                        <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="calFromDateSearch" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtToDateSearch">To Date</label>
                        <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="calToDateSearch" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="Search" runat="server" Text="Search" CssClass="btn Search" OnClick="Search_Click" />
                        <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn Back" OnClick="btnExportExcel_Click" Width="120px" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Claim Report</legend>
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Claim : </td>
                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" ToolTip="Excel Download..." Width="23" Height="23" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvData" CssClass="table table-bordered table-condensed Grid" runat="server" AllowPaging="true" PageSize="20"
                        ShowHeaderWhenEmpty="true"   AutoGenerateColumns="false"  
                       >
                        <Columns>
                            <asp:BoundField HeaderText="Activity No." DataField="Activity No.">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Dealer Code" DataField="DealerCode">
                                <ItemStyle  HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Dealer Name" DataField="Dealer Name">
                                <ItemStyle />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Activity" DataField="Activity"> 
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Activity Code" DataField="Activity Code">
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Functional Area" DataField="Functional Area">
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Location" DataField="Location">
                                <ItemStyle  HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="No.of Units" DataField="No. of Units">
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Event From" DataField="Event From">
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Event To" DataField="Event To">
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Total Expense" DataField="Total Expense">
                                <ItemStyle   HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ajax Exp. Sharing" DataField="Ajax Exp. Sharing">
                                <ItemStyle  HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ajax Approved Amt." DataField="Ajax Approved Amt.">
                                <ItemStyle   HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Invoice No." DataField="Invoice No.">
                                <ItemStyle  HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Invoice Date" DataField="Invoice Date">
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Taxable Amount" DataField="Taxable Amount">
                                <ItemStyle  HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="GST" DataField="GST">
                                <ItemStyle  HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Invoice Total" DataField="Total Amount">
                                <ItemStyle   HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="SAP Doc">
                                <ItemStyle   HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSAPDoc" runat="server" Text='<%# Bind("[SAP Doc]") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AE Inv. Accounted Date">
                                <ItemStyle  HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAEAccDate" runat="server" Text='<%# Bind("[AE Inv Accounted Date]") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Voucher. No.">
                                <ItemStyle   HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentVoucher" runat="server" Text='<%# Bind("[Payment Voucher No]") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Bind("[Payment Date]") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentValue" runat="server" Text='<%# Bind("[Payment Value]") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TDS">
                                <ItemTemplate>
                                    <asp:Label ID="lblTDS" runat="server" Text='<%# Bind("TDS") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Claim Release">
                                <ItemTemplate>
                                    <asp:Label ID="lblTDS" runat="server" Text='<%# Bind("[Claim Release]") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Level 1 Approved Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblLevel1ApprovedDate" runat="server" Text='<%# Bind("[Level 1 Approved Date]") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Level 2 Approved Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblLevel2ApprovedDate" runat="server" Text='<%# Bind("[Level 2 Approved Date]") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle   HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
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
