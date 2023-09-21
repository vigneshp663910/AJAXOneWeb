<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTSIRView.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketTSIRView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewService/UserControls/AddTSIR.ascx" TagPrefix="UC" TagName="UC_AddTSIR" %>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbActions_Click">Edit TSIR</asp:LinkButton>
                <asp:LinkButton ID="lbtnCheck" runat="server" OnClick="lbActions_Click">TSIR Check</asp:LinkButton>
                <asp:LinkButton ID="lbtnApprove" runat="server" OnClick="lbActions_Click">TSIR Approve</asp:LinkButton>
                <asp:LinkButton ID="lbtnReject" runat="server" OnClick="lbActions_Click">TSIR Reject</asp:LinkButton>
                <asp:LinkButton ID="lbtnSalesApproveL1" runat="server" OnClick="lbActions_Click">TSIR Sales Approve L1</asp:LinkButton>
                <asp:LinkButton ID="lbtnSalesApproveL2" runat="server" OnClick="lbActions_Click">TSIR Sales Approve L2</asp:LinkButton>
                <asp:LinkButton ID="lbtnSalesReject" runat="server" OnClick="lbActions_Click">TSIR Sales Reject</asp:LinkButton>
                <asp:LinkButton ID="lbtnSendBack" runat="server" OnClick="lbActions_Click">TSIR Send Back</asp:LinkButton>
                <asp:LinkButton ID="lbtnCancel" runat="server" OnClick="lbActions_Click">TSIR Cancel</asp:LinkButton>

            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">TSIR</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>TSIR Number : </label>
                <asp:Label ID="lblTsirNumber" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>TSIR Status : </label>
                <asp:Label ID="lblTsirStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>SRO Code : </label>
                <asp:Label ID="lblServiceCharge" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Nature Of Failures : </label>
                <asp:Label ID="lblNatureOfFailures" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>How Was Problem Noticed / Who / When : </label>
                <asp:Label ID="lblProblemNoticedBy" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Under What Condition Did The Failure Taken Place : </label>
                <asp:Label ID="lblUnderWhatConditionFailureTaken" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Failure Details : </label>
                <asp:Label ID="lblFailureDetails" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Points Checked : </label>
                <asp:Label ID="lblPointsChecked" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Possible Root Causes : </label>
                <asp:Label ID="lblPossibleRootCauses" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Specific Points Noticed : </label>
                <asp:Label ID="lblSpecificPointsNoticed" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Parts Invoice Number : </label>
                <asp:Label ID="lblPartsInvoiceNumber" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<style>
    .cc {
        visibility: visible !important
    }
</style>
<asp:TabContainer ID="tbpSaleQuotation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="10">
    <asp:TabPanel ID="tpnlFinancier" runat="server" HeaderText="Material" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="ServiceMaterialID">
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
                                <asp:TemplateField HeaderText="Avl Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAvailableQty" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base Price">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
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
                                <asp:TemplateField HeaderText="PO No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPONumber" Text='<%# DataBinder.Eval(Container.DataItem, "PONumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parts Invoice">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOldInvoice" Text='<%# DataBinder.Eval(Container.DataItem, "OldInvoice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlCallInformation" runat="server" HeaderText="Message">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvTSIRMessage" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" DataKeyNames="TSIRMessageID" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Message">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTSIRMessage" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRMessage")%>' runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTSIRMessage" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="400"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display To Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDisplayToDealer" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayToDealer")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="cbDisplayToDealer" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lblTSIRMessageAdd" runat="server" OnClick="lblTSIRMessageAdd_Click">Add</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>

</asp:TabContainer>


<asp:Panel ID="pnlSaleApprove" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Sale Approve</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button101" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageCustomerFeedback" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Amount</label>
                        <asp:TextBox ID="txtSalesApproveAmount" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaleApproveL1" runat="server" Text="Sale Approve" CssClass="btn Save" Width="150px" OnClick="btnSaleApproveL1_Click" />
            <asp:Button ID="btnSaleApproveL2" runat="server" Text="Sale Approve" CssClass="btn Save" Width="150px" OnClick="btnSaleApproveL2_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_SaleApprove" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSaleApprove" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddTSIR" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add TSIR</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAddTSIR" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_AddTSIR ID="UC_AddTSIR" runat="server"></UC:UC_AddTSIR>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddTSIR" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddTSIR_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddTSIR" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddTSIR" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>



<%--<script type="text/javascript">

    function collapseExpand(obj) {
        var gvObject = document.getElementById(obj);
        var imageID = document.getElementById('image' + obj);
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }
</script>--%>
