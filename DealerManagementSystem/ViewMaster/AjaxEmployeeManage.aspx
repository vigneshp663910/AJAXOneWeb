<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="AjaxEmployeeManage.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.AjaxEmployeeManage" %>

<%@ Register Src="~/ViewMaster/UserControls/AjaxEmployeeCreate.ascx" TagPrefix="UC" TagName="UC_Ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" DataTextField="DepartmentName" DataValueField="DepartmentId"></asp:DropDownList>

    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" />

    <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false"
            CssClass="TableView" BorderStyle="None" AllowPaging="true" PageSize="25">
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemStyle BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "EmpId")%></label>
                        <%--       <asp:LinkButton ID="lbEmpId" runat="server" OnClick="lbEmpId_Click">     </asp:LinkButton>--%>
                        <asp:TextBox ID="txtEId" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "EId")%>' Visible="false" />
                        <asp:Label ID="lblDealerEmployeeRoleID" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "DmsEmp.DealerEmployeeRole.DealerEmployeeRoleID")%>' Visible="false" ></asp:Label>
                         
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User ID">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "EmployeeUserID")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpId">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmployeeName">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "EmployeeName")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "Department.DepartmentName")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ReportingTo">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "ReportingTo.EmployeeName")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMS User Name">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "DmsEmp.LoginUserName")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMS Department">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "DmsEmp.DealerEmployeeRole.DealerDepartment.DealerDepartment")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMS Designation">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "DmsEmp.DealerEmployeeRole.DealerDesignation.DealerDesignation")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DMS Reporting To">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "DmsEmp.DealerEmployeeRole.ReportingTo.Name")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                    <ItemTemplate>
                        <asp:Button ID="btnAjaxEmpView" runat="server" Text="Edit" CssClass="btn Back" OnClick="btnAjaxEmpView_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>


    <asp:Panel ID="pnlAjax" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Ajax Employee Edit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" />
            </a>
        </div>
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <UC:UC_Ajax ID="UC_Ajax" runat="server"></UC:UC_Ajax>
                  
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AjaxEmp" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAjax" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

</asp:Content>
