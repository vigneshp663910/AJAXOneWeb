<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="AjaxEmployeeManage.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.AjaxEmployeeManage" %>

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
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
