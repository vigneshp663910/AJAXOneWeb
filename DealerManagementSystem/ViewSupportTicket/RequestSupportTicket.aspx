<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="RequestSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.RequestSupportTicket" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Scripts/styles.css" />
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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">New Task</legend>
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-6">
                        <label class="modal-label">Category<span style="color: red">*</span></label>                        
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="modal-label">Subcategory<span style="color: red">*</span></label>                        
                        <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="modal-label">Ticket Type<span style="color: red">*</span></label>                        
                        <asp:DropDownList ID="ddlTicketType" runat="server" Style="position: relative;" CssClass="TextBox form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="modal-label">Subject<span style="color: red">*</span></label>                        
                        <asp:TextBox ID="txtSubject" runat="server" Style="position: relative;" CssClass="TextBox form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="modal-label">Contact Name<span style="color: red">*</span></label>                       
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="TextBox form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="modal-label">Mobile No<span style="color: red">*</span></label>
                        <asp:TextBox ID="txtMobileNo" runat="server" Style="position: relative;" CssClass="TextBox form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-12">
                        <label class="modal-label">Ticket Note<span style="color: red">*</span></label>                        
                        <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Height="200px" CssClass="TextBox form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="modal-label">Attached File</label>                        
                        <asp:FileUpload ID="fu" runat="server" ClientIDMode="Static" onchange="this.form.submit()" Style="position: relative; top: 0px; left: 0px;" CssClass="TextBox file-upload" />
                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="gvFileAttached" runat="server" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle BorderStyle="None" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbltest" Text='<%# Eval("FileName") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemStyle BorderStyle="None" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbRemove" runat="server" OnClick="Remove_Click">
                                            <asp:Label ID="lblRemove" Text="Remove" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-12 text-center">
                        <div>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
        <%--<table style="width: 750px;">
                            <tr>
                                <td style="width: 150px;">
                                    
                                </td>
                                <td>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSubcategory" runat="server" Text="Subcategory" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox"></asp:DropDownList></td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTicketType" runat="server" Style="position: relative;" CssClass="TextBox"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Subject" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" runat="server" Width="400px" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTicketDescription" runat="server" Text="Ticket Note" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Height="200px" Width="600px" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Contact Name" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactName" runat="server" Height="100%" Style="position: relative;" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Mobile No" CssClass="label"></asp:Label><div style="color: red">*</div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobileNo" runat="server" Height="100%" Style="position: relative;" CssClass="TextBox"></asp:TextBox></td>
                                <td></td>

                            </tr>
                            <%--   <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Priority Level" CssClass="label"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPriorityLevel" runat="server" Style="position: relative;" CssClass="TextBox" Text="1"></asp:TextBox></td>
                    </tr>--%>
        <%--  <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Attached File" CssClass="label"></asp:Label></td>
                                <td>
                                    <asp:FileUpload ID="fu" runat="server" ClientIDMode="Static" onchange="this.form.submit()" Style="position: relative; top: 0px; left: 0px;" CssClass="TextBox" />
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvFileAttached" runat="server" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle BorderStyle="None" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltest" Text='<%# Eval("test") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemStyle BorderStyle="None" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbRemove" runat="server" OnClick="Remove_Click">
                                                        <asp:Label ID="lblRemove" Text="Remove" runat="server" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="height: 30px;">
                                    </div>
                                    <div style="float: left; width: 45%; height: 60px;">
                                    </div>
                                    <div style="float: right; width: 55%; height: 60px; font-size: 19px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" OnClick="btnSave_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>--%>
        <%-- </td>
                    <td>--%>
        <div style="display: none">
            <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="TableView">
                <Columns>
                    <asp:TemplateField HeaderText="Ticket ID">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbTicketNo" runat="server">
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
                    <%-- <asp:TemplateField HeaderText="SubCategory">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketSubCategory" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory.SubCategory")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%><asp:TemplateField HeaderText="Priority Leave">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblTicketSeverity" Text='<%# DataBinder.Eval(Container.DataItem, "PriorityLeave")%>' runat="server"></asp:Label>--%>
                                    <asp:TextBox ID="txtPriorityLeave" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "PriorityLevel")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ticket Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label5" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server"></asp:Label>
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
                    <asp:TemplateField HeaderText="Status">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <%-- </td>
                </tr>
            </table>--%>
    </div>
</asp:Content>
