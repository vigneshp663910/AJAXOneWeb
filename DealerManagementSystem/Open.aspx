<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open.aspx.cs" Inherits="DealerManagementSystem.Open" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <table>

                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="API Test"></asp:Label></td>
                    <td>
                        <asp:Button ID="btnAPITest" runat="server" Text="API Test" OnClick="btnAPITest_Click" />
                    </td>

                    <td>
                        <asp:Label ID="lblAPITest" runat="server"></asp:Label></td>
                </tr>
              </table>
    </form>
</body>
</html>
