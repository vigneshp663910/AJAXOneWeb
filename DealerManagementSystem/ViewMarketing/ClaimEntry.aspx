<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ClaimEntry.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ClaimEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <script type="text/javascript">
        function SetActualSharing() {
            var txtAjaxSharingA = document.getElementById('<%=txtAjaxSharingA.ClientID%>');
            var txtDealerSharingA = document.getElementById('<%=txtDealerSharingA.ClientID%>');
            var txtExpBudget = document.getElementById('<%=txtExpenses.ClientID%>');
            var TotalBudget = parseFloat('0' + txtExpBudget.value.replace(',', ''));
            var AjaxSharing = document.getElementById('<%=hdnAjaxSharing.ClientID%>').value;
            var AjaxSharingA = (parseFloat(TotalBudget) * parseFloat('0' + AjaxSharing) / 100).toFixed(0);
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
                        var txtExpenses = document.getElementById('<%=txtExpenses.ClientID%>');
                        if (data.length > 0) {
                            if (parseInt('0' + document.getElementById('<%= hdnActualID.ClientID %>').value) == 0) {
                                txtExpenses.value = data[0].Budget;
                                localStorage.setItem('AjaxSharing', data[0].AjaxSharing);
                                document.getElementById('lblAjaxSharingA').innerHTML = ' (' + data[0].AjaxSharing + '%)';
                                document.getElementById('lblDealerSharingA').innerHTML = ' (' + (100 - parseFloat(data[0].AjaxSharing)) + '%)';
                                document.getElementById('lblActUnits').innerHTML = 'No of Units(' + data[0].UnitDesc + ')';
                                SetActualSharing();
                            }
                        }
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function SetActualSharing() {
            var txtExpense = document.getElementById('<%=txtExpenses.ClientID%>');

            var txtNoofUnits = document.getElementById('<%=txtUnits.ClientID%>');
            var txtAjaxSharing = document.getElementById('<%= txtAjaxSharingA.ClientID%>');
            var txtDealerSharing = document.getElementById('<%=txtDealerSharingA.ClientID%>');
            var TotalBudget = parseFloat('0' + txtExpense.value);
            var AjaxSharing = document.getElementById('<%=hdnAjaxSharing.ClientID%>').value;
            txtAjaxSharing.value = (parseFloat(TotalBudget) * parseFloat('0' + AjaxSharing) / 100).toFixed(0);
            txtDealerSharing.value = TotalBudget - parseFloat(txtAjaxSharing.value);
        }
        function SaveActivityActual() {

            var ddlDealer = document.getElementById('<%=ddlDealer.ClientID %>');
            var ddlActivity = document.getElementById('<%=ddlActivity.ClientID %>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var txtExpense = document.getElementById('<%=txtExpenses.ClientID%>');
            var txtUnits = document.getElementById('<%=txtUnits.ClientID%>');
            var txtLocaton = document.getElementById('<%=txtLocation.ClientID%>');
            var txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>');

            if (ddlDealer.value == '0') {
                alert('Select Dealer');
                ddlActivity.focus();
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
            if (txtExpense.value == '0' || txtExpense.value == '') {
                alert('Enter Expenses ');
                txtExpense.focus();
                return false;
            }
            return true;
        }
    </script>
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
        <asp:HiddenField ID="hdnAjaxSharing" runat="server" />
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Claim Entry</legend>
                <div class="col-md-12">
                    <asp:Button ID="btnNew" Text="New Claim" runat="server" CssClass="btn Save" OnClick="btnNew_Click" Width="90px"></asp:Button>
                    <asp:Button ID="btnExisting" Style="margin-left: 15px" Text="Existing Claims" runat="server" CssClass="btn Back" OnClick="btnExisting_Click" Width="120px"></asp:Button>
                </div>
                <div class="col-md-12" id="divEntry" runat="server">
                    <table>
                        <tr>
                            <td>Detail</td>
                        </tr>
                    </table>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlDealer">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="false" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlPlannedActivity">Activity</label>
                            <asp:DropDownList ID="ddlActivity" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label id="lblActUnits" runat="server" for="txtUnits">No. of Units</label>
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
                            <%--<input type="text" runat="server" id="txtExpenses" onkeyup="SetActualSharing();" cssclass="form-control" />--%>
                            <asp:TextBox runat="server" ID="txtExpenses" CssClass="form-control" onkeyup="SetActualSharing();"></asp:TextBox>
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
                    <div class="col-md-12">
                        <asp:DataList ID="lstImages" OnItemDataBound="lstImages_ItemDataBound" runat="server" RepeatDirection="Horizontal" Visible="false">
                            <ItemTemplate>
                                <asp:Image ID="img" runat="server" ImageUrl='<%# Bind("AttachedFile") %>' Width="150px" Height="120px" CssClass="form-control"/>
                                <asp:HiddenField ID="hdnDocID" Value='<%# Bind("AD_PKDocID") %>' runat="server" />
                                <br />
                                <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click" CommandArgument='<%# Bind("AD_PKDocID") %>' runat="server" Text="Download"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" OnClientClick="return SaveActivityActual();" Text="Submit" runat="server" OnClick="btnSubmit_Click" CssClass="btn Save"></asp:Button>
                            <asp:Button type="submit" Text="Cancel" OnClientClick="Clear();" ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="btn Back" />
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnPkPlanID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnActualID" runat="server" Value="0" />
                </div>
            </fieldset>
        </div>
        <div class="col-md-12" id="divSearch" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Existing Entry</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlDealerSearch">Dealer</label>
                            <asp:DropDownList ID="ddlDealerSearch" runat="server" CssClass="form-control"></asp:DropDownList>
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
                        <div class="col-md-6 text-left">
                            <label class="modal-label">-</label>
                            <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" CssClass="btn Search" />
                            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_Click" CssClass="btn Back" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Detail</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvData" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                            OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Activity No" DataField="ControlNo">
                                    <ItemStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Activity" DataField="Activity">
                                    <ItemStyle Width="12%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Date" DataField="ActualPeriod">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="No. of Units">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ActualNoofUnits" Text='<%# Eval("ActualNoofUnits") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ActualBudget" Text='<%# Eval("ActualExpense") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="9%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ActualLocation" Text='<%# Eval("ActualLocation") %>'></asp:Label>
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
                                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%# Bind("PKActualID") %>' runat="server" Text="Edit"></asp:LinkButton>
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
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
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
                    <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1"
                        runat="server" Enabled="True" HorizontalSide="Center" VerticalOffset="200"
                        TargetControlID="pnlProg"></cc1:AlwaysVisibleControlExtender>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
