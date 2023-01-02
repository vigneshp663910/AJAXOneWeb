<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipmentView.ascx.cs" Inherits="DealerManagementSystem.ViewEquipment.UserControls.EquipmentView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>

<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>

<%--var geocoder;--%>

<style>
    .fieldset-borderAuto {
        border: solid 1px #cacaca;
        margin: 1px 0;
        border-radius: 5px;
        padding: 10px;
        background-color: #b4b4b4;
    }

        .fieldset-borderAuto tr {
            /* background-color: #000084; */
            background-color: inherit;
            font-weight: bold;
            color: white;
        }

        .fieldset-borderAuto:hover {
            background-color: blue;
        }
</style>

<script type="text/javascript">
    function ConfirmApprWarrantyTypeChg() {
        var x = confirm("Are you sure you want to approve the Warranty Type Change?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmRejWarrantyTypeChg() {
        var x = confirm("Are you sure you want to reject the Warranty Type Change?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmApprOwnershipChg() {
        var x = confirm("Are you sure you want to approve the Ownership Type Change?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmRejrOwnershipChg() {
        var x = confirm("Are you sure you want to reject the Ownership Type Change?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmApprWarrantyExpiryDateChg() {
        var x = confirm("Are you sure you want to approve the Warranty Expiry Date Change?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmRejWarrantyExpiryDateChg() {
        var x = confirm("Are you sure you want to approve the Warranty Expiry Date Change?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <%--<asp:LinkButton ID="lnkBtnEditWarranty" runat="server" OnClick="lnkBtnActions_Click">Update Warranty Type</asp:LinkButton>--%>
                <asp:LinkButton ID="lnkBtnUpdateCommDate" runat="server" OnClick="lnkBtnActions_Click">Update Commissioning Date</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnReqWarrantyTypeChange" runat="server" OnClick="lnkBtnActions_Click">Warranty Type Change Request</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnReqOwnershipChange" runat="server" OnClick="lnkBtnActions_Click">Ownership Change Request</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnReqWarrantyExpiryDateChange" runat="server" OnClick="lnkBtnActions_Click">Expiry Date Change Request</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnApprWarrantyTypeChangeReq" runat="server" OnClick="lnkBtnActions_Click" OnClientClick="return ConfirmApprWarrantyTypeChg();">Approve Warranty Type Change</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnRejWarrantyTypeChangeReq" runat="server" OnClick="lnkBtnActions_Click" OnClientClick="return ConfirmRejWarrantyTypeChg();">Reject Warranty Type Change</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnApprOwnershipChangeReq" runat="server" OnClick="lnkBtnActions_Click" OnClientClick="return ConfirmApprOwnershipChg();">Approve Ownership Change</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnRejOwnershipChangeReq" runat="server" OnClick="lnkBtnActions_Click" OnClientClick="return ConfirmRejrOwnershipChg();">Reject Ownership Change</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnApprWarrantyExpiryDateChangeReq" runat="server" OnClick="lnkBtnActions_Click" OnClientClick="return ConfirmApprWarrantyExpiryDateChg();">Approve Warranty Expiry Date Change</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnRejWarrantyExpiryDateChangeReq" runat="server" OnClick="lnkBtnActions_Click" OnClientClick="return ConfirmRejWarrantyExpiryDateChg();">Reject Warranty Expiry Date Change</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Equipment</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Model : </label>
                    <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Model Description : </label>
                    <asp:Label ID="lblModelDescription" runat="server" CssClass="label"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Warranty ExpiryDate : </label>
                    <asp:Label ID="lblWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>HMR Date : </label>
                    <asp:Label ID="lblCurrentHMRDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RF Warranty Start Date : </label>
                    <asp:Label ID="lblRFWarrantyStartDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>AMC : </label>
                    <asp:CheckBox ID="cbIsAMC" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
                <div class="col-md-12">
                    <label>Variants Fitting Date : </label>
                    <asp:Label ID="lblVariantsFittingDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>ESN : </label>
                    <asp:Label ID="lblESN" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Special Variants : </label>
                    <asp:Label ID="lblSpecialVariants" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Warranty Type : </label>
                    <asp:Label ID="lblWarrantyType" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Engine SerialNo : </label>
                    <asp:Label ID="lblEngineSerialNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>District : </label>
                    <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Engine Model : </label>
                    <asp:Label ID="lblEngineModel" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>HMR Value : </label>
                    <asp:Label ID="lblCurrentHMRValue" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RF Warranty Expiry Date : </label>
                    <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>AMC Start Date : </label>
                    <asp:Label ID="lblAMCStartDate" runat="server" CssClass="label"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Plant : </label>
                    <asp:Label ID="lblPlant" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>ProductionStatus : </label>
                    <asp:Label ID="lblProductionStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Manufacturing Date : </label>
                    <asp:Label ID="lblManufacturingDate" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Equipment SerialNo : </label>
                    <asp:Label ID="lblEquipmentSerialNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dispatched On : </label>
                    <asp:Label ID="lblDispatchedOn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Commisioning On : </label>
                    <asp:Label ID="lblCommisioningOn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Type Of Wheel Assembly : </label>
                    <asp:Label ID="lblTypeOfWheelAssembly" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>AMC Expiry Date : </label>
                    <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Material Code : </label>
                    <asp:Label ID="lblMaterialCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Chassis No : </label>
                    <asp:Label ID="lblChassisSlNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dispatch : </label>
                    <asp:Label ID="lblDispatch" runat="server" CssClass="label"></asp:Label>
                </div>


            </div>
        </div>
    </fieldset>
</div>

<asp:HiddenField ID="hfLatitude" runat="server" />
<asp:HiddenField ID="hfLongitude" runat="server" />
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpIbase" runat="server" ToolTip="IBase..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="IBase">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">IBase</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Installed BaseNo : </label>
                                <asp:Label ID="lblInstalledBaseNo" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Delivery Date : </label>
                                <asp:Label ID="lblDeliveryDate" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>IBase Warranty Start Date : </label>
                                <asp:Label ID="lblIbaseWarrantyStart" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>IBase Location : </label>
                                <asp:Label ID="lblIBaseLocation" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>IBase CreatedOn : </label>
                                <asp:Label ID="lblIBaseCreatedOn" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>IBase Warranty End Date : </label>
                                <asp:Label ID="lblIbaseWarrantyEnd" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Major Region : </label>
                                <asp:Label ID="lblMajorRegion" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Financial Year Of Dispatch : </label>
                                <asp:Label ID="lblFinancialYearOfDispatch" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerViewSoldTo ID="CustomerViewSoldTo" runat="server"></UC:UC_CustomerViewSoldTo>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlService" runat="server" HeaderText="Equipment Service">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Equipment Service:</td>
                                    <td>
                                        <asp:Label ID="lblRowCountService" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnServiceArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnServiceArrowLeft_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnServiceArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnServiceArrowRight_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <asp:GridView ID="gvICTickets1" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvICTickets1_PageIndexChanging" DataKeyNames="ICTicketID">
                            <Columns>
                                <asp:TemplateField HeaderText="Issues Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceMaterial.Material.MaterialCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issues Description">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceMaterial.Material.MaterialDescription")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Request Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Requested Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceType" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceType")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Warranty Material Replaced">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceMaterialM.Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Description">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceMaterialM.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HMR">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "CurrentHMRValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tsir Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceMaterialM.TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
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
    </asp1:TabPanel>

    <asp1:TabPanel ID="tabPnlAttachedFile" runat="server" HeaderText="Support Document">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="AttachedFileID">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attached File Type" HeaderStyle-Width="250px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "ReferenceName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                    <%--<asp:LinkButton ID="lnkBtnWarrantyTypeChangeAttachedFileDownload" runat="server" OnClick="lnkBtnWarrantyTypeChangeAttachedFileDownload_Click" Text="Download"></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnAttachedFileRemove" runat="server" OnClick="lnkBtnAttachedFileRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>

<asp:Panel ID="pnlUpdateCommiDate" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueUpdateCommiDate">Update Commissioning Date</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnPopupUpdateCommiDateClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer</label>
                        <asp:Label ID="lblCustomerC" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Model</label>
                        <asp:Label ID="lblModelC" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Equipment Serial No.</label>
                        <asp:Label ID="lblEquipmentSerialNoC" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">HMR</label>
                        <asp:Label ID="lblHMRC" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Date Of Commissioning</label>
                        <asp:TextBox ID="txtCommissioningDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="cxExpectedDateOfSale" runat="server" TargetControlID="txtCommissioningDate" PopupButtonID="txtCommissioningDate" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderDateOFCommi" runat="server" TargetControlID="txtCommissioningDate" WatermarkText="DD/MM/YYYY" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnUpdateCommiDate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateCommiDate_Click" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_UpdateCommiDate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlUpdateCommiDate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlWarrantyTypeChangeReq" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueWarrantyTypeChangeReq">Equipment Warranty Type Change Request</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnWarrantyTypeChangeReqClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageWarrantyTypeChangeReq" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer</label>
                        <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Model</label>
                        <asp:Label ID="lblModelP" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Equipment Serial No.</label>
                        <asp:Label ID="lblEquipmentSerialNoP" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Warranty Type</label>
                        <asp:DropDownList ID="ddlWarranty" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>


                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">File</label>
                            <asp:FileUpload ID="fileUpload" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                            <asp:Button ID="btnAddFile" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFile_Click" />
                        </div>
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvWarrantyTypeSupportDocument" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="FileName">
                                    <Columns>
                                        <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="500px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnvWarrantyTypeAttachedFileRemove" runat="server" OnClick="lnkBtnvWarrantyTypeAttachedFileRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnReqWarrantyTypeChange" runat="server" Text="Save" CssClass="btn Save" OnClick="btnReqWarrantyTypeChange_Click" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_WarrantyTypeChangeReq" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlWarrantyTypeChangeReq" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlOwnershipChangeReq" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueOwnershipChangeReq">Equipment Ownership Change Request</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnPopupDialogueOwnershipChangeReqClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageOwnershipChangeReq" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset4" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer</label>
                        <asp:Label ID="lblCustomerOwnership" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Model</label>
                        <asp:Label ID="lblModelOwnership" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Equipment Serial No.</label>
                        <asp:Label ID="lblEquipmentSerialNoOwnership" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer</label>
                        <asp:TextBox ID="txtBoxCustomerOwnershipChange" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Sold Date</label>
                        <asp:TextBox ID="txtSoldDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                        <asp1:CalendarExtender ID="cxSoldDate" runat="server" TargetControlID="txtSoldDate" PopupButtonID="txtSoldDate" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderSoldDate" runat="server" TargetControlID="txtSoldDate" WatermarkText="DD/MM/YYYY" />
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">File</label>
                            <asp:FileUpload ID="fileUploadOwnershipChange" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                            <asp:Button ID="btnAddFileOwnershipChange" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFileOwnershipChange_Click" />
                        </div>
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOwnershipChangeReqSupportDocument" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="FileName">
                                    <Columns>
                                        <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="500px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnAttachedFileOwnershipChangeRemove" runat="server" OnClick="lnkBtnAttachedFileOwnershipChangeRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnReqOwnershipChange" runat="server" Text="Save" CssClass="btn Save" OnClick="btnReqOwnershipChange_Click" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_OwnershipChangeReq" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlOwnershipChangeReq" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlWarrantyExpiryDateChangeReq" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueWarrantyExpiryDateChangeReq">Equipment Warranty Expiry Date Change Request</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnPopupDialogueWarrantyExpiryDateChangeReqClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageWarrantyExpiryDateChangeReq" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer</label>
                        <asp:Label ID="lblCustomerWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Model</label>
                        <asp:Label ID="lblModelWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Equipment Serial No.</label>
                        <asp:Label ID="lblEquipmentSerialNoWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Warranty Expiry Date</label>
                        <asp:Label ID="lblWarrantyExpiryDateP" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Warranty Expiry Date</label>
                        <asp:TextBox ID="txtWarrantyExpiryDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                        <asp1:CalendarExtender ID="cEWarrantyExpiryDate" runat="server" TargetControlID="txtWarrantyExpiryDate" PopupButtonID="txtDOB" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="txtBoxWartermarkExtenderWarrantyExpiryDate" runat="server" TargetControlID="txtWarrantyExpiryDate" WatermarkText="DD/MM/YYYY" />
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">File</label>
                            <asp:FileUpload ID="fileUploadWarrantyExpiryDateChange" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                            <asp:Button ID="btnAddFileWarrantyExpiryDateChange" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFileWarrantyExpiryDateChange_Click" />
                        </div>
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvWarrantyExpiryDateChangeSupportDocument" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="FileName">
                                    <Columns>
                                        <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="500px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnWarrantyExpiryDateChangeRemove" runat="server" OnClick="lnkBtnWarrantyExpiryDateChangeRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
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
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnReqWarrantyExpiryDate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnReqWarrantyExpiryDate_Click" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_WarrantyExpiryDateChangeReq" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlWarrantyExpiryDateChangeReq" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
