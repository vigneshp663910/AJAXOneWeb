<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcMaterialGroup.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcMaterialGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />

    <div class="col-md-12">

        <fieldset id="fsCriteria" class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
            <div class="col-md-12">

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Material Group</label>
                    <asp:TextBox ID="txtMaterialGroupCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Active</label>
                    <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">true</asp:ListItem>
                        <asp:ListItem Value="0">false</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12">

                    <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                    <asp:Button ID="btnCreateMaterialGroup" runat="server" CssClass="btn Save" Text="Create Material Group" Width="160px" OnClick="btnCreateMaterialGroup_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
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
                                            <td>Material Group(s):</td>

                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="imgBtnExportExcel_Click" ToolTip="Excel Download..." />
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="gvMaterialGroup" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialGroup" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialGroup")%>' runat="server" />
                                            <asp:Label ID="lblSpcMaterialGroupID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcMaterialGroupID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription1" Text='<%# DataBinder.Eval(Container.DataItem, "Description1")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description 2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription2" Text='<%# DataBinder.Eval(Container.DataItem, "Description2")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMTypeDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MType    ")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEditMaterialGroup" runat="server" OnClick="lblEditMaterialGroup_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDeleteMaterialGroup" runat="server" OnClick="lnkBtnDeleteMaterialGroup_Click" 
                                                  OnClientClick='<%# "return Confirmation(\"" +"Are you sure you want to delete : " + Eval("MaterialGroup") +" ?"  + "\");" %>'> 
                                                <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
                </fieldset>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlMaterialGroupCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create / Update Material Group</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <asp:Label ID="lblMaterialGroupEditMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblSpcMaterialGroupID" runat="server" Text="0" Visible="false" />
                <div class="col-md-5 col-sm-12">
                    <label>Material Group</label>
                    <asp:TextBox ID="txtMaterialGroupC" runat="server" MaxLength="9" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" OnTextChanged="txtMaterialGroupC_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Description 1</label>
                    <asp:Label ID="lblMaterialGroupDescription1C" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Description 2</label>
                    <asp:Label ID="lblMaterialGroupDescription2C" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Material Type</label>
                    <asp:DropDownList ID="ddlMaterialTypeC" runat="server" CssClass="form-control">
                        <asp:ListItem Value="X"></asp:ListItem>
                        <asp:ListItem Value="FERT">FERT</asp:ListItem>
                        <asp:ListItem Value="HALB">HALB</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3 col-sm-12">
                    <label>Active</label>
                    <div>
                        <asp:CheckBox ID="cbActive" runat="server" Checked="true" />
                    </div>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSpcModelSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSpcModelSave_Click" OnClientClick="return Confirmation('Are you sure you want to save?');" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_MaterialGroupCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMaterialGroupCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
    <script type="text/javascript"> 

        function Confirmation(Message) {
            var x = confirm(Message);
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>

