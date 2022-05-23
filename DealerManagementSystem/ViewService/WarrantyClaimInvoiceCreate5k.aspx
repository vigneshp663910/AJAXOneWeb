<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimInvoiceCreate5k.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyClaimInvoiceCreate5k" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function ConfirmCreate() {
            var x = confirm("No changes will be allowed after saving");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
        function collapseExpand1(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "../Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "../Images/grid_expand.png";
            }
        }
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
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Code</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control"/>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Year</label>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"/>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Month</label>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">All</asp:ListItem>
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
                        <label class="modal-label">Claim Number</label>
                        <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Claim Above 50K</legend>
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvClaimByClaimID" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="InvoiceNumber" OnRowDataBound="gvICTickets_RowDataBound" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="javascript:collapseExpand('InvoiceNumber-<%# Eval("InvoiceNumber") %>');">
                                        <img id="imageInvoiceNumber-<%# Eval("InvoiceNumber") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_collapse.png" height="10" width="10" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Dt" HeaderStyle-Width="55px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="55px">
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
                            <asp:TemplateField HeaderText="Model">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TSIR No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Through">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtThrough" runat="server" CssClass="input" Width="90"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LR/Docket No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLRNumber" runat="server" CssClass="input" Width="100"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Generate Invoice" HeaderStyle-Width="55px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate Invoice" BackColor="Aqua" CssClass="InputButton" OnClick="btnGenerateInvoice_Click" OnClientClick="return ConfirmCreate();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimStatus")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attachment">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPscID" Text='<%# DataBinder.Eval(Container.DataItem, "PscID")%>' runat="server" Visible="false" />
                                    <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None" CssClass="table table-bordered table-condensed Grid">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle BorderStyle="None" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("fileName") %>' OnClientClick='<%# Eval("Url") %>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <tr>
                                        <td colspan="100%" style="padding-left: 96px">
                                            <div id="InvoiceNumber-<%# Eval("InvoiceNumber") %>" style="display: inline; position: relative;">
                                                <asp:GridView ID="gvICTicketItems" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SAC / HSN Code">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWarrantyClaimItemID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceItemID")%>' runat="server" Visible="false" />
                                                                <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material" HeaderStyle-Width="78px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="150px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="40px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="42px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOM")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Base+Tax" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBaseTax" Text='<%# DataBinder.Eval(Container.DataItem, "BaseTax","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Failure Mat Remarks 1" HeaderStyle-Width="150px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialStatusRemarks1" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks1")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apr. 1 Amt" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproved1Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Amount")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apr. 1 Remarks" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproved1Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved1Remarks")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Failure Mat Remarks 2" HeaderStyle-Width="150px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialStatusRemarks2" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialStatusRemarks2")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apr 2 Amt" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproved2Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Amount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apr. 2 Remarks" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproved2Remarks" Text='<%# DataBinder.Eval(Container.DataItem, "Approved2Remarks")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SAP Doc" HeaderStyle-Width="150px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSAPDoc" Text='<%# DataBinder.Eval(Container.DataItem, "SAPDoc")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SAP Inv Value" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSAPInvoiceValue" Text='<%# DataBinder.Eval(Container.DataItem, "SAPInvoiceValue","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="#ffffff" />
                                                    <FooterStyle ForeColor="White" />
                                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>