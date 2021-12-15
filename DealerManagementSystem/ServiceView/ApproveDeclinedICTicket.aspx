<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ApproveDeclinedICTicket.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ApproveDeclinedICTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    <div class="container">

        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <div id="divICTicketManage" runat="server">
                        <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                            <tr>
                                <td>
                                    <div class="boxHead">
                                        <div class="logheading">Filter : IC Ticket Manage </div>
                                        <div style="float: right; padding-top: 0px">
                                            <a href="javascript:collapseExpand();">
                                                <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlFilterContent" runat="server">
                                        <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                            <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                                <table class="labeltxt fullWidth">
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer Code"></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="input"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Ticket "></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:TextBox ID="txtICTicketNumber" runat="server" CssClass="input"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="IC Ticket Date From "></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                                </div>
                                                            </div>
                                                            <div class="tbl-row-right">
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Ticket Date To"></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">

                                                                    <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                                </div>
                                                            </div>

                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td>
                                                            <div class="tbl-btn excelBtn">
                                                                <div class="tbl-col-btn">
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                                </div>
                                                                <div class="tbl-col-btn">
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                            <tr>
                                <td>
                                    <span id="txnHistory1:refreshDataGroup">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>IC Ticket Manage</td>
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
                                        <div class="InputButtonRight-contain">
                                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                                        </div>
                                        <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                            <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="ICTicketID" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging"  >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rbICTicketID" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="62px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           
                                                                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IC Ticket Date" HeaderStyle-Width="92px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dealer" HeaderStyle-Width="50px">
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
                                                    <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="75px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode","{0:d}")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName","{0:d}")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requested Date" HeaderStyle-Width="76px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate","{0:d}")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Model" HeaderStyle-Width="77px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EngineModel")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Service Type" HeaderStyle-Width="79px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceType")%>' runat="server"></asp:Label><div style="display: none">
                                                                <asp:Label ID="lblServiceTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceTypeID")%>' runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Service Priority" HeaderStyle-Width="147px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "ServicePriority.ServicePriority")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Service Status" HeaderStyle-Width="147px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatus")%>' runat="server"></asp:Label><div style="display: none">
                                                                <asp:Label ID="lblServiceStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatusID")%>' runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Margin Warranty">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsMarginWarranty")%>' Enabled="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Req Declined Reason">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblReqDeclinedReason" Text='<%# DataBinder.Eval(Container.DataItem, "ReqDeclinedReason")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="150px">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbView" runat="server" OnClick="lbView_Click">View </asp:LinkButton>
                                                               &ensp;&ensp;<asp:LinkButton ID="lbApprove" runat="server" OnClick="lbApprove_Click" >Approve </asp:LinkButton>
                                                            &ensp;&ensp;<asp:LinkButton ID="lbReject" runat="server" OnClick="lbReject_Click"  >Reject </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                     </asp:GridView>
                                         
                                        </div>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
     
</asp:Content>
