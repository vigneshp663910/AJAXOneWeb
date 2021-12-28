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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <label>Dealer</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>State</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Aadhaar Card No</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAadhaarCardNo" runat="server" CssClass="form-control" MaxLength="14" onkeydown="return isNumber(event);" onkeyUp="AadhaarCardNo(event)"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtAadhaarCardNo" WatermarkText="XXXX-XXXX-XXXX"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Status</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Based On Role</label>
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="cbBasedOnRole" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton btn Search" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton btn Search" OnClick="btnExportExcel_Click" Width="100px"/>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-2">
                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                </div>
                <div class="col-md-1">
                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">                
                <asp:GridView ID="gvDealerEmployee" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" AllowPaging="True" DataKeyNames="DealerEmployeeID" PageSize="20" OnPageIndexChanging="gvDealerEmployee_PageIndexChanging">
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
                                <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click">Edit</asp:LinkButton>&nbsp
                                <asp:LinkButton ID="lbView" runat="server" OnClick="lbView_Click">View</asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#f2f2f2" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
