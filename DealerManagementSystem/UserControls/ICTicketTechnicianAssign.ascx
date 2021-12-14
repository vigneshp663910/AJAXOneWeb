<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTechnicianAssign.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketTechnicianAssign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
<table id="txnHistory1:panelGridid3" style="height: 100%; width: 100%" class="IC_materialInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">Technician Information</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandTechnicianInformation();">
                        <img id="imgTechnicianInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlTechnicianInformation" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel3">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body3">
                        <asp:GridView ID="gvTechnician" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" OnRowDataBound="gvTechnician_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Technician">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="gvddlTechnician" runat="server" CssClass="TextBox" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Technician Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbTechnicianAdd" runat="server" OnClick="lbTechnicianAdd_Click">Add</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbTechnicianRemove" runat="server" OnClick="lbTechnicianRemove_Click">Remove</asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>


<script type="text/javascript">

    function collapseExpandTechnicianInformation(obj) {
        var gvObject = document.getElementById("MainContent_UC_TechnicianAssign_pnlTechnicianInformation");
        var imageID = document.getElementById("MainContent_UC_TechnicianAssign_imgTechnicianInformation");

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }

    $(document).ready(function () {
        var gvTickets = document.getElementById('MainContent_UC_TechnicianAssign_gvTechnician');

        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblItem = document.getElementById('MainContent_UC_TechnicianAssign_gvTechnician_lblUserName_' + i);
                if (lblItem != null) {
                    if (lblItem.innerHTML == "") {
                        lblItem.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
    });
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
