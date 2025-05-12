<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DealerBinLocation.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.DealerBinLocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Popup {
            transition: initial;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <asp:TabContainer ID="tbpBinLocation" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
        <asp:TabPanel ID="tpnlCreate" runat="server" HeaderText="Bin Creation" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12" id="divList" runat="server">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Dealer</label>
                                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Office</label>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-8 text-center">
                                        <label class="modal-label">-</label>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="80px" />
                                        <asp:Button ID="btnCreateBinLocation" runat="server" CssClass="btn Save" Text="Create" OnClick="btnCreateBinLocation_Click" Width="80px"></asp:Button>
                                        <asp:Button ID="BtnUploadBinLocation" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnUploadBinLocation_Click" />
                                        <asp:Button ID="btnDownloadBinLocation" runat="server" Text="Download Template" CssClass="btn Search" OnClick="btnDownloadBinLocation_Click" Width="180px" />
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
                                                            <asp:ImageButton ID="imgBtnExportExcelDealerBinLocation" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="imgBtnExportExcelDealerBinLocation_Click" ToolTip="Excel Download..." Width="23" Height="23" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvDealerBinLocation" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="gvDealerBinLocation_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeCodeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblOfficeCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bin Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerBinLocationID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBinLocationID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblBinName" Text='<%# DataBinder.Eval(Container.DataItem, "BinName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditDealerBinLocation" runat="server" OnClick="lnkEditDealerBinLocation_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDeleteDealerBinLocation" runat="server" OnClick="lnkDeleteDealerBinLocation_Click" OnClientClick="return ConfirmDelete();"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="HidDealerBinLocationID" runat="server" Visible="false" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel ID="tpnlConfiguration" runat="server" HeaderText="Bin Configuration" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12" runat="server">

                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image2" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Dealer</label>
                                        <asp:DropDownList ID="ddlCDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCDealer_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Office</label>
                                        <asp:DropDownList ID="ddlCOffice" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCOffice_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Bin</label>
                                        <asp:DropDownList ID="ddlCBin" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Material Code</label>
                                        <asp:TextBox ID="txtSMaterial" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnCSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" Width="80px" OnClick="btnCSearch_Click" />
                                        <asp:Button ID="btnCreateBinConfiguration" runat="server" CssClass="btn Save" Text="Create" Width="80px" OnClick="btnCreateBinConfiguration_Click"></asp:Button>
                                        <asp:Button ID="BtnUploadBinLocationConfig" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnUploadBinLocationConfig_Click" />
                                        <asp:Button ID="btnDownloadBinLocationConfig" runat="server" Text="Download Template" CssClass="btn Search" OnClick="btnDownloadBinLocationConfig_Click" Width="180px" />
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
                                                            <asp:Label ID="lblCRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnCArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnCArrowLeft_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnCArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnCArrowRight_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="imgBtnExportExcelDealerBinLocationMatMapping" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="imgBtnExportExcelDealerBinLocationMatMapping_Click" ToolTip="Excel Download..." Width="23" Height="23" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvDealerBinLocationConfig" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="gvDealerBinLocationConfig_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeCodeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblOfficeCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bin Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerBinLocationMaterialMappingID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBinLocationMaterialMappingID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblDealerBinLocationID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBinLocationID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblBinName" Text='<%# DataBinder.Eval(Container.DataItem, "BinName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Desc">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditDealerBinLocationConfig" runat="server" OnClick="lnkEditDealerBinLocationConfig_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDeleteDealerBinLocationConfig" runat="server" OnClick="lnkDeleteDealerBinLocationConfig_Click" OnClientClick="return ConfirmDeleteConfig();"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#ffffff" />
                                        <FooterStyle ForeColor="White" />
                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="HidDealerBinLocationMaterialMappingID" runat="server" Visible="false" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>


    <asp:Panel ID="pnlDealerBinLocationCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create Dealer Bin Location</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageDealerBinLocation" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Bin Location</legend>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Dealer<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlCDealerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCDealerCode_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Office<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlCOfficeName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Bin Name<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtBinName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_DealerBinLocationCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlDealerBinLocationCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlDealerBinLocationConfigCreate" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create Dealer Bin Location Configuration</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="PopupCloseConfig" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageDealerBinLocationConfig" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Bin Location Configuration</legend>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Dealer<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlCDealerConfig" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCDealerConfig_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Office<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlCDealerOfficeConfig" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCDealerOfficeConfig_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Bin<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlCDealerBinConfig" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Material<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSaveConfig" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveConfig_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_DealerBinLocationConfigCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlDealerBinLocationConfigCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlDealerBinLocationUpload" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Upload Dealer Bin Location</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMsg_DealerBinLocationUpload" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Bin Location</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlBinLocationDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBinLocationDealer_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Office</label>
                            <asp:DropDownList ID="ddlBinLocationOffice" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Upload</label>
                            <asp:FileUpload ID="fileUploadBinLocation" runat="server" />
                        </div>
                        <div class="col-md-12 col-sm-12 text-center">
                            <asp:Button ID="btnViewBinLocation" runat="server" Text="View" CssClass="btn Save" OnClick="btnViewBinLocation_Click" Width="100px" />
                            <asp:Button ID="BtnSaveBinLocation" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSaveBinLocation_Click" Width="100px" />
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="GVUploadBinLocation" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderText="Dealer" DataField="Dealer.DealerName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Dealer" DataField="DealerOffice.OfficeName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Bin Name" DataField="BinName"></asp:BoundField>
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
                    </div>

                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_DealerBinLocationUpload" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlDealerBinLocationUpload" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlDealerBinLocationConfigUpload" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Upload Dealer Bin Location Material Configuration</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMsg_DealerBinLocationConfigUpload" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Bin Location Material Configuration</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlBinLocationConfigDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBinLocationConfigDealer_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Office</label>
                            <asp:DropDownList ID="ddlBinLocationConfigOffice" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Upload</label>
                            <asp:FileUpload ID="fileUploadBinLocationConfig" runat="server" />
                        </div>
                        <div class="col-md-12 col-sm-12 text-center">
                            <asp:Button ID="btnViewBinLocationConfig" runat="server" Text="View" CssClass="btn Save" OnClick="btnViewBinLocationConfig_Click" Width="100px" />
                            <asp:Button ID="BtnSaveBinLocationConfig" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSaveBinLocationConfig_Click" Width="100px" />
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <asp:GridView ID="GVUploadBinLocationConfig" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderText="Dealer" DataField="Dealer.DealerName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Dealer" DataField="DealerOffice.OfficeName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Bin Name" DataField="BinName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Material" DataField="Material.MaterialCode"></asp:BoundField>
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
                    </div>

                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_DealerBinLocationConfigUpload" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlDealerBinLocationConfigUpload" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />



    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <script type="text/javascript">

        $(function () {

            $("#MainContent_txtMaterial").autocomplete({

                source: function (request, response) {

                    var param = { input: $('#MainContent_txtMaterial').val() };
                    $.ajax({
                        url: "DealerBinLocation.aspx/SearchSMaterial",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });
        });
        function ConfirmDelete() {
            var x = confirm("Are you sure you want to Delete Dealer Bin Location?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmDeleteConfig() {
            var x = confirm("Are you sure you want to Delete Dealer Bin Location Configuration?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
