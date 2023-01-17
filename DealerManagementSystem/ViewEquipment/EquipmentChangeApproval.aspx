<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentChangeApproval.aspx.cs" Inherits="DealerManagementSystem.ViewEquipment.EquipmentChangeApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewEquipment/UserControls/EquipmentView.ascx" TagPrefix="UC" TagName="UC_EquipmentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ConfirmApprove() {
            var x = confirm("Are you sure you want to Approve the Warranty Type request?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmReject() {
            var x = confirm("Are you sure you want to reject the Warranty Type request?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Requested Date From</label>
                                <asp:TextBox ID="txtReqDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="cxReqDateFrom" runat="server" TargetControlID="txtReqDateFrom" PopupButtonID="txtReqDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderReqDateFrom" runat="server" TargetControlID="txtReqDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Requested Date To</label>
                                <asp:TextBox ID="txtReqDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="cxReqDateTo" runat="server" TargetControlID="txtReqDateTo" PopupButtonID="txtReqDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderReqDateTo" runat="server" TargetControlID="txtReqDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Equipment Serial No.</label>
                                <asp:TextBox ID="txtEquipmentSerialNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnSearch_Click" />
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
                                                <td>Equipment Change Request(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCountEquipChgReq" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeftEquipChgReq" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftEquipChgReq_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRightEquipChgReq" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightEquipChgReq_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvEquipChgReq" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvEquipChgReq_PageIndexChanging">
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
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server" />
                                            <asp:Label ID="lblChangeID" Text='<%# DataBinder.Eval(Container.DataItem, "ChangeID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Engine SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEngineSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EngineSerialNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Equipment SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentSerialNo")%>' runat="server" />
                                            <asp:Label ID="lblEquipmentHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentHeaderID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Model Description">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModelDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ModelDescription")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Warranty Type">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEquipmentWarrantyTypeDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server" />
                                            <asp:Label ID="lblEquipmentWarrantyTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentWarrantyTypeID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                            <br /><asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Dispatched On">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchedOn" Text='<%# DataBinder.Eval(Container.DataItem, "DispatchedOn")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Warranty ExpiryDate">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyExpiryDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Change Requested">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReasonForApproval" Text='<%# DataBinder.Eval(Container.DataItem, "ChangeRequested")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Old Value">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOldValue" Text='<%# DataBinder.Eval(Container.DataItem, "OldValue")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="New Value">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNewValue" Text='<%# DataBinder.Eval(Container.DataItem, "NewValue")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewEquipmentChange" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewEquipmentChange_Click" Width="75px" Height="25px" />
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
        <div class="col-md-12" id="divEquipmentChangeView" runat="server" visible="false">
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
