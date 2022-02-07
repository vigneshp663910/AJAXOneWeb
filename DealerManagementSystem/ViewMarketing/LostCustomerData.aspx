<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LostCustomerData.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.LostCustomerData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .scrollingControlContainer {
            overflow-x: auto;
            overflow-y: scroll;
        }
    </style>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS_Scripts.js"></script>
    <script type="text/javascript">


        function SaveCustomerSale() {
            try {
                var txtLoctaion = document.getElementById('<%=txtLoctaion.ClientID %>');
                var txtAjaxPrice = document.getElementById('<%=txtAjaxPrice.ClientID %>');
                var txtComPrice = document.getElementById('<%=txtComPrice.ClientID %>');
                var txtConPerson = document.getElementById('<%=txtConPerson.ClientID %>');
                var txtContDeatil = document.getElementById('<%=txtContDeatil.ClientID %>');
                var txtCustName = document.getElementById('<%=txtCustName.ClientID %>');
                var txtNOofVisit = document.getElementById('<%=txtNOofVisit.ClientID %>');
                var txtQty = document.getElementById('<%=txtQty.ClientID %>');
                var txtremarks = document.getElementById('<%=txtremarks.ClientID %>');
                var ddlAjax = document.getElementById('<%=ddlAjax.ClientID%>');
                var ddlComModel = document.getElementById('<%=ddlComModel.ClientID%>');
                var ddlCompititor = document.getElementById('<%=ddlCompititor.ClientID%>');
                var ddlDealer = document.getElementById('<%=ddlDealer.ClientID%>');

                var ddlResType = document.getElementById('<%=ddlResType.ClientID%>');
                var ddlState = document.getElementById('<%=ddlState.ClientID%>');
                var ddlMon = document.getElementById('<%=ddlMon.ClientID%>')

                var hdnPKID = document.getElementById('<%=hdnPKID.ClientID%>');



                if ((ddlState.value == '0') || (ddlState.value == null)) {
                    alert('Please Select State');
                    ddlState.focus();
                    return false;
                }

                if ((ddlDealer.value == '0') || (ddlDealer.value == null)) {
                    alert('Please Select dealer');
                    ddlDealer.focus();
                    return false;
                }

                if ((txtCustName.value == '') || (txtCustName.value == null)) {
                    alert('Please Enter Customer Name');
                    txtCustName.focus();
                    return false;
                }
                if ((txtContDeatil.value == '') || (txtContDeatil.value == null)) {
                    alert('Please Enter Contact Detail');
                    txtContDeatil.focus();
                    return false;
                }
                if (ddlComModel.value == '0') {
                    alert('Please Select Competitor Model');
                    ddlComModel.focus();
                    return false;
                }
            }
            catch (err) {
                alert(err.message);
            }
        }

        function CheckNumeric(e) {


            // Allow: backspace, delete, tab, escape, enter and .

            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||

                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                console.log(e.keyCode);
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                console.log(e.keyCode);
                e.preventDefault();
            }

        }
    </script>


    <asp:UpdatePanel ID="updPanel" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>




            <div class="container-fluid">
                <div class="row text-center" style="background-color: #3665c2; color: white; margin-bottom: 10px">
                    <h4 style="width: 100%; padding-right: 10px; vertical-align: middle">Lost Customer Data<img style="float: right; cursor: pointer" id="imgdivEntry" src="../Images/grid_collapse.png" onclick="ShowHide(this,'divEntry')" /></h4>
                </div>

                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <asp:Button ID="btnNew" Width="100px" OnClientClick="return confirm('Do you want to create new entry');" runat="server" Text="Add" OnClick="btnNew_Click" />

                    </li>
                    <li class="nav-item" style="padding-left: 10px">
                        <asp:Button ID="btnSearchTab" Width="100px" runat="server" Text="Search" OnClick="btnSearchTab_Click" />

                    </li>

                </ul>
                <div class="tab-content">
                    <div id="divEntry" class="tab-pane active" runat="server">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">State</label>
                                <asp:DropDownList ID="ddlState" runat="server" Width="90%" DataSourceID="odbcState" DataTextField="State"
                                    DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Dealer</label>
                                <asp:DropDownList ID="ddlDealer" runat="server" Width="90%" DataTextField="CodeWithName" DataValueField="DID">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Location</label>
                                <asp:TextBox ID="txtLoctaion" runat="server" Width="90%"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Year</label>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Month</label>
                                <asp:DropDownList ID="ddlMon" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Customer Name</label>
                                <asp:TextBox ID="txtCustName" runat="server" Width="90%"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Contact Person</label>
                                <asp:TextBox ID="txtConPerson" runat="server" Width="90%"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Contact Detail</label>
                                <asp:TextBox ID="txtContDeatil" runat="server" MaxLength="10" Width="90%"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Ajax Model</label>
                                <asp:DropDownList ID="ddlAjax" runat="server" Width="90%"></asp:DropDownList>

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Ajax Price</label>
                                <asp:TextBox ID="txtAjaxPrice" runat="server" Width="90%"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="fteNumber" runat="server" TargetControlID="txtAjaxPrice" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Competitor Make</label>
                                <asp:DropDownList ID="ddlCompititor" AutoPostBack="true" OnSelectedIndexChanged="ddlCompititor_SelectedIndexChanged" runat="server" Width="90%"></asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Comeptitor Model</label>
                                <asp:DropDownList ID="ddlComModel" runat="server" Width="90%"></asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Comeptitor Price</label>
                                <asp:TextBox ID="txtComPrice" runat="server" Width="90%"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtComPrice" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Quantity</label>
                                <asp:TextBox ID="txtQty" runat="server" Width="90%"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtQty" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">No of Visits</label>
                                <asp:TextBox ID="txtNOofVisit" runat="server" Width="90%"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNOofVisit" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Reason Type</label>
                                <asp:DropDownList ID="ddlResType" runat="server" Width="90%"></asp:DropDownList>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-8 col-sm-12 col-12">
                                <label for="ddlState">Remarks</label>
                                <asp:TextBox ID="txtremarks" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="padding-top: 20px">
                                <asp:Button ID="btnSave" Text="Save" CssClass="InputButton" OnClick="btnSave_Click" runat="server" />
                            </div>
                        </div>


                        <asp:HiddenField ID="hdnPKID" runat="Server" />

                        <asp:ObjectDataSource ID="odbcState" runat="server" SelectMethod="GetState" TypeName="Business.BDMS_Geographical_Master">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="1" Name="CountryID" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odbcDealer" runat="server" SelectMethod="GetDealerByStateID" TypeName="Business.BDMS_CustomerSale">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="UserID" Type="Int64" />
                                <asp:ControlParameter ControlID="ddlState" DefaultValue="0" Name="StateID" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <div id="divSearch" visible="false" runat="server">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">State</label>
                                <asp:DropDownList ID="ddlStateSrch" runat="server" Width="90%" DataSourceID="odbcState" DataTextField="State"
                                    DataValueField="StateID" OnSelectedIndexChanged="ddlStateSrch_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Dealer</label>
                                <asp:DropDownList ID="ddlDlrSrch" runat="server" Width="90%" DataTextField="CodeWithName" DataValueField="DID">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">From Year</label>
                                <asp:DropDownList ID="ddlFromYear" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">From Month</label>
                                <asp:DropDownList ID="ddlFromMonth" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">To Year</label>
                                <asp:DropDownList ID="ddlToYear" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">To Month</label>
                                <asp:DropDownList ID="ddlToMonth" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Ajax Model</label>
                                <asp:DropDownList ID="ddlAjaxModelSrch" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Competitor Make</label>
                                <asp:DropDownList ID="ddlCompetitorSrch" AutoPostBack="true" OnSelectedIndexChanged="CompetitorSrch_SelectedIndexChanged" runat="server" Width="90%"></asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <label for="ddlState">Comeptitor Model</label>
                                <asp:DropDownList ID="ddlCompetitorMdlSrch" runat="server" Width="90%">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-1 col-lg-1 col-md-2 col-sm-3 col-6" style="padding-top: 20px">
                                <asp:Button ID="btnSearch" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" runat="server" />
                            </div>
                            <div class="col-xl-1 col-lg-1 col-md-2 col-sm-3 col-6" style="padding-top: 20px">
                                <asp:Button ID="btnExport" Text="Export" CssClass="InputButton" OnClick="btnExport_Click" runat="server" />
                            </div>
                        </div>
                        <br />
                        <asp:GridView ID="gvSearch" Width="100%" runat="server" CssClass="Grid" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="State" HeaderText="State" />
                                <asp:BoundField DataField="Dealer" HeaderText="Dealer" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                <asp:BoundField DataField="Competitor" HeaderText="Competitor" />
                                <asp:BoundField DataField="CompetitorModel" HeaderText="Competitor Model" />
                                <asp:BoundField DataField="CompPrice" HeaderText="Competition Price" />
                                <asp:BoundField DataField="ReasonType" HeaderText="Reason" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# Bind("CS_PkCustSaleID") %>' OnClick="lnkEdit_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="align-content-center">No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>

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
                    <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1"
                        runat="server" Enabled="True" HorizontalSide="Center" VerticalOffset="200"
                        TargetControlID="pnlProg"></cc1:AlwaysVisibleControlExtender>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
