<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="UserMonthlyVerification.aspx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.UserMonthlyVerification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/DealerView.ascx" TagPrefix="UC" TagName="UC_DealerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .portlet.box.green {
            border: 1px solid #5cd1db;
            border-top: 0;
        }

            .portlet.box.green > .portlet-title {
                background-color: #32c5d2;
            }

                .portlet.box.green > .portlet-title > .caption {
                    color: #fff;
                }

        .pull-right {
            float: right !important;
        }

        .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all).btn-sm {
            font-size: 11px;
            padding: 6px 18px 6px 18px;
        }

        .btn.yellow:not(.btn-outline) {
            color: #fff;
            background-color: #c49f47;
            border-color: #c49f47;
        }

        .form-group {
            margin-bottom: 5px;
        }

        b, optgroup, strong {
            font-weight: 700;
        }
    </style>

    <script type="text/javascript">
        function ConfirmActivate() {
            var x = confirm("Are you sure you want to Activate the User?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>

    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divDealerList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                        </div>
                        <%--<div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddDealer" runat="server" CssClass="btn Save" Text="Create Dealer" OnClick="btnAddDealer_Click" Width="150px"></asp:Button>
                        </div>--%>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Employee(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCountDealerEmployee" runat="server" CssClass="label"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDealerEmployeeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerEmployeeArrowLeft_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDealerEmployeeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerEmployeeArrowRight_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDealerEmployee" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                                        PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealerEmployee_PageIndexChanging">
                                        <Columns> 
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <asp:Label ID="lblDealerEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDepartment.DealerDepartment")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.DealerDesignation.DealerDesignation")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User ID" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.User.UserName")%>' runat="server" />
                                                    <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.User.UserID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Contact Number" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactNumber" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNumber")%></a>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Aadhaar Card No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAadhaarCardNo" Text='<%# DataBinder.Eval(Container.DataItem, "AadhaarCardNo")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                             <asp:TemplateField HeaderText="Actions" >
                                                <ItemTemplate>
                                                    <asp:Button ID="btnActivateUser" runat="server" Text="Activate"  OnClick="btnActivateUser_Click"  CssClass="btn Save" Width="100px" Height="25px" />
                                                    <asp:Button ID="btnDeactivateUser" runat="server" Text="Deactivate"  OnClick="btnActivateUser_Click"  CssClass="btn Save" Width="100px" Height="25px"/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                           <%-- <asp:TemplateField HeaderText="Update Status" SortExpression="Country">
                                                <ItemTemplate>

                                                    <div class="dropdown">
                                                        <div class="btn Approval" style="height: 25px">Actions</div>
                                                        <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                                            <asp:LinkButton ID="lnkBtnActivateUser" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmActivate();">Activate</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkBtnDeactivateUser" runat="server" OnClick="lbActions_Click">Deactivate</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div> 
    </div>


</asp:Content>
