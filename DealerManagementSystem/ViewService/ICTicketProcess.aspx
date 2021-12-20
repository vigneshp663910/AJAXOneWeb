<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketProcess.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ICTicketBasicInformation_N.ascx" TagPrefix="UC" TagName="UC_BasicInformation_N" %>
<%--<%@ Register Src="~/UserControls/DMS_ICTicketTechnicianAssign.ascx" TagPrefix="UC" TagName="UC_TechnicianAssign" %>--%>

<%--<%@ Register Src="~/UserControls/DMS_ICTicketServiceConfirmation.ascx" TagPrefix="UC" TagName="UC_ServiceConfirmation" %>--%>
<%@ Register Src="~/UserControls/ICTicketServiceCharges.ascx" TagPrefix="UC" TagName="DMS_ICTicketServiceCharges" %>
<%@ Register Src="~/UserControls/ICTicketTSIR.ascx" TagPrefix="UC" TagName="DMS_ICTicketTSIR" %>
<%@ Register Src="~/UserControls/ICTicketMaterialCharges.ascx" TagPrefix="UC" TagName="DMS_ICTicketMaterialCharges" %>
<%@ Register Src="~/UserControls/ICTicketNote.ascx" TagPrefix="UC" TagName="UC_ICTicketNote" %>
<%@ Register Src="~/UserControls/ICTicketTechnicianWorkInformation.ascx" TagPrefix="UC" TagName="UC_TechnicianWorkInformation" %>
<%@ Register Src="~/UserControls/ICTicketFSR.ascx" TagPrefix="UC" TagName="UC_DMS_ICTicketFSR" %>
<%@ Register Src="~/UserControls/ICTicketRestore.ascx" TagPrefix="UC" TagName="UC_DMS_ICTicketRestore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script>
        $(document).ready(function () {
            $("#DivTechnician").click(function () {
                $("#pnlTechnicianInformation").toggle(function () {

                    $(this).animate({ height: '150px', });
                });

            });
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="position: fixed; top: 35%; right: 46%" Width="100px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="container IC_ticketManageInfo">
                <div class="col2">
                    <div class="rf-p " id="txnHistory:j_idt1289">
                        <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                            <UC:UC_BasicInformation_N ID="UC_BasicInformation_N" runat="server"></UC:UC_BasicInformation_N>
                            <asp:Panel ID="pnlFSR" runat="server">
                                <UC:UC_DMS_ICTicketFSR ID="DMS_ICTicketFSR" runat="server"></UC:UC_DMS_ICTicketFSR>
                            </asp:Panel>

                            <asp:Panel ID="pnlDMS_ICTicketServiceCharges" runat="server">
                                <UC:DMS_ICTicketServiceCharges ID="DMS_ICTicketServiceCharges" runat="server"></UC:DMS_ICTicketServiceCharges>
                                <asp:Panel ID="pnlTSIR" runat="server">
                                    <UC:DMS_ICTicketTSIR ID="DMS_ICTicketTSIR" runat="server"></UC:DMS_ICTicketTSIR>
                                </asp:Panel>
                            </asp:Panel>

                            <asp:Panel ID="pnlDMS_ICTicketMaterialCharges" runat="server">
                                <UC:DMS_ICTicketMaterialCharges ID="DMS_ICTicketMaterialCharges" runat="server"></UC:DMS_ICTicketMaterialCharges>
                            </asp:Panel>
                            <asp:Panel ID="pnlServiceCenter" runat="server">
                                <asp:Label ID="lblMessageServiceCenter" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
                                <table id="txnHistorySC:panelGridid4" style="height: 100%; width: 100%">
                                    <tr>
                                        <td>
                                            <div class="boxHead">
                                                <div class="logheading">Service Center Attachments</div>
                                                <div style="float: right; padding-top: 0px">
                                                    <a href="javascript:collapseExpandAttachments();">
                                                        <img id="img1" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlAttachments2" runat="server">
                                                <div class="rf-p " id="txnHistorySC:inputFiltersPanel4">
                                                    <div class="rf-p-b " id="txnHistorySC:inputFiltersPanel_body01">
                                                        <asp:GridView ID="gvSCAttachment" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="AttachedFileID">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Attachment Description">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSCAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlSCAttachedName" runat="server" CssClass="TextBox">
                                                                            <%-- <asp:ListItem Value="0">Select</asp:ListItem>--%>
                                                                            <asp:ListItem Value="1">Annx-SER-1 Service Centre Process Chart</asp:ListItem>
                                                                            <asp:ListItem Value="2">Annx-SER-2 Service Centre flow chart for Dealer Major Sub assy  Recon sub assy</asp:ListItem>
                                                                            <asp:ListItem Value="3">Annx-SER-3 Check List site inspection Major Repair</asp:ListItem>
                                                                            <asp:ListItem Value="4">Annx-SER-4 Check List Loading at site</asp:ListItem>
                                                                            <asp:ListItem Value="5">Annx-SER-5 Check list Before unloading  at Service centre</asp:ListItem>
                                                                            <asp:ListItem Value="6">Annx-SER-6 Gate Entry Exit Record & Acknowledgement</asp:ListItem>
                                                                            <asp:ListItem Value="7">Annx-SER-7 Check list After unloading  at service centre</asp:ListItem>
                                                                            <asp:ListItem Value="8">Annx-SER-9 Cleaning Shot Blasting and Painting</asp:ListItem>
                                                                            <asp:ListItem Value="9">Annx-SER-10 Dealer Job work field and Service center</asp:ListItem>
                                                                            <asp:ListItem Value="10">Annx-SER-11 Check list Assembly process for Machines A2 & A4</asp:ListItem>
                                                                            <asp:ListItem Value="11">Annx-SER-12 Check List Final Performance test checklist A2000</asp:ListItem>
                                                                            <asp:ListItem Value="12">Annx-SER-13 Job work with SRO & Service Charges</asp:ListItem>
                                                                            <asp:ListItem Value="13">Annx-SER-14 MOM  Dealer with Customer</asp:ListItem>
                                                                            <asp:ListItem Value="14">Annx-SER-15 Machine List of Service Centre</asp:ListItem>
                                                                            <asp:ListItem Value="15">Annx-SER-16 Warranty certificate Refurbished machine</asp:ListItem>
                                                                            <asp:ListItem Value="16">Annx-SER-17 Tools List for dealer service centre</asp:ListItem>
                                                                            <asp:ListItem Value="17">Annx-SER-18 Service centre Skill Matrix Plan</asp:ListItem>
                                                                            <asp:ListItem Value="18">Annx-SER-19 HR safety Guide lines Service Centre</asp:ListItem>
                                                                            <asp:ListItem Value="19">Annx-SER-20 Service center viability study</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server" Visible="false"></asp:Label>
                                                                        <asp:UpdatePanel ID="upManageSubC1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:LinkButton ID="lnkServiceCenterAttachedFileDownload" runat="server" OnClick="lnkServiceCenterAttachedFileDownload_Click" Text="Download">   </asp:LinkButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lnkServiceCenterAttachedFileDownload" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblServiceCenterAttachedFileRemove" runat="server" OnClick="lblServiceCenterAttachedFileRemove_Click">Remove</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:LinkButton ID="lblServiceCenterAttachedFileAdd" runat="server" OnClick="lblServiceCenterAttachedFileAdd_Click">Add</asp:LinkButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lblServiceCenterAttachedFileAdd" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
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
                            </asp:Panel>
                            <asp:Panel ID="pnlDMS_ICTicketNote" runat="server">
                                <UC:UC_ICTicketNote ID="DMS_ICTicketNote" runat="server"></UC:UC_ICTicketNote>
                            </asp:Panel>
                            <asp:Panel ID="pnlAttachments" runat="server">
                                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
                                <table id="txnHistory2:panelGridid4" style="height: 100%; width: 100%">
                                    <tr>
                                        <td>
                                            <div class="boxHead">
                                                <div class="logheading">Attachments</div>
                                                <div style="float: right; padding-top: 0px">
                                                    <a href="javascript:collapseExpandAttachments();">
                                                        <img id="imgAttachments" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlAttachments1" runat="server">
                                                <div class="rf-p " id="txnHistory1:inputFiltersPanel4">
                                                    <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body01">
                                                        <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="false" DataKeyNames="AttachedFileID">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>

                                                                        <asp:UpdatePanel ID="upManage" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkServiceCenterAttachedFileDownload_Click" Text="Download"> </asp:LinkButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lblServiceCenterAttachedFileRemove_Click">Remove</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:LinkButton ID="lblAttachedFileAdd" runat="server" OnClick="lnkServiceCenterAttachedFileDownload_Click">Add</asp:LinkButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lblAttachedFileAdd" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
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
                            </asp:Panel>
                            <asp:Panel ID="pnlUC_TechnicianWorkInformation" runat="server">
                                <UC:UC_TechnicianWorkInformation ID="UC_TechnicianWorkInformation" runat="server"></UC:UC_TechnicianWorkInformation>
                            </asp:Panel>
                            <asp:Panel ID="pnlICTicketRestore" runat="server">
                                <UC:UC_DMS_ICTicketRestore ID="UC_DMS_ICTicketRestore" runat="server"></UC:UC_DMS_ICTicketRestore>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function collapseExpandAttachments(obj) {
            var gvObject = document.getElementById("MainContent_pnlAttachments");
            var imageID = document.getElementById("MainContent_imgAttachments");

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
    <script type="text/javascript">
        $(document).ready(function () {


            var gvAttachedFile = document.getElementById('MainContent_gvAttachedFile');

            if (gvAttachedFile != null) {
                for (var i = 0; i < gvAttachedFile.rows.length - 1; i++) {
                    var lblFileName = document.getElementById('MainContent_gvAttachedFile_lblFileName_' + i);
                    if (lblFileName != null) {
                        if (lblFileName.innerHTML == "") {
                            lblFileName.parentNode.parentNode.parentNode.style.display = "none";
                        }
                    }
                }
            }
        });
    </script>
</asp:Content>
