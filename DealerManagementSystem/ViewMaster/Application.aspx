<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Application.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Application</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <label>MainModule</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlMainModule" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-7"></div>
                    <div class="col-md-3 text-right">
                        <label>SubModule</label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtSubModule" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-7">
                        <div class="col-md-2 text-right ">
                            <asp:Button ID="btnRetrieve" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnRetrieve_Click"></asp:Button>
                        </div>
                        <div class="col-md-2 text-right ">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn Save" Text="Add" OnClick="btnAdd_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
