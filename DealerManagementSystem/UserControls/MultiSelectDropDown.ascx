<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiSelectDropDown.ascx.cs" Inherits="DealerManagementSystem.UserControls.MultiSelectDropDown" %>
<%--<asp:UpdatePanel ID="q" runat="server">
    <ContentTemplate> --%>
<style>
    .MultiSelect {
        height: 300px;
        width: 250px;
        overflow: auto;
        position: absolute;
        z-index: 9;
        background-color: white;
    }

    .MultiSelectHide {
        background-color: red;
    }

    .MultiSelectView {
        background-color: green;
    }

    .Arrow {
        height: 15px;
        width: 15px;
        padding-top: 1px;
        float: right;
        margin-right: 6px;
        margin-top: 7px;
    }

    input[type=checkbox], input[type=radio] {
        margin-top: 3px;
        margin-bottom: 0px;
        margin-left: 0px;
    }

    tr {
        background-color: white;
        font-weight: bold;
        color: black;
    }
</style>

<table style="border: 0px">
    <tr>
        <td style="background-color: white">
            <div class="form-control" style="width: 186px; height: calc(1.5em + .75rem + 2px);">
                <asp:Label ID="lblDisplay" runat="server" Text=""></asp:Label> 
                <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Images/ArrowDown.png" CssClass="Arrow" OnClick="btnView_Click" />
            </div>
            <asp:TextBox ID="txtbox" runat="server" Enabled="false" CssClass="form-control" BackColor="white" Visible="false"></asp:TextBox></td>
        <td style="background-color: white"></td>
    </tr>
    <tr>
        <td colspan="2" style="background-color: white">

            <div class="MultiSelect" id="divMultiSelect" runat="server" visible="false">
                <asp:CheckBoxList ID="cbList" runat="server"></asp:CheckBoxList>
            </div>
        </td>
    </tr>
</table>

