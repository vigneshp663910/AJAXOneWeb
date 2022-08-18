<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="OpenSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.OpenSupportTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-main-container .col, .page-main-container .col-1, .page-main-container .col-10, .page-main-container .col-11, .page-main-container .col-12, .page-main-container .col-2, .page-main-container .col-3, .page-main-container .col-4,
        .page-main-container .col-5, .page-main-container .col-6, .page-main-container .col-7, .page-main-container .col-8, .page-main-container .col-9, .page-main-container .col-auto, .page-main-container .col-lg, .page-main-container .col-lg-1,
        .page-main-container .col-lg-10, .page-main-container .col-lg-11, .page-main-container .col-lg-12, .page-main-container .col-lg-2, .page-main-container .col-lg-3, .page-main-container .col-lg-4, .page-main-container .col-lg-5,
        .page-main-container .col-lg-6, .page-main-container .col-lg-7, .page-main-container .col-lg-8, .page-main-container .col-lg-9, .page-main-container .col-lg-auto, .page-main-container .col-md, .page-main-container .col-md-1,
        .page-main-container .col-md-10, .page-main-container .col-md-11, .page-main-container .col-md-12, .page-main-container .col-md-2, .page-main-container .col-md-3, .page-main-container .col-md-4, .page-main-container .col-md-5,
        .page-main-container .col-md-6, .page-main-container .col-md-7, .page-main-container .col-md-8, .page-main-container .col-md-9, .page-main-container .col-md-auto, .page-main-container .col-sm, .page-main-container .col-sm-1,
        .page-main-container .col-sm-10, .page-main-container .col-sm-11, .page-main-container .col-sm-12, .page-main-container .col-sm-2, .page-main-container .col-sm-3, .page-main-container .col-sm-4, .page-main-container .col-sm-5,
        .page-main-container .col-sm-6, .page-main-container .col-sm-7, .page-main-container .col-sm-8, .page-main-container .col-sm-9, .page-main-container .col-sm-auto, .page-main-container .col-xl, .page-main-container .col-xl-1,
        .page-main-container .col-xl-10, .page-main-container .col-xl-11, .page-main-container .col-xl-12, .page-main-container .col-xl-2, .page-main-container .col-xl-3, .page-main-container .col-xl-4, .page-main-container .col-xl-5,
        .page-main-container .col-xl-6, .page-main-container .col-xl-7, .page-main-container .col-xl-8, .page-main-container .col-xl-9, .page-main-container .col-xl-auto {
            display: initial;
            padding-left: 15px;
            padding-right: 15px;
        }

        .page-main-container {
            width: 100%;
            position: relative;
        }

            .page-main-container .form-container .form-control {
                height: 35px;
                padding: 0px 7px;
            }

            .page-main-container .form-container .InputButton {
                height: 34px;
            }

        .form-container {
            padding: 30px 25px;
            background: #f9f9f9;
        }

        .page-main-container tr {
            background: none;
            border: none;
        }

        .form-container .file-upload {
            padding: 2px;
            height: auto;
            display: block;
            width: 100%;
        }

        .form-container-fields {
            border: 1px solid #369;
            padding: 25px;
            position: relative;
            border-radius: 5px;
        }

            .form-container-fields .label {
                margin-bottom: 3px;
                display: inline-block;
                font-weight: 600;
            }

            .form-container-fields .field-label {
                font-size: 14pt;
                font-family: Arial;
                text-align: left;
                color: #3E4095;
                position: absolute;
                top: -17px;
                left: 10px;
                background: #f9f9f9;
                padding: 3px 15px;
                font-weight: 600;
            }
    </style>
    <script>
        $(function () {
            var availableEmp = EmpArray;
            $("#MainContent_txtRequestedUserID").autocomplete({
                source: availableEmp
            });
        });
    </script>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <div class="container-fluid form-container">
        <asp:Panel ID="pnList" runat="server">
            <div class="form-container-fields">
                <span class="field-label">Open Task</span>
                <div class="row">
                    <div class="col-md-2 col-sm-6">
                        <asp:Label ID="Label2" runat="server" Text="Req Date From" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtRequestedDateFrom" runat="server" CssClass="TextBox form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-2 col-sm-6">

                        <asp:Label ID="Label3" runat="server" Text="To" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtRequestedDateTo" runat="server" CssClass="TextBox form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-2 col-sm-6">

                        <asp:Label ID="Label4" runat="server" Text="Requested User ID" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-6">
                        <asp:Label ID="Label1" runat="server" Text="Ticket ID" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTicketId" runat="server" CssClass="TextBox form-control" />
                    </div>
                    <div class="col-md-2 col-sm-6">
                        <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton btn Save" OnClick="btnSearch_Click" />
                    </div>
                </div>

            </div>
            <br />
            <div class="form-container-fields">
                <span class="field-label">Report</span>
                <div class="row">
                    <br />
                    <%--<div style="height:auto; width:800px; overflow-x:scroll ; overflow-y: hidden; padding-bottom:10px;"> --%>
                    <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
                        <asp:RadioButton ID="rbAssign" runat="server" Text="Assign" GroupName="ss" CssClass="label" Checked="true" />
                        <asp:RadioButton ID="rbSendForApproval" runat="server" Text="Send for Approval" GroupName="ss" CssClass="label" />
                        <%-- <asp:RadioButton ID="rbResolve" runat="server" Text="Resolve" GroupName="ss" CssClass="label" />--%>
                        <asp:RadioButton ID="rbReject" runat="server" Text="Reject" GroupName="ss" CssClass="label" />
                        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibMessage" runat="server" Width="30px" OnClick="ibMessage_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ticket ID">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbTicketNo" runat="server" OnClick="lbTicketNo_Click">
                                            <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ticket Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
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
                                <asp:TemplateField HeaderText="Requested By">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requested On">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
        </asp:Panel>


        <asp:Panel ID="pnView" runat="server" Visible="false">
            <div class="form-container-fields">
                <span class="field-label">Send For Approval Form</span>
                <div class="row">
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblRequestedOn" runat="server" Text="Requested By" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtRequestedBy" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label5" runat="server" Text="Category" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="label"></asp:Label>

                        <asp:TextBox ID="txtStatus" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-6">
                        <asp:Label ID="lblTicketDescription" runat="server" Text="Ticket Description" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Height="70px" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label6" runat="server" Text="Ticket Type" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTicketType" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label8" runat="server" Text="Approver" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlapprovar" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label7" runat="server" Text="Attached File" CssClass="label"></asp:Label>
                        <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle BorderStyle="None" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" Text='<%# Eval("text") %>' CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" CssClass="InputButton btn Save" Width="200px" OnClick="btnSendForApproval_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="Go Back" CssClass="InputButton btn Save" OnClick="btnBack_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlReject" runat="server" Visible="false">
            <div class="form-container-fields">
                <span class="field-label">Reject Form</span>
                <div class="row">

                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label9" runat="server" Text="Ticket No" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTicketNoReject" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-6">
                        <asp:Label ID="Label13" runat="server" Text="Description" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTicketNoRejectRemark" runat="server" TextMode="MultiLine"  CssClass="TextBox form-control"></asp:TextBox>
                    </div>
                   <div class="col-md-12 text-center">
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="InputButton btn Save" OnClick="btnReject_Click" />
                        <asp:Button ID="btnRejectBack" runat="server" Text="Go Back" CssClass="InputButton btn Save" OnClick="btnRejectBack_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

