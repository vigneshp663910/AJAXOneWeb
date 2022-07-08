<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="VisitTagetPlanning.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.VisitTagetPlanning" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">  
        function Calculation(TextID) {
            
            var row = TextID.parentNode.parentNode;
          //var grid = document.getElementById("<%= gvVisitTargetPlanning.ClientID%>");

            var NewTarget = $("input[id*=txtSalesColdCustomerVisitTarget]")[row.rowIndex - 1].value
            var ProspectTarget = $("input[id*=txtSalesProspectCustomertVisitTarget]")[row.rowIndex - 1].value
            var ExistTarget = $("input[id*=txtSalesExistCustomerVisitTarget]")[row.rowIndex - 1].value


            var totalTarget = parseInt(NewTarget) + parseInt(ProspectTarget) + parseInt(ExistTarget);

            var id = 'MainContent_gvVisitTargetPlanning_lblTotalVisitTarget_' + (row.rowIndex - 1);
            var lblTotalTarget = document.getElementById(id);
            lblTotalTarget.innerHTML = totalTarget;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <%--<fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Designation</label>
                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="btnEdit" runat="server" CssClass="btn Save" Text="Edit" OnClick="btnEdit_Click" Width="150px" Visible="false"></asp:Button>
                </div>
            </div>
        </fieldset>--%>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                <div class="col-md-12 Report">

                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Visit Target Palnning:</td>

                                        <td>
                                            <asp:Label ID="lblRowCountVTP" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTPArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVTPArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTPArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVTPArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <asp:GridView ID="gvVisitTargetPlanning" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" 
                                EmptyDataText="No Data Found" OnPageIndexChanging="gvVisitTargetPlanning_PageIndexChanging" DataKeyNames="SalesColdCustomerVisitTarget,SalesProspecCustomertVisitTarget,SalesExistCustomerVisitTarget">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "Department.DealerDepartment")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" SortExpression="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignation")%>' runat="server" />
                                            <asp:Label ID="lblDesignationID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignationID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Total Visit Target" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "TotalVisitTarget")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Sales Cold Customer Visit Target" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSalesColdCustomerVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "SalesColdCustomerVisitTarget")%>' runat="server" />--%>
                                            <asp:TextBox ID="txtSalesColdCustomerVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "SalesColdCustomerVisitTarget")%>'
                                                runat="server" CssClass="form-control" TextMode="Number"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Prospect Customert Visit Target" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSalesProspectCustomertVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "SalesProspecCustomertVisitTarget")%>' runat="server" />--%>
                                            <asp:TextBox ID="txtSalesProspectCustomertVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "SalesProspecCustomertVisitTarget")%>' runat="server" CssClass="form-control" TextMode="Number" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Exist Customer Visit Target" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSalesExistCustomerVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "SalesExistCustomerVisitTarget")%>' runat="server" />--%>
                                            <asp:TextBox ID="txtSalesExistCustomerVisitTarget" Text='<%# DataBinder.Eval(Container.DataItem, "SalesExistCustomerVisitTarget")%>' runat="server" CssClass="form-control" TextMode="Number" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn Save" Text="Update" OnClick="btnUpdate_Click" Width="150px"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
