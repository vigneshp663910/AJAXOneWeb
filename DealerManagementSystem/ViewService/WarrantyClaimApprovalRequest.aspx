<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimApprovalRequest.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyClaimApprovalRequest" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
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

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
    <div class="container">

        <div class="col2">
            <%--  <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <div style="width: 100%; height: 50px; background-color: #f2f2f2;">
                        <br />
                        <span style="font-family: Franklin Gothic Medium; color: black!important; font-size: 20px; margin-left: 9px;">Claim Request List</span>
                    </div>
                </div>
            </div>--%>

            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Request for Claim Approval </div>
                            <div style="float: right; padding-top: 0px">
                                <a href="javascript:collapseExpand();">
                                    <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                            </div>
                        </div>
                        <asp:Panel ID="pnlFilterContent" runat="server">
                            <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                    <table class="labeltxt">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label></td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" />
                                            </td>

                                        </tr>
                                          <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Claim Number :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtClaimID" runat="server" CssClass="input"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Claim Date From :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtClaimDate" runat="server" CssClass="input"  AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtClaimDate" PopupButtonID="txtClaimDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtClaimDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                       <%--     <td style="width: 20px"></td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Claim Date To"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtClaimDateT" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtClaimDateT" PopupButtonID="txtClaimDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtClaimDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>--%>
                                        </tr>
                                     <%--   <tr>
                                            <td>
                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Service Ticket :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtICTicket" runat="server" CssClass="input"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="IC Login Date From :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                            <td style="width: 20px"></td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Login Date To"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                        </tr> --%>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Request" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
 
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
                                                <td>Requested Claim Approval</td>
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

                            <div style="background-color: white">
                                <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="InvoiceNumber" OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:collapseExpand('InvoiceNumber-<%# Eval("InvoiceNumber") %>');">
                                                    <img id="imageInvoiceNumber-<%# Eval("InvoiceNumber") %>" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="10" width="10" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Claim Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantyInvoiceHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceHeaderID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Claim Dt" HeaderStyle-Width="55px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Model">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="75px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketID")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IC Ticket Dt" HeaderStyle-Width="75px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Restore Dt" HeaderStyle-Width="75px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "RestoreDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust. Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust. Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dealer">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dealer Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HMR">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblHMR" Text='<%# DataBinder.Eval(Container.DataItem, "HMR")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin Warranty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "MarginWarranty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MC Serial No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TSIR No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
                                           
                                                <tr>
                                                    <td colspan="100%" style="padding-left: 96px">
                                                        <div id="InvoiceNumber-<%# Eval("InvoiceNumber") %>" style="display: inline; position: relative;">
                                                            <asp:GridView ID="gvICTicketItems" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                                <Columns>


                                                                    <asp:TemplateField HeaderText="SAC / HSN Code">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                             <asp:Label ID="lblWarrantyInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceItemID")%>' runat="server" Visible="false" />
                                               
                                                                            <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Material" >
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Material Desc">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDesc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="150px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="80px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="72px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOM")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="100px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Base+Tax" HeaderStyle-Width="100px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBaseTax" Text='<%# DataBinder.Eval(Container.DataItem, "BaseTax","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                      
                                                                </Columns>

                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
                                    <HeaderStyle BackColor="#BFE4FF" Font-Bold="True" ForeColor="Black" />
                                    <RowStyle ForeColor="Black" BackColor="#bfe4ff" />
                                </asp:GridView>
                            </div>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>