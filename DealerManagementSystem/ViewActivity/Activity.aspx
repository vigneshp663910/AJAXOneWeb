<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="DealerManagementSystem.ViewActivity.Activity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerView.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
    <script type="text/javascript">  

        $(document).ready(function () {
            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            if (txtCustomerID.value != "") {
                //document.getElementById('divCustomerViewID').style.display = "block";
                //document.getElementById('divCustomerCreateID').style.display = "none";
                document.getElementById('divCustomerName').style.display = "none";

                document.getElementById('lblCustomerName').innerText = document.getElementById('MainContent_txtCustomerNameS').value;
                document.getElementById('lblContactPerson').innerText = document.getElementById('MainContent_txtContactPersonS').value;
                document.getElementById('lblMobile').innerText = document.getElementById('MainContent_txtMobileS').value;
            }
            //var value = document.getElementById("MainContent_ddlReferenceTypeE");
            //var getvalue = value.options[value.selectedIndex].value;
            //var gettext = value.options[value.selectedIndex].text;
            //alert("value:-" + " " + getvalue + " " + "Text:-" + " " + gettext);
        });


        $(function () {
            $("#MainContent_txtCustomerName").autocomplete({
                source: function (request, response) {
                    var param = { CustS: $('#MainContent_txtCustomerName').val() };
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
                        url: "Activity.aspx/GetCustomer",
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
        //$(function () {
        //    $('#div1').click(function () {
        //        AutoCustomer(document.getElementById('lblCustomerID1'), document.getElementById('lblCustomerName1'));
        //    });
        //});
        //$(function () {
        //    $('#div2').click(function () {
        //        AutoCustomer(document.getElementById('lblCustomerID2'), document.getElementById('lblCustomerName2'));
        //    });
        //});
        //$(function () {
        //    $('#div3').click(function () {
        //        AutoCustomer(document.getElementById('lblCustomerID3'), document.getElementById('lblCustomerName3'));
        //    });
        //});
        //$(function () {
        //    $('#div4').click(function () {
        //        AutoCustomer(document.getElementById('lblCustomerID4'), document.getElementById('lblCustomerName4'));
        //    });
        //});
        //$(function () {
        //    $('#div5').click(function () {
        //        AutoCustomer(document.getElementById('lblCustomerID5'), document.getElementById('lblCustomerName5'));
        //    });
        //});
        //function AutoCustomer(lblCustomerID, lblCustomerName) {

        //    var txtCustomer = document.getElementById('MainContent_txtCustomer');
        //    txtCustomer.value = lblCustomerName.innerText;
        //    document.getElementById('divAuto').style.display = "none";
        //}
        function UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {
            debugger;
            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            txtCustomerID.value = CustomerID.innerText;

            var txtCustomer = document.getElementById('MainContent_txtCustomerName');

            txtCustomer.value = CustomerName.innerText;

            //document.getElementById('lblCustomerName').innerText = CustomerName.innerText;
            //document.getElementById('lblContactPerson').innerText = ContactPerson.innerText;
            //document.getElementById('lblMobile').innerText = Mobile.innerText;

            //document.getElementById('MainContent_txtCustomerNameS').value = CustomerName.innerText;
            //document.getElementById('MainContent_txtContactPersonS').value = ContactPerson.innerText;
            //document.getElementById('MainContent_txtMobileS').value = Mobile.innerText;



            document.getElementById('UCdivAuto').style.display = "none";

            document.getElementById('divCustomerViewID').style.display = "block"; 
            document.getElementById('divCustomerName').style.display = "none";
        }

        $(function () {
            $('#divChangeCustomer').click(function () {
                var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
                txtCustomerID.value = "";
                var txtCustomer = document.getElementById('MainContent_txtCustomerName');
                txtCustomer.value = "";
                document.getElementById('divCustomerViewID').style.display = "none";
                document.getElementById('divCustomer').style.display = "block";
            });
        });

    </script>
    <script type="text/javascript"> 
        $(function () {
            $('#UCdiv0').click(function () {
                document.getElementById('UCdiv0').style.display = "none";
                document.getElementById('UCdiv1').style.display = "none";
                document.getElementById('UCdiv2').style.display = "none";
                document.getElementById('UCdiv3').style.display = "none";
                document.getElementById('UCdiv4').style.display = "none";
                document.getElementById('UCdiv5').style.display = "none";
            });
        });
        $(function () {
            $('#UCdiv1').click(function () {
                var CustomerID = document.getElementById('lblCustomerID1')
                var CustomerName = document.getElementById('lblCustomerName1')
                var ContactPerson = document.getElementById('lblContactPerson1')
                var Mobile = document.getElementById('lblMobile1')
                UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
                document.getElementById('divCustomerName').style.display = "none";
            });
        });
        $(function () {
            $('#UCdiv2').click(function () {
                var CustomerID = document.getElementById('lblCustomerID2')
                var CustomerName = document.getElementById('lblCustomerName2')
                var ContactPerson = document.getElementById('lblContactPerson2')
                var Mobile = document.getElementById('lblMobile2')
                UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
            });
        });
        $(function () {
            $('#UCdiv3').click(function () {
                var CustomerID = document.getElementById('lblCustomerID3')
                var CustomerName = document.getElementById('lblCustomerName3')
                var ContactPerson = document.getElementById('lblContactPerson3')
                var Mobile = document.getElementById('lblMobile3')
                UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
            });
        });
        $(function () {
            $('#UCdiv4').click(function () {
                var CustomerID = document.getElementById('lblCustomerID4')
                var CustomerName = document.getElementById('lblCustomerName4')
                var ContactPerson = document.getElementById('lblContactPerson4')
                var Mobile = document.getElementById('lblMobile4')
                UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
            });
        });
        $(function () {
            $('#UCdiv5').click(function () {
                var CustomerID = document.getElementById('lblCustomerID5')
                var CustomerName = document.getElementById('lblCustomerName5')
                var ContactPerson = document.getElementById('lblContactPerson5')
                var Mobile = document.getElementById('lblMobile5')
                UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
            });
        });
     


        $("#txtCustomerName").change(function () {

            alert("The text has been changed.");
        });
        function ShowID(obj) {
            document.getElementById('UCdivAuto').style.display = "none";
            document.getElementById('divCustomerName').style.display = "none";
        }
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

        .WatermarkCssClass {
            color: #aaa;
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
                      
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Employee</label>
                        <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control"   />
                    </div>
                 
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
                                             <asp:TemplateField HeaderText="Engineer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                             <asp:TemplateField HeaderText="Dealer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
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

                                            <%--<asp:TemplateField HeaderText="Referenece" SortExpression="Referenece">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferenece" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityReference.ReferenceTable")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Referenece Number" SortExpression="Referenece">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferenceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ReferenceNumber")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Effort Type" SortExpression="EffortType">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Effort Duration" SortExpression="EffortDuration">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEffortDuration" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDuration","{0:n}")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expense Type" SortExpression="Expense Type">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expenses" SortExpression="Expenses">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount","{0:n}")%>' runat="server" />
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
                                                    <asp:Button ID="btnTrackActivity" runat="server" Text="Track Activity" CssClass="btn Back" OnClick="btnTrackActivity_Click"
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

    <asp:Panel ID="pnlAddActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="btnAddActivityClose" runat="server" Text="X" CssClass="PopupClose" />
            </a>
        </div>
        <asp:Label ID="lblAddActivityMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="model-scroll">
                <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Activity Type</label>
                            <asp:DropDownList ID="ddlActivityTypeS" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Start Date</label>
                            <asp:Label ID="lblStartActivityDate" runat="server" Text="" CssClass="message" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Location</label>
                            <asp:TextBox ID="txtLocationS" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-12 col-sm-12">
                            <label class="modal-label">Remarks</label>
                            <asp:TextBox ID="txtRemarksS" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" runat="server" Text="Start" CssClass="btn Save" OnClick="btnStartActivity_Click" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AddActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlEndActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogueEndActivity">End Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="btnEndActivityClose" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <asp:Label ID="lblEndActivityMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <asp:Label ID="lblValidationMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <%--<div style="display: none">
                <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtCustomerNameS" runat="server" />
                <asp:TextBox ID="txtContactPersonS" runat="server" />
                <asp:TextBox ID="txtMobileS" runat="server" />
            </div>--%>
            <div class="model-scroll">
                <%--<div id="divCustomerViewID" style="display: none">
                    <fieldset class="fieldset-border">
                        <div class="col-md-12">

                            <div class="col-md-2 text-right">
                                <label>Customer Name</label>
                            </div>
                            <div class="col-md-4">
                                <label id="lblCustomerName"></label>

                            </div>
                            <div class="col-md-2 text-right">
                                <label>Contact Person</label>
                            </div>
                            <div class="col-md-4">
                                <label id="lblContactPerson"></label>

                            </div>
                            <div class="col-md-2 text-right">
                                <label>Mobile</label>
                            </div>
                            <div class="col-md-4">
                                <label id="lblMobile"></label>
                            </div>
                        </div>
                        <div id="divChangeCustomer">
                            <label>Change Customer</label>
                        </div>
                    </fieldset>
                </div>--%>
                <%--<div id="divCustomerCreateID">
                    <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                </div>--%>
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
                       <%-- <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Location</label>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <%--<div class="col-md-6 col-sm-12">
                            <label class="modal-label">Customer Code</label>
                            <asp:TextBox ID="txtCustomerCodeE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <%--<div class="col-md-6 col-sm-12">
                            <label class="modal-label">Reference Type</label>
                            <asp:DropDownList ID="ddlReferenceTypeE" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlReferenceTypeE_SelectedIndexChanged" AutoPostBack="true" />
                        </div>--%>
                        <%--<div class="col-md-6 col-sm-12" runat="server" id="divReferenceNumber">
                            <label class="modal-label">Reference Number</label>
                            <asp:TextBox ID="txtReferenceNumberE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <%--<div class="col-md-6 col-sm-12">
                            <label class="modal-label">Equipment Serial Number</label>
                            <asp:TextBox ID="txtEquipmentE" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>--%>
                        <%--<div class="col-md-12 col-sm-12" id="divCustomerName" runat="server" visible="false" >
                            <label class="modal-label">
                                Customer Name
                                </label>
                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver"
                                AutoCompleteType="Disabled"></asp:TextBox>--%>
                            <%--<div id="UCdivAuto" style="position: absolute; background-color: red; display: none; z-index: 1;">--%>
                           <%-- <div id="UCdivAuto" class="custom-auto-complete">--%>
                                <%--<div id="UCdiv0" class="auto-item" style="display: none">
                                    Click here for Customer 
                                </div>--%>
                                <%--<div id="UCdiv1" class="auto-item" style="display: none">
                                </div>
                                <div id="UCdiv2" class="auto-item" style="display: none">
                                </div>
                                <div id="UCdiv3" class="auto-item" style="display: none">
                                </div>
                                <div id="UCdiv4" class="auto-item" style="display: none">
                                </div>
                                <div id="UCdiv5" class="auto-item" style="display: none">
                                </div>
                            </div>
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCustomerName" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
                        </div>--%>
                        <%--  <div id="divCustomerViewID" style="display: none">
                            <fieldset class="fieldset-border">
                               <div class="col-md-12">

                                    <div class="col-md-2 text-right">
                                        <label>Customer Name</label>
                                    </div>
                                    <div class="col-md-4">
                                        <label id="lblCustomerName"></label>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Contact Person</label>
                                    </div>
                                    <div class="col-md-4">
                                        <label id="lblContactPerson"></label>
                                    </div>

                                    <div class="col-md-2 text-right">
                                        <label>Mobile</label>
                                    </div>
                                    <div class="col-md-4">
                                        <label id="lblMobile"></label>
                                    </div>
                                </div>
                                <div id="divChangeCustomer">
                                    <label>Change Customer</label>
                                </div>
                                  
                            </fieldset>
                        </div> --%>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Effort Type</label>
                            <asp:DropDownList ID="ddlEffortType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Effort Duration</label>
                            <asp:TextBox ID="txtEffortDuration" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Expense Type</label>
                            <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Expenses</label>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
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
