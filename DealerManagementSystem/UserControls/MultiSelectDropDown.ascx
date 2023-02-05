<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="MultiSelectDropDown.ascx.cs" Inherits="DealerManagementSystem.UserControls.MultiSelectDropDown" %>
<%--<asp:UpdatePanel ID="q" runat="server">
    <ContentTemplate> --%>
<style>
    .MultiSelect
    {
        height:300px; width:250px; overflow: auto; position:absolute; z-index: 9;
        background-color: #d8d8d8;
    }
     .MultiSelectHide
    {
        background-color:red;
    }   
     .MultiSelectView
    {
         background-color:green;
    }  
</style>
 
<table style="border:0px">
    <tr>
        <td style="background-color:white">
            <asp:TextBox ID="txtbox" runat="server" Enabled="false" CssClass="form-control" BackColor="white"></asp:TextBox></td>
        <td style="background-color:white"> 
            <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Images/ArrowDown.png" Width="20px" Height="20px"  OnClick="btnView_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="background-color:white">

            <div class="MultiSelect" id="divMultiSelect" runat="server" visible="false">
                <asp:CheckBoxList ID="cbList" runat="server"></asp:CheckBoxList>
            </div>
        </td>
    </tr>
</table>

