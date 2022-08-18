<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LostCustomerData.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.LostCustomerData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                var ddlMon = document.getElementById('<%=ddlMon.ClientID%>');
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
    <style>
        .scrollingControlContainer {
            overflow-x: auto;
            overflow-y: scroll;
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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Lost Customer Data</legend>
                <div class="col-md-12">
                    <asp:Button ID="btnNew" Width="100px" OnClientClick="return confirm('Do you want to create new entry');" runat="server" Text="Add" CssClass="btn Approval" OnClick="btnNew_Click" />
                    <asp:Button ID="btnSearchTab" Width="100px" runat="server" Text="Search" CssClass="btn Search" OnClick="btnSearchTab_Click" />
                </div>
                <div id="divEntry" class="col-md-12" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">State</label>
                            <asp:DropDownList ID="ddlState" runat="server" DataSourceID="odbcState" DataTextField="State"
                                DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" DataTextField="CodeWithName" DataValueField="DID" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Location</label>
                            <asp:TextBox ID="txtLoctaion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Year</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Month</label>
                            <asp:DropDownList ID="ddlMon" runat="server" CssClass="form-control">
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
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Customer Name</label>
                            <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Contact Person</label>
                            <asp:TextBox ID="txtConPerson" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Contact Detail</label>
                            <asp:TextBox ID="txtContDeatil" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Ajax Model</label>
                            <asp:DropDownList ID="ddlAjax" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Ajax Price</label>
                            <asp:TextBox ID="txtAjaxPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="fteNumber" runat="server" TargetControlID="txtAjaxPrice" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Competitor Make</label>
                            <asp:DropDownList ID="ddlCompititor" AutoPostBack="true" OnSelectedIndexChanged="ddlCompititor_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Comeptitor Model</label>
                            <asp:DropDownList ID="ddlComModel" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Comeptitor Price</label>
                            <asp:TextBox ID="txtComPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtComPrice" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Quantity</label>
                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtQty" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">No of Visits</label>
                            <asp:TextBox ID="txtNOofVisit" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNOofVisit" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Reason Type</label>
                            <asp:DropDownList ID="ddlResType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlState">Remarks</label>
                            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="btnSave" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" runat="server" />
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
                <div id="divSearch" class="col-md-12" visible="false" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">State</label>
                                <asp:DropDownList ID="ddlStateSrch" runat="server" DataSourceID="odbcState" DataTextField="State" CssClass="form-control"
                                    DataValueField="StateID" OnSelectedIndexChanged="ddlStateSrch_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">Dealer</label>
                                <asp:DropDownList ID="ddlDlrSrch" runat="server" DataTextField="CodeWithName" DataValueField="DID" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">From Year</label>
                                <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">From Month</label>
                                <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="form-control">
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
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">To Year</label>
                                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">To Month</label>
                                <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="form-control">
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
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">Ajax Model</label>
                                <asp:DropDownList ID="ddlAjaxModelSrch" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">Competitor Make</label>
                                <asp:DropDownList ID="ddlCompetitorSrch" AutoPostBack="true" OnSelectedIndexChanged="CompetitorSrch_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <label for="ddlState">Comeptitor Model</label>
                                <asp:DropDownList ID="ddlCompetitorMdlSrch" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 text-left">
                                <label class="modal-label">-</label>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn Search" OnClick="btnSearch_Click" runat="server" />
                                <asp:Button ID="btnExport" Text="Export" CssClass="btn Back" OnClick="btnExport_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 Report">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvSearch" Width="100%" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
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
                                    <AlternatingRowStyle BackColor="#ffffff" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                    <EmptyDataTemplate>
                                        <div class="align-content-center">No records found.</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>