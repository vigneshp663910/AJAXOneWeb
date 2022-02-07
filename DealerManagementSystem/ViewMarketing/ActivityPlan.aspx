<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ActivityPlan.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityPlan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS_Scripts.js"></script>
    <script type="text/javascript">

        function GetActivityData(ActivityID) {
            $.ajax({
                type: "POST",
                url: "YDMS_ActivityPlan.aspx/GetActivityInfo",
                data: '{ActivityID: "' + ActivityID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        var data = JSON.parse(response.d.toString());
                        var txtBudget = document.getElementById('<%=txtBudget.ClientID%>');
                        if (data.length > 0) {
                            txtBudget.value = data[0].Budget;
                            localStorage.setItem('AjaxSharing', data[0].AjaxSharing);
                            document.getElementById('txtFA').value = data[0].FunctionalArea;
                            document.getElementById('lblAjaxSharing').innerHTML = ' (' + data[0].AjaxSharing + '%)';
                            document.getElementById('lblDealerSharing').innerHTML = ' (' + (100 - parseFloat(data[0].AjaxSharing)) + '%)';
                            document.getElementById('lblUnits').innerHTML = 'No of Units(' + data[0].UnitDesc + ')';
                            SetExpBudget();
                        }
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function SaveActivityPlan() {
            var ddlDealer = document.getElementById('<%=ddlDealer.ClientID %>');
             var ddlActivity = document.getElementById('<%=ddlActivity.ClientID %>');
             var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
             var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
             var txtUnits = document.getElementById('<%=txtUnits.ClientID%>');
             var txtLocaton = document.getElementById('<%=txtLocation.ClientID%>');
             var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');
             var hdnPkPlanID = document.getElementById('<%=hdnPkPlanID.ClientID%>');
             if (ddlDealer.value == '0') {
                 alert('Select Dealer');
                 ddlDealer.focus();
                 return false;
             }
             if (ddlActivity.value == '0') {
                 alert('Select Activity');
                 ddlActivity.focus();
                 return false;
             }

             if (txtUnits.value == '0' || txtUnits.value == '') {
                 alert('Enter No. of Units');
                 txtUnits.focus();
                 return false;
             }
             if (txtFromDate.value == '') {
                 alert('Enter From Date');
                 txtFromDate.focus();
                 return false;
             }
             if (txtToDate.value == '') {
                 alert('Enter To Date');
                 txtToDate.focus();
                 return false;
             }



             $.ajax({
                 type: "POST",
                 url: "YDMS_ActivityPlan.aspx/SaveActivityPlan",
                 data: '{PKPlanID:"' + hdnPkPlanID.value + '",DealerID:"' + ddlDealer.value + '",ActivityID: "' + ddlActivity.value + '",Units: "' + txtUnits.value + '",FromDate: "' + txtFromDate.value + '",ToDate: "' + txtToDate.value + '", Location: "' + txtLocaton.value + '", Remarks: "' + txtRemarks.value + '"}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d) {
                         if (response.d.toString().split('|')[0] == "Saved") {
                             alert("Saved Successfully. Activity no for this activity is " + response.d.toString().split('|')[1]);
                             document.getElementById('<%=ddlDealerSearch.ClientID %>').value = ddlDealer.value;
                            Clear();
                            var buttonID = '<%= Search.ClientID %>';
                            $("#" + buttonID).click();
                        }
                        else {
                            alert(response.d.toString());
                        }

                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function SetExpBudget() {
            var txtBudget = document.getElementById('<%=txtBudget.ClientID%>');
            var txtExpBudget = document.getElementById('<%=txtExpBudget.ClientID%>');
            var txtNoofUnits = document.getElementById('<%=txtUnits.ClientID%>');
            var txtAjaxSharing = document.getElementById('<%=txtAjaxSharing.ClientID%>');
            var txtDealerSharing = document.getElementById('<%=txtDealerSharing.ClientID%>');
            var hdnAjaxSharing = document.getElementById('<%=hdnAjaxSharing.ClientID%>');
            var TotalBudget = parseFloat('0' + txtBudget.value) * parseFloat('0' + txtNoofUnits.value);
            txtExpBudget.value = TotalBudget;
            txtAjaxSharing.value = (parseFloat(TotalBudget) * parseFloat(hdnAjaxSharing.value) / 100).toFixed(0);
            txtDealerSharing.value = TotalBudget - parseFloat(txtAjaxSharing.value);
        }
        function Clear() {
            var ddlActivity = document.getElementById('<%=ddlActivity.ClientID%>');
            var ddlDealer = document.getElementById('<%=ddlDealer.ClientID%>');
            var txtUnits = document.getElementById('<%=txtUnits.ClientID%>');
            var txtExpBudget = document.getElementById('<%=txtExpBudget.ClientID%>');
            var txtBudget = document.getElementById('<%=txtBudget.ClientID%>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var txtLocation = document.getElementById('<%=txtLocation.ClientID%>');
            var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');
            var hdnPKPlanID = document.getElementById('<%=hdnPkPlanID.ClientID%>');
            ddlActivity.options.selectedIndex = 0;
            ddlDealer.options.selectedIndex = 0;

            txtBudget.value = ''; txtUnits.value = ''; txtExpBudget.value = ''; txtFromDate.value = ''; txtToDate.value = '';
            txtLocation.value = ''; txtRemarks.value = ''; hdnPKPlanID.value = '0';
        }

        function GetActivityPlanData(PKPlanID) {

            $.ajax({
                type: "POST",
                url: "YDMS_ActivityPlan.aspx/GetActivityPlanData",
                data: '{PKPlanID: "' + PKPlanID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        console.log(response.d);
                        var data = JSON.parse(response.d.toString());
                        var ddlActivity = document.getElementById('<%=ddlActivity.ClientID%>');
                        var ddlDealer = document.getElementById('<%=ddlDealer.ClientID%>');
                        var txtUnits = document.getElementById('<%=txtUnits.ClientID%>');
                        var txtExpBudget = document.getElementById('<%=txtExpBudget.ClientID%>');
                        var txtBudget = document.getElementById('<%=txtBudget.ClientID%>');

                        var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
                        var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
                        var txtLocation = document.getElementById('<%=txtLocation.ClientID%>');
                        var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');
                        var hdnPlanID = document.getElementById('<%=hdnPkPlanID.ClientID%>');
                        var txtAjaxSharing = document.getElementById('<%=txtAjaxSharing.ClientID%>');
                        var txtDealerSharing = document.getElementById('<%=txtDealerSharing.ClientID%>');


                        if (data.length > 0) {
                            hdnPlanID.value = data[0].AP_PKPlanID;
                            ddlDealer.value = data[0].AP_FKDealerID;
                            ddlActivity.value = data[0].AP_FKActivityID;
                            txtUnits.value = data[0].AP_NoofUnits;
                            var FromDate = new Date(data[0].AP_FromDate.substring(0, data[0].AP_FromDate.indexOf('T')));
                            var ToDate = new Date(data[0].AP_ToDate.substring(0, data[0].AP_ToDate.indexOf('T')));
                            txtFromDate.value = data[0].AP_FromDate.substring(0, data[0].AP_FromDate.indexOf('T'));
                            txtToDate.value = data[0].AP_ToDate.substring(0, data[0].AP_ToDate.indexOf('T'));
                            txtLocation.value = data[0].AP_Location;
                            txtRemarks.value = data[0].AP_Remarks;
                            txtBudget.value = data[0].AP_BudgetPerUnit;
                            txtExpBudget.value = data[0].AP_ExpBudget;
                            //GetActivityData(data[0].AP_FKActivityID);
                            var TotalBudget = parseFloat('0' + txtExpBudget.value.replace(',', ''));
                            var AjaxSharing = (parseFloat(TotalBudget) * parseFloat(data[0].AI_AjaxSharing) / 100).toFixed(0);

                            document.getElementById('lblAjaxSharing').innerHTML = ' (' + data[0].AI_AjaxSharing + '%)';
                            document.getElementById('lblDealerSharing').innerHTML = ' (' + (100 - parseFloat(data[0].AI_AjaxSharing)) + '%)';

                            txtAjaxSharing.value = AjaxSharing;
                            txtDealerSharing.value = TotalBudget - parseFloat(AjaxSharing);
                        }
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }

    </script>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
        <ContentTemplate>

            <div class="container-fluid">
                <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                    <h4 style="width: 100%; padding-right: 10px; vertical-align: middle">Activity Plan<img style="float: right; cursor: pointer" id="imgdivEntry" src="../Images/grid_collapse.png" onclick="ShowHide(this,'divEntry')" /></h4>
                </div>
                <div id="divEntry">
                    <div class="row">
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlDealer">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlActivity">Activity</label>
                            <asp:DropDownList ID="ddlActivity" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" runat="server" AutoPostBack="true">
                            </asp:DropDownList>

                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtFA" id="lblFA">Functional Area</label>
                            <input type="text" disabled="disabled" runat="server" id="txtFA" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtUnits" id="lblUnits" runat="server">No. of Units</label>
                            <input type="text" runat="server" id="txtUnits" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtFromDate">From Date</label>
                            <input type="text" runat="server" id="txtFromDate" />

                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtToDate">To Date</label>
                            <input type="text" runat="server" id="txtToDate" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtLocation">Location</label>
                            <input type="text" runat="server" id="txtLocation" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtBudget">Budget Per Unit</label>
                            <input type="text" disabled="disabled" runat="server" id="txtBudget" />

                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtExpBudget">Expected Budget</label>
                            <input type="text" disabled="disabled" runat="server" id="txtExpBudget" />

                        </div>



                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtAjaxSharing">Ajax Sharing %</label><label id="lblAjaxSharing" runat="server"></label>
                            <input type="text" disabled="disabled" runat="server" id="txtAjaxSharing" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtDealerSharing">Dealers Sharing %</label><label id="lblDealerSharing" runat="server"></label>
                            <input type="text" disabled="disabled" runat="server" id="txtDealerSharing" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtRemarks">Remarks</label>
                            <textarea runat="server" id="txtRemarks" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; padding-top: 30px">

                            <input type="submit" id="btnSubmit" onclick="return SaveActivityPlan();" value="Submit" runat="server">
                            <input type="submit" value="Cancel" onclick="return Clear();" runat="server">
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-xl-4 col-lg-4 col-md-8 col-sm-6 col-12">
                            <asp:HiddenField ID="hdnPkPlanID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnAjaxSharing" runat="server" />
                        </div>


                    </div>
                </div>
            </div>
            <hr />


            <div class="container-fluid">

                <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                    <h4 style="width: 100%; padding-right: 10px; vertical-align: middle">Existing Activity Plan Search<img style="float: right; cursor: pointer" id="imgdivSearch" src="../Images/grid_collapse.png" onclick="ShowHide(this,'divSearch')" /></h4>

                </div>
                <div id="divSearch">
                    <div class="row">
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlDealerSearch">Dealer</label>
                            <asp:DropDownList ID="ddlDealerSearch" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtFromDateSearch">From Date</label>
                            <asp:TextBox runat="server" ID="txtFromDateSearch"></asp:TextBox>
                            <cc1:CalendarExtender ID="calFromDateSearch" runat="server" TargetControlID="txtFromDateSearch" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtToDateSearch">To Date</label>
                            <asp:TextBox runat="server" ID="txtToDateSearch"></asp:TextBox>
                            <cc1:CalendarExtender ID="calToDateSearch" runat="server" TargetControlID="txtToDateSearch" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlActivitySearch">Activity</label>
                            <asp:DropDownList ID="ddlActivitySearch" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlStatus">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Done" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Not Done" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 28px;">

                            <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
                            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_Click" />

                        </div>
                    </div>
                    <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                        <h6>Detail</h6>
                    </div>
                    <asp:GridView ID="gvData" CssClass="gridclass" runat="server" ShowHeaderWhenEmpty="true" OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="false" Width="90%">
                        <Columns>

                            <asp:BoundField HeaderText="Activity No" DataField="Activity No">
                                <ItemStyle Width="12%" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Activity" DataField="Activity">
                                <ItemStyle Width="18%" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="From Date" DataField="From Date">
                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="To Date " DataField="To Date">
                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="No. of Units" DataField="No. of Units">
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Expected Budget" DataField="Expected Budget" DataFormatString="{0:#,#}">
                                <ItemStyle Width="8%" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Location" DataField="Location">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Remarks" DataField="Remarks">
                                <ItemStyle Width="22%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%# Bind("PKPlanID") %>' runat="server" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
