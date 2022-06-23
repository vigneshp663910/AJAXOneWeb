<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="DealerManagementSystem.ViewActivity.Activity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
    <script type="text/javascript">  

        $(document).ready(function () {
            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            if (txtCustomerID.value != "") {
                document.getElementById('divCustomerViewID').style.display = "block";
                document.getElementById('divCustomerCreateID').style.display = "none";

                document.getElementById('lblCustomerName').innerText = document.getElementById('MainContent_txtCustomerNameS').value;
                document.getElementById('lblContactPerson').innerText = document.getElementById('MainContent_txtContactPersonS').value;
                document.getElementById('lblMobile').innerText = document.getElementById('MainContent_txtMobileS').value;
            }
        });


        $(function () {
            //$("#MainContent_txtCustomer").autocomplete({
            //    source: function (request, response) {
            //        debugger
            //        var param = { CustS: $('#MainContent_txtCustomer').val() };
            //        $.ajax({
            //            type: 'POST',
            //            contentType: "application/json; charset=utf-8",
            //            /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
            //            url: "ColdVisits.aspx/GetCustomer",
            //            data: JSON.stringify(param),
            //            dataType: 'JSON',
            //            success: function (data) {
            //                debugger
            //                document.getElementById('divAuto').style.display = "block";
            //                var n = 0;
            //                for (var i = 1; i <= 5; i++) {
            //                    $(('#div' + i)).empty();
            //                    document.getElementById('div' + i).style.display = "none";
            //                }
            //                $.map(data.d, function (item) {
            //                    n = n + 1;
            //                    document.getElementById('div' + n).style.display = "block";
            //                    document.getElementById("div" + n).innerHTML = item;
            //                })
            //            },
            //            error: function () {
            //                alert("Error");
            //            }
            //        });
            //    },
            //    minLength: 3 //This is the Char length of inputTextBox    
            //});

            $("#MainContent_UC_Customer_txtCustomerName").autocomplete({
                source: function (request, response) {
                    var param = { CustS: $('#MainContent_UC_Customer_txtCustomerName').val() };
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
                        url: "ColdVisits.aspx/GetCustomer",
                        data: JSON.stringify(param),
                        dataType: 'JSON',
                        success: function (data) {
                            document.getElementById('UCdivAuto').style.display = "block";
                            var n = 0;
                            for (var i = 1; i <= 5; i++) {
                                $(('#div' + i)).empty();
                                document.getElementById('UCdiv' + i).style.display = "none";
                            }
                            $.map(data.d, function (item) {
                                n = n + 1;
                                document.getElementById('UCdiv' + n).style.display = "block";
                                document.getElementById("UCdiv" + n).innerHTML = item;
                            })
                            if (n == 0)
                                document.getElementById('UCdiv0').style.display = "none";
                            else
                                document.getElementById('UCdiv0').style.display = "block";
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                },
                minLength: 3 //This is the Char length of inputTextBox    
            });
        });
    </script>

    <script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
    <script type="text/javascript"> 
        $(function () {
            $('#div1').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID1'), document.getElementById('lblCustomerName1'));
            });
        });
        $(function () {
            $('#div2').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID2'), document.getElementById('lblCustomerName2'));
            });
        });
        $(function () {
            $('#div3').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID3'), document.getElementById('lblCustomerName3'));
            });
        });
        $(function () {
            $('#div4').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID4'), document.getElementById('lblCustomerName4'));
            });
        });
        $(function () {
            $('#div5').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID5'), document.getElementById('lblCustomerName5'));
            });
        });
        function AutoCustomer(lblCustomerID, lblCustomerName) {
            debugger
            var txtCustomer = document.getElementById('MainContent_txtCustomer');
            txtCustomer.value = lblCustomerName.innerText;
            document.getElementById('divAuto').style.display = "none";
        }
        function UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {
            debugger
            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            txtCustomerID.value = CustomerID.innerText;

            var txtCustomer = document.getElementById('MainContent_UC_Customer_txtCustomerName');

            txtCustomer.value = CustomerName.innerText;

            document.getElementById('lblCustomerName').innerText = CustomerName.innerText;
            document.getElementById('lblContactPerson').innerText = ContactPerson.innerText;
            document.getElementById('lblMobile').innerText = Mobile.innerText;

            document.getElementById('MainContent_txtCustomerNameS').value = CustomerName.innerText;
            document.getElementById('MainContent_txtContactPersonS').value = ContactPerson.innerText;
            document.getElementById('MainContent_txtMobileS').value = Mobile.innerText;



            document.getElementById('UCdivAuto').style.display = "none";

            document.getElementById('divCustomerViewID').style.display = "block";
            document.getElementById('divCustomerCreateID').style.display = "none";
        }

        $(function () {
            $('#divChangeCustomer').click(function () {
                var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
                txtCustomerID.value = "";
                var txtCustomer = document.getElementById('MainContent_UC_Customer_txtCustomerName');
                txtCustomer.value = "";
                document.getElementById('divCustomerViewID').style.display = "none";
                document.getElementById('divCustomerCreateID').style.display = "block";
            });
        });

    </script>

    <style type="text/css">
        html {
            height: 100%
        }

        body {
            height: 100%;
            margin: 0;
            padding: 0
        }

        #map_canvas {
            height: 100%
        }
    </style>
    <%-- <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">    </script>--%>

    <%--  <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk">    </script>--%>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>

    var geocoder;

    <style>
        .fieldset-borderAuto {
            border: solid 1px #cacaca;
            margin: 1px 0;
            border-radius: 5px;
            padding: 10px;
            background-color: #b4b4b4;
        }

            .fieldset-borderAuto tr {
                /* background-color: #000084; */
                background-color: inherit;
                font-weight: bold;
                color: white;
            }

            .fieldset-borderAuto:hover {
                background-color: blue;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfLatitude" runat="server" />
    <asp:HiddenField ID="hfLongitude" runat="server" />
    <asp:Label ID="lblActivityMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Activity Type</label>
                        <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Activity Number</label>
                        <asp:TextBox ID="txtActivityID" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Activity Date From</label>
                        <asp:TextBox ID="txtActivityDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Activity Date To</label>
                        <asp:TextBox ID="txtActivityDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <%--<div class="col-md-2 text-left">
                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>--%>

                    <%--<div class="col-md-2 text-left">
                        <label>Customer Name</label>
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>--%>
                    <%--<div class="col-md-2 text-left">
                        <label>Equipment Serial Number</label>
                        <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>--%>
                    <div class="col-md-2 text-left">
                        <label>Reference Type</label>
                        <asp:DropDownList ID="ddlReferenceType" runat="server" CssClass="form-control" />
                    </div>
                     <div class="col-md-2 text-left">
                        <label>Reference Number</label>
                        <asp:TextBox ID="txtReferenceNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearchActivity" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        <asp:Button ID="btnAddActivity" runat="server" CssClass="btn Save" Text="Add Activity" OnClick="btnAddActivity_Click" Width="150px"></asp:Button>
                    </div>
                </div>
            </fieldset>
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
                                                <td>Activities:</td>

                                                <td>
                                                    <asp:Label ID="lblRowCountActivity" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnActivityArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnActivityArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnActivityArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnActivityArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 Report">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                        PageSize="10" AllowPaging="true" OnPageIndexChanging="gvActivity_PageIndexChanging" EmptyDataText="No Data Found" DataKeyNames="ActivityStartLatitude,ActivityStartLongitude,ActivityEndLatitude,ActivityEndLongitude">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Activity Number">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityID" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityID")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Sales Engineer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "PUser.SalesEngineer")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Activity Type">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityType" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityType.ActivityTypeName")%>' runat="server" />
                                                    <asp:Label ID="lblActivityTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityType.ActivityTypeID")%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" SortExpression="Start Date">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblStartDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityStartDate","{0:d}")%>' runat="server" />--%>
                                                    <asp:Label ID="lblStartDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityStartDate")%>' runat="server" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" SortExpression="End Date">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblEndDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityEndDate","{0:d}")%>' runat="server" />--%>
                                                    <asp:Label ID="lblEndDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityEndDate")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location" SortExpression="Location">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Customer" SortExpression="Customer Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomer" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Equipment" SortExpression="Equipment">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEquipment" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment.EquipmentSerialNo")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    
                                            <asp:TemplateField HeaderText="Referenece" SortExpression="Referenece">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferenece" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityReference.ReferenceTable")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Referenece Number" SortExpression="Referenece">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferenceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ReferenceNumber")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expenses" SortExpression="Expenses">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" SortExpression="Action">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEndActivity" runat="server" Text="End Activity" CssClass="btn Back" OnClick="btnEndActivity_Click"
                                                         Width="105px" Height="25px" />
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
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>       
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:panel id="pnlAddActivity" runat="server" cssclass="Popup" style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:button id="btnAddActivityClose" runat="server" text="X" cssclass="PopupClose" />
        </a>
    </div>
    <asp:label id="lblAddActivityMessage" runat="server" text="" cssclass="message" visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Activity Type</label>
                        <asp:dropdownlist id="ddlActivityTypeS" runat="server" cssclass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Start Date</label>
                        <asp:Label ID="lblStartActivityDate" runat="server" Text="" CssClass="message" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:button id="btnSave" runat="server" text="Start" cssclass="btn Save" onclick="btnStartActivity_Click" />
                    </div>
                </div>
            </fieldset>
        </div>        
    </div>
