<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EnquiryN.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.EnquiryN" %>

<%@ Register Src="~/ViewPreSale/UserControls/EnquiryViewN.ascx" TagPrefix="UC" TagName="UC_EnquiryView" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var hdfCustomerID = document.getElementById('MainContent_UC_EnquiryView_UC_Customer_hdfCustomerID');
            if (hdfCustomerID != null)
                if (hdfCustomerID.value != "") {
                    document.getElementById('divCustomerViewID').style.display = "block";
                    document.getElementById('divCustomerCreateID').style.display = "none";

                    document.getElementById('lblCustomerName').innerText = document.getElementById('MainContent_UC_EnquiryView_UC_Customer_hdfCustomerName').value;
                    document.getElementById('lblContactPerson').innerText = document.getElementById('MainContent_UC_EnquiryView_UC_Customer_hdfContactPerson').value;
                    document.getElementById('lblMobile').innerText = document.getElementById('MainContent_UC_EnquiryView_UC_Customer_hdfMobile').value;
                }
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('[id*=MainContent_lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <%--  <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
        <asp:ListItem Text="Mango" Value="1" />
        <asp:ListItem Text="Apple" Value="2" />
        <asp:ListItem Text="Banana" Value="3" />
        <asp:ListItem Text="Guava" Value="4" />
        <asp:ListItem Text="Orange" Value="5" />
    </asp:ListBox>--%>

    <div class="col-md-12">

        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />

                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Employee</label>
                        <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Enquiry Number</label>
                        <asp:TextBox ID="txtSEnquiryNumber" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Customer Name</label>
                        <asp:TextBox ID="txtSCustomerName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">From Date</label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="calendarextender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromDate" WatermarkText="DD/MM/YYYY" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">To Date</label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtToDate" WatermarkText="DD/MM/YYYY" />
                    </div>

                    <div class="col-md-2 text-left">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSSource" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label class="modal-label">Sales Channel Type</label>
                        <asp:DropDownList ID="ddlSSalesChannelType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Unattended</asp:ListItem>
                            <asp:ListItem Value="6">In Progress</asp:ListItem>
                            <asp:ListItem Value="4">Converted To Lead</asp:ListItem>
                            <asp:ListItem Value="5">Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-sm-12 text-center">
          
                         <%--<label class="modal-label">Action</label>--%>
                        <asp:Button ID="BtnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="BtnSearch_Click" Width="100"/>
                        <asp:Button ID="BtnAdd" runat="server" Text="Add Enquiry" CssClass="btn Save" Width="100px" OnClick="BtnAdd_Click" />
                       <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />--%>                    
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12">

                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Enquiry:</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnEnqArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnEnqArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnEnqArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnEnqArrowRight_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" ToolTip="Excel Download..." />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenEnquiryID" runat="server" />
                            <asp:GridView ID="gvEnquiry" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10" runat="server" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="false" Width="100%"
                                OnRowDataBound="gvEnquiry_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="<i class='fa fa-eye fa-1x' aria-hidden='true'></i>" ItemStyle-HorizontalAlign="Center">

                                        <ItemTemplate>
                                            <%--<asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EnquiryID")%>' CssClass="btn Back" Width="75px" Height="25px" OnClick="BtnView_Click" />--%>
                                            <asp:ImageButton ID="BtnView" ImageUrl="~/Images/Preview.png" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EnquiryID")%>' ToolTip="View..." Height="20px" Width="20px" ImageAlign="Middle"  OnClick="BtnView_Click" />
                                            <%-- <itemstyle width="10px" horizontalalign="Center"></itemstyle>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Enquiry">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEnquiryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "EnquiryNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblEnquiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "EnquiryDate","{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName"></asp:BoundField>
                                    <asp:BoundField HeaderText="PersonName" DataField="PersonName"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                            <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Mail")%>'><%# DataBinder.Eval(Container.DataItem, "Mail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="State" DataField="State.State"></asp:BoundField>
                                    <asp:BoundField HeaderText="District" DataField="District.District"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemStyle VerticalAlign="Middle" Font-Size="XX-Small"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Product" DataField="Product"></asp:BoundField>
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks"></asp:BoundField>
                                    <asp:BoundField HeaderText="Source" DataField="Source.Source"></asp:BoundField>
                                    <asp:BoundField HeaderText="S.Channel" DataField="SalesChannelType.ItemText"></asp:BoundField>
                                    <asp:BoundField HeaderText="Status" DataField="Status.Status"></asp:BoundField>
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
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />

                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
                <%--</div>--%>
            </div>
        </div>
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_EnquiryView ID="UC_EnquiryView" runat="server"></UC:UC_EnquiryView>
        </div>
    </div>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlAddEnquiry" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Add Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblAddEnquiryMessage" runat="server" CssClass="message" Visible="false" />
                <UC:UC_AddEnquiry ID="UC_AddEnquiry" runat="server"></UC:UC_AddEnquiry>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AddEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <script type="text/javascript"> 
        function GetCustomerAuto() {
            $("#MainContent_UC_EnquiryView_UC_Customer_hdfCustomerID").val('');
            var param = { Cust: $('#MainContent_UC_EnquiryView_UC_Customer_txtCustomerName').val() }
            var Customers = [];
            if ($('#MainContent_UC_EnquiryView_UC_Customer_txtCustomerName').val().trim().length >= 3) {
                $.ajax({
                    url: "ColdVisits.aspx/GetCustomer1",
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
                        $('#MainContent_UC_EnquiryView_UC_Customer_txtCustomerName').autocomplete({
                            source: function (request, response) { response(Customers) },
                            select: function (e, u) {
                                $("#MainContent_UC_EnquiryView_UC_Customer_hdfCustomerID").val(u.item.value1);
                                document.getElementById('divCustomerViewID').style.display = "block";
                                document.getElementById('divCustomerCreateID').style.display = "none";

                                $("#MainContent_UC_EnquiryView_UC_Customer_hdfCustomerName").val(u.item.value);
                                $("#MainContent_UC_EnquiryView_UC_Customer_hdfContactPerson").val(u.item.ContactPerson);
                                $("#MainContent_UC_EnquiryView_UC_Customer_hdfMobile").val(u.item.Mobile);

                                document.getElementById('lblCustomerName').innerText = u.item.value;
                                document.getElementById('lblContactPerson').innerText = u.item.ContactPerson;
                                document.getElementById('lblMobile').innerText = u.item.Mobile;

                            },
                            open: function (event, ui) {
                                $(this).autocomplete("widget").css({
                                    "max-width":
                                        $('#MainContent_UC_EnquiryView_UC_Customer_txtCustomerName').width() + 48,
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
                $('#MainContent_UC_EnquiryView_UC_Customer_txtCustomerName').autocomplete({
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


            parentIDC = "#MainContent_UC_EnquiryView_UC_AddLead_";
            parentID = "MainContent_UC_EnquiryView_UC_AddLead_";

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
