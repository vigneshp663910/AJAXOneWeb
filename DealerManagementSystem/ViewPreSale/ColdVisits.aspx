<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ColdVisits.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.ColdVisits" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/Effort.ascx" TagPrefix="UC" TagName="UC_Effort" %>
<%@ Register Src="~/ViewPreSale/UserControls/Expense.ascx" TagPrefix="UC" TagName="UC_Expense" %>
<%@ Register Src="~/ViewPreSale/UserControls/CustomerView.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>
<%@ Register Src="~/UserControls/CustomerAutocomplete.ascx" TagPrefix="UC" TagName="UC_CustomerAutocomplete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
    <script type="text/javascript">  
        $(function () {
            $("#MainContent_txtCustomer").autocomplete({
                source: function (request, response) {
                    debugger
                    var param = { CustS: $('#MainContent_txtCustomer').val() };
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
                        url: "ColdVisits.aspx/GetCustomer",
                        data: JSON.stringify(param),
                        dataType: 'JSON',
                        success: function (data) {
                            debugger
                            document.getElementById('divAuto').style.display = "block";
                            var n = 0;
                            for (var i = 1; i <= 5; i++) {
                                $(('#div' + i)).empty();
                                document.getElementById('div' + i).style.display = "none";
                            }
                            $.map(data.d, function (item) {
                                n = n + 1;
                                document.getElementById('div' + n).style.display = "block";
                                document.getElementById("div" + n).innerHTML = item;
                            })
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                },
                minLength: 3 //This is the Char length of inputTextBox    
            });

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
            var txtCustomer = document.getElementById('MainContent_txtCustomer');
            txtCustomer.value = lblCustomerName.innerText;
            document.getElementById('divAuto').style.display = "none";
        }
        function UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {

            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            txtCustomerID.value = CustomerID.innerText;

            var txtCustomer = document.getElementById('MainContent_UC_Customer_txtCustomerName');

            txtCustomer.value = CustomerName.innerText;

            document.getElementById('lblCustomerName').innerText = CustomerName.innerText;
            document.getElementById('lblContactPerson').innerText = ContactPerson.innerText;
            document.getElementById('lblMobile').innerText = Mobile.innerText;

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-right">
                            <label>Date From</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Date To</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Customer</label>
                        </div>
                        <div class="col-md-2"> 
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <div id="divAuto" style="position: absolute; background-color: red; z-index: 1;">
                                <div id="div1" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div2" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div3" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div4" class="fieldset-borderAuto" style="display: none">
                                </div>
                                <div id="div5" class="fieldset-borderAuto" style="display: none">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Mobile</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Country</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-right">
                            <label>State</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAddColdVisit" runat="server" CssClass="btn Save" Text="Add Cold Visit" OnClick="btnAddColdVisit_Click" Width="150px"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cold Visit No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblColdVisitID" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblColdVisitNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cold Visit Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblColdVisitDate" Text='<%# DataBinder.Eval(Container.DataItem, "ColdVisitDate")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionType" Text='<%# DataBinder.Eval(Container.DataItem, "ActionType.ActionType")%>' runat="server" />
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
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Mobile")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.EMail")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" Width="70px" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>Action</asp:ListItem>
                                                <asp:ListItem>Add Effort</asp:ListItem>
                                                <asp:ListItem>Add Expense</asp:ListItem>
                                            </asp:DropDownList>
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
        <div class="col-md-12" id="divCustomerView" runat="server" visible="false">
            <div class="text-right">
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
                <span id="PopupDialogue">Add Cold Visit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
            </div>
            <div class="col-md-12">
                <div style="display: none">
                    <asp:TextBox ID="txtCustomerID" runat="server" ></asp:TextBox>
                </div>
                <div id="divCustomerViewID" style="display: none">
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
                </div>
                <div id="divCustomerCreateID">
                    <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                </div>
                <fieldset class="fieldset-border">
                    <div class="col-md-12">

                        <div class="col-md-2 text-right">
                            <label>Cold Visit Date</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtColdVisitDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Action Type</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlActionType" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-2 text-right">
                            <label>Remark</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

        <asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Cold Visit Effort </span><a href="#" role="button">
                    <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
            </div>
            <div class="col-md-12">

                <UC:UC_Effort ID="UC_Effort" runat="server"></UC:UC_Effort>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSaveEffort" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveEffort_Click" />
                </div>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvEffort" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortType" Text='<%# DataBinder.Eval(Container.DataItem, "EffortType.EffortType")%>' runat="server" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortDate" Text='<%# DataBinder.Eval(Container.DataItem, "EffortDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort Start Time" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortStartTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortStartTime")%>' runat="server" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort End Time" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffortEndTime" Text='<%# DataBinder.Eval(Container.DataItem, "EffortEndTime")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effort" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffort" Text='<%# DataBinder.Eval(Container.DataItem, "Effort")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

        <asp:Panel ID="pnlExpense" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Cold Visit Expense</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" />
                </a>
            </div>
            <div class="col-md-12">
                <UC:UC_Expense ID="UC_Expense" runat="server"></UC:UC_Expense>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSaveExpense" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveExpense_Click" />
                </div>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                        <Columns>
                            <%-- <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
            </ItemTemplate>
        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Sales Engineer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSEContactName" Text='<%# DataBinder.Eval(Container.DataItem, "SalesEngineer.ContactName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseType" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseType.ExpenseType")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense Date" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseDate" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDate")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" SortExpression="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
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
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="MPE_Expense" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlExpense" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>
