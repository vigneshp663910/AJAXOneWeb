<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcProductGroup.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcProductGroup" %>

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
                    <label class="modal-label">Product Group</label>
                    <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">PG Code</label>
                    <asp:TextBox ID="txtPGCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
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
                    <asp:Button ID="btnCreateProductGroup" runat="server" CssClass="btn Save" Text="Create Product Group" Width="160px" OnClick="btnCreateProductGroup_Click"></asp:Button>
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
                                            <td>Product Group(s):</td> 
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="gvProductGroup" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="PG Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPGCode" Text='<%# DataBinder.Eval(Container.DataItem, "PGCode")%>' runat="server" />
                                            <asp:Label ID="lblSpcProductGroupID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcProductGroupID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PG Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPGDescription" Text='<%# DataBinder.Eval(Container.DataItem, "PGDescription")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivitionID" Text='<%# DataBinder.Eval(Container.DataItem, "Division.DivisionID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblDivition" Text='<%# DataBinder.Eval(Container.DataItem, "Division.DivisionCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PGS Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPGSCode" Text='<%# DataBinder.Eval(Container.DataItem, "PGSCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEditProductGroup" runat="server" OnClick="lblEditProductGroup_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDeleteProductGroup" runat="server" OnClick="lnkBtnDeleteProductGroup_Click"  
                                                 OnClientClick='<%# "return Confirmation(\"" +"Are you sure you want to delete : " + Eval("PGCode") +" ?"  + "\");" %>'
                                                >
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

    <asp:Panel ID="pnlProductGroupCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create / Update Product Group</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <asp:Label ID="lblModelEditMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblSpcProductGroupID" runat="server" Text="0" Visible="false" />
                <div class="col-md-5 col-sm-12">
                    <label>Division</label>
                    <asp:DropDownList ID="ddlDivisionC" runat="server" CssClass="form-control"  />
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>PG Code</label>
                    <asp:TextBox ID="txtSpcPGCodeC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div> 
                <div class="col-md-5 col-sm-12">
                    <label>PG Description</label>
                     <asp:TextBox ID="txtSpcPGDescriptionC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"  ></asp:TextBox>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>PGS Code</label>     
                    <asp:TextBox ID="txtSpcPGSCodeC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"  ></asp:TextBox>
                </div> 
                <div class="col-md-5 col-sm-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarksC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-12">
                    <label>Active</label>
                     <div>
                    <asp:CheckBox ID="cbActive" runat="server" Checked="true"  /></div>
                </div> 
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSpcProductGroupSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSpcProductGroupSave_Click" OnClientClick="return Confirmation('Are you sure you want to save?');" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_ProductGroupCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlProductGroupCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

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

