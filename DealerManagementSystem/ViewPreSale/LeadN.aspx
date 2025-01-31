<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LeadN.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.LeadN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadView.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLead.ascx" TagPrefix="UC" TagName="UC_AddLead" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var hdfCustomerID = document.getElementById('MainContent_UC_Customer_hdfCustomerID');
            if (hdfCustomerID.value != "") {
                document.getElementById('divCustomerViewID').style.display = "block";
                document.getElementById('divCustomerCreateID').style.display = "none";

                document.getElementById('lblCustomerName').innerText = document.getElementById('MainContent_UC_Customer_hdfCustomerName').value;
                document.getElementById('lblContactPerson').innerText = document.getElementById('MainContent_UC_Customer_hdfContactPerson').value;
                document.getElementById('lblMobile').innerText = document.getElementById('MainContent_UC_Customer_hdfMobile').value;
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset id="fsCriteria" class="fieldset-border" >
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Employee</label>
                        <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Lead Number</label>
                        <asp:TextBox ID="txtLeadNumber" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Lead Date From</label>
                        <asp:TextBox ID="txtLeadDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Lead Date To</label>
                        <asp:TextBox ID="txtLeadDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="col-md-2 text-left">
                        <label>Qualification</label>
                        <asp:DropDownList ID="ddlSQualification" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSSource" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label class="modal-label">Sales Channel</label>
                        <asp:DropDownList ID="ddlSSalesChannelType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Customer</label>
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Country</label>
                        <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Region</label>
                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Product Type</label>
                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-10 text-left">
                        <label class="modal-label">Action</label>
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        <asp:Button ID="btnAddLead" runat="server" CssClass="btn Save" Text="Add Lead" OnClick="btnAddLead_Click" Width="150px"></asp:Button>
                        <%--<asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />--%>
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Lead(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnLeadArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeadArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnLeadArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnLeadArrowRight_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" ToolTip="Excel Download..." />
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                                <div style="float: right; overflow: auto;">
                                    <%--<div style="float :left">
                                             
                                        </div>--%>
                                    <div style="float: right">
                                        <img id="fs" alt="" src="../Images/NormalScreen.png" onclick="ScreenControl(2)" width="23" height="23" style="display: none;" />
                                        <img id="rs" alt="" src="../Images/FullScreen.jpg" onclick="ScreenControl(1)" width="23" height="23" style="display: block;" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                            PageSize="15" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging" EmptyDataText="No Data Found"
                            OnRowDataBound="gvLead_RowDataBound">

                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<i class='fa fa-eye fa-1x' aria-hidden='true'></i>" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:Button ID="btnViewLead" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewLead_Click" Width="75px" Height="25px" />--%>
                                        <asp:ImageButton ID="btnViewLead" ImageUrl="~/Images/Preview.png" runat="server" ToolTip="View..." Height="20px" Width="20px" ImageAlign="Middle" OnClick="btnViewLead_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lead Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadID" Text='<%# DataBinder.Eval(Container.DataItem, "LeadID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "LeadNumber")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "LeadDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Type">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDivision" runat="server" ImageUrl="~/Images/SpareParts.png" Width="25" Height="25" />
                                        <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQualification" Text='<%# DataBinder.Eval(Container.DataItem, "Qualification.Qualification")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source.Source")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="S.Channel" DataField="SalesChannelType.ItemText"></asp:BoundField>--%>

                                <asp:TemplateField HeaderText="S.Channel" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White">
                                    <ItemStyle VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Image ID="imgChannel" runat="server" ImageUrl="~/Images/b2c.png" Width="40" Height="40" />

                                        <asp:Label ID="lblChannel" Text='<%# DataBinder.Eval(Container.DataItem, "SalesChannelType.ItemText")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerFullName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created">
                                    <ItemStyle VerticalAlign="Middle" Font-Size="XX-Small" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server" />
                                        <br />
                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_LeadView ID="UC_LeadView" runat="server"></UC:UC_LeadView>
        </div>
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
    <asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Lead</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageLead" runat="server" Text="" CssClass="message" Visible="false" />
                <fieldset class="fieldset-border">
                    <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                    <UC:UC_AddLead ID="UC_AddLead" runat="server"></UC:UC_AddLead>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <script type="text/javascript"> 
        function GetCustomerAuto() {
            $("#MainContent_UC_Customer_hdfCustomerID").val('');
            var param = { Cust: $('#MainContent_UC_Customer_txtCustomerName').val() }
            var Customers = [];
            if ($('#MainContent_UC_Customer_txtCustomerName').val().trim().length >= 3) {
                $.ajax({
                    url: "Visit.aspx/GetCustomer1",
                    contentType: "application/json; charset=utf-8",
                    type: 'POST',
                    data: JSON.stringify(param),
                    dataType: 'JSON',
                    success: function (data) {
                        var DataList = JSON.parse(data.d);
                        for (i = 0; i < DataList.length; i++) {
                            Customers[i] = {
                                value: DataList[i].CustomerName,
                                value1: DataList[i].CustomerID,
                                CustomerType: DataList[i].CustomerType,
                                ContactPerson: DataList[i].ContactPerson,
                                Mobile: DataList[i].Mobile
                            };
                        }
                        $('#MainContent_UC_Customer_txtCustomerName').autocomplete({
                            source: function (request, response) { response(Customers) },
                            select: function (e, u) {
                                $("#MainContent_UC_Customer_hdfCustomerID").val(u.item.value1);
                                document.getElementById('divCustomerViewID').style.display = "block";
                                document.getElementById('divCustomerCreateID').style.display = "none";

                                $("#MainContent_UC_Customer_hdfCustomerName").val(u.item.value);
                                $("#MainContent_UC_Customer_hdfContactPerson").val(u.item.ContactPerson);
                                $("#MainContent_UC_Customer_hdfMobile").val(u.item.Mobile);

                                document.getElementById('lblCustomerName').innerText = u.item.value;
                                document.getElementById('lblContactPerson').innerText = u.item.ContactPerson;
                                document.getElementById('lblMobile').innerText = u.item.Mobile;


                            },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $('#MainContent_UC_Customer_txtCustomerName').width() + 48,
                                });
                                $(this).autocomplete("widget").scrollTop(0);
                            }
                        }).focus(function (e) {
                            $(this).autocomplete("search");
                        }).click(function () {
                            $(this).autocomplete("search");
                        }).data('ui-autocomplete')._renderItem = function (ul, item) {

                            var inner_html = FormatAutocompleteList(item);
                            return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                                .data('item.autocomplete', item)
                                .append(inner_html)
                                .appendTo(ul);
                        };

                    }
                });
            }
            else {
                $('#MainContent_UC_Customer_txtCustomerName').autocomplete({
                    source: function (request, response) {
                        response($.ui.autocomplete.filter(Customers, ""))
                    }
                });
            }
        }
    </script>
    <script type="text/javascript">

        function GetProjectAuto(id) {
            debugger;
            var parentIDC = "";
            var parentID = "";
            if ($('#MainContent_UC_AddLead_txtProject').val().trim().length >= 3) {
                parentIDC = "#MainContent_UC_AddLead_"
                parentID = "MainContent_UC_AddLead_"
            }
            else if ($('#MainContent_UC_LeadView_UC_AddLead_txtProject').val().trim().length >= 3) {
                parentIDC = "#MainContent_UC_LeadView_UC_AddLead_"
                parentID = "MainContent_UC_LeadView_UC_AddLead_"
            }
            else {
                return;
            }
            var param = { Pro: $(parentIDC + 'txtProject').val() }
            var Customers = [];
            if ($(parentIDC + 'txtProject').val().trim().length >= 3) {
                $.ajax({
                    url: "LeadN.aspx/GetProject",
                    contentType: "application/json; charset=utf-8",
                    type: 'POST',
                    data: JSON.stringify(param),
                    dataType: 'JSON',
                    success: function (data) {
                        var DataList = JSON.parse(data.d);
                        for (i = 0; i < DataList.length; i++) {
                            Customers[i] = {
                                value: DataList[i].ProjectName,
                                ProjectID: DataList[i].ProjectID,
                                TenderNumber: DataList[i].TenderNumber,
                                State: DataList[i].State.State,
                                District: DataList[i].District.District,
                                ProjectValue: DataList[i].Value,
                                ContractAwardDate: DataList[i].ContractAwardDate,
                                ContractEndDate: DataList[i].ContractEndDate
                            };
                        }
                        $(parentIDC + 'txtProject').autocomplete({
                            source: function (request, response) { response(Customers) },
                            select: function (e, u) {
                                debugger;

                                $(parentIDC + "hdfProjectID").val(u.item.ProjectID);
                                document.getElementById(parentID + "txtProject").disabled = true;
                                //document.getElementById('divCustomerViewID').style.display = "block";
                                //document.getElementById('divCustomerCreateID').style.display = "none";

                                //document.getElementById('lblCustomerName').innerText = u.item.value;
                                //document.getElementById('lblContactPerson').innerText = u.item.ContactPerson;
                                //document.getElementById('lblMobile').innerText = u.item.Mobile;

                            },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $(parentIDC + 'txtProject').width() + 48,
                                });
                                $(this).autocomplete("widget").scrollTop(0);
                            }
                        }).focus(function (e) {
                            $(this).autocomplete("search");
                        }).click(function () {
                            $(this).autocomplete("search");
                        }).data('ui-autocomplete')._renderItem = function (ul, item) {

                            var inner_html = FormatAutocompleteProject(item);
                            return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                                .data('item.autocomplete', item)
                                .append(inner_html)
                                .appendTo(ul);
                        };

                    }
                });
            }
            else {
                $(parentIDC + 'txtProject').autocomplete({
                    source: function (request, response) {
                        response($.ui.autocomplete.filter(Customers, ""))
                    }
                });
            }
        }
    </script>


</asp:Content>
