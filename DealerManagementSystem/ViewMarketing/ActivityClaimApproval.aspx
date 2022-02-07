<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ActivityClaimApproval.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityClaimApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function GetActivityData(ActivityID) {
            $.ajax({
                type: "POST",
                url: "YDMS_ActivityInfoM.aspx/GetActivityInfo",
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
        <ContentTemplate>

            <div class="container-fluid" id="divSearch" runat="server">

                <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                    <h4 style="width: 100%; padding-right: 10px; vertical-align: middle">Activity Claim Approval
                  <asp:Label ID="lbllevel" runat="server" EnableTheming="false"></asp:Label>
                    </h4>
                </div>
                <div id="divSearch1">
                    <div class="row">
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlDealerSearch">Dealer</label>
                            <asp:DropDownList ID="ddlDealerSearch" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtControlNoSrch">Activity No.</label>
                            <input type="text" runat="server" id="txtControlNoSrch" />
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtFromDateSearch">From Date</label>
                            <asp:TextBox runat="server" ID="txtFromDateSearch"></asp:TextBox>
                            <cc1:calendarextender id="calFromDateSearch" runat="server" targetcontrolid="txtFromDateSearch" format="dd-MMM-yyyy"></cc1:calendarextender>

                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="txtToDateSearch">To Date</label>
                            <asp:TextBox runat="server" ID="txtToDateSearch"></asp:TextBox>
                            <cc1:calendarextender id="calToDateSearch" runat="server" targetcontrolid="txtToDateSearch" format="dd-MMM-yyyy"></cc1:calendarextender>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlAppStatus">Approval Status</label>
                            <asp:DropDownList ID="ddlAppStatus" runat="server">
                                <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xl-1 col-lg-1 col-md-2 col-sm-3 col-6" style="text-align: right; vertical-align: bottom; padding-top: 28px;">

                            <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
                        </div>
                        <div class="col-xl-1 col-lg-1 col-md-3 col-sm-3 col-6" style="text-align: right; vertical-align: bottom; padding-top: 28px;">
                            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_Click" />

                        </div>
                    </div>
                    <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                        <h6>Detail</h6>
                    </div>
                    <asp:GridView ID="gvData" CssClass="gridclass" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="98%" OnRowDataBound="gvData_RowDataBound">
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
                    </asp:GridView>
                </div>
            </div>
            <div class="container-fluid" id="divStatus" runat="server">
                <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                    <h4 style="width: 100%; padding-right: 10px; vertical-align: middle">Activity Actual<img style="float: right; cursor: pointer" id="imgdivEntry" src="../Images/grid_collapse.png" onclick="ShowHide(this,'divEntry')" /></h4>
                </div>
                <div id="divEntry">
                    <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                        <h6>Plan Detail</h6>
                    </div>
                    <div class="row">
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlDealer">Dealer</label>
                            <asp:TextBox Enabled="false" ID="txtDealer" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                            <label for="ddlPlannedActivity">Activity</label>
                            <asp:TextBox Enabled="false" ID="txtPlannedActivity" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_Period">
                            <label for="lblPeriod">Period</label>
                            <asp:TextBox Enabled="false" ID="lblPeriod" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_Units">
                            <label for="lblUnitsPlanned">No. of Unit Planned</label>
                            <asp:TextBox Enabled="false" ID="lblUnitsPlanned" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_Budget">
                            <label for="lblBudgetPerUnit">Budget Per Unit</label>
                            <asp:TextBox Enabled="false" ID="lblBudgetPerUnit" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_TotalBudget">
                            <label for="lblExpectedBudget">Expected Budget</label>
                            <asp:TextBox Enabled="false" ID="lblExpectedBudget" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_AjaxSharing">
                            <label for="txtAjaxSharing">Ajax Sharing</label><label id="lblAjaxSharing"></label>
                            <input type="text" disabled="disabled" runat="server" id="txtAjaxSharing" />

                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_DlrSharing">
                            <label for="txtDealerSharing">Dealers Sharing</label><label id="lblDealerSharing"></label>
                            <input type="text" disabled="disabled" runat="server" id="txtDealerSharing" />

                        </div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" id="Plan_Location">
                            <label for="lblPlanLocation">Location</label>
                            <asp:TextBox Enabled="false" ID="lblPlanLocation" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                        <h6>Actual Detail</h6>
                    </div>

                    <div id="divDone">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtUnits">Actual No. of Units</label>
                                <input type="text" disabled="disabled" onkeyup="SetActualBudget();" runat="server" id="txtUnits" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtFromDate">From Date</label>
                                <input type="text" disabled="disabled" runat="server" id="txtFromDate" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtToDate">To Date</label>
                                <input type="text" disabled="disabled" runat="server" id="txtToDate" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtExpBudget">Actual Expense</label>
                                <input type="text" disabled="disabled" runat="server" id="txtExpBudget" onkeyup="SetActualSharing();" />


                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtAjaxSharing">Ajax Sharing</label><label id="lblAjaxSharingA"></label>
                                <input type="text" disabled="disabled" runat="server" id="txtAjaxSharingA" />

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtDealerSharing">Dealers Sharing</label><label id="lblDealerSharingA"></label>
                                <input type="text" disabled="disabled" runat="server" id="txtDealerSharingA" />

                            </div>


                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtLocation">Location</label>
                                <input type="text" disabled="disabled" runat="server" id="txtLocation" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="txtRemarks">Remarks</label>
                                <textarea runat="server" disabled="disabled" id="txtRemarks" />
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-4 col-sm-6 col-12">
                                <label for="lstImages">
                                    <h6>Images and Attachments</h6>
                                </label>
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
                        <div class="row" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                            <h6>Approvals</h6>
                        </div>
                        <div class="row">
                            <div class="col-xl-6 col-lg-6 col-md-8 col-sm-12 col-12" id="divApp1" runat="server">
                                <div class="row">
                                    <div>
                                        <h6>Approval Level 1</h6>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-12">
                                        <label for="ddlAppStatus1">Approval Status 1</label>
                                        <asp:DropDownList ID="ddlAppStatus1" runat="server" onchange="FillAmount(this,1)">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Approve" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Reject" Value="9"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-12">
                                        <label for="ddlAppStatus1">Approved Amount</label>
                                        <input type="text" runat="server" id="txtApp1Amount" onkeyup="CheckAppAmount(this,1)" />
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-12">
                                        <label for="txtApp1Remarks">Remarks</label>
                                        <textarea runat="server" id="txtApp1Remarks" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-6 col-lg-6 col-md-8 col-sm-12 col-12" id="divApp2" runat="server">
                                <div class="row">
                                    <h6>Approval Level 2</h6>
                                </div>
                                <div class="row">

                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-12">
                                        <label for="ddlAppStatus2">Approval Status 2</label>
                                        <asp:DropDownList ID="ddlAppStatus2" runat="server" onchange="FillAmount(this,2)">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Approve" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="Reject" Value="11"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-12">
                                        <label for="ddlAppStatus1">Approved Amount</label>
                                        <input type="text" runat="server" id="txtApp2Amount" onkeyup="CheckAppAmount(this,2)" />
                                    </div>
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-12">
                                        <label for="txtApp2Remarks">Remarks</label>
                                        <textarea runat="server" id="txtApp2Remarks" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-10 col-lg-10 col-md-8 col-sm-6 col-12"></div>
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; padding-top: 30px">

                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click"></asp:Button>
                            <asp:Button type="submit" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" runat="server" />
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-xl-4 col-lg-4 col-md-8 col-sm-6 col-12">
                            <asp:HiddenField ID="hdnPkPlanID" runat="server" Value="0" />
                        </div>


                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updMainProg" runat="server" AssociatedUpdatePanelID="updPanel">
                <ProgressTemplate>
                    <asp:Panel ID="pnlProg" runat="server" Width="280px" BackImageUrl="~/Images/LoadingPnlbg.png"
                        Height="95px" CssClass="progPanel">
                        <br />
                        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/Loading.gif" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:alwaysvisiblecontrolextender id="AlwaysVisibleControlExtender1"
                runat="server" enabled="True" horizontalside="Center" verticaloffset="200"
                targetcontrolid="pnlProg">
            </cc1:alwaysvisiblecontrolextender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
