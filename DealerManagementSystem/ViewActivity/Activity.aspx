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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Activity Type</label>
                        <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Activity ID</label>
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
                    <div class="col-md-2 text-left">
                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>

                    <div class="col-md-2 text-left">
                        <label>Customer Name</label>
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Equipment</label>
                        <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Reference</label>
                        <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control" />
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

                        <asp:GridView ID="gvActivity" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                            PageSize="10" AllowPaging="true">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityID" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityID")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Engineer">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesEngineer" Text='<%# DataBinder.Eval(Container.DataItem, "ActivitySalesEngineer")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityType" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" SortExpression="Start Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityStartDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" SortExpression="End Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityStartDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" SortExpression="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer" SortExpression="Customer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Equipment" SortExpression="Equipment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipment" Text='<%# DataBinder.Eval(Container.DataItem, "Equipment")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Remark" SortExpression="Remark">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Referenece" SortExpression="Referenece">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferenece" Text='<%# DataBinder.Eval(Container.DataItem, "Referenece")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%--   <asp:Button ID="btnViewActivity" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewActivity_Click" Width="75px" Height="25px" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divDetailsActivity" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <%--   <UC:UC_ViewActivity ID="UC_ViewActivity" runat="server"></UC:UC_ViewActivity>--%>
        </div>
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>


    <asp:Panel ID="pnlActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageActivity" runat="server" Text="" CssClass="message" Visible="false" />
                <fieldset class="fieldset-border">
                    <div id="divCustomerCreateID">
                        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-left">
                                    <label>Activity Type</label>
                                    <asp:DropDownList ID="ddlActivityTypeS" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2 text-left">
                                    <label>Activity Date</label>
                                    <asp:Label ID="lblActivityDate" runat="server" Text="" CssClass="message" />
                                </div>

                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnStart" runat="server" CssClass="btn Save" Text="Start" OnClick="btnStart_Click" Width="150px"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp1:ModalPopupExtender ID="MPE_Activity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlEndActivity" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
                <fieldset class="fieldset-border"> 
                    <fieldset class="fieldset-border" id="Fieldset3" runat="server">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-left">
                                <label>Activity Type</label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label>Activity Date</label>
                                <asp:Label ID="Label2" runat="server" Text="" CssClass="message" />
                            </div>

                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnEndActivity" runat="server" CssClass="btn Save" Text="End Activity" OnClick="btnEndActivity_Click" Width="150px"></asp:Button>
                            </div>
                        </div>
                    </fieldset>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="Button3" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp1:ModalPopupExtender ID="MPE_EndActivity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEndActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

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
</asp:Content>
