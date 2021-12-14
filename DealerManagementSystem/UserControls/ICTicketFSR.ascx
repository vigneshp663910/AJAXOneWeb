<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketFSR.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketFSR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/AvailabilityOfOtherMachine.ascx" TagPrefix="UC" TagName="UC_AvailabilityOfOtherMachine" %>


<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<%--<script type="text/javascript">
    function UploadFile(fileUpload) {
        if (fileUpload.value != '') {
            document.getElementById("<%=btnUpload.ClientID %>").click();
        }
    }
</script>--%>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<asp:HiddenField ID="hfFSR" runat="server" Value="X"></asp:HiddenField>
<asp:HiddenField ID="hfFSRAttachments" runat="server" Value="X"></asp:HiddenField>
<asp:HiddenField ID="hfFSRAvailabilityOfOtherMachine" runat="server" Value="X"></asp:HiddenField>
<table id="txnHistory5:panelGridid2" style="height: 100%; width: 100%" class="IC_basicInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">FSR</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandFSR();">
                        <img id="imgFSR" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlFSR" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel2">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body2">
                        <table class="labeltxt fullWidth">
                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="lblModeOfPayment" runat="server" CssClass="label" Text="Mode Of Payment"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="TextBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Operator Name"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtOperatorName" runat="server" CssClass="input"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label56" runat="server" CssClass="label" Text="Operator Contact No"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtOperatorNumber" runat="server" CssClass="input"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label48" runat="server" CssClass="label" Text="Machine Maintenance Level"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:DropDownList ID="ddlMachineMaintenanceLevel" runat="server" CssClass="TextBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label49" runat="server" CssClass="label" Text="Is Rental"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:CheckBox ID="cbIsRental" runat="server" />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label51" runat="server" CssClass="label" Text="Rental Contractor Name"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtRentalName" runat="server" CssClass="input"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label54" runat="server" CssClass="label" Text="Rental Contractor Contact No"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtRentalNumber" runat="server" CssClass="input"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label60" runat="server" CssClass="label" Text="Nature Of Complaint"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtNatureOfComplaint" runat="server" CssClass="TextBox" Width="700px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label57" runat="server" CssClass="label" Text="Observation"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtObservation" runat="server" CssClass="input" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label58" runat="server" CssClass="label" Text="Work Carried Out"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtWorkCarriedOut" runat="server" CssClass="input" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <%-- <tr >
                                <td colspan="3">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left"> 
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Here Only Part Number should be Added"></asp:Label>

                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtSERecommendedParts" runat="server" CssClass="input" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="3">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label16" runat="server" CssClass="label" Text="SE Suggestion"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtReport" runat="server" CssClass="input" TextMode="MultiLine" Width="700px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                          <%--  <tr style="display: none">
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label27" runat="server" CssClass="label" Text="Before Machine Restore"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:FileUpload ID="fuMachineBefore" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" Style="display: none" />
                                            <asp:Label ID="lblMachineBefore" runat="server" CssClass="label" Text=""></asp:Label>
                                            <asp:LinkButton ID="lbMachineBeforeRemove" runat="server" OnClick="lbMachineBeforeRemove_Click" Visible="false">Remove</asp:LinkButton>
                                            <asp:LinkButton ID="lbMachineBeforeDownload" runat="server" OnClick="lbMachineBeforeDownload_Click" Visible="false">Download</asp:LinkButton>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label59" runat="server" CssClass="label" Text="After Machine Restore"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:FileUpload ID="fuMachineAfter" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                            <asp:Label ID="lblMachineAfter" runat="server" CssClass="label" Text=""></asp:Label>
                                            <asp:LinkButton ID="lbMachineAfterRemove" runat="server" OnClick="lbMachineAfterRemove_Click" Visible="false">Remove</asp:LinkButton>
                                            <asp:LinkButton ID="lbMachineAfterDownload" runat="server" OnClick="lbMachineAfterDownload_Click" Visible="false">Download</asp:LinkButton>
                                        </div>
                                    </div>
                                </td>
                            </tr>--%>
                        </table>
                        <asp:Button ID="btnSave" runat="server" Text="Generate/ Update  FSR" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlAttachments" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <UC:UC_AvailabilityOfOtherMachine ID="DMS_AvailabilityOfOtherMachine" runat="server"></UC:UC_AvailabilityOfOtherMachine>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="Label2" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
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
                            <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="AttachedFileID" OnRowDataBound="gvAttachedFile_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Attachment Description">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSRAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FSRAttachedName.FSRAttachedName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFSRAttachedName" runat="server" CssClass="TextBox" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>

                                            <asp:UpdatePanel ID="upManageSubC" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download">                                                     </asp:LinkButton>
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
                                            <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lblAttachedFileRemove_Click">Remove</asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lblAttachedFileAdd" runat="server" OnClick="lblAttachedFileAdd_Click">Add</asp:LinkButton>
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

<script type="text/javascript">

    function collapseExpandFSR(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketFSR_pnlFSR");
        var imageID = document.getElementById("MainContent_DMS_ICTicketFSR_imgFSR");
        var hfCallInformation = document.getElementById('<%= hfFSR.ClientID %>');
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfCallInformation.value = 'X';
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfCallInformation.value = ' ';
        }
    }

    function collapseExpandFSRAttachments(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketFSR_pnlFSRAttachments");
        var imageID = document.getElementById("MainContent_DMS_ICTicketFSR_imgFSRAttachments");
        var hfCallInformation = document.getElementById('<%= hfFSRAttachments.ClientID %>');
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfCallInformation.value = 'X';
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfCallInformation.value = ' ';
        }
    }

    function collapseExpandFSRAvailabilityOfOtherMachine(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketFSR_DMS_AvailabilityOfOtherMachine_pnlAvailabilityOfOtherMachine");
        var imageID = document.getElementById("MainContent_DMS_ICTicketFSR_DMS_AvailabilityOfOtherMachine_imgAvailabilityOfOtherMachine");
        var hfCallInformation = document.getElementById('<%= hfFSRAvailabilityOfOtherMachine.ClientID %>');
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfCallInformation.value = 'X';
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfCallInformation.value = ' ';
        }
    }



    function InIEvent() { }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint').val() };
                    $.ajax({
                        url: "DMS_ICTicketProcess.aspx/SearchMaterialNatureOfComplaint",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });
        });
    };
    $(function () {
        $("#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint').val() };
                $.ajax({
                    url: "DMS_ICTicketProcess.aspx/SearchMaterialNatureOfComplaint",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");  
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox  
        });
    });
</script>
