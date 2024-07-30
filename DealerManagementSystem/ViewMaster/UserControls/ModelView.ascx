<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModelView.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.ModelView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lnkEditModel" runat="server" OnClick="lnkBtnActions_Click">Edit Model</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnAddDrawing" runat="server" OnClick="lnkBtnActions_Click">Add Drawing</asp:LinkButton>
                <asp:LinkButton ID="lnkAddSpecification" runat="server" OnClick="lnkBtnActions_Click">Add Specification</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div runat="server" class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Model View</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Make : </label>
                    <asp:Label ID="lblMake" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Product Type : </label>
                    <asp:Label ID="lblProductType" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Model : </label>
                    <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:TabContainer ID="tbpPresalesMasters" runat="server" ToolTip="Presales Masters..." Font-Bold="True" Font-Size="Medium">
    <asp:TabPanel ID="tbPnlProduct" runat="server" HeaderText="Drawing" Font-Bold="True" ToolTip="Drawing">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvProductDrawing" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drawing Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductDrawingType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductDrawingType.ProductDrawingTypeName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drawing Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDrawingDescription" Text='<%# DataBinder.Eval(Container.DataItem, "DrawingDescription")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ProductDrawingID") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                    <asp:Label ID="lblFileType" Text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblProductDrawingDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductDrawingID")%>' OnClick="lblProductDrawingDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tbPnlProductSpecification" runat="server" HeaderText="Specification" Font-Bold="True" ToolTip="Specification">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <asp:GridView ID="GVProductSpecification" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification Text">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecificationText" Text='<%# DataBinder.Eval(Container.DataItem, "SpecificationText")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecificationDescription" Text='<%# DataBinder.Eval(Container.DataItem, "SpecificationDescription")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OrderBy No">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderByNo" Text='<%# DataBinder.Eval(Container.DataItem, "OrderByNo")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblProductSpecificationEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductSpecificationID")%>' OnClick="lblProductSpecificationEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lblProductSpecificationDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductSpecificationID")%>' OnClick="lblProductSpecificationDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:HiddenField ID="Hid_ProductSpecificationID" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>
<asp:Panel ID="pnlAddDrawing" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueAddDrawing">Add Drawing</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnAddDrawingClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAddDrawingMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-6">
                    <label class="modal-label">Drawing Type</label>
                    <asp:DropDownList ID="ddlDrawingType" runat="server" CssClass="form-control" DataTextField="ProductDrawingTypeName" DataValueField="ProductDrawingTypeID" />
                </div>
                <div class="col-md-12">
                    <label class="modal-label">Drawing Description</label>
                    <asp:TextBox ID="txtDrawingDescription" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUpload" runat="server" accept=".png,.jpg,.jpeg,.gif" /></td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddDrawing" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddDrawing_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddDrawing" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddDrawing" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEditProduct" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueEditProduct">Edit Model</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnEditProductClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblEditProductMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <label class="modal-label">Make</label>
                    <asp:DropDownList ID="ddlProductMake" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <label class="modal-label">Product Type</label>
                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <label class="modal-label">Model</label>
                    <asp:TextBox ID="txtProduct" runat="server" placeholder="Product" CssClass="form-control"></asp:TextBox>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateProduct" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateProduct_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EditProduct" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEditProduct" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlProductSpecification" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueProductSpecification">Product Specification</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnProductSpecificationClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblProductSpecificationMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <label class="modal-label">Specification Text</label>
                        <asp:TextBox ID="txtSpecText" runat="server" placeholder="Specification Text" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="modal-label">OrderBy No</label>
                        <asp:TextBox ID="txtOrderByNo" runat="server" placeholder="OrderBy No" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <label class="modal-label">Specification Description</label>
                        <asp:TextBox ID="txtSpecDesc" runat="server" placeholder="Specification Description" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveProductSpecification" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveProductSpecification_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ProductSpecification" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlProductSpecification" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
