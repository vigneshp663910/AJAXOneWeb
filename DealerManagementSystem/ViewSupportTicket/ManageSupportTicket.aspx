<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ManageSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.ManageSupportTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewSupportTicket/UserControls/SupportTicketView.ascx" TagPrefix="UC" TagName="UC_SupportTicketView" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {

            var asnQunatity = 0;
            var gvTickets = document.getElementById('MainContent_gvTickets');

            if (gvTickets != null) {
                for (var i = 0; i < gvTickets.rows.length - 1; i++) {

                    var lblTicketSeverity = document.getElementById('MainContent_gvTickets_lblTicketSeverity_' + i);
                    var lblTicketStatus = document.getElementById('MainContent_gvTickets_lblTicketStatus_' + i);
                    var lblCreatedOn = document.getElementById('MainContent_gvTickets_lblCreatedOn_' + i);

                    if (lblCreatedOn != null) {



                        var pattern = /(\d{2})\/(\d{2})\/(\d{4})/;

                        var dt = new Date(lblCreatedOn.innerText.replace(pattern, '$3-$2-$1'));
                        if (dt == "Invalid Date") {
                            var CreatedOn = lblCreatedOn.innerText.split('/');
                            dt = new Date(CreatedOn[2].split(' ')[0], CreatedOn[1] - 1, CreatedOn[0]);
                        }

                        var someDate = new Date();

                        if (lblTicketSeverity.innerHTML == "SEVERITY  1 -  Address The call within 4 hrs") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 1);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                        else if (lblTicketSeverity.innerHTML == "SEVERITY  2  -  Address the call with 2days") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 2);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                        else if (lblTicketSeverity.innerHTML == "SEVERITY  3  - Address the call with in 1 week") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 7);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f";
                                }
                            }
                        }
                        else if (lblTicketSeverity.innerHTML == "SEVERITY  4  - Address the call with in 1 month") {
                            if ((lblTicketStatus.innerHTML == "Assigned") || (lblTicketStatus.innerHTML == "In Progress")) {
                                someDate.setDate(someDate.getDate() - 31);
                                if (dt < someDate) {
                                    lblTicketSeverity.parentNode.parentNode.style.background = "#ef5f5f"; ss
                                }
                            }
                        }
                    }
                }
            }
        });
    </script>--%>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Width="100%" Visible="false"/>
    <div class="col-md-12" id="divList" runat="server">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Ticket No</label>
                        <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Subcategory</label>
                        <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Severity</label>
                        <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Status</label>
                        <asp:ListBox ID="lbStatus" runat="server" SelectionMode="Multiple" CssClass="TextBox form-control"></asp:ListBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Ticket Type</label>
                        <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Created From</label>
                        <asp:TextBox ID="txtTicketFrom" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Created To</label>
                        <asp:TextBox ID="txtTicketTo" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Created By</label>
                        <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Assigned To</label>
                        <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Approval To</label>
                        <asp:DropDownList ID="ddlApprovalTo" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton btn Save" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="InputButton btn Save" OnClick="btnExcel_Click" />
                    </div>
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
                                        <td>Task(s):</td>

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

                    <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvTickets_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Ticket ID">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibMessage" runat="server" Width="30px" OnClick="ibMessage_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket ID">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubCategory">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketSubCategory" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory.SubCategory")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" Text='<%# DataBinder.Eval(Container.DataItem, "MobileNo")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Age">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAge" Text='<%# DataBinder.Eval(Container.DataItem, "age")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SLA">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSla" Text='<%# DataBinder.Eval(Container.DataItem, "SLA")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
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
</asp:Content>
