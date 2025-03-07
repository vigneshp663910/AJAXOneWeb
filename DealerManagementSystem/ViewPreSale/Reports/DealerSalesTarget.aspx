<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerSalesTarget.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.DealerSalesTarget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Material Upload</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <asp:Label ID="lblMessageMaterialUpload" runat="server" Text="" CssClass="message" />
        <fieldset class="fieldset-border">
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Upload Material</label>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnUploadTarget" runat="server" Text="Add" CssClass="btn Save" OnClick="btnUploadTarget_Click" />
                </div>
            </div>
        </fieldset>
</asp:Content>
