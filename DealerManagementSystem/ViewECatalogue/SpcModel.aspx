<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcModel.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcModel" %>

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
                    <label class="modal-label">Model/PM Code</label>
                    <asp:TextBox ID="txtModelCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-2 col-sm-12">
                    <label>Active</label>
                    <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">true</asp:ListItem>
                        <asp:ListItem Value="0">false</asp:ListItem>
                    </asp:DropDownList>
                </div>
                  <div class="col-md-2 col-sm-12">
                    <label>Publish</label>
                    <asp:DropDownList ID="ddlPublish" runat="server" CssClass="form-control">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">true</asp:ListItem>
                        <asp:ListItem Value="0">false</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12">

                    <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                    <asp:Button ID="btnCreateModel" runat="server" CssClass="btn Save" Text="Create Model" Width="120px" OnClick="btnCreateModel_Click"></asp:Button>
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
                                            <td>Model(s):</td>

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
                            <asp:GridView ID="gvModel" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SlNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlNo" Text='<%# DataBinder.Eval(Container.DataItem, "SlNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model / PM Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpcModelCode" Text='<%# DataBinder.Eval(Container.DataItem, "SpcModelCode")%>' runat="server" />
                                            <asp:Label ID="lblSpcModelID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcModelID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Model Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpcModel" Text='<%# DataBinder.Eval(Container.DataItem, "SpcModel")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PG Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpcProductGroupID" Text='<%# DataBinder.Eval(Container.DataItem, "ProductGroup.SpcProductGroupID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblPGCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductGroup.PGCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purpose">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurpose" Text='<%# DataBinder.Eval(Container.DataItem, "PurposeDetail")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Publish">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsPublish" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPublish")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialGroup" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialGroup.MaterialGroup")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEditModel" runat="server" OnClick="lblEditModel_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDeleteModel" runat="server" OnClick="lnkBtnDeleteModelp_Click" 
                                                 OnClientClick='<%# "return Confirmation(\"" +"Are you sure you want to delete : " + Eval("SpcModelCode") +" ?"  + "\");" %>' 

                                                > <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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

    <asp:Panel ID="pnlModelCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create / Update Model</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <asp:Label ID="lblModelEditMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblSpcModelID" runat="server" Text="0" Visible="false" />
                <div class="col-md-5 col-sm-12">
                    <label>Product Group</label>
                    <asp:DropDownList ID="ddlSpcProductGroupC" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSpcProductGroupC_SelectedIndexChanged" />
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>SlNo</label>
                    <asp:TextBox ID="txtSlNoC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                 <div class="col-md-5 col-sm-12">
                    <label>Material Group</label>
                    <asp:DropDownList ID="ddlSpcMaterialGroupC" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged ="ddlSpcMaterialGroupC_SelectedIndexChanged"/>
                </div> 
                <div class="col-md-5 col-sm-12">
                    <label>Model / PM Code</label>
                    <asp:Label ID="lblSpcModelCodeC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:Label>
                    <asp:TextBox ID="txtSpcModelCodeC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Product Model Description</label>
                    <asp:Label ID="lblSpcModelC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" ></asp:Label>
                    <asp:TextBox ID="txtSpcModelC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Purpose</label>
                    <asp:DropDownList ID="ddlPurposeC" runat="server" CssClass="form-control">
                        <asp:ListItem Value="C">Catalogue</asp:ListItem>
                        <asp:ListItem Value="M">MBR</asp:ListItem>
                        <asp:ListItem Value="B">Both</asp:ListItem>
                    </asp:DropDownList>
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
                <div class="col-md-3 col-sm-12">
                    <label>Publish</label>
                    <div>
                    <asp:CheckBox ID="cbPublish" runat="server"  Checked="true" /></div>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSpcModelSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSpcModelSave_Click" OnClientClick="return Confirmation('Are you sure you want to save?');" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_ModelCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlModelCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

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
