<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketEscalationConfig.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.ICTicketEscalationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Escalation Hours</label>
                    <asp:DropDownList ID="ddlSEscalationHours" runat="server" CssClass="form-control">
                        <asp:ListItem Selected="True" Value="0">select</asp:ListItem>
                        <asp:ListItem Value="24 BasedOnModels">24 BasedOnModels</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="48">48</asp:ListItem>
                        <asp:ListItem Value="74">74</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-8 text-center">
                    <label class="modal-label">-</label>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" OnClick="btnSearch_Click" Width="80px" />
                    <asp:Button ID="btnCreate" runat="server" CssClass="btn Save" Text="Create" OnClick="btnCreate_Click" Width="80px"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="col-md-12 Report">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
            <div class="col-md-12 Report">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" OnClick="imgBtnExportExcel_Click" ToolTip="Excel Download..." Width="23" Height="23" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvICTicketEscalationConfig" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Region">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblEscalationMatrixID" Text='<%# DataBinder.Eval(Container.DataItem, "EscalationMatrixID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ToMailID">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblToMailID" Text='<%# DataBinder.Eval(Container.DataItem, "ToMailID")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CcMailID">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblCcMailID" Text='<%# DataBinder.Eval(Container.DataItem, "CcMailID")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EscalationHours">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblEscalationHours" Text='<%# DataBinder.Eval(Container.DataItem, "EscalationHours")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created By">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created On">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditICTicketEscalationConfig" runat="server" OnClick="lnkEditICTicketEscalationConfig_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                <asp:LinkButton ID="lnkDeleteICTicketEscalationConfig" runat="server" OnClientClick="return ConfirmDelete();" OnClick="lnkDeleteICTicketEscalationConfig_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
                <asp:HiddenField ID="HidEscalationMatrixID" runat="server" Visible="false" />
            </div>
        </fieldset>
    </div>
    <asp:Panel ID="pnlICTicketEscalationConfigCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create ICTicket Escalation Config</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageICTicketEscalationConfig" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">ICTicket Escalation Config</legend>
                    <div class="col-md-12">
                        <div class="col-md-12" id="divRegion" runat="server" visible="false">
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Region<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Subject<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">To MailID<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtToMailID" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Cc MailID<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtCcMailID" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-12" id="divEscalationHours" runat="server" visible="false">
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Escalation Hours<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlEscalationHours" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">select</asp:ListItem>
                                    <asp:ListItem Value="24 BasedOnModels">24 BasedOnModels</asp:ListItem>
                                    <asp:ListItem Value="24" Selected="True">24</asp:ListItem>
                                    <asp:ListItem Value="48">48</asp:ListItem>
                                    <asp:ListItem Value="74">74</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12" id="divDescription" runat="server" visible="false">
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">Description<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" OnClientClick="return ConfirmCreate();" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_ICTicketEscalationConfigCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlICTicketEscalationConfigCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
    <script type="text/javascript">
        function ConfirmDelete() {
            var x = confirm("Are you sure you want to Delete ICTicket Escalation Config?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmCreate() {
            var x = confirm("Are you sure you want to Create ICTicket Escalation Config?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
