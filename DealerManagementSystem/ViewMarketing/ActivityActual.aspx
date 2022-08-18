<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ActivityActual.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityActual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            SetMaxDate();
        })
        function SetMaxDate() {

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            today = yyyy + '-' + mm + '-' + dd;
            document.getElementById('<%= txtFromDate.ClientID %>').min = yyyy + "-" + mm + "-01";
            document.getElementById('<%= txtToDate.ClientID %>').min = yyyy + "-" + mm + "-01";
            document.getElementById('<%= txtFromDate.ClientID %>').max = today;
            document.getElementById('<%= txtToDate.ClientID %>').max = today;
        }
        function SetActualBudget() {
        }
        function SetActualSharing() {
            var txtAjaxSharingA = document.getElementById('<%=txtAjaxSharingA.ClientID%>');
            var txtDealerSharingA = document.getElementById('<%=txtDealerSharingA.ClientID%>');
            var txtExpBudget = document.getElementById('<%=txtExpBudget.ClientID%>');
            var TotalBudget = parseFloat('0' + txtExpBudget.value.replace(',', ''));
            var AjaxSharing = parseFloat('0' + document.getElementById('<%=hdnAjaxSharing.ClientID%>').value);
            var AjaxSharingA = (parseFloat(TotalBudget) * AjaxSharing / 100).toFixed(0);
            var DealerSharingA = TotalBudget - parseFloat(AjaxSharingA);
            txtAjaxSharingA.value = addCommas(AjaxSharingA);
            txtDealerSharingA.value = addCommas(DealerSharingA);
        }
        function GetActivityData(ActivityID) {
            $.ajax({
                type: "POST",
                url: "ActivityInfoM.aspx/GetActivityInfo",
                data: '{ActivityID: "' + ActivityID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        var data = JSON.parse(response.d.toString());
                        if (data.length > 0) {



                        }
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function FormatDate(d) {
            var day = d.getDate();
            var month = d.getMonth() + 1;
            var year = d.getFullYear();
            month = month.toString().length < 2 ? ('0' + month.toString()) : month.toString();
            var retDate = day + "-" + (month) + '-' + year;
            return retDate;
        }
        function addCommas(nStr) {
            nStr += '';
            var x = nStr.split('.');
            var x1 = x[0];
            var x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }
        function GetActivityPlanData(PKPlanID) {

            $.ajax({
                type: "POST",
                url: "ActivityPlan.aspx/GetActivityPlanData",
                data: '{PKPlanID: "' + PKPlanID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        console.log(response.d);
                        var data = JSON.parse(response.d.toString());
                        var lblPeriod = document.getElementById('<%=lblPeriod.ClientID%>');
                        var lblLocation = document.getElementById('<%=lblPlanLocation.ClientID%>');
                        var lblExpBudget = (document.getElementById('<%=lblExpectedBudget.ClientID%>'));
                        var lblBudgetPerUnit = (document.getElementById('<%=lblBudgetPerUnit.ClientID%>'));
                        var lblUnitsPlanned = document.getElementById('<%=lblUnitsPlanned.ClientID%>');
                        var txtAjaxSharing = document.getElementById('<%=txtAjaxSharing.ClientID%>');
                        var txtDealerSharing = document.getElementById('<%=txtDealerSharing.ClientID%>');
                        var txtBudget = document.getElementById('<%=lblExpectedBudget.ClientID%>')
                        SetMaxDate();

                        var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
                        var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
                        var txtLocation = document.getElementById('<%=txtLocation.ClientID%>');
                        var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');
                        var hdnPlanID = document.getElementById('<%=hdnPkPlanID.ClientID%>');

                        if (data.length > 0) {

                            hdnPlanID.value = data[0].AP_PKPlanID;
                            var FromDate = new Date(data[0].AP_FromDate.substring(0, data[0].AP_FromDate.indexOf('T')));
                            var toDate = new Date(data[0].AP_ToDate.substring(0, data[0].AP_ToDate.indexOf('T')));

                            lblPeriod.value = FormatDate(FromDate) + ' to ' + FormatDate(toDate);
                            lblLocation.value = data[0].AP_Location;
                            lblExpBudget.value = addCommas(data[0].AP_ExpBudget);
                            lblBudgetPerUnit.value = addCommas(data[0].AP_BudgetPerUnit);
                            lblUnitsPlanned.value = data[0].AP_NoofUnits + ' ' + data[0].AP_Unit;
                            document.getElementById('lblActUnits').innerHTML = "Actual No of units(" + data[0].AP_Unit + ")"
                            var TotalBudget = parseFloat('0' + txtBudget.value.replace(',', ''));
                            AjaxSharing = (parseFloat(TotalBudget) * parseFloat(data[0].AP_AjaxSharing) / 100).toFixed(0);

                            document.getElementById('lblAjaxSharing').innerHTML = ' (' + data[0].AP_AjaxSharing + '%)';
                            document.getElementById('lblDealerSharing').innerHTML = ' (' + (100 - parseFloat(data[0].AP_AjaxSharing)) + '%)';
                            document.getElementById('lblAjaxSharingA').innerHTML = ' (' + data[0].AP_AjaxSharing + '%)';
                            document.getElementById('lblDealerSharingA').innerHTML = ' (' + (100 - parseFloat(data[0].AP_AjaxSharing)) + '%)';

                            txtAjaxSharing.value = addCommas(AjaxSharing);
                            txtDealerSharing.value = addCommas(TotalBudget - parseFloat(AjaxSharing));
                            localStorage.setItem('AjaxSharing', data[0].AP_AjaxSharing);

                            GetActivityData(data[0].AP_FKActivityID);
                            SetActualSharing();
                        }
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function SaveActivityActual() {

            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID %>');
            var ddlActivity = document.getElementById('<%=ddlPlannedActivity.ClientID %>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var txtExpense = document.getElementById('<%=txtExpBudget.ClientID%>');
            var txtUnits = document.getElementById('<%=txtUnits.ClientID%>');
            var txtLocaton = document.getElementById('<%=txtLocation.ClientID%>');
            var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');
            var txtNDRemarks = document.getElementById('<%=txtNotDoneRemarks.ClientID%>');
            if (ddlStatus.value == '0') {
                alert('Select Status');
                ddlStatus.focus();
                return false;
            }
            if (ddlStatus.value == '1') {
                if (ddlActivity.value == '0') {
                    alert('Select Planned Activity');
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
                if (txtExpense.value == '0' || txtExpense.value == '') {
                    alert('Enter Actual Expenses ');
                    txtExpense.focus();
                    return false;
                }
            }
            else {
                if (txtNDRemarks.value == '') {
                    alert('Enter Remarks');
                    txtNDRemarks.focus();
                }
            }

            return true;
        }
        function Clear() {
            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID%>');
            var ddlActivity = document.getElementById('<%=ddlPlannedActivity.ClientID%>');
            var ddlDealer = document.getElementById('<%=ddlDealer.ClientID%>');
            var txtUnits = document.getElementById('<%=txtUnits.ClientID%>');
            var txtExpBudget = document.getElementById('<%=txtExpBudget.ClientID%>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var txtLocation = document.getElementById('<%=txtLocation.ClientID%>');
            var txtNDRemrks = document.getElementById('<%=txtNotDoneRemarks.ClientID%>');
            var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');
            var hdnPKPlanID = document.getElementById('<%=hdnPkPlanID.ClientID%>');
            var lblPeriod = document.getElementById('<%=lblPeriod.ClientID%>');
            var lblLocation = document.getElementById('<%=lblPlanLocation.ClientID%>');
            var lblExpBudget = document.getElementById('<%=lblExpectedBudget.ClientID%>');
            var lblBudgetPerUnit = document.getElementById('<%=lblBudgetPerUnit.ClientID%>');
            var lblUnitsPlanned = document.getElementById('<%=lblUnitsPlanned.ClientID%>');
            document.getElementById('<%=txtAjaxSharing.ClientID%>').value = '';
            document.getElementById('<%=txtDealerSharing.ClientID%>').value = '';
            document.getElementById('<%=txtAjaxSharingA.ClientID%>').value = '';
            document.getElementById('<%=txtDealerSharingA.ClientID%>').value = '';
            document.getElementById('lblAjaxSharing').innerHTML = '';
            document.getElementById('lblDealerSharing').innerHTML = '';
            document.getElementById('lblAjaxSharingA').innerHTML = '';
            document.getElementById('lblDealerSharingA').innerHTML = '';
            ddlActivity.options.selectedIndex = 0;
            ddlDealer.options.selectedIndex = 0;
            ddlStatus.options.selectedIndex = 0;
            txtUnits.value = ''; txtExpBudget.value = ''; txtFromDate.value = ''; txtToDate.value = '';
            txtLocation.value = ''; txtRemarks.value = ''; hdnPKPlanID.value = '0';
            lblPeriod.value = ''; lblLocation.value = ''; lblExpBudget.value = ''; lblBudgetPerUnit.value = ''; lblUnitsPlanned.value = '';
            txtNDRemrks.value = '';
        }
        function CheckStatus(Status) {
            var ddlActivity = document.getElementById('<%=ddlPlannedActivity.ClientID%>');
            if (ddlActivity.value == 0) {
                alert('Select Planned Activity');
                document.getElementById('<%=ddlStatus.ClientID%>').value = 0;
                return false;
            }
            if (Status == 1) {
                document.getElementById('divDone').style.display = '';
                document.getElementById('divNotDone').style.display = 'none';
            }
            else if (Status == 2) {
                document.getElementById('divDone').style.display = 'none';
                document.getElementById('divNotDone').style.display = '';
            }
            else {
                document.getElementById('divDone').style.display = 'none';
                document.getElementById('divNotDone').style.display = 'none';
            }
        }
        function uploadComplete(s, e) {
            console.log(s);
            console.log(e);
            alert('File Uploaded Successfully!');
        }
        function uploadError(s, e) {
            console.log(e);
        }
        function readURL(input, ImageID) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#Img' + ImageID).attr('src', e.target.result);
                    $('#Img' + ImageID).css("display", "block");
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        function FileUpladed() {
            alert('File Uploaded Successfully!');
            CheckStatus(1);
        }
        function FileError(msg) {
            alert(msg);
            CheckStatus(1);
        }
        function FileDeleted() {
            CheckStatus(1);
        }
        function SetActualDates(FromDate, ToDate) {
            document.getElementById('<%= txtFromDate.ClientID %>').value = FromDate;
            document.getElementById('<%= txtToDate.ClientID %>').value = ToDate;
        }
        function PrintInvoice(vardata) {
            window.open('ActivityInvoice.aspx?AID=' + vardata, 'newwindow', 'toolbar=no,location=no,menubar=no,width=1000,height=600,titlebar=no, fullscreen=no,resizable=yes,scrollbars=yes,top=60,left=60'); return false;

        }
    </script>
    <style type="text/css">
        .uploadButton {
            background-color: transparent;
            background-image: url('../Images/Attach.png');
            background-size: 50px;
            background-repeat: no-repeat;
        }

        .FileUploadClass {
            background-color: white;
            font-size: 5px;
        }

            .FileUploadClass input {
                background-color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS_Scripts.js"></script>--%>
    <div class="col-md-12">
        <div class="col-md-12" id="divEntry">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Actual</legend>
                <div class="col-md-12">
                    <table>
                        <tr>
                            <td>Plan Detail</td>
                        </tr>
                    </table>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlDealer">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlPlannedActivity">Planned Activity</label>
                            <asp:DropDownList ID="ddlPlannedActivity" OnSelectedIndexChanged="ddlPlannedActivity_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="lblPeriod">Period</label>
                            <asp:TextBox Enabled="false" ID="lblPeriod" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="lblUnitsPlanned">No. of Unit Planned</label>
                            <asp:TextBox Enabled="false" ID="lblUnitsPlanned" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="lblBudgetPerUnit">Budget Per Unit</label>
                            <asp:TextBox Enabled="false" ID="lblBudgetPerUnit" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="lblExpectedBudget">Expected Budget</label>
                            <asp:TextBox Enabled="false" ID="lblExpectedBudget" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="txtAjaxSharing">Ajax Sharing</label><label id="lblAjaxSharing" runat="server"></label>
                            <%--<input type="text" disabled="disabled" runat="server" id="txtAjaxSharing" cssclass="form-control" />--%>
                            <asp:TextBox ID="txtAjaxSharing" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="txtDealerSharing">Dealers Sharing</label><label id="lblDealerSharing" runat="server"></label>
                            <%--<input type="text" disabled="disabled" runat="server" id="txtDealerSharing" cssclass="form-control" />--%>
                            <asp:TextBox ID="txtDealerSharing" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="lblPlanLocation">Location</label>
                            <asp:TextBox Enabled="false" ID="lblPlanLocation" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>Actual Detail</td>
                        </tr>
                    </table>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="txtUnits">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" onchange="CheckStatus(this.value);" CssClass="form-control">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Done" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Not Done" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12" id="divNotDone" style="display: none">
                            <label for="txtNotDoneRemarks">Remarks</label>
                            <textarea runat="server" id="txtNotDoneRemarks" cssclass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-12" id="divDone" style="display: none">
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label id="lblActUnits" runat="server" for="txtUnits">Actual No. of Units</label>
                                <%--<input type="text" onkeyup="SetActualBudget();" runat="server" id="txtUnits" cssclass="form-control" />--%>
                                <asp:TextBox runat="server" ID="txtUnits" CssClass="form-control" onkeyup="SetActualBudget();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtFromDate">From Date</label>
                                <%--<input type="text" runat="server" id="txtFromDate" cssclass="form-control" />--%>
                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                                <%--<cc1:CalendarExtender ID="CalFrom" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtToDate">To Date</label>
                                <%--<input type="text" runat="server" id="txtToDate" cssclass="form-control" />--%>
                                <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                                <%--<cc1:CalendarExtender ID="CalTo" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtExpBudget">Actual Expense</label>
                                <%--<input type="text" runat="server" id="txtExpBudget" onkeyup="SetActualSharing();" cssclass="form-control" />--%>
                                <asp:TextBox ID="txtExpBudget" runat="server" CssClass="form-control" onkeyup="SetActualSharing();"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtAjaxSharing">Ajax Sharing</label><label id="lblAjaxSharingA" runat="server"></label>
                                <%--<input type="text" disabled="disabled" runat="server" id="txtAjaxSharingA" cssclass="form-control" />--%>
                                <asp:TextBox ID="txtAjaxSharingA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtDealerSharing">Dealers Sharing</label><label id="lblDealerSharingA" runat="server"></label>
                                <%--<input type="text" disabled="disabled" runat="server" id="txtDealerSharingA" cssclass="form-control" />--%>
                                <asp:TextBox ID="txtDealerSharingA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtLocation">Location</label>
                                <%--<input type="text" runat="server" id="txtLocation" cssclass="form-control" />--%>
                                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="txtRemarks">Remarks</label>
                                <%--<textarea runat="server" id="txtRemarks" cssclass="form-control" />--%>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>Images and Attachments</td>
                            </tr>
                        </table>
                        <div class="col-md-12" id="divAttach" runat="server">
                            <div class="col-md-2 col-sm-12">
                                <label for="flUplaod1">Attachment 1</label>
                                <asp:FileUpload ID="flUpload1" runat="server" onchange="readURL(this,1)" capture="camera" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="flUplaod2">Attachment 2</label>
                                <asp:FileUpload ID="flUpload2" runat="server" onchange="readURL(this,2)" capture="camera" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="flUplaod3">Attachment 3</label>
                                <asp:FileUpload ID="flUpload3" runat="server" onchange="readURL(this,3)" capture="camera" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="flUplaod4">Attachment 4</label>
                                <asp:FileUpload ID="flUpload4" runat="server" onchange="readURL(this,4)" capture="camera" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <img id="Img1" src="" width="100px" height="80px" style="margin: 10px; display: none;" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <img id="Img2" src="" width="100px" height="80px" style="margin: 10px; display: none;" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <img id="Img3" src="" width="100px" height="80px" style="margin: 10px; display: none;" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <img id="Img4" src="" width="100px" height="80px" style="margin: 10px; display: none;" />
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAjaxSharing" runat="server" />
                        <div class="col-md-12">
                            <asp:DataList ID="lstImages" OnItemDataBound="lstImages_ItemDataBound" runat="server" RepeatDirection="Horizontal" Visible="false">
                                <ItemTemplate>
                                    <asp:Image ID="img" runat="server" ImageUrl='<%# Bind("AttachedFile") %>' Width="150px" Height="120px" />
                                    <asp:HiddenField ID="hdnDocID" Value='<%# Bind("AD_PKDocID") %>' runat="server" />
                                    <br />
                                    <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click" CommandArgument='<%# Bind("AD_PKDocID") %>' runat="server" Text="Download"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" OnClientClick="return SaveActivityActual();" CssClass="btn Search" Text="Submit" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                            <asp:Button type="submit" Text="Cancel" OnClientClick="Clear();" CssClass="btn Back" ID="btnCancel" OnClick="btnCancel_Click" runat="server" />
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnPkPlanID" runat="server" Value="0" />
                </div>
            </fieldset>
        </div>
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Existing Activity Actual Search</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlDealerSearch">Dealer</label>
                        <asp:DropDownList ID="ddlDealerSearch" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlDateOn">Date on</label>
                        <asp:DropDownList ID="ddlDateOn" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Plan" Value="P"></asp:ListItem>
                            <asp:ListItem Text="Actual" Value="A"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtFromDateSearch">From Date</label>
                        <asp:TextBox runat="server" ID="txtFromDateSearch" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="calFromDateSearch" runat="server" TargetControlID="txtFromDateSearch" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtToDateSearch">To Date</label>
                        <asp:TextBox runat="server" ID="txtToDateSearch" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="calToDateSearch" runat="server" TargetControlID="txtToDateSearch" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-4 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="Search" runat="server" Text="Search" CssClass="btn Search" OnClick="Search_Click" />
                        <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn Back" OnClick="btnExcel_Click" Width="120px" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Actual Detail Report</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvData" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true" OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="Activity No" DataField="ControlNo">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Activity" DataField="Activity">
                                <ItemStyle Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plan Period" DataField="PlanPeriod">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="Status">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Actual Period" DataField="ActualPeriod">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="No. of Units">
                                <HeaderTemplate>
                                    <table width="100%" style="text-align: center">
                                        <tr>
                                            <td colspan="2" style="text-align: center">No. of Units
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" style="text-align: center">Plan
                                            </td>
                                            <td width="50%" style="text-align: center">Actual
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="50%" style="text-align: center">
                                                <asp:Label runat="server" ID="PlanNoofUnits" Text='<%# Eval("PlanNoofUnits") %>'></asp:Label>
                                            </td>
                                            <td width="50%" style="text-align: center">
                                                <asp:Label runat="server" ID="ActualNoofUnits" Text='<%# Eval("ActualNoofUnits") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expense">
                                <HeaderTemplate>
                                    <table width="100%" style="text-align: center">
                                        <tr>
                                            <td colspan="2" style="text-align: center">Expenses
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" style="text-align: center">Plan
                                            </td>
                                            <td width="50%" style="text-align: center">Actual
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="50%" style="text-align: right">
                                                <asp:Label runat="server" ID="ExpBudget" Text='<%# Eval("ExpBudget") %>'></asp:Label>
                                            </td>
                                            <td width="50%" style="text-align: right">
                                                <asp:Label runat="server" ID="ActualBudget" Text='<%# Eval("ActualExpense") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Width="9%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <HeaderTemplate>
                                    <table width="100%" style="text-align: center">
                                        <tr>
                                            <td colspan="2" style="text-align: center">Location
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" style="text-align: left">Plan
                                            </td>
                                            <td width="50%" style="text-align: left">Actual
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="50%">
                                                <asp:Label runat="server" ID="PlanLocation" Text='<%# Eval("PlanLocation") %>'></asp:Label>
                                            </td>
                                            <td width="50%">
                                                <asp:Label runat="server" ID="ActualLocation" Text='<%# Eval("ActualLocation") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Remarks" DataField="ActualRemarks">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Approval Status" DataField="ApprovalStatus">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Approved Amount" DataField="ApprovedAmount">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" CommandArgument='<%# Bind("PKPlanID") %>' OnClick="lnkEdit_Click" runat="server" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGenerateInvoice" Visible="false" CommandArgument='<%# Bind("PKActualID") %>' OnClick="lnkGenerateInvoice_Click" runat="server" Text="Generate Invoice"></asp:LinkButton>
                                    <asp:HiddenField ID="hdnApprovalLevel" runat="server" Value='<%# Bind("AAP_ApprovalLevel") %>' />
                                    <asp:HiddenField ID="hdnApprovalStatus" runat="server" Value='<%# Bind("ApprovalStatusID") %>' />
                                    <asp:HiddenField ID="hdnInvID" runat="server" Value='<%# Bind("AIH_PkHdrID") %>' />
                                    <asp:HiddenField ID="hdnActualID" runat="server" Value='<%# Bind("PKActualID") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
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
</asp:Content>
