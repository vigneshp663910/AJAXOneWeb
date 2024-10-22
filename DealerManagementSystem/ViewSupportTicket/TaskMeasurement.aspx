<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="TaskMeasurement.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.TaskMeasurement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/Rating.css">
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
                            <label class="modal-label">From</label>
                            <asp:TextBox ID="txtTicketFrom" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="modal-label">To</label>
                            <asp:TextBox ID="txtTicketTo" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Subcategory</label>
                            <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Status</label>
                            <asp:ListBox ID="lbStatus" runat="server" SelectionMode="Multiple" CssClass="form-control" Height="50px"></asp:ListBox>
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
                            <label>Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>SLA Status</label>
                            <asp:DropDownList ID="ddlSLA" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Within SLA</asp:ListItem>
                                <asp:ListItem Value="2">Not Within SLA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>Rating</label>
                            <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-control">
                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton btn Save" OnClick="btnSearch_Click" />
                        <%--<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="InputButton btn Save"/>--%>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
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
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15">
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
                                        <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ratings">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <ajaxToolkit:Rating runat="server" ID="Rating" Width="125px" Enabled="false"
                                            MaxRating="5"
                                            CurrentRating='<%# Eval("Rating") %>'
                                            CssClass="ratingStar"
                                            StarCssClass="ratingItem"
                                            WaitingStarCssClass="Saved"
                                            FilledStarCssClass="Filled"
                                            EmptyStarCssClass="Empty">
                                        </ajaxToolkit:Rating>
                                        <asp:HiddenField ID="HidRating" runat="server" Value="20" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SubCategory">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketSubCategory" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory.SubCategory")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Severity">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketSeverity" Text='<%# DataBinder.Eval(Container.DataItem, "Severity.Severity")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ticket Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned To">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedTo" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].AssignedTo.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned By">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedBy" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].AssignedBy.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedOn" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].AssignedOn")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInprogressOn" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].InprogressOn")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effort (H)">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffort" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].Effort")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolution Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketResolutionType" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].ResolutionType.ResolutionType")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolution">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblResolution" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].Resolution")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolved On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblResolvedOn" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].ResolvedOn")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation To AssignedHrs">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreationToAssignedHrs" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].CreationToAssignedHrs")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation To ResolvedHrs">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreationToResolvedHrs" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].CreationToResolvedHrs")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned To ResolvedHrs">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignedToResolvedHrs" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].AssignedToResolvedHrs")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inprogress To ResolvedHrs">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInprogressToResolvedHrs" Text='<%# Convert.ToInt32(Eval("TicketItems.Count")) == 0 ? "" : Eval("TicketItems[0].InprogressToResolvedHrs")%>' runat="server"></asp:Label>
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
    </div>
</asp:Content>
