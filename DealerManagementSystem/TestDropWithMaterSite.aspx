<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TestDropWithMaterSite.aspx.cs" Inherits="DealerManagementSystem.TestDropWithMaterSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
  


     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script> 
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
     <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
                <asp:ListItem Text="Mango" Value="1" />
                <asp:ListItem Text="Apple" Value="2" />
                <asp:ListItem Text="Banana" Value="3" />
                <asp:ListItem Text="Guava" Value="4" />
                <asp:ListItem Text="Orange" Value="5" />
            </asp:ListBox>
   
</asp:Content>
