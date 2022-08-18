<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ReassignSupportTicket.aspx.cs" Inherits="DealerManagementSystem.ViewSupportTicket.ReassignSupportTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

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


    <style>
        .ChatButton {
            position: fixed;
            right: 0;
            top: 183px;
            list-style: none;
        }

        .ChatDataDisplay {
            position: fixed;
            display: block;
            right: 50px;
        }

        .ChatDataNoneDisplay {
            position: fixed;
            display: none;
            right: 10px;
        }

        .TextAlignRight {
            text-align: right;
        }

        .Left {
            float: left;
        }

        .Right {
            float: right;
        }
    </style>
    <script>
        document.getElementById('lblMessage').scrollIntoView(true);
        function toggleColor() {
            if ($('#MainContent_Div1').attr('class') == "ChatDataNoneDisplay") {
                $('#MainContent_Div1').removeClass('ChatDataNoneDisplay')
                $('#MainContent_Div1').addClass('ChatDataDisplay')
            }
            else {
                $('#MainContent_Div1').removeClass('ChatDataDisplay')
                $('#MainContent_Div1').addClass('ChatDataNoneDisplay')
            }
        }
    </script>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <%-- <div id="Chat" runat="server" class="ChatButton">

        <img src="Images/Char.png" alt="Quick Links" width="70" height="118" onclick="toggleColor()" />
    </div>--%>
    <div class="container-fluid form-container">
        <div class="form-container-fields">
            <span class="field-label">Task Header</span>
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
                </Columns>
                 <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>
        </div>
        <br />
        <div class="form-container-fields">
            <span class="field-label">Task Details</span>
            <asp:GridView ID="gvTicketItem" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Status">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "ItemStatus.Status")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned To">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedTo" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedTo.ContactName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned By">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Effort (H)">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resolution Type">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTicketResolutionType" Text='<%# DataBinder.Eval(Container.DataItem, "ResolutionType.ResolutionType")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resolution">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblResolution" Text='<%# DataBinder.Eval(Container.DataItem, "Resolution")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TR Number">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TRNumber")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TR Moved">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTicketResolutionType" Text='<%# DataBinder.Eval(Container.DataItem, "TRClosed")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TR Moved On">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblTRClosedOn" Text='<%# DataBinder.Eval(Container.DataItem, "TRClosedOn")%>' runat="server"></asp:Label>
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
        <br />
        <div class="form-container-fields">
            <span class="field-label">Task Form</span>
            <div class="row">
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblActualDuration" runat="server" Text="Worked (H)" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:TextBox ID="txtActualDuration" runat="server" CssClass="TextBox form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-6">
                    <asp:Label ID="lblAssignedTo" runat="server" Text="Assigned To" CssClass="label"></asp:Label><span style="color: red">*</span>
                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
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
                    <asp:Label ID="Label1" runat="server" Text="Assigner   Remark" CssClass="label"></asp:Label>
                    <asp:TextBox ID="txtAssignerRemark" runat="server" TextMode="MultiLine" CssClass="TextBox form-control"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnReassign" runat="server" Text="Reassign" CssClass="InputButton btn Save" OnClick="btnReassign_Click" />
                </div>

            </div>
        </div>

    </div>
</asp:Content>
