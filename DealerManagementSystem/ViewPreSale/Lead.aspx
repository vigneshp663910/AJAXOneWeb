<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Lead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        * {
            box-sizing: border-box
        }

        /* Set height of body and the document to 100% */
        body, html {
            height: 100%;
            margin: 0;
            font-family: Arial;
        }

        /* Style tab links */
        .tablink {
            background-color: #555;
            color: white;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 16px;
            font-size: 17px;
            width: 25%;
        }

            .tablink:hover {
                background-color: #777;
            }

        /* Style the tab content (and add height:100% for full page content) */
        .tabcontent {
            color: white;
            display: none;
            padding: 100px 20px;
            height: 100%;
        }

        #Home {
            background-color: red;
        }

        #News {
            background-color: green;
        }

        #Contact {
            background-color: blue;
        }

        #About {
            background-color: orange;
        }
    </style>
    <script>
        

        // Get the element with id="defaultOpen" and click on it
      /*  document.getElementById("defaultOpen").click();*/
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox" DataTextField="Category" DataValueField="CategoryID" />
    <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="TextBox" DataTextField="ProgressStatus" DataValueField="ProgressStatusID" />
    <asp:DropDownList ID="ddlQualification" runat="server" CssClass="TextBox" DataTextField="Qualification" DataValueField="QualificationID" />
    <asp:DropDownList ID="ddlSource" runat="server" CssClass="TextBox" DataTextField="Source" DataValueField="SourceID" />
    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox" DataTextField="Status" DataValueField="StatusID" />
    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" BorderColor="Silver"></asp:TextBox>
    <asp:TextBox ID="txtCustomer" runat="server" CssClass="TextBox" BorderColor="Silver"></asp:TextBox>
    <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox" BorderColor="Silver"></asp:TextBox>
    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />


     
    
    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="TextBox" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
    <asp:DropDownList ID="ddlState" runat="server" CssClass="TextBox" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="TextBox" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
    <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="TextBox" DataTextField="Tehsil" DataValueField="TehsilID" />  
</asp:Content>
