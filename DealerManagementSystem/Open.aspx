<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open.aspx.cs" Inherits="DealerManagementSystem.Open" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 
<body>

    <form id="form1" runat="server">
       
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="API Test"></asp:Label></td>
                <td>
                    <asp:Button ID="btnAPITest" runat="server" Text="API Test" OnClick="btnAPITest_Click" />
                </td>
                <td></td>
                

                <td>
                    <asp:Label ID="lblAPITest" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSMS" runat="server" Text="Update SMS" OnClick="btnSMS_Click" />
                </td>
            </tr>
            <tr>
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
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Customer Code"></asp:Label>
                    <asp:TextBox ID="txtCustomerCodeMiss" runat="server"></asp:TextBox>
                    <asp:Button ID="btnCustomerMiss" runat="server" Text="Customer Miss" OnClick="btnCustomerMiss_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Button ID="btnDealerAddress" runat="server" Text="Dealer Address from SAP" OnClick="btnDealerAddress_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Button ID="btnIntegrationWarrantyClaimAnnexureToSAP" runat="server" Text="Integration Warranty Claim Annexure To SAP" OnClick="btnIntegrationWarrantyClaimAnnexureToSAP_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Sales Quotation Flow From Sap" OnClick="Button1_Click" />
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Button ID="btnMttrEscalation" runat="server" Text="Mttr Escalation" OnClick="btnMttrEscalation_Click" />
                </td>
            </tr>
              <tr>
                <td>
                    <asp:Button ID="btnSqlJob" runat="server" Text="Sql Job" OnClick="btnSqlJob_Click" />
                </td>
            </tr>
        </table>
        <div id="chart" style="width: 100%; height: 500px;"></div>
    </form>
</body>
</html>
