<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTechnicianWorkInformation.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketTechnicianWorkInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%"  Font-Bold="true" Font-Size="24px"/>
<table id="txnHistory2:panelGridid3" style="height: 100%; width: 100%" class="IC_materialInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">Technician Work Hours Information</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandTechnicianWorkHoursInformation();">
                        <img id="imgTechnicianWorkHoursInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlTechnicianWorkHoursInformation" runat="server">
                <div class="rf-p " id="txnHistory2:inputFiltersPanel3">
                    <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body">
                        <asp:GridView ID="gvTechnicianWorkDays" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" OnRowDataBound="gvTechnicianWorkDays_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Technician">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName_ContactName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName_ContactName")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblServiceTechnicianWorkDateID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceTechnicianWorkDateID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="gvddlTechnician" runat="server" CssClass="TextBox" OnSelectedIndexChanged="gvddlTechnician_SelectedIndexChanged" AutoPostBack="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Worked Day">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkedDay" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtWorkedDate" runat="server" CssClass="TextBox" onkeyup="return removeText('MainContent_gvServiceCharges_txtServiceDate');"></asp:TextBox>
                                        <asp:CalendarExtender ID="ceWorkedDate" runat="server" TargetControlID="txtWorkedDate" PopupButtonID="txtWorkedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtWorkedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Worked Hours">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtWorkedHours" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbWorkedDayRemove" runat="server" OnClick="lbWorkedDayRemove_Click">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbWorkedDayAdd" runat="server" OnClick="lbWorkedDayAdd_Click">Add</asp:LinkButton>
                                    </FooterTemplate>
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
    function collapseExpandTechnicianWorkHoursInformation(obj) {
        var gvObject = document.getElementById("MainContent_UC_TechnicianWorkInformation_pnlTechnicianWorkHoursInformation");
        var imageID = document.getElementById("MainContent_UC_TechnicianWorkInformation_imgTechnicianWorkHoursInformation");

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
        var gvTickets = document.getElementById('MainContent_UC_TechnicianWorkInformation_gvTechnicianWorkDays');

        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblItem = document.getElementById('MainContent_UC_TechnicianWorkInformation_gvTechnicianWorkDays_lblUserName_ContactName_' + i);
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
