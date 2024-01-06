<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Visit.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Visit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/ColdVisitsView.ascx" TagPrefix="UC" TagName="UC_ColdVisitsView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerView.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var hdfCustomerID = document.getElementById('MainContent_UC_Customer_hdfCustomerID');
            if (hdfCustomerID.value != "") {
                document.getElementById('divCustomerViewID').style.display = "block";
                document.getElementById('divCustomerCreateID').style.display = "none";

                document.getElementById('lblCustomerName').innerText = document.getElementById('MainContent_UC_Customer_hdfCustomerName').value;
                document.getElementById('lblContactPerson').innerText = document.getElementById('MainContent_UC_Customer_hdfContactPerson').value;
                document.getElementById('lblMobile').innerText = document.getElementById('MainContent_UC_Customer_hdfMobile').value;

            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfPersonMet" runat="server" Value="0" />
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Employee</label>
                            <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Action Type</label>
                            <asp:DropDownList ID="ddlSActionType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
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
                            <label class="modal-label">Region</label>
                           <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddColdVisit" runat="server" CssClass="btn Save" Text="Add Customer Visit" OnClick="btnAddColdVisit_Click" Width="150px"></asp:Button>
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
                                                <td>Customer Visit(s):</td>

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

                            <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblColdVisitID" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblColdVisitNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblColdVisitDate" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitDate","{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "ActionType.ActionType")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealer" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.Designation.DealerDesignation")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Engineer">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbViewCustomer" runat="server" OnClick="lbViewCustomer_Click">
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                            </asp:LinkButton><asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%></a>
                                            </asp:Label><br />
                                            <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%>'><%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnViewColdVisit" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewColdVisit_Click" Width="50px" Height="33px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Latitude Longitude">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLatitude" Text='<%# DataBinder.Eval(Container.DataItem, "Latitude")%>' runat="server" />
                                            <asp:Label ID="Label1" Text="," runat="server" />
                                            <asp:Label ID="lblLongitude" Text='<%# DataBinder.Eval(Container.DataItem, "Longitude")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Track" SortExpression="Action">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%-- <asp:Label ID="lblLatitude" Text='<%# DataBinder.Eval(Container.DataItem, "Latitude")%>' runat="server" Visible="false" />
                                             <asp:Label ID="lblLongitude" Text='<%# DataBinder.Eval(Container.DataItem, "Longitude")%>' runat="server" Visible="false"/>--%>
                                            <asp:Button ID="btnTrackActivity" runat="server" Text="Track" CssClass="btn Back" OnClick="btnTrackActivity_Click"
                                                Width="105px" Height="25px" />
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
        <div>
            <div class="" id="boxHere"></div>
            <div class="back-buttton coldvisit" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" Visible="false" />
            </div>
            <div class="col-md-12" id="divCustomerView" runat="server" visible="false">
                <UC:UC_CustomerView ID="UC_CustomerView" runat="server"></UC:UC_CustomerView>
            </div>
            <div class="col-md-12" id="divColdVisitView" runat="server" visible="false">
                <UC:UC_ColdVisitsView ID="UC_ColdVisitsView" runat="server"></UC:UC_ColdVisitsView>
            </div>
        </div>
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Customer Visit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div style="display: none">
                <asp:TextBox ID="txtCustomerID" runat="server" /><asp:TextBox ID="txtCustomerNameS" runat="server" />
                <asp:TextBox ID="txtContactPersonS" runat="server" /><asp:TextBox ID="txtMobileS" runat="server" />

            </div>
            <div class="model-scroll">
                <asp:Label ID="lblMessageColdVisit" runat="server" Text="" CssClass="message" Visible="false" />

                <%--    <div id="divCustomerCreateID">--%>
                <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                <%-- </div>--%>

                <fieldset class="fieldset-border">
                    <div class="col-md-12">

                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Visit Date</label>
                            <asp:TextBox ID="txtVisitDate" runat="server" CssClass="form-control" BorderColor="Silver"  ></asp:TextBox>
                            <asp1:CalendarExtender ID="ceVisitDate" runat="server" TargetControlID="txtVisitDate" PopupButtonID="txtVisitDate" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtVisitDate" WatermarkText="DD/MM/YYYY"></asp1:TextBoxWatermarkExtender>

                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Action Type</label>
                            <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Importance</label>
                            <asp:DropDownList ID="ddlImportance" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Person Met</label>
                            <select id="selectPersonMet" onchange="SetPersonMetInHiddenField()" class="form-control">
                                <option value="0">Select</option>
                            </select>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Location</label>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Remark</label>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                        </div>

                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlTrackActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueTrackActivity">Track Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <div id="map_canvas" style="width: 100%; height: 500px"></div>
            </div>
        </div>
    </asp:Panel>
    <asp1:ModalPopupExtender ID="MPE_TrackActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlTrackActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:HiddenField ID="hfLatitude" runat="server" />
    <asp:HiddenField ID="hfLongitude" runat="server" />
    <script> 
        function success(position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            document.getElementById('MainContent_hfLatitude').value = latitude;
            document.getElementById('MainContent_hfLongitude').value = longitude;
            status.textContent = '';
        }
        function error() {
            status.textContent = 'Unable to retrieve your location';
        }

        if (!navigator.geolocation) {
            status.textContent = 'Geolocation is not supported by your browser';

        } else {
            status.textContent = 'Locating…';
            navigator.geolocation.getCurrentPosition(success, error);
        }
    </script>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>
    <script type="text/javascript">
        var markers = JSON.parse('<%=ConvertDataTabletoString() %>');
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
            zoom: 9.6,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var infoWindow = new google.maps.InfoWindow();
        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        for (i = 0; i < markers.length; i++) {
            var data = markers[i]

            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: { url: data.image, scaledSize: new google.maps.Size(25, 25) },
            });

            (function (marker, data) {

                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent(data.description);
                    infoWindow.open(map, marker);
                });
            })(marker, data);
        }

    </script>




    <script type="text/javascript">



        function GetCustomerAuto() {
            $("#MainContent_UC_Customer_hdfCustomerID").val('');
            var param = { Cust: $('#MainContent_UC_Customer_txtCustomerName').val() }
            var Customers = [];
            if ($('#MainContent_UC_Customer_txtCustomerName').val().trim().length >= 3) {
                $.ajax({
                    url: "Visit.aspx/GetCustomer1",
                    contentType: "application/json; charset=utf-8",
                    type: 'POST',
                    data: JSON.stringify(param),
                    dataType: 'JSON',
                    success: function (data) {
                        var DataList = JSON.parse(data.d);
                        for (i = 0; i < DataList.length; i++) {
                            Customers[i] = {
                                value: DataList[i].CustomerName,
                                value1: DataList[i].CustomerID,
                                CustomerType: DataList[i].CustomerType,
                                ContactPerson: DataList[i].ContactPerson,
                                Mobile: DataList[i].Mobile,
                                Address: DataList[i].Address1
                            };
                        }
                      <%-- ddlPersonMet = document.getElementById("<%=ddlPersonMet.ClientID %>");
                        ddlPersonMet.options.length == 0;
                        var opt = new Option("Select Year", "0");
                        ddlPersonMet.options.add(opt);
                        var opt = new Option("Select Year1", "1");
                        ddlPersonMet.options.add(opt);--%>


                        $('#MainContent_UC_Customer_txtCustomerName').autocomplete({
                            source: function (request, response) { response(Customers) },
                            select: function (e, u) {
                                debugger;
                                $("#MainContent_UC_Customer_hdfCustomerID").val(u.item.value1);
                                document.getElementById('divCustomerViewID').style.display = "block";
                                document.getElementById('divCustomerCreateID').style.display = "none";

                                $("#MainContent_UC_Customer_hdfCustomerName").val(u.item.value);
                                $("#MainContent_UC_Customer_hdfContactPerson").val(u.item.ContactPerson);
                                $("#MainContent_UC_Customer_hdfMobile").val(u.item.Mobile);

                                document.getElementById('lblCustomerName').innerText = u.item.value;
                                document.getElementById('lblContactPerson').innerText = u.item.ContactPerson;
                                document.getElementById('lblMobile').innerText = u.item.Mobile;

                                GetStudentDetails(u.item.value1);
                            },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $('#MainContent_UC_Customer_txtCustomerName').width() + 48,
                                });
                                $(this).autocomplete("widget").scrollTop(0);
                            }
                        }).focus(function (e) {
                            $(this).autocomplete("search");
                        }).click(function () {
                            $(this).autocomplete("search");
                        }).data('ui-autocomplete')._renderItem = function (ul, item) {

                            var inner_html = FormatAutocompleteList(item);
                            return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                                .data('item.autocomplete', item)
                                .append(inner_html)
                                .appendTo(ul);
                        };
                    }
                });
            }
            else {
                $('#MainContent_UC_Customer_txtCustomerName').autocomplete({
                    source: function (request, response) {
                        response($.ui.autocomplete.filter(Customers, ""))
                    }
                });
            }
        }
    </script>

    <script type="text/javascript">    
        var ddlPersonMet;
        function GetStudentDetails(custID) {
            debugger;
            ddlPersonMet = document.getElementById("selectPersonMet");
            var userContext = { custID: custID };
            //  PageMethods.GetStudentDetails(OnSuccess, failedHandler, userContext);
            PageMethods.GetStudentDetails(custID, OnSuccess, OnError);
        }
        //window.onload = GetStudentDetails;
        function OnError(error) {
            alert(error);
        }
        function OnSuccess(response) {
            ddlPersonMet.options.length = 0;
            AddOption("Select", "0");
            for (var i in response) {
                AddOption(response[i].Name, response[i].Id);
            }
        }
        function AddOption(text, value) {
            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddlPersonMet.options.add(option);
        }

        function SetPersonMetInHiddenField() {
            ddlPersonMet = document.getElementById("selectPersonMet");
            document.getElementById('MainContent_hfPersonMet').value = ddlPersonMet.value;
        }
    </script>
</asp:Content>