</asp:panel>
    <ajaxtoolkit:modalpopupextender id="MPE_AddActivity" runat="server" targetcontrolid="lnkMPE" popupcontrolid="pnlAddActivity" backgroundcssclass="modalBackground" cancelcontrolid="btnCancel" />

    <asp:Panel ID="pnlEndActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueEndActivity">End Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="btnEndActivityClose" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <asp:Label ID="lblEndActivityMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="model-scroll">               
                <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                    <div class="col-md-12">
                         <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Activity Number</label>
                            <asp:Label ID="lblActivityIDE" runat="server" Text="" CssClass="message" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Activity Type</label>
                            <asp:Label ID="lblActivityTypeE" runat="server" Text="" CssClass="message" />
                            <asp:Label ID="lblActivityTypeIDE" runat="server" Text="" CssClass="message" Visible="false" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">End Date</label>
                            <asp:Label ID="lblEndActivityDate" runat="server" Text="" CssClass="message" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Location</label>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-6 col-sm-12">
                            <label class="modal-label">Customer Code</label>
                            <asp:TextBox ID="txtCustomerCodeE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Reference Type</label>
                            <asp:DropDownList ID="ddlReferenceTypeE" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Reference Number</label>
                            <asp:TextBox ID="txtReferenceNumberE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-6 col-sm-12">
                            <label class="modal-label">Equipment Serial Number</label>
                            <asp:TextBox ID="txtEquipmentE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Expenses</label>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>                      
                        
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnEndActivityE" runat="server" CssClass="btn Save" Text="End Activity" OnClick="btnEndActivityE_Click" Width="150px"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <asp1:ModalPopupExtender ID="MPE_EndActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEndActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

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

    <script type="text/javascript">
         

        var markers = JSON.parse('<%=ConvertDataTabletoString() %>');
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].lat, markers[0].lng), 
            zoom: 4.6,
            mapTypeId: google.maps.MapTypeId.ROADMAP 
        };
        var infoWindow = new google.maps.InfoWindow();
        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        for (i = 0; i < markers.length; i++) {
            var data = markers[i]
            debugger; 
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
</asp:Content>
