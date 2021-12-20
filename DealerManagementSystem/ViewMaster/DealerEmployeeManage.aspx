<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerEmployeeManage.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.DealerEmployeeManage" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                if ((iKeyCode > 95 && iKeyCode < 106)) {
                    return true;
                }
                else {
                    return false;
                }

            }
            return true;
        }
        function AadhaarCardNo(event) {
            var val = document.getElementById("<%=txtAadhaarCardNo.ClientID %>").value;
            // val = val.replace("-", "");
            val = replaceAll(val, "-", "");
            //alert(t.value);
            if (val.length > 8) {
                document.getElementById('<%=txtAadhaarCardNo.ClientID %>').value = val.substring(0, 4) + "-" + val.substring(4, 8) + "-" + val.substring(8, 12);
            }
            else if (val.length > 4) {
                document.getElementById('<%=txtAadhaarCardNo.ClientID %>').value = val.substring(0, 4) + "-" + val.substring(4, 8);
            }
    }
    function replaceAll(str, term, replacement) {
        return str.replace(new RegExp(escapeRegExp(term), 'g'), replacement);
    }
    function escapeRegExp(string) {
        return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
    }
    </script>

</asp:Content>
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
                                    <div class="logheading">Filter : Dealer Employee Manage </div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpand();">
                                            <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlFilterContent" runat="server">
                                    <div class="rf-p " id="txnHistory1:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory1:inputFiltersPanel_body">
                                            <table class="labeltxt fullWidth">
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="TextBox" Width="250px" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="State"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">

                                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="TextBox"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Aadhaar Card No"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtAadhaarCardNo" runat="server" CssClass="input" MaxLength="14" onkeydown="return isNumber(event);" onkeyUp="AadhaarCardNo(event)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtAadhaarCardNo" WatermarkText="XXXX-XXXX-XXXX"></asp:TextBoxWatermarkExtender>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtName" runat="server" CssClass="input"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Status"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Based On Role"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:CheckBox ID="cbBasedOnRole" runat="server" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-btn excelBtn">
                                                            <div class="tbl-col-btn">
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                            </div>
                                                            <div class="tbl-col-btn"></div>
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
                            <td><span id="txnHistory1:refreshDataGroup">
                                <div class="boxHead">
                                    <div class="logheading">
                                        <div style="float: left">
                                            <table>
                                                <tr>
                                                    <td>Dealer Employee Manage</td>
                                                    <td>
                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="InputButtonRight-contain">
                                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" OnClick="btnExportExcel_Click" />
                                </div>
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                    <asp:GridView ID="gvDealerEmployee" runat="server" AutoGenerateColumns="False" CssClass="TableGrid" AllowPaging="True" DataKeyNames="DealerEmployeeID" PageSize="20" OnPageIndexChanging="gvDealerEmployee_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Dealer Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="62px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="250px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="162px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Father Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFatherName" Text='<%# DataBinder.Eval(Container.DataItem, "FatherName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="192px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" Text='<%# DataBinder.Eval(Container.DataItem, "Email")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "State.State" )%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aadhaar Card No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "AadhaarCardNo" )%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAN No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "PANNo")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="76px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedByNmae" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblCreatedByID" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.UserID")%>' runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="76px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Approved">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbIsAjaxHPApproved" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsAjaxHPApproved")%>' Enabled="false" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="76px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click">Edit </asp:LinkButton>&ensp;&ensp;
                                                                    <asp:LinkButton ID="lbView" runat="server" OnClick="lbView_Click">View </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </span></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
