<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DefaultUserModule.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.DefaultUserModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset id="fsCriteria" class="fieldset-border" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Department</label>
                       <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                    </div>  
                </div>
            </fieldset>
        </div>
    </div>
    <asp:Panel ID="pnlUser" runat="server">
       

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
                                            <td>Role(s):</td>

                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnUserArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnUserArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnUserArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnUserArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: right; overflow: auto;">
                                        <%--<div style="float :left">
                                             
                                        </div>--%>
                                        <div style="float: right">
                                            <img id="fs" alt="" src="../Images/NormalScreen.png" onclick="ScreenControl(2)" width="23" height="23" style="display: none;" />
                                            <img id="rs" alt="" src="../Images/FullScreen.jpg" onclick="ScreenControl(1)" width="23" height="23" style="display: block;" />
                                        </div>
                                    </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvDealerDesignation" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" BorderStyle="None" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvDealerDesignation_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15px" ItemStyle-ForeColor="White" ItemStyle-BackColor="#039caf">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="15px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="350px">
                                    <ItemStyle BorderStyle="None"   HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDesignationID" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignationID")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbRole" runat="server" OnClick="lbRole_Click"> 
                                            <asp:Label ID="lblDealerDesignation" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignation")%>'></asp:Label>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               

                                <asp:TemplateField HeaderText="Department">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" BorderStyle="None"   />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerDepartment" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "Department.DealerDepartment")%>'></asp:Label>
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
                    <asp:Label ID="Label1" runat="server" Text="Department" CssClass="label"></asp:Label></td>
                <td>
                    <asp:Label ID="lblDepartment" runat="server" CssClass="label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Designation" CssClass="label"></asp:Label></td>
                <td>
                    <asp:Label ID="lblDesignation" runat="server" CssClass="label"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="5">
                    <div>
                        <br />
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Module Authentication</span>
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

       <%-- <table>
            <tr>
                <td>
                    <div>
                        <br />
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Module Authentication</span>
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
                        <span style="font-size: 12pt; font-family: Arial; text-align: left; color: #3E4095; padding-left: 1px">Dashboard Authentication</span>
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
