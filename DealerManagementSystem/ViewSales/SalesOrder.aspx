<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesOrder.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SalesOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Filter : Sales Order Report </div>
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
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Dealer Code"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="Customer :"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="input"></asp:TextBox>

                                                    </td>
                                                </tr>
                                               
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Invoice Number"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="input"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Order Date From :"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOrderDateFrom" runat="server" CssClass="hasDatepicker"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDateFrom" PopupButtonID="txtOrderDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtOrderDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Order Date To"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOrderDateTo" runat="server" CssClass="hasDatepicker"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtOrderDateTo" PopupButtonID="txtOrderDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtOrderDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="SO Number"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSONumber" runat="server" CssClass="input"></asp:TextBox>

                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text=" SO Date From"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSODateFrom" runat="server" CssClass="hasDatepicker"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSODateFrom" PopupButtonID="txtSODateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtSODateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </td>
                                                    <td style="width: 20px"></td>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="SO Date To"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSODateTo" runat="server" CssClass="hasDatepicker"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSODateTo" PopupButtonID="txtSODateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSODateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Material"></asp:Label></td>
                                                    <td>

                                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="input"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="right">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                        &nbsp;
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />

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
                                                        <td>Sales Order Report</td>

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
                                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="1480px" AllowPaging="true" PageSize="20"
                                            OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="62px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Customer")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CustomerName" HeaderStyle-Width="200px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Invoice Number" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SO Number" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSONumber" Text='<%# DataBinder.Eval(Container.DataItem, "SONumber")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SO Date" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSODate" Text='<%# DataBinder.Eval(Container.DataItem, "SODate","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SO Qty" HeaderStyle-Width="45px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOQty" Text='<%# DataBinder.Eval(Container.DataItem, "SoQty","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Part Number" HeaderStyle-Width="128px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PartNumber")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mat Type" HeaderStyle-Width="80px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMatType" Text='<%# DataBinder.Eval(Container.DataItem, "MatType")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Division" HeaderStyle-Width="55px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "Division")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="45px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Basic" HeaderStyle-Width="65px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "Basic","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount" HeaderStyle-Width="65px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Basic After Disc" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "BasicAfterDisc","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax" HeaderStyle-Width="65px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "Tax","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Freight" HeaderStyle-Width="70px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "FreightInsurance","{0:n}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amt" HeaderStyle-Width="90px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmt","{0:n}")%>' runat="server"></asp:Label>
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

    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }


    </script>

</asp:Content>