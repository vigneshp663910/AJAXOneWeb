<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketTSIRApprove.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketTSIRApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketTSIRView.ascx" TagPrefix="UC" TagName="UC_TSIRView" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            var gvTickets = document.getElementById('MainContent_gvTSIRMessage');

            if (gvTickets != null) {
                for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                    var lblNoteType = document.getElementById('MainContent_gvTSIRMessage_lblTSIRMessage_' + i);
                    if (lblNoteType != null) {
                        if (lblNoteType.innerHTML == "") {
                            lblNoteType.parentNode.parentNode.style.display = "none";
                        }
                    }
                }
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Code</label>
                            <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer Code</label>
                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">TSIR No</label>
                            <asp:TextBox ID="txtTSIRNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">TSIR Date From</label>
                            <asp:TextBox ID="txtTSIRDateFrom" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTSIRDateFrom" PopupButtonID="txtTSIRDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtTSIRDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">TSIR Date To</label>
                            <asp:TextBox ID="txtTSIRDateTo" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTSIRDateTo" PopupButtonID="txtTSIRDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtTSIRDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">IC Ticket</label>
                            <asp:TextBox ID="txtICTicketNumber" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">IC Ticket Date From</label>
                            <asp:TextBox ID="txtICLoginDateFrom" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">IC Ticket Date To</label>
                            <asp:TextBox ID="txtICLoginDateTo" runat="server" AutoComplete="Off" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">SRO Code</label>
                            <asp:TextBox ID="txtSroCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12" style="display: none">
                            <label class="modal-label">Model</label>
                            <asp:DropDownList ID="ddlModelID" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 text-left">
                            <label class="modal-label">-</label>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">TSIR Approve</legend>
                    <div class="col-md-12 Report">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" DataKeyNames="TsirID" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="IC Ticket">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TsirNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tsir Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "TsirDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Machine Serial Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentSerialNo")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HMR">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.CurrentHMRValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nature Of Complaint">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "NatureOfFailures")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Priority" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ServicePriority.ServicePriority")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Of Warranty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTypeOfWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.TypeOfWarranty.TypeOfWarranty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Margin Warranty" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "ICTicket.IsMarginWarranty")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "Status.StatusID")%>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnView_Click" Width="50px" Height="33px" />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
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

    <div class="col-md-12" id="divTSIRView" runat="server" visible="false">
        <div class="col-md-12 lead-back-btn">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBack_Click" />
            </div>
        </div> 
        <UC:UC_TSIRView ID="UC_TSIRView" runat="server"></UC:UC_TSIRView>
    </div> 
   
</asp:Content>
