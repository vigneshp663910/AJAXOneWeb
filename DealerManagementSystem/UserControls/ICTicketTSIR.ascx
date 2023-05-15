<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTSIR.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketTSIR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">

    function collapseExpandTSIR(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketTSIR_pnlTSIR");
        var imageID = document.getElementById("MainContent_DMS_ICTicketTSIR_imgTSIR");
        var hfCallInformation = document.getElementById('<%= hfTSIR.ClientID %>');
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

    function collapseExpand(obj) {
        var gvObject = document.getElementById(obj);
        var imageID = document.getElementById('image' + obj);

        var hfgvRow = document.getElementById('<%= hfgvRow.ClientID %>');

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfgvRow.value = obj;
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfgvRow.value = "";
        }

    }

    function OpenInNewTab(url) {
        var win = window.open(url, '_blank');
        win.focus();
    }
    $(document).ready(function () {
        var hfgvRow = document.getElementById('<%= hfgvRow.ClientID %>');
        if (hfgvRow.value != "") {
            collapseExpand(hfgvRow.value)
        }
    });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                var hfgvRow = document.getElementById('<%= hfgvRow.ClientID %>');
                if (hfgvRow.value != "") {
                    collapseExpand(hfgvRow.value)
                }
            });
            };

            function InIEvent() {

            }

            $(document).ready(InIEvent);
</script>
<asp:HiddenField ID="hfgvRow" runat="server" Value=""></asp:HiddenField>
<asp:HiddenField ID="hfTSIR" runat="server" Value=""></asp:HiddenField>

<asp:Label ID="lblMessageTSIR" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<table id="txnHistory1:panelGridid6" style="height: 100%; width: 100%" class="IC_materialInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">TSIR</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandTSIR();">
                        <img id="imgTSIR" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlTSIR" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel6">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body6">
                        <asp:GridView ID="gvTSIR" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="TsirID" OnRowDataBound="gvTSIR_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:collapseExpand('TsirID-<%# Eval("TsirID") %>');">
                                            <img id="imageTsirID-<%# Eval("TsirID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbCheck" runat="server" OnCheckedChanged="cbCheck_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TsirNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tsir Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "TsirDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Tsir Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "Status.StatusID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SRO Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceCharge.Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="SRO Code Description">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceCharge.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    <%--</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                         <asp:LinkButton ID="lblCancelTSIR" runat="server" OnClick="lblCancelTSIR_Click">Cancel</asp:LinkButton>--%>
                                        <tr>
                                            <td colspan="100%" style="padding-left: 96px">
                                                <div id="TsirID-<%# Eval("TsirID") %>" style="display: none; position: relative;">
                                                    <table>
                                                        <tr>
                                                            <td colspan="100%" style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="AttachedFileID" ShowFooter="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Note Type">
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFSRAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FSRAttachedName.FSRAttachedName")%>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>                                                                                
                                                                                <asp:UpdatePanel ID="upManage" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownloadR_Click" Text="Download">
                                                                                              </asp:LinkButton>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lblAttachedFileRemoveR_Click">Remove</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                <asp:DropDownList ID="ddlFSRAttachedName" runat="server" CssClass="TextBox" /></td>
                                                            <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" /></td>
                                                            <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                                <asp:UpdatePanel ID="upManage" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lblAttachedFileAdd" runat="server" OnClick="lblAttachedFileAddR_Click">Add</asp:LinkButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lblAttachedFileAdd" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                      <div class="rf-p " id="txnHistory:inputFiltersPanel2">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body2">

                        <table class="labeltxt fullWidth">

                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="SRO Code"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:DropDownList ID="ddlServiceChargeID" runat="server" CssClass="TextBox" Width="400px" DataValueField="ServiceChargeID" DataTextField="MaterialCode" />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label10" runat="server" CssClass="label" Text="Nature Of Failures"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtNatureOfFailures" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label11" runat="server" CssClass="label" Text="How Was Problem Noticed / Who  / When"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtProblemNoticedBy" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>

                                        </div>

                                    </div>
                                </td>

                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label28" runat="server" CssClass="label" Text="Under What Condition Did The Failure Taken Place"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtUnderWhatConditionFailureTaken" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Failure Details"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtFailureDetails" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>


                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label22" runat="server" CssClass="label" Text="Points Checked"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtPointsChecked" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="lblHMRValue" runat="server" CssClass="label" Text="Possible Root Causes"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtPossibleRootCauses" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>

                                <td class="auto-style1">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label23" runat="server" CssClass="label" Text="Specific Points Noticed"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtSpecificPointsNoticed" runat="server" CssClass="TextBox" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                                <tr>
                                <td class="auto-style1">
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Parts Invoice Number"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtPartsInvoiceNumber" runat="server" CssClass="TextBox" AutoComplete="SP"   Width="400px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
 
                            </tr>
                        </table>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                    </div>
                </div>
                </div>
            </asp:Panel>         
            <asp:Panel ID="pnlCallInformation" runat="server">
              
            </asp:Panel>
        </td>
    </tr>
</table>