<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ActivityInfoM.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityInfoM" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet"  />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
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
                url: "YDMS_ActivityInfoM.aspx/GetActivityInfo",
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
                url: "YDMS_ActivityInfoM.aspx/SaveActivityInfo",
                data: '{ActivityID: "' + ddlActivity.value + '",FunctionaAreaID: "' + ddlFunctionalArea.value + '",UnitID: "' + ddlUnit.value + '",dblBudget: "' + txtBudget.value + '", dblAjaxSharing: "' + txtAjaxSharing.value + '", dblDealerSharing: "' + txtDealerSharing.value + '", SAC: "' + txtSAC.value + '", GST: "' + ddlGST.value + '",ActivityType:"'+ddlActivityType.value+'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        if (response.d.toString() == "Saved") {
                            alert("Saved Successfully" );
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
        <ContentTemplate>
    <div class="container-fluid">
        <div class="row" style="background-color:#3665c2;color:white;margin-bottom:10px">
            <h4 >Activity Information Master</h4>
            </div>
        <div class="row">
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="ddlActivity">Activity</label>
                <asp:DropDownList ID="ddlActivity" runat="server">

                </asp:DropDownList>
                
            </div>
             <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="ddlFunctionalArea">Activity Type</label>
                <asp:DropDownList ID="ddlActivityType" runat="server">
                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                    <asp:ListItem Text="Field Activity" Value="FA"></asp:ListItem>
                    <asp:ListItem Text="Invoice Activity" Value="IA"></asp:ListItem>
                </asp:DropDownList>                
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="ddlFunctionalArea">Functional Area</label>
                <asp:DropDownList ID="ddlFunctionalArea" runat="server">

                </asp:DropDownList>                
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="txtSAC">SAC</label>
                <input type="text" runat="server" id="txtSAC" />                
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="ddlGST">GST %</label>
                <asp:DropDownList ID="ddlGST" runat="server">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="5 %" Value="5"></asp:ListItem>
                    <asp:ListItem Text="12%" Value="12"></asp:ListItem>
                    <asp:ListItem Text="18%" Value="18"></asp:ListItem>
                    <asp:ListItem Text="28%" Value="28"></asp:ListItem>
                </asp:DropDownList>                
            </div>
            
        
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="ddlUnit">Unit</label>
                <asp:DropDownList ID="ddlUnit" runat="server">

                </asp:DropDownList>                
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="txtBudget">Budget Per Unit</label>
                <input type="text" runat="server" id="txtBudget" />
                
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="txtAjaxSharing">Ajax Sharing %</label>
                <input type="text" onkeyup="CheckValue(this);SetDealerSharing(this)" runat="server" id="txtAjaxSharing" />
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                <label for="txtDealerSharing">Dealer Sharing %</label>
                <input type="text" runat="server" disabled="disabled" id="txtDealerSharing" />
            </div>
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align:right;padding-top:30px;">
                <input type="submit" id="btnSubmit" onclick="return SaveActivityInfo();" value="Submit" runat="server">
                <input type="submit" value="Cancel" onclick="return Clear();" runat="server">
            </div>
        </div>
        
    </div>
    <hr />
    

        
    <div class="container-fluid">
          
           <div class="row" style="background-color:#3665c2;color:white;margin-bottom:10px">
            <h4 >Filter: Activity Information Master</h4>
            </div>
               <div class="row">
                     <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for="ddlActivitySearch">Activity</label>
                        <asp:DropDownList ID="ddlActivitySearch" runat="server"></asp:DropDownList>
                    </div>
                 
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align:right;vertical-align:bottom; padding-top:28px;">                        
                        <asp:Button ID="Search" runat="server" Text="Search" OnClick="btnSearch_Click" />                                                
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align:right;vertical-align:bottom; padding-top:28px;">
                       <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_Click" />
                        
                    </div>
               </div>
         <div class="row" style="background-color:#3665c2;color:white;margin-bottom:10px">
            <h6 >Detail</h6>
            </div>
           
        <asp:GridView ID="gvData"  CssClass="gridclass" AllowPaging="true" PageSize="20" runat="server" ShowHeaderWhenEmpty="true" 
            OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="false" Width="95%" OnPageIndexChanging="gvData_PageIndexChanging"
            OnPageIndexChanged="gvData_PageIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="Activity" DataField="ActivityName" >
                    <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Code" DataField="ActivityCode" >
                    <ItemStyle Width="10%" HorizontalAlign="Center"/>
                </asp:BoundField>
                 <asp:BoundField HeaderText="Activity Type" DataField="ActivityType" >
                    <ItemStyle Width="8%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Functional Area" DataField="FunctionalArea" >
                    <ItemStyle Width="8%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="SAC" DataField="SAC" >
                    <ItemStyle Width="8%" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="GST" DataField="GST" DataFormatString="{0:n0}" HeaderStyle-HorizontalAlign="Center" >
                    <ItemStyle Width="8%"  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Unit" DataField="Unit" HeaderStyle-HorizontalAlign="Center" >
                    <ItemStyle Width="8%"  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Budget per Unit" DataField="AI_Budget" HeaderStyle-HorizontalAlign="Center" >
                    <ItemStyle Width="10%"  HorizontalAlign="Right"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Ajax Sharing %" DataField="AI_AjaxSharing" HeaderStyle-HorizontalAlign="Center" >
                    <ItemStyle Width="10%"  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Dealer Sharing %" DataField="AI_DealerSharing" HeaderStyle-HorizontalAlign="Center" >
                    <ItemStyle Width="10%"  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click"  CommandArgument='<%# Bind("ActivityID") %>' runat="server" Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField id="hdnSWidth" runat="server" />
</asp:Content>

