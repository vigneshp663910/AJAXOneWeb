<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PresalesMasters.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.PresalesMasters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <asp:TabContainer ID="tbpPresalesMasters" runat="server" ToolTip="Presales Masters..." Font-Bold="True" Font-Size="Medium">
            <asp:TabPanel ID="tpnlSourceOfEnquiry" runat="server" HeaderText="Source Of Enquiry" Font-Bold="True" ToolTip="Source Of Enquiry...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Lead Source</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlLeadSource" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSearchLeadSource" runat="server" CssClass="btn Search" Text="Search" OnClick="btnSearchLeadSource_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvLeadSource" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvLeadSource_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LeadSource" SortExpression="LeadSource">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLeadSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source")%>' runat="server" />
                                                    <asp:Label ID="lblLeadSourceID" Text='<%# DataBinder.Eval(Container.DataItem, "SourceID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtLeadSource" runat="server" placeholder="Lead Source" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblLeadSourceEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SourceID")%>' OnClick="lblLeadSourceEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblLeadSourceDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SourceID")%>' OnClick="lblLeadSourceDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddLeadSource" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddLeadSource_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tpnlTypeOfActivity" runat="server" HeaderText="Type Of Activity" Font-Bold="True" ToolTip="Type Of Activity...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Action Type</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSearchActionType" runat="server" CssClass="btn Search" Text="Search" OnClick="btnSearchActionType_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvActionType" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvActionType_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ActionType" SortExpression="ActionType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "ActionType")%>' runat="server" />
                                                    <asp:Label ID="lblActionTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ActionTypeID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtActionType" runat="server" placeholder="Action Type" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblActionTypeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ActionTypeID")%>' OnClick="lblActionTypeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblActionTypeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ActionTypeID")%>' OnClick="lblActionTypeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddActionType" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddActionType_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tpnlCustomerAttributeMain" runat="server" HeaderText="Customer Attribute Main" Font-Bold="True" ToolTip="Customer Attribute Main...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Attribute Main</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlCustomerAttributeMain" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSearchCustomerAttributeMain" runat="server" CssClass="btn Search" Text="Search" OnClick="btnSearchCustomerAttributeMain_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerAttributeMain" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvCustomerAttributeMain_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attribute Main" SortExpression="AttributeMain">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerAttributeMain" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeMain")%>' runat="server" />
                                                    <asp:Label ID="lblCustomerAttributeMainID" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeMainID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCustomerAttributeMain" runat="server" placeholder="Action Type" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblCustomerAttributeMainEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AttributeMainID")%>' OnClick="lblCustomerAttributeMainEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblCustomerAttributeMainDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AttributeMainID")%>' OnClick="lblCustomerAttributeMainDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddCustomerAttributeMain" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddCustomerAttributeMain_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tpnlCustomerAttributeSub" runat="server" HeaderText="Customer Attribute Sub" Font-Bold="True" ToolTip="Customer Attribute Sub...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Attribute Main</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlSCustomerAttributeMain" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Attribute Sub</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlCustomerAttributeSub" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSearchCustomerAttributeSub" runat="server" CssClass="btn Search" Text="Search" OnClick="btnSearchCustomerAttributeSub_Click"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvCustomerAttributeSub" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvCustomerAttributeSub_PageIndexChanging" OnDataBound="gvCustomerAttributeSub_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AttributeMain" SortExpression="AttributeMain">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttributeMain" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeMain.AttributeMain")%>' runat="server" />
                                                    <asp:Label ID="lblAttributeMainID" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeMain.AttributeMainID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlGAttributeMain" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AttributeSub" SortExpression="AttributeSub">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttributeSub" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeSub")%>' runat="server" />
                                                    <asp:Label ID="lblAttributeSubID" Text='<%# DataBinder.Eval(Container.DataItem, "AttributeSubID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAttributeSub" runat="server" placeholder="AttributeSub" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblCustomerAttributeSubEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AttributeSubID")%>' OnClick="lblCustomerAttributeSubEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblCustomerAttributeSubDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AttributeSubID")%>' OnClick="lblCustomerAttributeSubDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddCustomerAttributeSub" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddCustomerAttributeSub_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tpnlEffort" runat="server" HeaderText="Type Of Effort" Font-Bold="True" ToolTip="Types Of Effort...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Effort Type</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlEffortType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEffortType_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvEffortType" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvEffortType_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EffortType" SortExpression="EffortType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType")%>' runat="server" />
                                                    <asp:Label ID="lblEffortTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "EffortTypeID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtEffortType" runat="server" placeholder="Effort Type" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblEffortTypeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EffortTypeID")%>' OnClick="lblEffortTypeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblEffortTypeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EffortTypeID")%>' OnClick="lblEffortTypeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddEffortType" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddEffortType_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            
            <asp:TabPanel ID="tpnlExpence" runat="server" HeaderText="Type Of Expense" Font-Bold="True" ToolTip="Type Of Expense...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specifiy Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Expense Type</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenseType_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvExpenseType" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvExpenseType_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExpenseType" SortExpression="ExpenseType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType")%>' runat="server" />
                                                    <asp:Label ID="lblExpenseTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseTypeID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtExpenseType" runat="server" placeholder="Expense Type" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblExpenseTypeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ExpenseTypeID")%>' OnClick="lblExpenseTypeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lblExpenseTypeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ExpenseTypeID")%>' OnClick="lblExpenseTypeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddExpenseType" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddExpenseType_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tbPnlMake" runat="server" HeaderText="Make" Font-Bold="True" ToolTip="Make">
                <contenttemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvMake" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvMake_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Make" SortExpression="Make">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Make")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMake" runat="server" placeholder="Make" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkMakeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MakeID")%>' OnClick="lnkBtnMakeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkMakeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MakeID")%>' OnClick="lnkBtnMakeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddOrUpdateMake" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateMake_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </contenttemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tbPnlProductType" runat="server" HeaderText="Product Type" Font-Bold="True" ToolTip="Product Type">
                <contenttemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvProductType" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" AllowPaging="True" ShowFooter="True" OnPageIndexChanging="gvProductType_PageIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Type" SortExpression="ProductType">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductType" runat="server" placeholder="Product Type" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnProductTypeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductTypeID")%>' OnClick="lnkBtnProductTypeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnProductTypeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductTypeID")%>' OnClick="lnkBtnProductTypeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddOrUpdateProductType" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateProductType_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" />
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
                    </div>
                </contenttemplate>
            </asp:TabPanel>

            <asp:TabPanel ID="tbPnlProduct" runat="server" HeaderText="Product" Font-Bold="True" ToolTip="Product">
                <contenttemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" OnPageIndexChanging="gvProduct_PageIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" SortExpression="Product">
                                                <ItemStyle VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem, "Product")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProduct" runat="server" placeholder="Product" CssClass="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnProductEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>' OnClick="lnkBtnProductEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBtnProductDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>' OnClick="lnkBtnProductDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="BtnAddOrUpdateProduct" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateProduct_Click" Width="70px" Height="33px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </contenttemplate>
            </asp:TabPanel>

        </asp:TabContainer>
    </div>
</asp:Content>
