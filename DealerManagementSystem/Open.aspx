<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open.aspx.cs" Inherits="DealerManagementSystem.Open" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://www.google.com/jsapi"></script>
    
    <script type="text/javascript"> 
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart1);
        function drawChart1() {
            var country = $("#DrpMonth option:selected").val();
            var options = { title: country + ' Distribution', is3D: false };
            $.ajax({
                type: "POST",
                url: '/Open/GetChartData',
                data: "{country: '" + country + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var data1 = google.visualization.arrayToDataTable(data.d);
                    var chart = new google.visualization.AreaChart($("#chart")[0]);
                    chart.draw(data1, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="true">
            <asp:ListItem Text="Finland" Value="Finland" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Brazil" Value="Brazil"></asp:ListItem>
            <asp:ListItem Text="USA" Value="USA"></asp:ListItem>
            <asp:ListItem Text="Italy" Value="Italy"></asp:ListItem>
            <asp:ListItem Text="Germany" Value="Germany"></asp:ListItem>
        </asp:DropDownList>


        
        <table>

            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="API Test"></asp:Label></td>
                <td>
                    <asp:Button ID="btnAPITest" runat="server" Text="API Test" OnClick="btnAPITest_Click" />
                </td>
                <td>
                    <asp:Button ID="BtnMaterial" runat="server" Text="Material" OnClick="BtnMaterial_Click" />
                </td>
                <td>
                    <asp:Button ID="BtnMaterialSupersede" runat="server" Text="MaterialSupersede" OnClick="BtnMaterialSupersede_Click" />
                </td>


                <td>
                    <asp:Label ID="lblAPITest" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:TextBox ID="txtCustomerId" runat="server"></asp:TextBox>
                    <asp:Button ID="BtnCreateCustomer" runat="server" Text="Create Customer" OnClick="BtnCreateCustomer_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:TextBox ID="txtQuotationID" runat="server"></asp:TextBox>
                    <asp:Button ID="BtnCreateQuotation" runat="server" Text="Create Quotation" OnClick="BtnCreateQuotation_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label34" runat="server" Text="Enquiry Indiamart"></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnEnquiryIndiamart" runat="server" Text="Enquiry Indiamart" OnClick="btnEnquiryIndiamart_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Enquiry Indiamart"></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnUpdateAddressFromSapToSql" runat="server" Text="Update Address From SapToSql" OnClick="btnUpdateAddressFromSapToSql_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="SMS"></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnSMS" runat="server" Text="Update SMS" OnClick="btnSMS_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Mail"></asp:Label>
                </td>

                <td>
                    <asp:Button ID="btnMail" runat="server" Text="Update Mail" OnClick="btnMail_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnSalesQuotationDetails" runat="server" Text="SalesQuotationDocumentDetails" OnClick="BtnSalesQuotationDetails_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp1:CalendarExtender ID="calendarextender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromDate" WatermarkText="From" />
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtToDate" WatermarkText="To" />
                </td>

                <td>
                    <asp:TextBox ID="txtEnquiryNo" runat="server" PlaceHolder="EnquiryNo"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="txtDelaerCode" runat="server" PlaceHolder="DealerCode"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="txtCustomerCode" runat="server" PlaceHolder="CustomerCode"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnEnquiryDetails" runat="server" Text="EnquiryDetails" OnClick="BtnEnquiryDetails_Click" />
                </td>
            </tr>

              <tr>
                <td>
                    <br />
                     <asp:Label ID="Label5" runat="server" Text="Customer Code"></asp:Label>
                    <asp:TextBox ID="txtCustomerCodeMiss" runat="server"></asp:TextBox>
                    <asp:Button ID="btnCustomerMiss" runat="server" Text="Customer Miss" OnClick="btnCustomerMiss_Click" />
                </td>
            </tr>
            
              <tr>
                <td>
                    <br />
                     <asp:Label ID="Label6" runat="server" Text="API E Invoice"></asp:Label> 
                    <asp:Button ID="btnAPIEInvoice" runat="server" Text="API E Invoice" OnClick="btnAPIEInvoice_Click" />
                </td>
            </tr>
        </table>

        <div id="chart" style="width: 100%; height: 500px;"></div>
    </form>
</body>
</html>
