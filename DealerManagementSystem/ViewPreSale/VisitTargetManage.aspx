<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="VisitTargetManage.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.VisitTargetManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">  
        function Calculation(TextID) {
            debugger
            var row = TextID.parentNode.parentNode;
          //var grid = document.getElementById("<%= gvVisitTarget.ClientID%>");

            var NewTarget = $("input[id*=txtNewCustomerTarget]")[row.rowIndex - 1].value
            var ProspectTarget = $("input[id*=txtProspectCustomerTarget]")[row.rowIndex - 1].value
            var ExistTarget = $("input[id*=txtExistCustomerTarget]")[row.rowIndex - 1].value


            var totalTarget = parseInt(NewTarget) + parseInt(ProspectTarget) + parseInt(ExistTarget);

            var id = 'MainContent_gvVisitTarget_lblTotalTarget_' + (row.rowIndex - 1);
            var lblTotalTarget = document.getElementById(id);
            lblTotalTarget.innerHTML = totalTarget;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12" id="divList" runat="server">
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Year</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Month</label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12" style="display: none">
                    <label class="modal-label">Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" />
                </div>


                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="btnEdit" runat="server" CssClass="btn Save" Text="Edit" OnClick="btnEdit_Click" Width="150px"></asp:Button>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Listing</legend>
            <div class="col-md-12 Report">
                <asp:GridView ID="gvVisitTarget" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnDataBound="OnDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Code" ItemStyle-Width="30px">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblVisitTargetID" Text='<%# DataBinder.Eval(Container.DataItem, "VisitTargetID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblDealerEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.DealerEmployeeID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName">
                            <ItemTemplate>
                                <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Employee.Name")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Year" SortExpression="Year" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Month" SortExpression="Month" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Target" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalTarget" Text='<%# DataBinder.Eval(Container.DataItem, "TotalTarget")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="New Customer Target" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblNewCustomerTarget" Text='<%# DataBinder.Eval(Container.DataItem, "NewCustomerTarget")%>' runat="server" />
                                <asp:TextBox ID="txtNewCustomerTarget" Text='<%# DataBinder.Eval(Container.DataItem, "NewCustomerTarget")%>'
                                    runat="server" CssClass="form-control" TextMode="Number" Visible="false" onblur="Calculation(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prospect Customer Target" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblProspectCustomerTarget" Text='<%# DataBinder.Eval(Container.DataItem, "ProspectCustomerTarget")%>' runat="server" />
                                <asp:TextBox ID="txtProspectCustomerTarget" Text='<%# DataBinder.Eval(Container.DataItem, "ProspectCustomerTarget")%>' runat="server" CssClass="form-control" TextMode="Number" Visible="false" onblur="Calculation(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exist Customer Target" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblExistCustomerTarget" Text='<%# DataBinder.Eval(Container.DataItem, "ExistCustomerTarget")%>' runat="server" />
                                <asp:TextBox ID="txtExistCustomerTarget" Text='<%# DataBinder.Eval(Container.DataItem, "ExistCustomerTarget")%>' runat="server" CssClass="form-control" TextMode="Number" Visible="false" onblur="Calculation(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Total Actual" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalActual" Text='<%# DataBinder.Eval(Container.DataItem, "TotalActual")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="New Customer Actual" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblNewCustomerActual" Text='<%# DataBinder.Eval(Container.DataItem, "NewCustomerActual")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prospect Customer Actual" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblProspectCustomerActual" Text='<%# DataBinder.Eval(Container.DataItem, "ProspectCustomerActual")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exist Customer Actual" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblExistCustomerActual" Text='<%# DataBinder.Eval(Container.DataItem, "ExistCustomerActual")%>' runat="server" />
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
        </fieldset>
    </div>

</asp:Content>





