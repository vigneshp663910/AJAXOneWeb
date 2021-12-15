<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTSIRView.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketTSIRView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">
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

<asp:Label ID="lblMessageTSIR" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<asp:UpdatePanel ID="upManage" runat="server">
    <ContentTemplate>



          <table id="txnHistory1:panelGridid5" style="height: 100%; width: 100%">
            <tr>
                <td>
                    <div class="boxHead">
                        <div class="logheading">Material Charges</div>
                        <div style="float: right; padding-top: 0px">
                            <a href="javascript:collapseExpandMaterialCharges();">
                                <img id="imgMaterialCharges" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                        </div>
                    </div>

                    <asp:Panel ID="pnlMaterialCharges" runat="server">
                        <div class="rf-p " id="txnHistory:inputFiltersPanel5">
                            <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body5">
                                <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-Width="30px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Desc">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material S/N">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prime Faulty Part">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbIsFaultyPart" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsFaultyPart")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FLD Material">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FLD Material S/N">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDefectiveMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recomened Parts">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbRecomenedParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsRecomenedParts")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Base Price">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate> 
                                                 <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quotation  Parts">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbQuotationParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuotationParts")%>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Source">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialSource" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSource")%>' runat="server"></asp:Label>
                                                 <asp:Label ID="Label24" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSourceID")%>' runat="server"  Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlMaterialSourceF" runat="server" CssClass="TextBox" Width="80px" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qtn No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery No.">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Claim No.">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Material Return Status">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantyMaterialReturnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyMaterialReturnStatus")%>' runat="server"></asp:Label>
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
        <table id="txnHistory1:panelGridid6" style="height: 100%; width: 100%" class="IC_materialInfo">
            <tr>
                <td>
                    <div class="boxHead">
                        <div class="logheading">TSIR</div>
                        <div style="float: right; padding-top: 0px">
                            <a href="javascript:collapseExpandNotes();">
                                <img id="imgNotes" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                        </div>
                    </div>
                    <asp:Panel ID="pnlCallInformation" runat="server">
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
                                                    <asp:TextBox ID="txtServiceCharge" runat="server" CssClass="TextBox" AutoComplete="SP"></asp:TextBox>
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
                                </table>

                            </div>
                        </div>
                    </asp:Panel>

                   <%-- <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="AttachedFileID" ShowFooter="false">
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
                        </Columns>
                    </asp:GridView>--%>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

