<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LeadDeviation.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.LeadDeviation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlSDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Date From</label>
                        <asp:TextBox ID="txtSDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Date To</label>
                        <asp:TextBox ID="txtSDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSSource" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Customer</label>
                        <asp:TextBox ID="txtSCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Region</label>
                        <asp:DropDownList ID="ddlSRegion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Product Type</label>
                        <asp:DropDownList ID="ddlSProductType" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        <asp:Button ID="btnAddLead" runat="server" CssClass="btn Save" Text="Add Lead" OnClick="btnAddLead_Click" Width="150px"></asp:Button>
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true"  Width="100px" />
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Lead(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnLeadArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeadArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnLeadArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnLeadArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                            PageSize="10" AllowPaging="true"   EmptyDataText="No Data Found">
                            <Columns>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn Back" Width="75px" Height="25px" OnClick="btnApprove_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate> 
                                        <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Dealer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate> 
                                        <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Customer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expected Date Of Sale">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpectedDateOfSale" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedDateOfSale")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Application">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMainApplication" Text='<%# DataBinder.Eval(Container.DataItem, "MainApplication")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Customer Feedback">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerFeedback" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFeedback")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Next FollowUp Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNextFollowUpDate" Text='<%# DataBinder.Eval(Container.DataItem, "NextFollowUpDate")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="B2B">
                                    <ItemTemplate>
                                        <asp:Label ID="lblB2B" Text='<%# DataBinder.Eval(Container.DataItem, "B2B")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedByName")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Appoved">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAppovedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AppovedBy")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblAppovedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AppovedOn")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
    <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Lead</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageLead" runat="server" Text="" CssClass="message" Visible="false" />
                <fieldset class="fieldset-border" id="fldCountry" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Customer</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Product Type</label>
                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Source</label>
                            <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Expected Date of Sale</label>
                            <asp:TextBox ID="txtExpectedDateOfSale" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="cxExpectedDateOfSale" runat="server" TargetControlID="txtExpectedDateOfSale" PopupButtonID="txtExpectedDateOfSale" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtExpectedDateOfSale" WatermarkText="DD/MM/YYYY" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Application</label>
                            <asp:DropDownList ID="ddlApplication" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label>Customer Feedback</label>
                            <asp:TextBox ID="txtCustomerFeedback" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label>Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Next FollowUp Date</label>
                            <asp:TextBox ID="txtNextFollowUpDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="cxNextFollowUpDate" runat="server" TargetControlID="txtNextFollowUpDate" PopupButtonID="txtNextFollowUpDate" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtNextFollowUpDate" WatermarkText="DD/MM/YYYY" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

</asp:Content>
