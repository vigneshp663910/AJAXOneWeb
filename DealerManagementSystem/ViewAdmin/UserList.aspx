<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.UserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
        <asp:Panel ID="pnlUser" runat="server">
            <div>
                <div class="diveField">
                    <div class="diveFieldLabel">
                        <asp:Label ID="lblEmp" runat="server" Text="User ID" CssClass="label"></asp:Label>
                    </div>
                    <div class="diveFieldText">
                        <asp:TextBox ID="txtEmp" runat="server" CssClass="TextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="diveField">
                    <div class="diveFieldLabel">
                        <asp:Label ID="Label2" runat="server" Text="Contact Name" CssClass="label"></asp:Label>
                    </div>
                    <div class="diveFieldText">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="TextBox"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" />
            </div>
            <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false"
                    CssClass="TableView" BorderStyle="None" AllowPaging="true" OnPageIndexChanging="gvEmployee_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="User Id">
                            <ItemStyle BorderStyle="None" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbUserID" runat="server" OnClick="lbEmpId_Click">
                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' Visible="false" />
                                    <asp:TextBox ID="txtVendor" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:TextBox>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtEmployeeNameBy" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField> 
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
