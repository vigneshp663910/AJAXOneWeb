<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="UserAccessManagement.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.UserAccessManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function ConfirmReset() {
            var x = confirm("Are you sure you want to reset the User Password?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>User ID</label>
                        <asp:TextBox ID="txtEmp" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Name</label>
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-1 text-left">
                        <label>IsLocked</label>
                        <asp:DropDownList ID="ddlIsLocked" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1 text-left">
                        <label>IsEnabled</label>
                        <asp:DropDownList ID="ddlIsEnabled" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                            <asp:ListItem Value="2">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1 text-left">
                        <label>AJAXOne</label>
                        <asp:DropDownList ID="ddlAJAXOne" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                            <asp:ListItem Value="2">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2 text-left">
                        <label>Department</label> 
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Designation</label> 
                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <%--<br />--%>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearch_Click"></asp:Button>
                    </div>

                </div>
            </fieldset>
        </div>
    </div>
    <asp:Panel ID="pnlUser" runat="server">
        <%--<div>
            <div class="diveField">
                <div class="diveFieldLabel">
                    <asp:Label ID="lblEmp" runat="server" Text="User ID" CssClass="label"></asp:Label>
                </div>
                <div class="diveFieldText">
                    <asp:TextBox ID="txtEmp" runat="server" CssClass="TextBox"></asp:TextBox>
                </div>
            </div>
            <div class="diveField">
                <div class="diveFieldLabel">
                    <asp:Label ID="Label2" runat="server" Text="Contact Name" CssClass="label"></asp:Label>
                </div>
                <div class="diveFieldText">
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="TextBox"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" />
        </div>--%>

        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <%-- <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">--%>

                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>User(s):</td>

                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnUserArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnUserArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnUserArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnUserArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" BorderStyle="None" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvEmployee_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dealer Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExternalReferenceID" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "ExternalReferenceID")%>'></asp:Label>
                                        <asp:TextBox ID="txtExternalReferenceID" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ExternalReferenceID")%>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Id">
                                    <ItemStyle BorderStyle="None" Width="150px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserID" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbUserID" runat="server" OnClick="lbEmpId_Click">
                                            <asp:Label ID="lblUserName" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:Label> 
                                        </asp:LinkButton>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--     <asp:TemplateField HeaderText="User Id">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                        <ItemTemplate>
                        
                            <asp:Label ID="lblContactName" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="350px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactName" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>'></asp:Label>
                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="350px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMail" runat="server" CssClass="label">
                                             <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Mail")%>'><%# DataBinder.Eval(Container.DataItem, "Mail")%></a>
                                        </asp:Label>
                                        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Mail")%>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="right" BorderStyle="None" Width="150px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNumber" runat="server" CssClass="label"> 
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNumber")%></a>
                                        </asp:Label>
                                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Is Locked?" ItemStyle-Width="80px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lbIsLocked" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "IsLocked")%>'></asp:Label>--%>
                                        <asp:CheckBox ID="cbIsLocked" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsLocked")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Enabled?" ItemStyle-Width="80px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lbIsLocked" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "IsLocked")%>'></asp:Label>--%>
                                        <asp:CheckBox ID="cbIsEnabled" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsEnabled")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AJAX One" ItemStyle-Width="80px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbAjaxOne" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "ajaxOne")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Technician?" ItemStyle-Width="80px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblIsTechnician" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "IsTechnician")%>'></asp:Label>--%>
                                        <asp:CheckBox ID="cbIsTechnician" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsTechnician")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disabled Service" ItemStyle-Width="80px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" Width="80px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbIsDisabledService" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsDisabledService")%>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="140px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="InputButton" OnClick="btnEdit_Click" Width="60px" Height="20px" Font-Size="X-Small" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="InputButton" OnClick="GvbtnUpdate_Click" Width="60px" Height="20px" Font-Size="X-Small" Visible="false" />
                                                </td>
                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="InputButton" OnClick="btnCancel_Click" Width="60px" Height="20px" Font-Size="X-Small" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>

                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Password">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="150px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPassWord" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "PassWord")%>'></asp:Label>
                                        <asp:TextBox ID="txtPassWord" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "PassWord")%>' Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Reset Password" ItemStyle-Width="140px">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                    <asp:Button ID="btnUserResetPassword" runat="server" Text="Reset" CssClass="InputButton" OnClick="btnUserResetPassword_Click" OnClientClick="return ConfirmReset();" Width="60px" Height="20px" Font-Size="X-Small" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>

                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Last Login Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None" Width="150px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastLoginDate" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "LastLoginDate")%>'></asp:Label>
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
                    <%-- </div>--%>
                </fieldset>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlModule" runat="server" Visible="false">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="User ID: " CssClass="label"></asp:Label></td>
                <td>
                    <asp:Label ID="lblUserID" runat="server" CssClass="label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Username: " CssClass="label"></asp:Label></td>
                <td>
                    <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Department: " CssClass="label"></asp:Label></td>
                <td>
                    <asp:Label ID="lblDepartment" runat="server" CssClass="label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Designation: " CssClass="label"></asp:Label></td>
                <td>
                    <asp:Label ID="lblDesignation" runat="server" CssClass="label"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Module  Authentication</span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table>

        <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
            <asp:GridView ID="gvModule" runat="server" AutoGenerateColumns="false" ShowHeader="False"
                BorderStyle="None" OnRowDataBound="gvICTickets_RowDataBound" DataKeyNames="ModuleMasterID">
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle BorderStyle="None" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ModuleName")%>' Font-Bold="true" Font-Size="15px"></asp:Label><asp:CheckBox ID="cbAll" runat="server" Text="Select All" OnCheckedChanged="cbAll_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 50px">
                                        <br />
                                        <asp:DataList ID="dlModule" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="1" CellSpacing="10" DataKeyField="SubModuleMasterID" OnItemDataBound="dlModule_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="item">
                                                    <span>
                                                        <strong>
                                                            <asp:CheckBox ID="cbSMId" runat="server" />
                                                        </strong>
                                                    </span>
                                                    <span><%# Eval("SubModuleName") %></span>
                                                </div>
                                                <div style="padding-left: 50px">
                                                    <asp:Label ID="lblSubModuleMasterID" runat="server" Text='<%# Eval("SubModuleMasterID") %>' Font-Bold="true" Font-Size="15px" Visible="false"></asp:Label>
                                                    <asp:DataList ID="dlChildModule" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="1" CellSpacing="10" DataKeyField="SubModuleChildID">
                                                        <ItemTemplate>
                                                            <div class="item">
                                                                <span>
                                                                    <strong>
                                                                        <asp:CheckBox ID="cbChildId" runat="server" />
                                                                    </strong>
                                                                </span>
                                                                <span><%# Eval("ChildName") %></span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>

                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <br />
                                    </td>
                                </tr>
                            </table>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <%--        <table>
            <tr>
                <td>
                    <div>
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Module  Authentication</span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table>--%>

        <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
            <asp:GridView ID="gvSubModuleChild" runat="server" AutoGenerateColumns="false"
                BorderStyle="None" OnRowDataBound="gvSubModuleChild_RowDataBound" DataKeyNames="SubModuleChildID">
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle BorderStyle="None" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ModuleName")%>' Font-Bold="true" Font-Size="15px"></asp:Label>
                                        <asp:CheckBox ID="cbAll" runat="server" Text="Select All" OnCheckedChanged="cbAll_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataList ID="dlModule" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="6" CellSpacing="10" DataKeyField="SubModuleChildID">
                                            <ItemTemplate>
                                                <div class="item">
                                                    <span>
                                                        <strong>
                                                            <asp:CheckBox ID="cbSMId" runat="server" />
                                                        </strong>
                                                    </span>
                                                    <span><%# Eval("ChildName") %></span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <table>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Dashboard  Authentication</span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:CheckBox ID="cbAllDashboard" runat="server" Text="Select All Dashboard" AutoPostBack="true" OnCheckedChanged="cbAllDashboard_CheckedChanged" />
        <asp:DataList ID="dlDashboard" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="3" CellSpacing="10" DataKeyField="DashboardID">
            <ItemTemplate>
                <div class="item">
                    <span>
                        <strong>
                            <asp:CheckBox ID="cbSMId" runat="server" />
                        </strong>
                    </span>
                    <span><%# Eval("DashboardName") %></span>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>

    <asp:Panel ID="pnlDealer" runat="server" Visible="false">
        <table>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Dealer  Authentication</span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:CheckBox ID="cbAllDealer" runat="server" Text="Select All Dealer" OnCheckedChanged="cbAllDealer_CheckedChanged" AutoPostBack="true" />
        <asp:DataList ID="dlDealer" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="10" CellSpacing="10" DataKeyField="DID">
            <ItemTemplate>
                <div class="item">
                    <span>
                        <strong>
                            <asp:CheckBox ID="cbSMId" runat="server" />
                        </strong>
                    </span>
                    <span><%# Eval("UserName") %></span>
                </div>
            </ItemTemplate>
        </asp:DataList>
  

     
        <table>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Mobile Feature Access</span>
                        <div style="height: 5px; background-color: #0072c6;"></div>
                    </div>
                </td>
            </tr>
        </table> 
        <asp:DataList ID="dlMobileFeatureAccess" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="3" CellSpacing="10" DataKeyField="UserMobileFeatureID">
            <ItemTemplate>
                <div class="item">
                    <span>
                        <strong>
                            <asp:CheckBox ID="cbSMId" runat="server" />
                        </strong>
                    </span>
                    <span><%# Eval("FeatureName") %></span>
                </div>
            </ItemTemplate>
        </asp:DataList> 
          </asp:Panel>
    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="InputButton" OnClick="btnUpdate_Click" Visible="false" />
    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton" OnClick="btnBack_Click" Visible="false" />
</asp:Content>

