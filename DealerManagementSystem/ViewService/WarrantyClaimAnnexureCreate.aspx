<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimAnnexureCreate.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyClaimAnnexureCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var gvTickets = document.getElementById('MainContent_gvICTickets');
            if (gvTickets != null) {
                for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                    var lblApprovedAmount = document.getElementById('MainContent_gvICTickets_lblApprovedAmount_' + i);
                    if (parseFloat(lblApprovedAmount.innerHTML) == 0) {
                        lblApprovedAmount.parentNode.parentNode.style.background = "#fff8a1";
                    }
                    else if (parseFloat(lblApprovedAmount.innerHTML) > 7000) {
                        lblApprovedAmount.parentNode.parentNode.style.background = "#ffb8b8";
                    }
                }
            }
        });

        function collapseExpand(obj) {
            var gvObject = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "../Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "../Images/grid_expand.png";
            }
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }

        function ConfirmCreate() {
            var x = confirm("No changes will be allowed after saving");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <table class="labeltxt">
                            <tr>
                                <td colspan="2">
                                    <h3 style="margin-block-start: 1px; margin-block-end: 1px;">CONSOLIDATION IS EFFECTIVE FROM</h3>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><b>1.	NEPI CLAIM  & COMMISSIONING  :: RESTORED DATE >=01.09.2018. Manual settlement for RESTORED DATE < 01.09.2018</b></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><b>2.	WARRANTY CLAIM  ::  IC LOGIN DATE >=16.11.2018 .  Manual settlement for   IC LOGIN DATE < 16.11.2018</b></td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Code</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoPostBack="true"  OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Year</label>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Approved Month</label>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" BorderColor="Silver">
                            <asp:ListItem Value="01">Jan</asp:ListItem>
                            <asp:ListItem Value="02">Feb</asp:ListItem>
                            <asp:ListItem Value="03">Mar</asp:ListItem>
                            <asp:ListItem Value="04">Apr</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">Jun</asp:ListItem>
                            <asp:ListItem Value="07">Jul</asp:ListItem>
                            <asp:ListItem Value="08">Aug</asp:ListItem>
                            <asp:ListItem Value="09">Sep</asp:ListItem>
                            <asp:ListItem Value="10">Oct</asp:ListItem>
                            <asp:ListItem Value="11">Nov</asp:ListItem>
                            <asp:ListItem Value="12">Dec</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Month Range</label>
                        <asp:DropDownList ID="ddlMonthRange" runat="server" CssClass="form-control" BorderColor="Silver">
                           <%-- <asp:ListItem Value="1">W1 (1st to 7th) </asp:ListItem>
                            <asp:ListItem Value="2">W2 (8th to 15th)</asp:ListItem>
                            <asp:ListItem Value="3">W3 (16th to 23rd) </asp:ListItem>
                            <asp:ListItem Value="4">W4 (24th to 31st)</asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Type</label>
                        <asp:DropDownList ID="ddlInvoiceTypeID" runat="server" CssClass="form-control" BorderColor="Silver">
                            <asp:ListItem Value="1">NEPI & Commission</asp:ListItem>
                            <asp:ListItem Value="2">Warranty Below 50K</asp:ListItem>
                            <asp:ListItem Value="5">Warranty Partial Below 50K </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12" id="d1" runat="server" visible="false">
                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Delivery Challan" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtDeliveryChallan" runat="server" CssClass="form-control" BorderColor="Silver" Visible="false" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Create Annexure</legend>
                <div class="col-md-12 Report">
                    <div class="col-md-12">
                        <div class="col-md-12 col-sm-12">
                            <asp:Label ID="lblAnnexureNumber" runat="server" CssClass="label" Text="" Font-Bold="false" Font-Size="20px"></asp:Label>
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" BackColor="#1deaff" Width="100px"/>
                            <asp:Button ID="btnGenerate" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnGenerate_Click" Visible="false"/>
                        </div>
                    </div>
                    <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="InvoiceNumber" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="Zero record fond">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No" HeaderStyle-Width="62px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "SLID")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IC Ticket ID" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketID")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IC Ticket Date" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Restore Date" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "RestoreDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Date" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust. Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust. Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HMR">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHMR" Text='<%# DataBinder.Eval(Container.DataItem, "HMR")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine Serial Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SAC / HSN Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material" HeaderStyle-Width="78px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="55px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimAmount")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Amount" HeaderStyle-Width="65px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedAmount")%>' runat="server"></asp:Label>
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
</asp:Content>
