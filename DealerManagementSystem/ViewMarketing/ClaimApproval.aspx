<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ClaimApproval.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ClaimApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>--%>
    <script type="text/javascript">
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
                            localStorage.setItem('AjaxSharing', data[0].AjaxSharing);
                            document.getElementById('lblAjaxSharing').innerHTML = ' (' + data[0].AjaxSharing + '%)';
                            document.getElementById('lblDealerSharing').innerHTML = ' (' + (100 - parseFloat(data[0].AjaxSharing)) + '%)';
                            document.getElementById('lblAjaxSharingA').innerHTML = ' (' + data[0].AjaxSharing + '%)';
                            document.getElementById('lblDealerSharingA').innerHTML = ' (' + (100 - parseFloat(data[0].AjaxSharing)) + '%)';
                            var txtAjaxSharing = document.getElementById('<%=txtAjaxSharing.ClientID%>');
                            var txtDealerSharing = document.getElementById('<%=txtDealerSharing.ClientID%>');
                            var txtBudget = document.getElementById('<%=lblExpectedBudget.ClientID%>')
                            var TotalBudget = parseFloat('0' + txtBudget.value);
                            txtAjaxSharing.value = (parseFloat(TotalBudget) * parseFloat(localStorage.AjaxSharing) / 100).toFixed(0);
                            txtDealerSharing.value = TotalBudget - parseFloat(txtAjaxSharing.value);
                        }
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function SetActualDates(FromDate, ToDate) {
            document.getElementById('<%= txtFromDate.ClientID %>').value = FromDate;
            document.getElementById('<%= txtToDate.ClientID %>').value = ToDate;
        }
        function FillAmount(ddlStatus, iType) {
            if (iType == 1) {
                if (ddlStatus.value == 8) {
                    document.getElementById('<%= txtApp1Amount.ClientID %>').value = document.getElementById('<%= txtAjaxSharingA.ClientID %>').value;
                }
                else {
                    document.getElementById('<%= txtApp1Amount.ClientID %>').value = 0;
                }
            }
            if (iType == 2) {
                if (ddlStatus.value == 10) {
                    document.getElementById('<%= txtApp2Amount.ClientID %>').value = document.getElementById('<%= txtApp1Amount.ClientID %>').value;
                }
                else {
                    document.getElementById('<%= txtApp2Amount.ClientID %>').value = 0;
                }
            }
        }
        function CheckAppAmount(ctl, iType) {
            if (iType == 1) {
                if (parseFloat(ctl.value) > parseFloat(document.getElementById('<%= txtAjaxSharingA.ClientID %>').value)) {
                    alert('Approval amount can not be greater than claimed amount(Ajax Share)');
                    document.getElementById('<%= txtApp1Amount.ClientID %>').value = document.getElementById('<%= txtAjaxSharingA.ClientID %>').value;
                }
            }
            if (iType == 2) {
                if (parseFloat(ctl.value) > parseFloat(document.getElementById('<%= txtApp1Amount.ClientID %>').value)) {
                    alert('Approval amount can not be greater than approved amount');
                    document.getElementById('<%= txtApp2Amount.ClientID %>').value = document.getElementById('<%= txtApp1Amount.ClientID %>').value;
                }
            }
        }
        function HidePlan(blnCheck) {
            if (blnCheck) {
                var arrdivs = ['Plan_Period', 'Plan_Units', 'Plan_Budget', 'Plan_TotalBudget', 'Plan_AjaxSharing', 'Plan_DlrSharing', 'Plan_Location'];
                for (var i = 0; i < arrdivs.length; i++) {
                    document.getElementById(arrdivs[i]).style.display = 'none';
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
        <div class="col-md-12" id="divSearch" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Claim Approval<asp:Label ID="lbllevel" runat="server" EnableTheming="false"></asp:Label></legend>
                <div class="col-md-12" id="divSearch1">
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlDealerSearch">Dealer</label>
                            <asp:DropDownList ID="ddlDealerSearch" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="txtControlNoSrch">Activity No.</label>
                            <%--<input type="text" runat="server" id="txtControlNoSrch" CssClass="form-control"/>--%>
                            <asp:TextBox ID="txtControlNoSrch" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlAppStatus">Approval Status</label>
                            <asp:DropDownList ID="ddlAppStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="Search" runat="server" Text="Search" CssClass="btn Search" OnClick="Search_Click" />
                            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn Back" OnClick="btnExcel_Click" Width="120px" />
                        </div>
                    </div>
                    <div class="col-md-12 Report">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Detail</legend>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvData" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="98%" OnRowDataBound="gvData_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Dealer" DataField="Dealer">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Activity No." DataField="ControlNo">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Activity" DataField="Activity">
                                            <ItemStyle Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Plan Period" DataField="PlanPeriod">
                                            <ItemStyle Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Actual Period" DataField="ActualPeriod">
                                            <ItemStyle Width="12%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No of Units">
                                            <HeaderTemplate>
                                                <table width="100%" style="text-align: center">
                                                    <tr>
                                                        <td colspan="2" style="text-align: center">No of Units
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
                                            <ItemStyle Width="8%" />
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
                                                        <td width="50%" style="text-align: center">
                                                            <asp:Label runat="server" ID="ExpBudget" Text='<%# Eval("ExpBudget") %>'></asp:Label>
                                                        </td>
                                                        <td width="50%" style="text-align: center">
                                                            <asp:Label runat="server" ID="ActualBudget" Text='<%# Eval("ActualExpense") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" />
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
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Remarks" DataField="ActualRemarks">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Approval Status" DataField="ApprovalStatus">
                                            <ItemStyle Width="4%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" CommandArgument='<%# Bind("AA_PKActualID") %>' OnClick="lnkEdit_Click" Visible='<%# Eval("EditStatus").ToString() == "E" ? true : false %>' runat="server" Text="Update"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnActualID" runat="server" Value='<%# Bind("AA_PKActualID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
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
            </fieldset>
        </div>
        <div class="col-md-12" id="divStatus" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Actual</legend>
                <div class="col-md-12" id="divEntry">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Plan Detail</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlDealer">Dealer</label>
                                <asp:TextBox Enabled="false" ID="txtDealer" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlPlannedActivity">Activity</label>
                                <asp:TextBox Enabled="false" ID="txtPlannedActivity" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_Period">
                                <label for="lblPeriod">Period</label>
                                <asp:TextBox Enabled="false" ID="lblPeriod" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_Units">
                                <label for="lblUnitsPlanned">No. of Unit Planned</label>
                                <asp:TextBox Enabled="false" ID="lblUnitsPlanned" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_Budget">
                                <label for="lblBudgetPerUnit">Budget Per Unit</label>
                                <asp:TextBox Enabled="false" ID="lblBudgetPerUnit" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_TotalBudget">
                                <label for="lblExpectedBudget">Expected Budget</label>
                                <asp:TextBox Enabled="false" ID="lblExpectedBudget" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_AjaxSharing">
                                <label for="txtAjaxSharing">Ajax Sharing</label><label id="lblAjaxSharing"></label>
                                <%--<input type="text" disabled="disabled" runat="server" id="txtAjaxSharing" />--%>
                                <asp:TextBox ID="txtAjaxSharing" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_DlrSharing">
                                <label for="txtDealerSharing">Dealers Sharing</label><label id="lblDealerSharing"></label>
                                <%--<input type="text" disabled="disabled" runat="server" id="txtDealerSharing" />--%>
                                <asp:TextBox ID="txtDealerSharing" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2 col-sm-12" id="Plan_Location">
                                <label for="lblPlanLocation">Location</label>
                                <asp:TextBox Enabled="false" ID="lblPlanLocation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Actual Detail</legend>
                        <div id="divDone">
                            <div class="col-md-12">
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtUnits">Actual No. of Units</label>
                                    <%--<input type="text" disabled="disabled" onkeyup="SetActualBudget();" runat="server" id="txtUnits" />--%>
                                    <asp:TextBox runat="server" ID="txtUnits" CssClass="form-control" onkeyup="SetActualBudget();" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtFromDate">From Date</label>
                                    <%--<input type="text" disabled="disabled" runat="server" id="txtFromDate" />--%>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalFrom" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtToDate">To Date</label>
                                    <%--<input type="text" disabled="disabled" runat="server" id="txtToDate" />--%>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalTo" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtExpBudget">Actual Expense</label>
                                    <%--<input type="text" disabled="disabled" runat="server" id="txtExpBudget" onkeyup="SetActualSharing();" />--%>
                                    <asp:TextBox ID="txtExpBudget" runat="server" CssClass="form-control" Enabled="false" onkeyup="SetActualSharing();"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtAjaxSharing">Ajax Sharing</label><label id="lblAjaxSharingA"></label>
                                    <%--<input type="text" disabled="disabled" runat="server" id="txtAjaxSharingA" />--%>
                                    <asp:TextBox ID="txtAjaxSharingA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtDealerSharing">Dealers Sharing</label><label id="lblDealerSharingA"></label>
                                    <%--<input type="text" disabled="disabled" runat="server" id="txtDealerSharingA" />--%>
                                    <asp:TextBox ID="txtDealerSharingA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtLocation">Location</label>
                                    <%--<input type="text" disabled="disabled" runat="server" id="txtLocation" />--%>
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label for="txtRemarks">Remarks</label>
                                    <%--<textarea runat="server" disabled="disabled" id="txtRemarks" />--%>
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label for="lstImages">Images and Attachments</label>
                                    <asp:DataList ID="lstImages" OnItemDataBound="lstImages_ItemDataBound" runat="server" RepeatDirection="Horizontal" Visible="false">
                                        <ItemTemplate>
                                            <asp:Image ID="img" runat="server" ImageUrl='<%# Bind("AttachedFile") %>' Width="150px" Height="120px" CssClass="form-control" />
                                            <asp:HiddenField ID="hdnDocID" Value='<%# Bind("AD_PKDocID") %>' runat="server" />
                                            <br />
                                            <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click" CommandArgument='<%# Bind("AD_PKDocID") %>' runat="server" Text="Download"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                            <h4>
                                <label>Approvals</label></h4>
                            <div class="col-md-12" id="divApp1" runat="server">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Approval Level 1</legend>
                                    <div class="col-md-12">
                                        <div class="col-md-2 col-sm-12">
                                            <label for="ddlAppStatus1">Approval Status 1</label>
                                            <asp:DropDownList ID="ddlAppStatus1" runat="server" onchange="FillAmount(this,1)" CssClass="form-control">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Approve" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Reject" Value="9"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-12">
                                            <label for="ddlAppStatus1">Approved Amount</label>
                                            <%--<input type="text" runat="server" id="txtApp1Amount" onkeyup="CheckAppAmount(this,1)" />--%>
                                            <asp:TextBox ID="txtApp1Amount" runat="server" onkeyup="CheckAppAmount(this,1)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 col-sm-12">
                                            <label for="txtApp1Remarks">Remarks</label>
                                            <%--<textarea runat="server" id="txtApp1Remarks" />--%>
                                            <asp:TextBox ID="txtApp1Remarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-12" id="divApp2" runat="server">
                                <fieldset class="fieldset-border">
                                    <legend style="background: none; color: #007bff; font-size: 17px;">Approval Level 2</legend>
                                    <div class="col-md-12">
                                        <div class="col-md-2 col-sm-12">
                                            <label for="ddlAppStatus2">Approval Status 2</label>
                                            <asp:DropDownList ID="ddlAppStatus2" runat="server" onchange="FillAmount(this,2)" CssClass="form-control">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Approve" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Reject" Value="11"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 col-sm-12">
                                            <label for="ddlAppStatus1">Approved Amount</label>
                                            <%--<input type="text" runat="server" id="txtApp2Amount" onkeyup="CheckAppAmount(this,2)" />--%>
                                            <asp:TextBox ID="txtApp2Amount" runat="server" onkeyup="CheckAppAmount(this,2)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 col-sm-12">
                                            <label for="txtApp2Remarks">Remarks</label>
                                            <%--<textarea runat="server" id="txtApp2Remarks" />--%>
                                            <asp:TextBox ID="txtApp2Remarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </fieldset>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn Save" OnClick="btnSubmit_Click"></asp:Button>
                        <asp:Button type="submit" Text="Cancel" ID="btnCancel" CssClass="btn Back" OnClick="btnCancel_Click" runat="server" />
                    </div>
                    <asp:HiddenField ID="hdnPkPlanID" runat="server" Value="0" />
                </div>
            </fieldset>
        </div>
    </div>
    <%--<asp:UpdatePanel ID="updPanel" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
        <ContentTemplate>
            <asp:UpdateProgress ID="updMainProg" runat="server" AssociatedUpdatePanelID="updPanel">
                <ProgressTemplate>
                    <asp:Panel ID="pnlProg" runat="server" Width="280px" BackImageUrl="~/Images/LoadingPnlbg.png"
                        Height="95px" CssClass="progPanel">
                        <br />
                        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/Loading.gif" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1"
                runat="server" Enabled="True" HorizontalSide="Center" VerticalOffset="200"
                TargetControlID="pnlProg"></cc1:AlwaysVisibleControlExtender>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

