<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ActivityInfoM.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityInfoM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Initialize() {
            ddlActivity = document.getElementById('<%=ddlActivity.ClientID%>');
            ddlFunctionalArea = document.getElementById('<%=ddlFunctionalArea.ClientID%>');
            ddlUnit = document.getElementById('<%=ddlUnit.ClientID%>');
            txtBudget = document.getElementById('<%=txtBudget.ClientID%>');
            txtAjaxSharing = document.getElementById('<%=txtAjaxSharing.ClientID%>');
            txtDealerSharing = document.getElementById('<%=txtDealerSharing.ClientID%>');
            ddlGST = document.getElementById('<%=ddlGST.ClientID%>');
            txtSAC = document.getElementById('<%=txtSAC.ClientID%>');
            ddlActivityType = document.getElementById('<%=ddlActivityType.ClientID%>');
            document.getElementById('<%=hdnSWidth.ClientID%>').value = screen.availWidth;
            console.log(screen.availWidth);
        }
        function CheckValue(ctl) {
            if (ctl.value != '') {
                if (parseInt(ctl.value) < 0) {
                    ctl.value = 0;
                }
                else if (parseInt(ctl.value) > 100) {
                    ctl.value = 100;
                }
            }
        }
        function BindData(data) {
            Initialize();
            try {
                if (data.length > 0) {
                    ddlActivity.value = data[0].ActivityID;
                    ddlFunctionalArea.value = data[0].FunctionalAreaID;
                    ddlUnit.value = data[0].Unit;
                    txtBudget.value = data[0].Budget;
                    txtAjaxSharing.value = data[0].AjaxSharing;
                    txtDealerSharing.value = data[0].DealerSharing;
                    ddlGST.value = data[0].GST;
                    txtSAC.value = data[0].SAC;
                    ddlActivityType.value = data[0].ActivityType;
                }
            }
            catch (err) {
                console.log(err);
            }
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
                        console.log(response.d);
                        var data = JSON.parse(response.d.toString());
                        BindData(data);
                    }
                },
                failure: function (response) {
                    console.log(response);
                }
            });
            return false;
        }
        function SaveActivityInfo() {
            Initialize();
            if (ddlActivity.value == '0') {
                alert('Select Activity');
                ddlActivity.focus();
                return false;
            }
            if (ddlActivityType.value == '0') {
                alert('Select Activity Type');
                ddlActivityType.focus();
                return false;
            }
            if (ddlFunctionalArea.value == '0') {
                alert('Select Functional Area');
                ddlActivity.focus();
                return false;
            }
            if (ddlUnit.value == '0') {
                alert('Select Unit');
                ddlUnit.focus();
                return false;
            }
            if (ddlUnit.value != '12') {
                if (parseFloat('0' + txtBudget.value) == 0) {
                    alert('Enter Budget');
                    txtBudget.focus();
                    return false;
                }
                if (txtAjaxSharing.value == '') {
                    alert('Enter Ajax Sharing');
                    txtAjaxSharing.focus();
                    return false;
                }
            }
            if (txtSAC.value == '') {
                alert('Enter SAC');
                txtSAC.focus();
                return false;
            }
            if (ddlGST.value == '0') {
                alert('Select GST');
                ddlGST.focus();
                return false;
            }
            var data = '{ActivityID: "' + ddlActivity.value + '",FunctionaAreaID: "' + ddlFunctionalArea.value + '",UnitID: "' + ddlUnit.value + '",dblBudget: "' + txtBudget.value + '", dblAjaxSharing: "' + txtAjaxSharing.value + '", dblDealerSharing: "' + txtDealerSharing.value + '", SAC: "' + txtSAC.value + '", GST: "' + ddlGST.value + '"}';
            console.log(data);
            $.ajax({
                type: "POST",
                url: "ActivityInfoM.aspx/SaveActivityInfo",
                data: '{ActivityID: "' + ddlActivity.value + '",FunctionaAreaID: "' + ddlFunctionalArea.value + '",UnitID: "' + ddlUnit.value + '",dblBudget: "' + txtBudget.value + '", dblAjaxSharing: "' + txtAjaxSharing.value + '", dblDealerSharing: "' + txtDealerSharing.value + '", SAC: "' + txtSAC.value + '", GST: "' + ddlGST.value + '",ActivityType:"' + ddlActivityType.value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        if (response.d.toString() == "Saved") {
                            alert("Saved Successfully");
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
        function SetDealerSharing() {
            var txtAjaxSharing = document.getElementById('<%=txtAjaxSharing.ClientID%>');
            var txtDealerSharing = document.getElementById('<%=txtDealerSharing.ClientID%>');
            txtDealerSharing.value = 100 - parseFloat('0' + txtAjaxSharing.value);
        }
        function Clear() {
            Initialize();
            ddlActivity.options.selectedIndex = 0;
            ddlFunctionalArea.options.selectedIndex = 0;
            ddlUnit.options.selectedIndex = 0; txtBudget.value = ''; txtAjaxSharing.value = '', txtDealerSharing.value = '';
            ddlGST.value = '0'; txtSAC.value = '';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>--%>
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Information Master</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlActivity">Activity</label>
                        <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlFunctionalArea">Activity Type</label>
                        <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Field Activity" Value="FA"></asp:ListItem>
                            <asp:ListItem Text="Invoice Activity" Value="IA"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlFunctionalArea">Functional Area</label>
                        <asp:DropDownList ID="ddlFunctionalArea" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtSAC">SAC</label>
                        <%--<input type="text" runat="server" id="txtSAC" CssClass="form-control"/>--%>
                        <asp:TextBox ID="txtSAC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlGST">GST %</label>
                        <asp:DropDownList ID="ddlGST" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="5 %" Value="5"></asp:ListItem>
                            <asp:ListItem Text="12%" Value="12"></asp:ListItem>
                            <asp:ListItem Text="18%" Value="18"></asp:ListItem>
                            <asp:ListItem Text="28%" Value="28"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlUnit">Unit</label>
                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtBudget">Budget Per Unit</label>
                        <%--<input type="text" runat="server" id="txtBudget" CssClass="form-control"/>--%>
                        <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtAjaxSharing">Ajax Sharing %</label>
                        <%--<input type="text" onkeyup="CheckValue(this);SetDealerSharing(this)" runat="server" id="txtAjaxSharing" CssClass="form-control"/>--%>
                        <asp:TextBox ID="txtAjaxSharing" runat="server" CssClass="form-control" onkeyup="CheckValue(this);SetDealerSharing(this)"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label for="txtDealerSharing">Dealer Sharing %</label>
                        <%--<input type="text" runat="server" disabled="disabled" id="txtDealerSharing" CssClass="form-control"/>--%>
                        <asp:TextBox ID="txtDealerSharing" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 text-left">
                        <label class="modal-label">-</label>
                        <input type="submit" id="btnSubmit" class="btn Save" onclick="return SaveActivityInfo();" value="Submit" runat="server">
                        <input type="submit" value="Cancel" class="btn Back" onclick="return Clear();" runat="server">
                    </div>
                </div>
            </fieldset>

            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label for="ddlActivitySearch">Activity</label>
                        <asp:DropDownList ID="ddlActivitySearch" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-10 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="Search" runat="server" Text="Search" CssClass="btn Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="btn Back" OnClick="btnExcel_Click" Width="100px" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Activity Information Master Report</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvData" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" runat="server" ShowHeaderWhenEmpty="true"
                        OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvData_PageIndexChanging"
                        OnPageIndexChanged="gvData_PageIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="Activity" DataField="ActivityName">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Code" DataField="ActivityCode">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Activity Type" DataField="ActivityType">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Functional Area" DataField="FunctionalArea">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="SAC" DataField="SAC">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="GST" DataField="GST" DataFormatString="{0:n0}" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Unit" DataField="Unit" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Budget per Unit" DataField="AI_Budget" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ajax Sharing %" DataField="AI_AjaxSharing" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Dealer Sharing %" DataField="AI_DealerSharing" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%# Bind("ActivityID") %>' runat="server" Text="Edit"></asp:LinkButton>
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
    <asp:HiddenField ID="hdnSWidth" runat="server" />
</asp:Content>

