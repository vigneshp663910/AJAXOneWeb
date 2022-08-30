<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTsirAttachments.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.AddTsirAttachments" %>

 
<table id="txnHistory2:panelGridid4" style="height: 100%; width: 100%">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">FSR Attachments</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandFSRAttachments();">
                        <img id="imgFSRAttachments" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlFSRAttachments" runat="server">
                <div class="rf-p " id="txnHistory1:inputFiltersPanel4">
                    <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body01">
                        <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true"  >
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">TSIR</label>
            <asp:DropDownList ID="ddlTSIR" runat="server" CssClass="form-control"   DataValueField="ServiceChargeID" DataTextField="MaterialCode" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">FSR Attached Name</label>
            <asp:DropDownList ID="ddlFSRAttachedName" runat="server" CssClass="TextBox" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">File</label>
            <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
        </div>
        <div class="col-md-12 col-sm-12">

            <asp:LinkButton ID="lblAttachedFileAdd" runat="server"  >Add</asp:LinkButton>

        </div>
    </div>
</fieldset>

