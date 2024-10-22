<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="TaskSummaryDetails.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.TaskSummaryDetails" %>

<%@ Register Src="~/ViewSupportTicket/UserControls/SupportTicketView.ascx" TagPrefix="UC" TagName="UC_SupportTicketView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>Ticket No</label>
                            <asp:TextBox ID="txtTicketNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Year</label>
                            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Month</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="modal-label">From</label>
                            <asp:TextBox ID="txtTicketFrom" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="modal-label">To</label>
                            <asp:TextBox ID="txtTicketTo" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2">
                            <label>Created By</label>
                            <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Assigned To</label>
                            <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Severity</label>
                            <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>SLA</label>
                            <asp:DropDownList ID="ddlSLA" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Created">Created</asp:ListItem>
                                <asp:ListItem Value="Assigned">Assigned</asp:ListItem>
                                <asp:ListItem Value="InProgress" Selected="True">InProgress</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>Status</label>
                            <asp:ListBox ID="lbStatus" runat="server" SelectionMode="Multiple" CssClass="form-control" Height="50px"></asp:ListBox>
                        </div>
                        <div class="col-md-2">
                            <label>Action to Perform</label>
                            <asp:DropDownList ID="ddlReportSelection" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Ticket" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Department/Category"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Categorywise"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Assign To"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Created By"></asp:ListItem>
                                <asp:ListItem Value="6" Text="SLA"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton btn Save" OnClick="btnSearch_Click" />
                        <%--<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="InputButton btn Save"/>--%>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12" runat="server">
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>List(s):</td>
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
                </div>
            </div>
            <div class="col-md-12" id="RptTickets" runat="server">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" ShowFooter="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Created" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Created")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opened">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Opened" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Opened")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Assigned" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="2"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_progress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Inprogress")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="3"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approval" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approval")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="6"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="8"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Resolved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resolved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="4"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Closed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Closed")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="5"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ForceClosed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Rejected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rejected")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="11"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deleted">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Deleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Deleted")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="10"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per%">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPer" Text='<%# DataBinder.Eval(Container.DataItem, "Per")%>' runat="server"></asp:Label>
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
            <div class="col-md-12" id="RptDeptCatewise" runat="server" visible="false">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvDeptCatewise" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" ShowFooter="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartmentID" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryID" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Created" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Created")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opened">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Opened" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Opened")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Assigned" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="2"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_progress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Inprogress")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="3"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approval" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approval")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="6"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="8"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Resolved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resolved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="4"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Closed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Closed")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="5"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ForceClosed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Rejected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rejected")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="11"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deleted">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Deleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Deleted")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="10"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per%">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPer" Text='<%# DataBinder.Eval(Container.DataItem, "Per")%>' runat="server"></asp:Label>
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
            <div class="col-md-12" id="RptCategorywise" runat="server" visible="false">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvCategorywise" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" ShowFooter="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryID" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Created" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Created")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opened">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Opened" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Opened")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Assigned" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="2"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_progress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Inprogress")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="3"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approval" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approval")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="6"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="8"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Resolved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resolved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="4"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Closed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Closed")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="5"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ForceClosed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Rejected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rejected")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="11"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deleted">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Deleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Deleted")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="10"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per%">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPer" Text='<%# DataBinder.Eval(Container.DataItem, "Per")%>' runat="server"></asp:Label>
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
            <div class="col-md-12" id="RptAssignTo" runat="server" visible="false">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvAssignTo" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" ShowFooter="true" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedTo")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartmentID" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryID" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Created" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Created")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="0"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opened">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Opened" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Opened")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="1"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assigned">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Assigned" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="2"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inprogress">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_progress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Inprogress")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="3"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Approval" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approval")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="6"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Approved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="8"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resolved">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Resolved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resolved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="4"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closed">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Closed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Closed")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="5"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ForceClosed">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Rejected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rejected")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="11"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deleted">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Deleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Deleted")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="10"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Per%">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPer" Text='<%# DataBinder.Eval(Container.DataItem, "Per")%>' runat="server"></asp:Label>
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
            <div class="col-md-12" id="RptCreatorBy" runat="server" visible="false">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvCreatorBy" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" ShowFooter="true" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartmentID" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryID" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Created" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Created")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="0"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opened">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Opened" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Opened")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="1"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Assigned" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="2"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_progress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Inprogress")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="3"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approval" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approval")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="6"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Approved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="8"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Resolved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Resolved")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="4"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Closed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Closed")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="5"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ForceClosed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Rejected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rejected")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="11"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deleted">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Deleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Deleted")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="10"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Per%">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPer" Text='<%# DataBinder.Eval(Container.DataItem, "Per")%>' runat="server"></asp:Label>
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
            <div class="col-md-12" id="RptSLA" runat="server" visible="false">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvSLA" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMonthName" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryID" Text='<%# DataBinder.Eval(Container.DataItem, "CategoryID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLA Achieved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Yes" runat="server" ToolTip="Click" Text='<%# DataBinder.Eval(Container.DataItem, "Yes")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="Yes"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLA Not Achieved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_No" runat="server" ToolTip="Click" Text='<%# DataBinder.Eval(Container.DataItem, "No")%>' OnClick="TicketHeaderDetails_Click" CommandArgument="No"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLA Achieved %" ControlStyle-ForeColor="White" ControlStyle-Font-Bold="true">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lnk_YesPer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SLAYesPer")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLA Not Achieved %" ControlStyle-ForeColor="White" ControlStyle-Font-Bold="true">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lnk_NoPer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SLANoPer")%>'></asp:Label>
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

            <div class="col-md-12" id="ViewTicketList" runat="server">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>List(s):</td>
                                            <td>
                                                <asp:Label ID="lblViewRowCount" runat="server" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="GVTicketDetails" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15" OnPageIndexChanging="GVTicketDetails_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ticket ID">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_HeaderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' OnClick="lnk_HeaderID_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation To AssignedHrs">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreationToAssignedHours" Text='<%# DataBinder.Eval(Container.DataItem, "Creation To Assigned")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation To ResolvedHrs">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreationToResolvedHours" Text='<%# DataBinder.Eval(Container.DataItem, "Creation To Resolved")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned To ResolvedHrs">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedToResolvedHours" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned To Resolved")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress To ResolvedHrs">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInprogressToResolvedHours" Text='<%# DataBinder.Eval(Container.DataItem, "Inprogress To Resolved")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rating">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRating" Text='<%# DataBinder.Eval(Container.DataItem, "Rating")%>' runat="server"></asp:Label>
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
        <div class="col-md-12" id="divSupportTicketView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_SupportTicketView ID="UC_SupportTicketView" runat="server"></UC:UC_SupportTicketView>
        </div>
        <div style="display: none">
            <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
    </div>
</asp:Content>
