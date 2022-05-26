<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="RollingPlanModelWise.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.RollingPlanModelWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .cal_Theme1 .ajax__calendar_prev {
    display:none;
    
}
        .cal_Theme1 .ajax__calendar_next {
    display:none;
    
}
        .ajax__calendar_invalid .ajax__calendar_day
        {
            display:none;
        }
        .lblDayPlan{
            color:#0b4562;
            font-size:8pt;
            font-family:Arial;
            font-weight:bold;
            margin-top: 50px;    
            margin-right: 5px;    
            box-shadow:#467d7e 10px 0px unset;
            
        }
        .lblDayPlan table{
            background-color:azure;
        }
        .lblDayPlan td{
            padding:2px;
            border-bottom:1px solid;
            border-top:1px solid;
        }
        #divCalander
        {
            width:85%;padding: 17px;position:absolute;top:10px;left:150px;background-color:white;border:1px solid #1a425c;border-radius:5px;
            z-index:3000;
            text-align:center;
        }
        .calCell{
            background: rgb(147,193,217);
            background: radial-gradient(circle, rgba(147,193,217,1) 0%, rgba(52,179,209,1) 100%);
        }
    </style>
    <script type="text/javascript">
        function Validate() {
            try {
                var ddlDealer = document.getElementById('<%=ddlDealer.ClientID %>');
                var ddlProduct = document.getElementById('<%=ddlProduct.ClientID %>');
                var ddlModel = document.getElementById('<%=ddlModel.ClientID %>');
                var txtDate = document.getElementById('<%=txtDate.ClientID %>');
                var txtNo = document.getElementById('<%=txtNo.ClientID %>');
                if (ddlDealer.value == '0') {
                    alert("Select Dealer");
                    ddlDealer.focus();
                    return false;
                }
                if (ddlProduct.value == "") {
                    alert("Select Product");
                    ddlProduct.focus();
                    return false;
                }
                if (ddlModel.value == "") {
                    alert("Select Model");
                    ddlModel.focus();
                    return false;
                }
                if (txtDate.value == "") {
                    alert('Please select date');
                    txtDate.focus();
                    return false;
                }
                if (txtNo.value == "") {
                    alert('Please select Nos');
                    txtNo.focus();
                    return false;
                }
                var arrlblModel = document.getElementsByClassName('lblModel');
                for (var i = 0; i < arrlblModel.length; i++) {
                    var hdnID = document.getElementById(arrlblModel[i].id.replace('lblModel', 'hdnModelID'));
                    var lblDate = document.getElementById(arrlblModel[i].id.replace('lblModel', 'lblDate'));
                    if (hdnID.value == ddlModel.value && txtDate.value == lblDate.innerHTML) {
                        alert('Model already selected');
                        return false;
                    }
                }
            }
            catch (err) {
                alert(err.message);
                return false;
            }
        }
        function CheckNumber(e) {

        }
        function ShowCal() {
            document.getElementById('divCalander').style.display = '';
            return false;
        }
        function HideCal() {
            document.getElementById('divCalander').style.display = 'none';
            return false;
        }
    </script>
      <asp:UpdatePanel ID="updPanel" runat="server">        
          <Triggers>
              <asp:PostBackTrigger ControlID="btnExport" />
          </Triggers>
        <ContentTemplate>    
             <div class="row" style="background-color:#3665c2;color:white;margin-bottom:10px;padding-left:20px">
            <h4 style="width:100%;padding-right:10px;vertical-align:middle" >Rolling Plan<img style="float:right;cursor:pointer" id="imgdivEntry" src="../Images/grid_collapse.png" onclick="ShowHide(this,'divEntry')" /></h4>            
            </div>
            <div class="container-fluid" id="divHdr" runat="server">
                <div class="row" >
                     <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for="ddlDealer">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" runat="server" Width="100%"></asp:DropDownList>                
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for="ddlDealer">Year</label>
                        <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server" Width="100%"></asp:DropDownList>                
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for="ddlDealer">Month</label>
                        <asp:DropDownList ID="ddlMonth" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%"></asp:DropDownList>                
                    </div>
                    <div class="col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12" style="text-align:right">
                        <asp:LinkButton ID="lnkCal" runat="server" OnClientClick="return ShowCal();" Text="View Calander"></asp:LinkButton>
                        
                    </div>
                </div>
             </div>
            <div class="container-fluid" id="divEntry" runat="server">
                <div class="row" style="margin-top:20px" >
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for="ddlDealer">Product</label>
                        <asp:DropDownList ID="ddlProduct" runat="server" Width="100%"></asp:DropDownList>                
                        <cc1:CascadingDropDown ID="CDDProduct" runat="server" TargetControlID="ddlProduct" UseContextKey="true" ServiceMethod="GetProduct" PromptText="Select"
                             Category="ProductID"></cc1:CascadingDropDown>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for="ddlDealer">Model</label>
                        <asp:DropDownList ID="ddlModel" runat="server" Width="100%"></asp:DropDownList>                
                        <cc1:CascadingDropDown ID="CDDModel" runat="server" ParentControlID="ddlProduct" UseContextKey="true" TargetControlID="ddlModel" ServiceMethod="GetModels" PromptText="Select"
                             Category="ModelID"></cc1:CascadingDropDown>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label  for='<%= txtDate.ClientID %>'>Date</label>
                        <asp:TextBox ID="txtDate" autocomplete="off" runat="server" Width="100%"></asp:TextBox>                
                        <cc1:CalendarExtender ID="CalDate" CssClass="cal_Theme1"  runat="server" TargetControlID="txtDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                    </div>
                     <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        <label for='<%= txtNo.ClientID %>'>Number</label>
                       
                          <asp:TextBox ID="txtNo" autocomplete="off" runat="server" Width="100%"></asp:TextBox> 
                         <cc1:FilteredTextBoxExtender ID="fteNumber" runat="server" TargetControlID="txtNo" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="padding-top:30px">
                        
                        <asp:Button id="btnAdd" OnClientClick="return Validate();" runat="server" Text="Add" OnClick="btnAdd_Click" Width="150px" />
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="padding-top:30px">
                        
                        <asp:Button id="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" Width="150px" />
                    </div>
                </div>
                <div class="container-fluid" style="margin-top:20px;text-align:left" >
                       <asp:GridView ID="gvPlan" CssClass="gridclass" runat="server" Width="70%" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvPlan_RowDataBound"
                           OnDataBound="gvPlan_DataBound">

                           <Columns>
                               <asp:TemplateField HeaderText="Product">
                                   <ItemTemplate>
                                       <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("Product") %>'></asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle Width="20%" />
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Model">
                                   <ItemTemplate>
                                       <asp:Label ID="lblModel" CssClass="lblModel" EnableTheming="false" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle Width="35%" />
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Date">
                                   <ItemTemplate>
                                       <asp:Label ID="lblDate" runat="server" Text='<%# Bind("PlanDate") %>'></asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle Width="20%" HorizontalAlign="Center" />
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Nos">
                                   <ItemTemplate>
                                       <asp:Label ID="lblNo" runat="server" Text='<%# Bind("PlanNo") %>'></asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle Width="15%" HorizontalAlign="Center" />
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Action">
                                   <ItemTemplate>
                                       <asp:LinkButton ID="lnkDel" Visible='<%# DateTime.Now.Day<6 %>' OnClick="lnkDel_Click" runat="server" Text="Delete"></asp:LinkButton>
                                       <asp:HiddenField ID="hdnModelID" runat="server" Value='<%# Bind("ModelID") %>' />                                          
                                       <asp:HiddenField ID="hdnTag" runat="server" Value='<%# Bind("Tag") %>' />                                          
                                   </ItemTemplate>
                                   <ItemStyle Width="10%" HorizontalAlign="Center" />
                               </asp:TemplateField>
                               
                           </Columns>
                           
                       </asp:GridView>
                </div>
                <div class="container-fluid" style="margin-top:20px;" >
                    <div class="col-xl-10 col-lg-10 col-md-12 col-sm-12 col-12" style="padding-left:69%">
                        <asp:Button ID="btnSubmit" runat="server" Width="150px" Visible="false" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                        
                    </div>
                 </div>
            </div>
            <div class="container-fluid" id="divCalander" style="display:none">
                
                <div class="container-fluid" style="text-align:right;" >
                     <asp:LinkButton ID="imgClose" runat="server" Text="Close" OnClientClick="return HideCal()"></asp:LinkButton>
                </div>
                <div class="row" >                
                 <asp:Calendar ID="CalPlan" OnDayRender="CalPlan_DayRender" runat="server" ShowGridLines="true" ShowNextPrevMonth="false" >
                     <DayStyle Height="80px" Width="200px" VerticalAlign="Top" HorizontalAlign="Right" />                     
                 </asp:Calendar>   
                    </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
