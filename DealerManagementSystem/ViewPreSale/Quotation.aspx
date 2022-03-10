<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Quotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/SalesQuotationView.ascx" TagPrefix="UC" TagName="UC_QuotationView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                    <div class="col-md-12">

                       <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label> 

                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp1:TextBoxWatermarkExtender>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label> 
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp1:TextBoxWatermarkExtender>
                        </div>

                       <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer</label> 
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtCustomer" WatermarkText="Customer" WatermarkCssClass="WatermarkCssClass" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Mobile</label> 
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtMobile" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Country</label> 
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" />
                        </div>


                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddQuotation" runat="server" CssClass="btn Save" Text="Add Quotation" OnClick="btnAddQuotation_Click" Width="150px" Visible="false"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref Quotation ID">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuotationID" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblRefQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "RefQuotationNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RefQuotationDate">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "RefQuotationDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QuotationNo">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quotation Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quotation Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuotationType" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationType.QuotationType")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbViewCustomer" runat="server" OnClick="lbViewCustomer_Click">
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Customer.CustomerFullName")%>' runat="server" />
                                            </asp:LinkButton><asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Customer.CustomerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Customer.ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Customer.Mobile")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" Text='<%# DataBinder.Eval(Container.DataItem, "Lead.Customer.EMail")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewQuotation" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewQuotation_Click" Width="50px" Height="33px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>

                        </div>
                    </fieldset>
                 </div></div></div><div class="col-md-12" id="divColdVisitView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_QuotationView ID="UC_QuotationView" runat="server"></UC:UC_QuotationView>
        </div>
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Cold Visit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"> <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>


        <div class="col-md-12">
            <div style="display: none">
                <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox></div><div class="model-scroll">
                <asp:Label ID="lblMessageColdVisit" runat="server" Text="" CssClass="message" Visible="false" />
                <div id="divCustomerViewID" style="display: none">
                    <fieldset class="fieldset-border">
                        <div class="col-md-12">

                            <div class="col-md-2 text-right">
                                <label>Customer Name</label> </div><div class="col-md-4">
                                <label id="lblCustomerName"></label>
                            </div>
                            <div class="col-md-2 text-right">
                                <label>Contact Person</label> </div><div class="col-md-4">
                                <label id="lblContactPerson"></label>
                            </div>

                            <div class="col-md-2 text-right">
                                <label>Mobile</label> </div><div class="col-md-4">
                                <label id="lblMobile"></label>
                            </div>
                        </div>
                        <div id="divChangeCustomer">
                            <label>Change Customer</label> </div></fieldset> </div><%--  <div id="divCustomerCreateID">
                    <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                </div>--%><fieldset class="fieldset-border">
                    <div class="col-md-12">

                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Cold Visit Date</label> <asp:TextBox ID="txtColdVisitDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox></div><div class="col-md-6 col-sm-12">
                            <label class="modal-label">Action Type</label> <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Importance</label> <asp:DropDownList ID="ddlImportance" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Remark</label> <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox></div></div></fieldset> </div><div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

</asp:Content>
