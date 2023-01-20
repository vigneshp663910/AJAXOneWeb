<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiSelectDropDown.ascx.cs" Inherits="DealerManagementSystem.UserControls.MultiSelectDropDown" %>
<%--<asp:UpdatePanel ID="q" runat="server">
    <ContentTemplate> --%>

        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtbox" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="Button" OnClick="btnView_Click" /></td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="cbList" runat="server" OnSelectedIndexChanged="cbList_SelectedIndexChanged"></asp:CheckBoxList></td>
            </tr>
        </table> 
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
