<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentOwnershipChangeApproval.aspx.cs" Inherits="DealerManagementSystem.ViewEquipment.EquipmentOwnershipChangeApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewEquipment/UserControls/EquipmentView.ascx" TagPrefix="UC" TagName="UC_EquipmentViewOwnershipChg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ConfirmApprove() {
            var x = confirm("Are you sure you want to approve the Ownership Change request?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmReject() {
            var x = confirm("Are you sure you want to reject the Ownership Change request?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageEquipOwnershipChgReq" runat="server" Text="" CssClass="message" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Requested Date From</label>
                                <asp:TextBox ID="txtOwnershipChgReqDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="cEOwnershipChgReqDateFrom" runat="server" TargetControlID="txtOwnershipChgReqDateFrom" PopupButtonID="txtOwnershipChgReqDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderOwnershipChgReqDateFrom" runat="server" TargetControlID="txtOwnershipChgReqDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Requested Date To</label>
                                <asp:TextBox ID="txtOwnershipChgReqDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="cEOwnershipChgReqDateTo" runat="server" TargetControlID="txtOwnershipChgReqDateTo" PopupButtonID="txtOwnershipChgReqDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtOwnershipChgReqDateTo" runat="server" TargetControlID="txtOwnershipChgReqDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Equipment Serial No.</label>
                                <asp:TextBox ID="txtEquipmentSerialNoEquipOwnershipChgReq" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearchEquipOwnershipChgReq" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnSearchEquipOwnershipChgReq_Click" />
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
                                                <td>Equipment Ownership Change Request(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCountEquipOwnershipChgReq" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeftEquipOwnershipChgReq" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftEquipOwnershipChgReq_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRightEquipOwnershipChgReq" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightEquipOwnershipChgReq_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvEquipOwnershipChgReq" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvEquipOwnershipChgReq_PageIndexChanging">
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
                                             <asp:Label ID="lblOwnershipChangeID" Text='<%# DataBinder.Eval(Container.DataItem, "OwnershipChangeID")%>' runat="server" Visible="false" />
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
                                    <%--<asp:TemplateField HeaderText="Customer Code">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Customer Name">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerName")%>' runat="server" />
                                            <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerID")%>' runat="server" visible="false"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Customer Code Change Requested">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOwnershipChgReqCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name Change Requested">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOwnershipChgReqCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerName")%>' runat="server" />
                                            <asp:Label ID="lblOwnershipChgReqCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.Customer.CustomerID")%>' runat="server"  visible="false" />
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
                                    <asp:TemplateField HeaderText="Warranty ExpiryDate">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.WarrantyExpiryDate")%>' runat="server" />
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
                                            <asp:Button ID="btnViewEquipOwnershipChgReq" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewEquipOwnershipChgReq_Click" Width="75px" Height="25px" />
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
        <div class="col-md-12" id="divEquipOwnershipChgReqView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12" runat="server" id="tblDashboard">
                <UC:UC_EquipmentViewOwnershipChg ID="UC_EquipmentView" runat="server"></UC:UC_EquipmentViewOwnershipChg>
                <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
                <div class="col-md-12 text-center">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
