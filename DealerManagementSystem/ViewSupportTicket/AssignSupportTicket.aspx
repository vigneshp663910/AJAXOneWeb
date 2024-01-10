<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="AssignSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.AssignSupportTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script>
        $('[data-url]').each(function () {
            var $this = $(this);
            $this.html('<a href="' + $this.attr('data-url') + '">' + $this.text() + '</a>');
        });
    </script>--%>

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Task Header</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
                        <Columns>
                            <asp:TemplateField HeaderText="Ticket ID">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderID")%>' runat="server" />
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
                            <asp:TemplateField HeaderText="Repeat">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRepeat" Text='<%# DataBinder.Eval(Container.DataItem, "Repeat")%>' runat="server"></asp:Label>
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
                            <asp:TemplateField HeaderText="Subject">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTicketDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
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
                            <%-- <asp:TemplateField HeaderText="Department">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.Department.DepartmentName")%>' runat="server"></asp:Label>
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
            </fieldset>
        </div>
        <br />
        <div class="form-container-fields">
            <span class="field-label">Ticket Form</span>
            <div class="row">
                <br />
                <div style="display: none">
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="Label3" runat="server" Text="Requested By" CssClass="label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtRequestedBy" runat="server" Enabled="false" CssClass="TextBox form-control" Visible="false" />
                        <asp:TextBox ID="txtRequestedOn" runat="server" CssClass="TextBox form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblTicketNo" runat="server" Text="Ticket No" CssClass="label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtTicketNo" runat="server" CssClass="TextBox form-control" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblTicketType" runat="server" Text="Ticket Type" CssClass="label" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlTicketType" runat="server" CssClass="TextBox form-control" Enabled="false" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="label" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox form-control" Enabled="false" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <asp:Label ID="lblTicketDescription" runat="server" Text="Requester Remark" CssClass="label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtRequesterRemark" runat="server" TextMode="MultiLine" Height="100%" CssClass="TextBox form-control" Enabled="false" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblSubcategory" runat="server" Text="Subcategory" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblSeverity" runat="server" Text="Severity" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlSeverity" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblAssignedTo" runat="server" Text="Assigned To" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label4" runat="server" Text="Attached File" CssClass="label"></asp:Label>
                    <asp:FileUpload ID="fu" runat="server" ClientIDMode="Static" onchange="this.form.submit()" CssClass="TextBox form-control" />

                    <asp:GridView ID="gvNewFileAttached" runat="server" ShowHeader="False" BorderStyle="None" AutoGenerateColumns="false">
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
                                    <asp:LinkButton ID="lbRemove" runat="server">
                                        <asp:Label ID="lblRemove" Text="Remove" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="gvFileAttached" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                        <Columns>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("text") %>' CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="Label2" runat="server" Text="Support Type" CssClass="label"></asp:Label>
                    <asp:DropDownList ID="ddlSupportType" runat="server" CssClass="TextBox form-control">
                        <asp:ListItem>L1</asp:ListItem>
                        <asp:ListItem>L2</asp:ListItem>
                        <asp:ListItem>L3</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12 col-sm-6">
                    <asp:Label ID="Label1" runat="server" Text="Assigner Remark" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtAssignerRemark" runat="server" TextMode="MultiLine" CssClass="TextBox form-control"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Assign" CssClass="InputButton btn Save" OnClick="btnSave_Click" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
