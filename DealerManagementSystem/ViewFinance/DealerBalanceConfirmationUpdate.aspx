<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerBalanceConfirmationUpdate.aspx.cs" Inherits="DealerManagementSystem.ViewFinance.DealerBalanceConfirmationUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12" id="divList" runat="server">
        <fieldset class="fieldset-border" id="FldSearch" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">From Date</label>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp1:CalendarExtender ID="calendarextender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromDate" WatermarkText="DD/MM/YYYY" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">To Date</label>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtToDate" WatermarkText="DD/MM/YYYY" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Status</label>
                    <asp:DropDownList ID="ddlBalanceConfirmationStatus" runat="server" CssClass="form-control" />
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearch_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
            <div class="col-md-12 Report">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Dealer Balance Confirmation:</td>
                                    <td>
                                        <asp:Label ID="lblRowCountDealerBalConfirm" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerBalConfirmArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerBalConfirmArrowLeft_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnDealerBalConfirmArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerBalConfirmArrowRight_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <asp:GridView ID="gvDealerBalanceConfirmation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnPageIndexChanging="gvDealerBalanceConfirmation_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Code" ItemStyle-Width="30px">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                                <asp:Label ID="lblDealerBalanceConfirmationID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBalanceConfirmationID")%>' runat="server" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Date","{0:d}")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor Balance" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorBalance" Text='<%# DataBinder.Eval(Container.DataItem, "VendorBalance")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor Balance As Per Dealer" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorBalanceAsPerDealer" Text='<%# DataBinder.Eval(Container.DataItem, "VendorBalanceAsPerDealer")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Balance" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerBalance" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerBalance")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Balance As Per Dealer" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerBalanceAsPerDealer" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerBalanceAsPerDealer")%>' runat="server" />

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Outstanding As Per Ajax" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalOutstandingAsPerAjax" Text='<%# DataBinder.Eval(Container.DataItem, "TotalOutstandingAsPerAjax")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Outstanding As Per Dealer" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalOutstandingAsPerDealer" Text='<%# DataBinder.Eval(Container.DataItem, "TotalOutstandingAsPerDealer")%>' runat="server" />

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Currency">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCurrency" Text='<%# DataBinder.Eval(Container.DataItem, "Currency" )%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalanceConfirmationStatusG" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server" />
                                <asp:Label ID="lblBalanceConfirmationStatusIDG" Text='<%# DataBinder.Eval(Container.DataItem, "StatusID")%>' runat="server" Visible="false" />



                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnBalanceConfirmationEdit" runat="server" OnClick="lnkBtnBalanceConfirmationEdit_Click"><i class="fa fa-fw fa-edit" style="font-size: 18px"></i></asp:LinkButton>
                                <%-- <asp:Button ID="btnUpdateBalanceConfirmation" runat="server" Text="Update" CssClass="btn Back" OnClick="btnUpdateBalanceConfirmation_Click" Width="70px" Height="33px" Visible="false" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnUpdateBalanceConfirmation_Click" Width="70px" Height="33px" Visible="false" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <script type="text/javascript">

        function collapseExpand(obj) {
            var gvObject = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);
            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
    </script>



    <asp:Panel ID="pnlEdit" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueReachedSite">Edit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button16" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:Label ID="lblReachedSiteMessage" runat="server" Text="" CssClass="message" Visible="false" /> 
            <fieldset class="fieldset-border"> 
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Vendo rBalance As Per Dealer</label>
                        <asp:TextBox ID="txtVendorBalanceAsPerDealer" runat="server" CssClass="form-control" />
                        <asp:Label ID="lblDealerBalanceConfirmationID" runat="server" Visible="false" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer Balance As Per Dealere</label>
                        <asp:TextBox ID="txtCustomerBalanceAsPerDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Total Outstanding As Per Dealer</label>
                        <asp:TextBox ID="txtTotalOutstandingAsPerDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Balance Confirmation Status</label>
                        <asp:DropDownList ID="ddlBalanceConfirmationStatusG" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <table>
                            <tr style="background-color: white;">
                                <td>
                                    <asp:FileUpload ID="fu" runat="server" CssClass="form-control" ViewStateMode="Inherit" /></td>
                                <td>
                                    <asp:LinkButton ID="lblAttachedFileAdd" runat="server" OnClick="lblAttachedFileAddR_Click" CssClass="LabelValue">Add</asp:LinkButton></td>
                            </tr>
                        </table>


                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                    </div>
                </div>
            </fieldset>

            <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" ShowHeader="false" CssClass="TableGrid" DataKeyNames="AttachedFileID" ShowFooter="false">
                <Columns>
                    <asp:TemplateField>
                        <%-- <ItemStyle BorderStyle="None" />--%>
                        <ItemTemplate>
                            <asp:Label ID="lbltest" Text='<%# Eval("FileName") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:UpdatePanel ID="upManage" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownloadR_Click" Text="Download">
                                    </asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkDownload" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lblAttachedFileRemoveR_Click">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Edit" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />



    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>
