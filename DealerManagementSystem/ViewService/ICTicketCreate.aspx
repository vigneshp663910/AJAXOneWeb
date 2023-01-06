<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketCreate.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" rel="Stylesheet">
    <script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>

    <script>
        function GetCustomers() {
            $("#MainContent_hdfCustomerId").val('');
            var param = { CustS: $('#MainContent_txtCustomer').val() };
            var Customers = [];
            if ($('#MainContent_txtCustomer').val().trim().length >= 3) {
                $.ajax({
                    url: 'ICTicketCreate.aspx/GetCustomer',
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
                                    //  value2: DataList[i].CustomerName + ',' + DataList[i].CustomerName
                                    // value3: data.list[i].CustomerType, value4: data.list[i].MobileNumber,
                                    //  value5: data.list[i].CustomerCode
                                };
                            }
                        }

                        //for (i = 0; i < 10; i++) {
                        //    Customers[i] = { value: 'Customer ' + i };
                        //}
                        // alert(JSON.stringify(Customers));
                        $('#MainContent_txtCustomer').autocomplete({
                            source: function (request, response) {
                                response(Customers)
                            },
                            select: function (e, u) {
                                $("#MainContent_hdfCustomerId").val(u.item.value1);
                            },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $('#MainContent_txtCustomer').width() + 48,
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
                $('#MainContent_txtCustomer').autocomplete({
                    source: function (request, response) {
                        response($.ui.autocomplete.filter(Customers, ""))
                    }
                });
            }
        }





        function GetCustomers1() {
            var param = { CustS: $('#MainContent_txtCustomer').val() };
            var Customers = [];
            if ($('#MainContent_txtCustomer').val().trim().length >= 3) {
                $.ajax({
                    url: 'ICTicketCreate.aspx/GetCustomer',
                    contentType: "application/json; charset=utf-8",
                    type: 'POST',
                    data: JSON.stringify(param),
                    dataType: 'JSON',
                    success: function (data) {
                        for (i = 0; i < 10; i++) {
                            Customers[i] = { value: 'Customer ' + i };
                        }
                        $('#MainContent_txtCustomer').autocomplete({
                            source: function (request, response) { response(Customers) },
                            select: function (e, u) { $("#MainContent_txtCustomer").val(u.item.value); },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $('#MainContent_txtCustomer').width() + 48,
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
                $('#MainContent_txtCustomer').autocomplete({
                    source: function (request, response) {
                        response($.ui.autocomplete.filter(Customers, ""))
                    }
                });
            }
        }



        //function GetCustomers() {

        //    if ($('#MainContent_txtCustomer').val().trim().length > 2) {
        //      //  $('<span class="fa fa-refresh fa-spin autocompleteLoader"></span>').insertAfter('#MainContent_txtCustomer');
        //        var param = { CustS: $('#MainContent_txtCustomer').val() };
        //        var customers = [];
        //        debugger;
        //        $.ajax({
        //            type: 'POST',
        //            contentType: "application/json; charset=utf-8",
        //            url: 'ICTicketCreate.aspx/GetCustomer',
        //            /*data: { searchString: $('#MainContent_txtCustomer').val() },*/
        //            data: JSON.stringify(param),
        //            dataType: 'JSON',
        //            success: function (data) {
        //                debugger;
        //                var DataList = JSON.parse(data.d);
        //                if (DataList != null && DataList.length > 0) {
        //                    for (i = 0; i < DataList.length; i++) {
        //                        customers[i] = {
        //                            value: DataList[i].CustomerName,
        //                            value1: DataList[i].CustomerID,
        //                            value2: DataList[i].CustomerName + ',' + DataList[i].CustomerName
        //                            // value3: data.list[i].CustomerType, value4: data.list[i].MobileNumber,
        //                            //  value5: data.list[i].CustomerCode
        //                        };
        //                    }
        //                    debugger;
        //                    $('#MainContent_txtCustomer').autocomplete({
        //                        source: function (request, response) { response($.ui.autocomplete.filter(customers, "")); debugger;},
        //                        minLength: 2,
        //                        select: function (e, u) { $('#MainContent_hdfCustomerId').val(u.item.value1); debugger;},
        //                        open: function (event, ui) {
        //                            $(this).autocomplete("widget").css({ "max-width": $('#MainContent_txtCustomer').width() + 48 });
        //                            $(this).autocomplete("widget").scrollTop(0);
        //                            debugger;
        //                        }
        //                    }).data('ui-autocomplete')._renderItem = function (ul, item) {
        //                        debugger;
        //                        var inner_html = FormatAutocompleteList(item);
        //                        return $('<li class="" style = "padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;" ></li > ')
        //                            .data('item.autocomplete', item)
        //                            .append(inner_html)
        //                            .appendTo(ul);
        //                    };
        //                }
        //            }
        //        });
        //    }
        //}
        function FormatAutocompleteList(item) {
            var inner_html = '<a>';
            inner_html += '<p style="margin:0;">sdssd<strong>' + item.value + '</strong></p>';
            inner_html += '</a>';
            return inner_html;


        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="hdfCustomerId" runat="server" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Customer</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"
                                onKeyUp="GetCustomers()"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Equipment</label>
                            <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <div class="col-md-12 Report">
            <div class="table-responsive">
                <asp:GridView ID="gvEquipment" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="bCheck">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:RadioButton ID="rbCheck" runat="server" GroupName="G" />
                                <asp:Label ID="lblEquipmentHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentHeaderID")%>' runat="server" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EquipmentSerialNo" SortExpression="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentSerialNo")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Code" SortExpression="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerID")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
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
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Contact Number<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Contact Person<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Complaint Description<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtComplaintDescription" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="35"></asp:TextBox>
                </div>
                <%-- <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Information<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtInformation" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>--%>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Requested Date<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Service Priority<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlServicePriority" runat="server" CssClass="form-control" DataTextField="ServicePriority" DataValueField="ServicePriorityID" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Country<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">State<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">District<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn Save" Text="Save" OnClick="btnSave_Click"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
