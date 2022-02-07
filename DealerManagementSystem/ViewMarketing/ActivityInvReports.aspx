<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ActivityInvReports.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityInvReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS_Scripts.js"></script>
    <script type="text/javascript">        
        $(document).ready(function () {
            document.getElementById('divData').style.width = screen.availWidth;
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
            <h4 style="width: 100%; padding-right: 10px; vertical-align: middle">Activity Invoice Report<img style="float: right; cursor: pointer" id="imgdivEntry" src="../Images/grid_collapse.png"
                onclick="ShowHide(this,'divEntry')" /></h4>
        </div>
        <div id="divSearch">
            <div class="row">
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                    <label for="ddlDealerSearch">Dealer</label>
                    <asp:DropDownList ID="ddlDealerSearch" runat="server"></asp:DropDownList>
                </div>
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                    <label for="ddlFunctionalArea">Activity Type</label>
                    <asp:DropDownList ID="ddlActivityType" runat="server">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Field Activity" Value="FA"></asp:ListItem>
                        <asp:ListItem Text="Invoice Activity" Value="IA"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                    <label for="txtFromDateSearch">From Date</label>
                    <asp:TextBox runat="server" ID="txtFromDateSearch"></asp:TextBox>
                    <cc1:CalendarExtender ID="calFromDateSearch" runat="server" TargetControlID="txtFromDateSearch" Format="dd-MMM-yyyy"></cc1:CalendarExtender>

                </div>
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                    <label for="txtToDateSearch">To Date</label>
                    <asp:TextBox runat="server" ID="txtToDateSearch"></asp:TextBox>
                    <cc1:CalendarExtender ID="calToDateSearch" runat="server" TargetControlID="txtToDateSearch" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                </div>
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                    <label for="txtToDateSearch">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="95%">
                        <asp:ListItem Text="Invoiced" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Inv. Accounted Not Paid" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Paid" Value="2"></asp:ListItem>
                        <asp:ListItem Text="All" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-6 col-lg-6 col-md-4 col-sm-12 col-12" style="text-align: right; vertical-align: bottom; padding-top: 8px;">
                </div>
                <div class="col-xl-1 col-lg-1 col-md-2 col-sm-3 col-12" style="text-align: right; vertical-align: bottom; padding-top: 8px;">
                    <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />

                </div>
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 8px;">
                    <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_Click" />

                </div>
                <div class="col-xl-1 col-lg-1 col-md-2 col-sm-3 col-12" style="text-align: right; vertical-align: bottom; padding-top: 8px;">
                    <asp:Button ID="btnSapData" runat="server" Text="Data for SAP" OnClick="btnSapData_Click" />

                </div>
            </div>
            <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                <h6>Detail</h6>
            </div>
            <div id="divData" style="overflow: scroll">
                <asp:GridView ID="gvData" CssClass="gridclass" runat="server"
                    AllowPaging="true" PageSize="20"
                    ShowHeaderWhenEmpty="true" OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="false" Width="130%"
                    OnPageIndexChanging="gvData_PageIndexChanging" OnPageIndexChanged="gvData_PageIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Activity No." DataField="Activity No.">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Dealer Code" DataField="DealerCode">
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Dealer Name" DataField="Dealer Name">
                            <ItemStyle Width="5%" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="Activity" DataField="Activity">
                            <ItemStyle Width="5%" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Activity Code" DataField="Activity Code">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Functional Area" DataField="Functional Area">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Location" DataField="Location">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="No.of Units" DataField="No. of Units">
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Event From" DataField="Event From">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Event To" DataField="Event To">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Expense" DataField="Total Expense">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ajax Exp. Sharing" DataField="Ajax Exp. Sharing">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ajax Approved Amt." DataField="Ajax Approved Amt.">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Invoice No." DataField="Invoice No.">

                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Invoice Date" DataField="InvoiceDate">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Taxable Amount" DataField="TaxableAmount">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="GST" DataField="GST">
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Invoice Total" DataField="TotalAmount">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                            <HeaderStyle />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="SAP Doc">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblSAPDoc" runat="server" Text='<%# Bind("SAPDoc") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AE Inv. Accounted Date">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblAEAccDate" runat="server" Text='<%# Eval("Inv_Accounted_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Voucher. No.">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentVoucher" runat="server" Text='<%# Bind("Payment_Voucher_No") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment Date">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Bind("Payment_Date") %>'></asp:Label>

                            </ItemTemplate>
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Value">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentValue" runat="server" Text='<%# Bind("Payment_Value") %>'></asp:Label>

                            </ItemTemplate>
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                            <HeaderStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TDS">
                            <ItemTemplate>
                                <asp:Label ID="lblTDS" runat="server" Text='<%# Bind("TDS") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:ImageButton ID="lnkEdit" Width="40px" ImageUrl="~/FileFormat/Pdf_Icon.jpg" OnClick="lnkEdit_Click" runat="server" Text="Download"></asp:ImageButton>
                                <asp:HiddenField ID="hdnInvID" runat="server" Value='<%# Bind("AIH_PkHdrID") %>' />
                                <asp:HiddenField ID="hdnActualID" runat="server" Value='<%# Bind("PKActualID") %>' />
                                <asp:HiddenField ID="hdnInvNo" runat="server" Value='<%# Bind("AIH_InvoiceNo") %>' />
                                <asp:HiddenField ID="hdnTaxableAmount" runat="server" Value='<%# Bind("TaxableAmount") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
