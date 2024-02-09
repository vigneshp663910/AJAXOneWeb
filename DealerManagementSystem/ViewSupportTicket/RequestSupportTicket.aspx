<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="RequestSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.RequestSupportTicket" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Scripts/styles.css" />
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
                    <asp:TemplateField HeaderText="Priority Leave">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
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
    </div>
</asp:Content>