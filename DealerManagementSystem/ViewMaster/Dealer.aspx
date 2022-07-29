<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Dealer.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Dealer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/DealerView.ascx" TagPrefix="UC" TagName="UC_DealerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .portlet.box.green {
            border: 1px solid #5cd1db;
            border-top: 0;
        }

            .portlet.box.green > .portlet-title {
                background-color: #32c5d2;
            }

                .portlet.box.green > .portlet-title > .caption {
                    color: #fff;
                }

        .pull-right {
            float: right !important;
        }

        .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all).btn-sm {
            font-size: 11px;
            padding: 6px 18px 6px 18px;
        }

        .btn.yellow:not(.btn-outline) {
            color: #fff;
            background-color: #c49f47;
            border-color: #c49f47;
        }

        .form-group {
            margin-bottom: 5px;
        }

        b, optgroup, strong {
            font-weight: 700;
        }
    </style>


    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblDealerMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divDealerList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label>Region</label>
                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddDealer" runat="server" CssClass="btn Save" Text="Create Dealer" OnClick="btnAddDealer_Click" Width="150px" Visible="false" ></asp:Button>
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
                                                <td>Dealer(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDealerArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnDealerArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnDealerArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnDealerArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <asp:GridView ID="gvDealer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealer_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate> 
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" /> 
                                            <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Name">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Email")%>'><%# DataBinder.Eval(Container.DataItem, "Email")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "Country")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Region">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region.Region")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "StateN.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Team Lead">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTL" Text='<%# DataBinder.Eval(Container.DataItem, "TL.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Manager">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSM" Text='<%# DataBinder.Eval(Container.DataItem, "SM.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewCustomer" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewDealer_Click" Width="75px" Height="25px" />
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
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divDealerView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12" runat="server" id="tblDashboard">
                <UC:UC_DealerView ID="UC_DealerView" runat="server"></UC:UC_DealerView>

            </div>
        </div>


        <%--  <div class="col-md-12">
        <asp:Panel ID="pnlDealer" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Create Dealer</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
            </div>
            <div class="col-md-12">
                <div class="model-scroll">
                    <asp:Label ID="lblMessageCustomer" runat="server" Text="" CssClass="message" Visible="false" />
                    <UC:UC_DealerCreate ID="UC_Dealer" runat="server"></UC:UC_DealerCreate>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="MPE_Dealer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
    </div>--%>
    </div>


</asp:Content>
