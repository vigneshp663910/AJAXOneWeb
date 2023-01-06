<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentWarrantyExpiryDateApproval.aspx.cs" Inherits="DealerManagementSystem.ViewEquipment.EquipmentWarrantyExpiryDateApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewEquipment/UserControls/EquipmentView.ascx" TagPrefix="UC" TagName="UC_EquipmentView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ConfirmApprove() {
            var x = confirm("Are you sure you want to approve the Warranty Expiry Date Change request?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmReject() {
            var x = confirm("Are you sure you want to reject the Warranty Expiry Date Change request?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageEquipWarrantyExpiryDateChgReq" runat="server" Text="" CssClass="message" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Requested Date From</label>
                                <asp:TextBox ID="txtWarrantyExpiryDateChgReqDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="cxOwnershipChgReqDateFrom" runat="server" TargetControlID="txtWarrantyExpiryDateChgReqDateFrom" PopupButtonID="txtWarrantyExpiryDateChgReqDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderWarrantyExpiryDateChgReqDateFrom" runat="server" TargetControlID="txtWarrantyExpiryDateChgReqDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Requested Date To</label>
                                <asp:TextBox ID="txtWarrantyExpiryDateChgReqDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="cxWarrantyExpiryDateChgReqDateTo" runat="server" TargetControlID="txtWarrantyExpiryDateChgReqDateTo" PopupButtonID="txtWarrantyExpiryDateChgReqDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtWarrantyExpiryDateChgReqDateTo" runat="server" TargetControlID="txtWarrantyExpiryDateChgReqDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Equipment Serial No.</label>
                                <asp:TextBox ID="txtEquipmentSerialNoEquipWarrantyExpiryDateChgReq" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchEquipWarrantyExpiryDateChgReq" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnSearchEquipWarrantyExpiryDateChgReq_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">

                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Equipment Warranty Expiry Date Change Request(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCountEquipWarrantyExpiryDateChgReq" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeftEquipWarrantyExpiryDateChgReq" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftEquipWarrantyExpiryDateChgReq_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRightEquipWarrantyExpiryDateChgReq" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightEquipWarrantyExpiryDateChgReq_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvEquipWarrantyExpiryDateChgReq" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvEquipWarrantyExpiryDateChgReq_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Model">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentModel.Model")%>' runat="server" />
                                             <asp:Label ID="lblWarrantyExpiryDateChangeID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyExpiryDateChangeID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Engine SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEngineSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EngineSerialNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipment SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentSerialNo")%>' runat="server" />
                                            <asp:Label ID="lblEquipmentHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentHeaderID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model Description">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModelDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentModel.ModelDescription")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warranty Type">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEquipmentWarrantyTypeDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentWarrantyType.Description")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Code">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dispatched On">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchedOn" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.DispatchedOn")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warranty Expiry">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOldWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "OldWarrantyExpiryDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warranty Expiry Date Requested">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNewWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "NewWarrantyExpiryDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Action" SortExpression="Action">
                                        <ItemTemplate>
                                            <div class="dropdown">
                                                <div class="btn Approval" style="height: 25px">Action</div>
                                                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                                    <asp:LinkButton ID="lnkBtnApprove" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmApprove();">Approve</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnReject" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmReject();">Reject</asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewEquipWarrantyExpiryDateChgReq" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewEquipWarrantyExpiryDateChgReq_Click" Width="75px" Height="25px" />
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
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divEquipWarrantyExpiryDateChgReqView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12" runat="server" id="tblDashboard">
                <UC:UC_EquipmentView ID="UC_EquipmentView" runat="server"></UC:UC_EquipmentView>
                <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
                <div class="col-md-12 text-center">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
