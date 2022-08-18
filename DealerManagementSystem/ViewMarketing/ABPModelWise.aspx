<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ABPModelWise.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ABPModelWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .cal_Theme1 .ajax__calendar_prev {
            display: none;
        }

        .cal_Theme1 .ajax__calendar_next {
            display: none;
        }

        .ajax__calendar_invalid .ajax__calendar_day {
            display: none;
        }

        .lblDayPlan {
            color: #0b4562;
            font-size: 8pt;
            font-family: Arial;
            font-weight: bold;
            margin-top: 50px;
            margin-right: 5px;
            box-shadow: #467d7e 10px 0px unset;
        }

            .lblDayPlan table {
                background-color: azure;
            }

            .lblDayPlan td {
                padding: 2px;
                border-bottom: 1px solid;
                border-top: 1px solid;
            }

        #divCalander {
            width: 85%;
            padding: 17px;
            position: absolute;
            top: 10px;
            left: 150px;
            background-color: white;
            border: 1px solid #1a425c;
            border-radius: 5px;
            z-index: 3000;
            text-align: center;
        }

        .calCell {
            background: rgb(147,193,217);
            background: radial-gradient(circle, rgba(147,193,217,1) 0%, rgba(52,179,209,1) 100%);
        }
    </style>
    <script type="text/javascript">

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
        function CheckNumeric(e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }

        }
        function Validate() {
            try {
                var ddlDealer = document.getElementById('<%=ddlDealer.ClientID %>');
                var gvID = '<%=gvPlan.ClientID %>';
                var ddlProduct = document.getElementById(gvID + '_ddlProduct');
                var ddlModel = document.getElementById(gvID + '_ddlModel');
                var arrMonths = ['txtApr', 'txtMay', 'txtJun', 'txtJul', 'txtAug', 'txtSep', 'txtOct', 'txtNov', 'txtDec', 'txtJan', 'txtFeb', 'txtMar'];
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
                var arrlblModel = document.getElementsByClassName('lblModel');
                for (var i = 0; i < arrlblModel.length; i++) {
                    var hdnID = document.getElementById(arrlblModel[i].id.replace('lblModel', 'hdnModelID'));
                    if (hdnID.value == ddlModel.value) {
                        alert('Duplicate Model');
                        return false;
                    }
                }
                var Plan = 0;
                for (var i = 0; i < arrMonths.length; i++) {
                    Plan += parseInt('0' + document.getElementById(gvID + "_" + arrMonths[i]).value);
                }
                if (Plan == 0) {
                    alert("No plan to save");
                    return false;
                }
            }
            catch (err) {
                alert(err.message);
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"/>
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">ABP Model Wise</legend>
                <div class="col-md-12" id="divHdr" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlDealer">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label for="ddlDealer">Year</label>
                            <asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-8 text-left">
                            <label class="modal-label">-</label>
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn Back" OnClick="btnExport_Click" Width="120px" />
                            <asp:Button ID="btnExportAll" runat="server" Text="Export to Excel All Dealer" CssClass="btn Back" OnClick="btnExportAll_Click" Width="200px" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12" id="divEntry" runat="server">
                    <div class="col-md-12 Report">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Details</legend>
                            <div class="col-md-12 Report">
                                <asp:GridView ID="gvPlan" CssClass="table table-bordered table-condensed Grid" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="false" OnRowDataBound="gvPlan_RowDataBound"
                                    OnDataBound="gvPlan_DataBound" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("Product") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CDDProduct" runat="server" TargetControlID="ddlProduct" UseContextKey="true" ServiceMethod="GetProduct" PromptText="Select" ContextKey=""
                                                    Category="ProductID"></cc1:CascadingDropDown>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                            <FooterStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" CssClass="lblModel" EnableTheming="false" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <cc1:CascadingDropDown ID="CDDModel" runat="server" ParentControlID="ddlProduct" UseContextKey="true" TargetControlID="ddlModel" ServiceMethod="GetModels" PromptText="Select"
                                                    Category="ModelID"></cc1:CascadingDropDown>
                                            </FooterTemplate>
                                            <ItemStyle Width="20%" />
                                            <FooterStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:Label ID="Apr" runat="server" Text='<%# Bind("Apr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtApr" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:Label ID="May" runat="server" Text='<%# Bind("May") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMay" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:Label ID="Jun" runat="server" Text='<%# Bind("Jun") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtJun" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Q1">
                                            <ItemTemplate>
                                                <asp:Label ID="Q1" runat="server" Text='<%# Bind("Q1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                =
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:Label ID="Jul" runat="server" Text='<%# Bind("Jul") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtJul" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:Label ID="Aug" runat="server" Text='<%# Bind("Aug") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAug" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:Label ID="Sep" runat="server" Text='<%# Bind("Sep") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtSep" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Q2">
                                            <ItemTemplate>
                                                <asp:Label ID="Q2" runat="server" Text='<%# Bind("Q2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                =
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:Label ID="Oct" runat="server" Text='<%# Bind("Oct") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtOct" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:Label ID="Nov" runat="server" Text='<%# Bind("Nov") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNov" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:Label ID="Dec" runat="server" Text='<%# Bind("Dec") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDec" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Q3">
                                            <ItemTemplate>
                                                <asp:Label ID="Q3" runat="server" Text='<%# Bind("Q3") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                =
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:Label ID="Jan" runat="server" Text='<%# Bind("Jan") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtJan" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:Label ID="Feb" runat="server" Text='<%# Bind("Feb") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFeb" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:Label ID="Mar" runat="server" Text='<%# Bind("Mar") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMar" onkeydown="CheckNumeric(event)" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Q4">
                                            <ItemTemplate>
                                                <asp:Label ID="Q4" runat="server" Text='<%# Bind("Q4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                =
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="Total" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                =
                                            </FooterTemplate>
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <FooterStyle Width="4%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" OnClick="lnkDel_Click" runat="server" Text="Delete" Width="50px" Height="33px"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnModelID" runat="server" Value='<%# Bind("ModelID") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAdd" OnClientClick="return Validate()" runat="server" CssClass="btn Save" OnClick="btnAdd_Click" Text="Save & Add More" Width="135px" Height="33px"/>
                                            </FooterTemplate>
                                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                                            <FooterStyle Width="6%" HorizontalAlign="Center" />
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
            </fieldset>
        </div>
    </div>
</asp:Content>
