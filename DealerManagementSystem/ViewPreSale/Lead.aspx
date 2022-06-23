<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Lead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadView.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLead.ascx" TagPrefix="UC" TagName="UC_AddLead" %>
<%--<%@ Register Src="~/ViewPreSale/UserControls/CustomerSearch.ascx" TagPrefix="UC" TagName="UC_CustomerSearch" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<style>
        .Popup {
            display: block;
            z-index: 1002;
            outline: 0px;
            height: auto;
            width: 800px;
            top: 128px;
            left: 283px;
            position: absolute;
            padding: 0.2em;
            overflow: hidden;
            border-radius: 6px;
            border: 1px solid #CCC;
            background: #fefefe 50% bottom repeat-x;
            color: #666;
            font-family: Segoe UI,Arial,sans-serif;
            font-size: 1.1em;
            margin: 0 1% 0 1%;
        }

        .PopupHeader {
            border: 1px solid #333;
            background: #333 url(Ajax/Images/Feedbackheader.png) 50% 50% repeat-x;
            color: #fff;
            font-weight: bold;
            cursor: move;
            padding: 0.4em 1em;
            position: relative;
            border-radius: 6px;
            font-family: Segoe UI,Arial,sans-serif;
            font-size: 1.1em;
        }

        .clearfix:after {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }

        .PopupHeader a {
            color: #fff;
        }

        #PopupDialogue {
            float: left;
            font-size: 13px;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }

        .PopupClose {
            float: right;
            color: black;
            font-size: 8px;
            width: 15px;
            height: 15px;
            padding: inherit;
        }

        .modal-backdrop {
            background-color: gray;
        }

        .modalBackground {
            background-color: #000000bd;
        }
    </style>--%>
    <script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
    <script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
    <script type="text/javascript">  

        $(document).ready(function () {
            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            if (txtCustomerID.value != "") {
                document.getElementById('divCustomerViewID').style.display = "block";
                document.getElementById('divCustomerCreateID').style.display = "none";

                document.getElementById('lblCustomerName').innerText = document.getElementById('MainContent_txtCustomerNameS').value;
                document.getElementById('lblContactPerson').innerText = document.getElementById('MainContent_txtContactPersonS').value;
                document.getElementById('lblMobile').innerText = document.getElementById('MainContent_txtMobileS').value;
            }
        });


        $(function () {
            //$("#MainContent_txtCustomer").autocomplete({
            //    source: function (request, response) {
            //        debugger
            //        var param = { CustS: $('#MainContent_txtCustomer').val() };
            //        $.ajax({
            //            type: 'POST',
            //            contentType: "application/json; charset=utf-8",
            //            /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
            //            url: "ColdVisits.aspx/GetCustomer",
            //            data: JSON.stringify(param),
            //            dataType: 'JSON',
            //            success: function (data) {
            //                debugger
            //                document.getElementById('divAuto').style.display = "block";
            //                var n = 0;
            //                for (var i = 1; i <= 5; i++) {
            //                    $(('#div' + i)).empty();
            //                    document.getElementById('div' + i).style.display = "none";
            //                }
            //                $.map(data.d, function (item) {
            //                    n = n + 1;
            //                    document.getElementById('div' + n).style.display = "block";
            //                    document.getElementById("div" + n).innerHTML = item;
            //                })
            //            },
            //            error: function () {
            //                alert("Error");
            //            }
            //        });
            //    },
            //    minLength: 3 //This is the Char length of inputTextBox    
            //});

            $("#MainContent_UC_Customer_txtCustomerName").autocomplete({
                source: function (request, response) {
                    var param = { CustS: $('#MainContent_UC_Customer_txtCustomerName').val() };
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
                      //  url: "ColdVisits.aspx/GetCustomer",
                        url: "Lead.aspx/GetCustomer",
                        data: JSON.stringify(param),
                        dataType: 'JSON',
                        success: function (data) {
                            document.getElementById('UCdivAuto').style.display = "block";
                            var n = 0;
                            for (var i = 1; i <= 5; i++) {
                                $(('#div' + i)).empty();
                                document.getElementById('UCdiv' + i).style.display = "none";
                            }
                            $.map(data.d, function (item) {
                                n = n + 1;
                                document.getElementById('UCdiv' + n).style.display = "block";
                                document.getElementById("UCdiv" + n).innerHTML = item;
                            })
                            if (n == 0)
                                document.getElementById('UCdiv0').style.display = "none";
                            else
                                document.getElementById('UCdiv0').style.display = "block";
                        },
                        error: function () {
                            alert("Error");
                        }
                    });
                },
                minLength: 3 //This is the Char length of inputTextBox    
            });
        });
    </script>

    <script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
    <script type="text/javascript"> 
        $(function () {
            $('#div1').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID1'), document.getElementById('lblCustomerName1'));
            });
        });
        $(function () {
            $('#div2').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID2'), document.getElementById('lblCustomerName2'));
            });
        });
        $(function () {
            $('#div3').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID3'), document.getElementById('lblCustomerName3'));
            });
        });
        $(function () {
            $('#div4').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID4'), document.getElementById('lblCustomerName4'));
            });
        });
        $(function () {
            $('#div5').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID5'), document.getElementById('lblCustomerName5'));
            });
        });
        function AutoCustomer(lblCustomerID, lblCustomerName) {
            debugger
            var txtCustomer = document.getElementById('MainContent_txtCustomer');
            txtCustomer.value = lblCustomerName.innerText;
            document.getElementById('divAuto').style.display = "none";
        }
        function UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile) {
            debugger
            var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
            txtCustomerID.value = CustomerID.innerText;

            var txtCustomer = document.getElementById('MainContent_UC_Customer_txtCustomerName');

            txtCustomer.value = CustomerName.innerText;

            document.getElementById('lblCustomerName').innerText = CustomerName.innerText;
            document.getElementById('lblContactPerson').innerText = ContactPerson.innerText;
            document.getElementById('lblMobile').innerText = Mobile.innerText;

            document.getElementById('MainContent_txtCustomerNameS').value = CustomerName.innerText;
            document.getElementById('MainContent_txtContactPersonS').value = ContactPerson.innerText;
            document.getElementById('MainContent_txtMobileS').value = Mobile.innerText;



            document.getElementById('UCdivAuto').style.display = "none";

            document.getElementById('divCustomerViewID').style.display = "block";
            document.getElementById('divCustomerCreateID').style.display = "none";
        }

        $(function () {
            $('#divChangeCustomer').click(function () {
                var txtCustomerID = document.getElementById('MainContent_txtCustomerID');
                txtCustomerID.value = "";
                var txtCustomer = document.getElementById('MainContent_UC_Customer_txtCustomerName');
                txtCustomer.value = "";
                document.getElementById('divCustomerViewID').style.display = "none";
                document.getElementById('divCustomerCreateID').style.display = "block";
            });
        });

    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
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
                  <%--  <div class="col-md-2 text-left">
                        <label>Progress Status</label>
                        <asp:DropDownList ID="ddlSProgressStatus" runat="server" CssClass="form-control" />
                    </div>--%>

                    <div class="col-md-2 text-left">
                        <label>Status</label>
                        <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control" />
                    </div>

                   <%-- <div class="col-md-2 text-left">
                        <label>Category</label>
                        <asp:DropDownList ID="ddlSCategory" runat="server" CssClass="form-control" />
                    </div>--%>

                    <div class="col-md-2 text-left">
                        <label>Qualification</label>
                        <asp:DropDownList ID="ddlSQualification" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Source</label>
                        <asp:DropDownList ID="ddlSSource" runat="server" CssClass="form-control" />
                    </div>

                  <%--  <div class="col-md-2 text-left">
                        <label>Lead Type</label>
                        <asp:DropDownList ID="ddlSType" runat="server" CssClass="form-control" />
                    </div>--%>

                    <div class="col-md-2 text-left">
                        <label>Customer</label>
                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                     <%--   <div id="divAuto" style="position: absolute; background-color: red; z-index: 1;">
                            <div id="div1" class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="div2" class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="div3" class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="div4" class="fieldset-borderAuto" style="display: none">
                            </div>
                            <div id="div5" class="fieldset-borderAuto" style="display: none">
                            </div>
                        </div>--%>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Country</label>
                        <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>State</label>
                        <asp:DropDownList ID="ddlSState" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        <asp:Button ID="btnAddLead" runat="server" CssClass="btn Save" Text="Add Lead" OnClick="btnAddLead_Click" Width="150px"></asp:Button>
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
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging" EmptyDataText="No Data Found">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lead Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadID" Text='<%# DataBinder.Eval(Container.DataItem, "LeadID")%>' runat="server" Visible="false" />
                                        <asp:Label ID="lblLeadNumber" Text='<%# DataBinder.Eval(Container.DataItem, "LeadNumber")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lead Date" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadDate" Text='<%# DataBinder.Eval(Container.DataItem, "LeadDate","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Product Type" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "Category.Category")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Progress Status" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProgressStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ProgressStatus.ProgressStatus")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Qualification" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQualification" Text='<%# DataBinder.Eval(Container.DataItem, "Qualification.Qualification")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSource" Text='<%# DataBinder.Eval(Container.DataItem, "Source.Source")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Type.Type")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Code" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Code" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewLead" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewLead_Click" Width="75px" Height="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
            <div style="display: none">
                <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtCustomerNameS" runat="server" />
                <asp:TextBox ID="txtContactPersonS" runat="server" />
                <asp:TextBox ID="txtMobileS" runat="server" />
            </div>
            <div class="model-scroll">
                <asp:Label ID="lblMessageLead" runat="server" Text="" CssClass="message" Visible="false" />
                <fieldset class="fieldset-border">
                    <div id="divCustomerViewID" style="display: none">
                        <fieldset class="fieldset-border">
                            <div class="col-md-12">

                                <div class="col-md-2 text-right">
                                    <label>Customer Name</label>
                                </div>
                                <div class="col-md-4">
                                    <label id="lblCustomerName"></label>
                                </div>
                                <div class="col-md-2 text-right">
                                    <label>Contact Person</label>
                                </div>
                                <div class="col-md-4">
                                    <label id="lblContactPerson"></label>
                                </div>

                                <div class="col-md-2 text-right">
                                    <label>Mobile</label>
                                </div>
                                <div class="col-md-4">
                                    <label id="lblMobile"></label>
                                </div>
                            </div>
                            <div id="divChangeCustomer">
                                <label>Change Customer</label>
                            </div>

                        </fieldset>
                    </div>
                    <div id="divCustomerCreateID">
                        <UC:UC_CustomerCreate ID="UC_Customer" runat="server"></UC:UC_CustomerCreate>
                    </div>
                     <UC:UC_AddLead ID="UC_AddLead" runat="server"></UC:UC_AddLead>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_Customer" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
      <style>
        .fieldset-borderAuto {
            border: solid 1px #cacaca;
            margin: 1px 0;
            border-radius: 5px;
            padding: 10px;
            background-color: #b4b4b4;
        }

            .fieldset-borderAuto tr {
                /* background-color: #000084; */
                background-color: inherit;
                font-weight: bold;
                color: white;
            }

            .fieldset-borderAuto:hover {
                background-color: blue;
            }
    </style>
</asp:Content>




