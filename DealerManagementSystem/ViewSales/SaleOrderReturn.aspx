<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SaleOrderReturn.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SaleOrderReturn" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/ViewSales/UserControls/SaleOrderReturnView.ascx" TagPrefix="UC" TagName="UC_SaleOrderReturnView" %>
<%@ Register Src="~/ViewSales/UserControls/SaleOrderReturnCreate.ascx" TagPrefix="UC" TagName="UC_SaleOrderReturnCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function GetCustomers() {
            $("#MainContent_UC_SaleOrderReturnCreate_hdfCustomerId").val('');
            var param = { CustS: $('#MainContent_UC_SaleOrderReturnCreate_txtCustomer').val() };
            var Customers = [];
            if ($('#MainContent_UC_SaleOrderReturnCreate_txtCustomer').val().trim().length >= 3) {
                $.ajax({
                    url: 'SaleOrderReturn.aspx/GetCustomer',
                    contentType: "application/json; charset=utf-8",
                    type: 'POST',
                    data: JSON.stringify(param),
                    dataType: 'JSON',
                    success: function (data) {
                        var DataList = JSON.parse(data.d);
                        if (DataList != null && DataList.length > 0) {
                            for (i = 0; i < DataList.length; i++) {
                                Customers[i] = {
                                    value: DataList[i].CustomerName,
                                    value1: DataList[i].CustomerID,
                                    ContactPerson: DataList[i].ContactPerson,
                                    Mobile: DataList[i].Mobile,
                                    CustomerType: DataList[i].CustomerType,
                                    Address: DataList[i].Address1
                                };
                            }
                        }
                        $('#MainContent_UC_SaleOrderReturnCreate_txtCustomer').autocomplete({
                            source: function (request, response) {
                                response(Customers)
                            },
                            select: function (e, u) {
                                $("#MainContent_UC_SaleOrderReturnCreate_hdfCustomerId").val(u.item.value1);
                            },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $('#MainContent_UC_SaleOrderReturnCreate_txtCustomer').width() + 48,
                                });
                                $(this).autocomplete("widget").scrollTop(0);
                            }
                        }).focus(function (e) {
                            $(this).autocomplete("search");
                        }).click(function () {
                            $(this).autocomplete("search");
                        }).data('ui-autocomplete')._renderItem = function (ul, item) {
                            var inner_html = FormatAutocompleteList(item);
                            return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;"></li>')
                                .data('item.autocomplete', item)
                                .append(inner_html)
                                .appendTo(ul);
                        };

                    }
                });
            }
            else {
                $('#MainContent_UC_SaleOrderReturnCreate_txtCustomer').autocomplete({
                    source: function (request, response) {
                        response($.ui.autocomplete.filter(Customers, ""))
                    }
                });
            }
        }

        function FormatAutocompleteList(item) {

            var inner_html = '<a class="customer">';
            inner_html += '<p class="customer-name-info"><label>' + item.value + '</label></p>';
            inner_html += '<div class=customer-info><label class="contact-number">Contact :' + item.ContactPerson + '(' + item.Mobile + ') </label>';
            inner_html += '<label class="customer-type">' + item.CustomerType + '</label></div>';
            inner_html += '<p class="customer-address"><label>' + item.Address + '</label></p>';
            inner_html += '</a>';
            return inner_html;
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessageSoReturn" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Office</label>
                            <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Division</label>
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer Code</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Sales Return Number</label>
                            <asp:TextBox ID="txtSoReturnNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Sales Return Date From</label>
                            <asp:TextBox ID="txtSoReturnDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSoReturnDateFrom" PopupButtonID="txtSoReturnDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSoReturnDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Sales Return Date To</label>
                            <asp:TextBox ID="txtSoReturnDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSoReturnDateTo" PopupButtonID="txtSoReturnDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSoReturnDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Sales Return Status</label>
                            <asp:DropDownList ID="ddlReturnStatus" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="65px" />
                            <asp:Button ID="btnCreateSoReturn" runat="server" CssClass="btn Save" Text="Create Sales Return" OnClick="btnCreateSoReturn_Click" Width="150px"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Sales Return(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCountSoReturn" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeftSoReturn" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftSoReturn_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRightSoReturn" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightSoReturn_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvSoReturn" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewSoReturn" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewSoReturn_Click" Width="75px" Height="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Return Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleOrderReturnID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderReturnID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblSaleOrderNumberReturn" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderReturnNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblSaleOrderReturnDate" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderReturnDate","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDelivery.SaleOrder.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDelivery.SaleOrder.Dealer.DealerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Office">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDelivery.SaleOrder.Dealer.DealerOffice.OfficeName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDelivery.SaleOrder.Division.DivisionCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDelivery.SaleOrder.Customer.CustomerCode")%>' runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDelivery.SaleOrder.Customer.CustomerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Return Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoReturnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ReturnStatus.Status")%>' runat="server"></asp:Label>
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
        <div class="col-md-12" id="divSoReturnDetailsView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="SoReturnViewboxHere"></div>
                <div class="back-buttton" id="SoReturnViewBackBtn" style="text-align: right">
                    <asp:Button ID="btnSaleOrderReturnViewBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnSaleOrderReturnViewBack_Click" />
                </div>
            </div>
            <UC:UC_SaleOrderReturnView ID="UC_SaleOrderReturnView" runat="server"></UC:UC_SaleOrderReturnView>
        </div>
        <div class="col-md-12" id="divSaleOrderReturnCreate" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="SoReturnCreateboxHere"></div>
                <div class="back-buttton" id="SoReturnCreateBackBtn" style="text-align: right">
                    <asp:Button ID="btnSaleOrderReturnCreateBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnSaleOrderReturnCreateBack_Click" />
                </div>
            </div>
            <asp:Label ID="lblMessageSoReturnCreate" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-12">
                <%--<fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>--%>
                <UC:UC_SaleOrderReturnCreate ID="UC_SaleOrderReturnCreate" runat="server"></UC:UC_SaleOrderReturnCreate>
                <div class="col-md-12 text-center" id="divCreateSalesReturn" runat="server">
                    <asp:Button ID="btnCreateSalesReturn" runat="server" Text="Create Sales Return" CssClass="btn Search" OnClick="btnCreateSalesReturn_Click" Width="150px" />
                </div>
                <div class="col-md-12 text-center" id="divSave" runat="server" visible="false">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" Width="100px" />
                </div>
                <%--</fieldset>--%>
            </div>
        </div>
    </div>
</asp:Content>
