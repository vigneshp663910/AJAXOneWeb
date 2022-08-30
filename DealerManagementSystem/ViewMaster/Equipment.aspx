<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Equipment.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Equipment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerView.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfLatitude" runat="server" />
    <asp:HiddenField ID="hfLongitude" runat="server" />
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"/>
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer  Name</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer Code</label>
                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Mobile</label>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Country</label>
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddColdVisit" runat="server" CssClass="btn Save" Text="Create Customer" OnClick="btnAddColdVisit_Click" Width="150px"></asp:Button>
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                        </div>
                    </div>
                </fieldset>
            </div>
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
                                                <td>Equipment(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnCustArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnCustArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnCustArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnCustArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div> 
                            <asp:GridView ID="gvEquipment" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCustomer_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <%--  <asp:LinkButton ID="lbViewCustomer" runat="server" OnClick="lbViewCustomer_Click">--%>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFullName")%>' runat="server" />
                                            <%--   </asp:LinkButton>--%>
                                            <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                            <br />
                                             <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EMail")%>'><%# DataBinder.Eval(Container.DataItem, "EMail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Draft">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbDraft" runat="server" Enabled="false" CssClass="mycheckBig" Checked ='<%# DataBinder.Eval(Container.DataItem, "IsDraft")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewCustomer" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewCustomer_Click" Width="75px" Height="25px" />
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
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divCustomerView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12" runat="server" id="tblDashboard">
                <UC:UC_CustomerView ID="UC_CustomerView" runat="server"></UC:UC_CustomerView>
                <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
                <div class="col-md-12 text-center">
                </div>
            </div>
        </div>
        
    </div>



    <div class="col-md-12">
        <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Create Customer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
            </div>
            <div class="col-md-12">
                <div class="model-scroll">
                    <asp:Label ID="lblMessageCustomer" runat="server" Text="" CssClass="message" Visible="false" />
                    <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

     
</asp:Content>