<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketNote.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketNote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
<table id="txnHistory1:panelGridid6" style="height: 100%; width: 100%" class="IC_materialInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">Notes Information</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandNotes();">
                        <img id="imgNotes" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlNotes" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel6">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body6">

                        <asp:GridView ID="gvNotes" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="ServiceNoteID" OnRowDataBound="gvNotes_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Note Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoteType" Text='<%# DataBinder.Eval(Container.DataItem, "NoteType.NoteType")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlNoteType" runat="server" CssClass="TextBox" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblComments" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtComments" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblNoteRemove" runat="server" OnClick="lblNoteRemove_Click">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lblNoteAdd" runat="server" OnClick="lblNoteAdd_Click">Add</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="footer" />
                        </asp:GridView>

                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>


<script type="text/javascript">
    function collapseExpandNotes(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketNote_pnlNotes");
        var imageID = document.getElementById("MainContent_DMS_ICTicketNote_imgNotes");

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }

</script>
<style>
    .footer {
        height: 15px;
        width: 100%;
    }

        .footer td {
            border: none;
        }

        .footer th {
            border: none;
        }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        var gvTickets = document.getElementById('MainContent_DMS_ICTicketNote_gvNotes');

        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblNoteType = document.getElementById('MainContent_DMS_ICTicketNote_gvNotes_lblNoteType_' + i);
                if (lblNoteType != null) {
                    if (lblNoteType.innerHTML == "") {
                        lblNoteType.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
    });
</script>
