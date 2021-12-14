<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketServiceCharges.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketServiceCharges" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
 

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<table id="txnHistory1:panelGridid4" style="height: 100%; width: 100%">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">Service Charges</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandServiceCharges();">
                        <img id="imgServiceCharges" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlServiceCharges" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel4">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body4">

                        <asp:GridView ID="gvServiceCharges" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="ServiceChargeID" OnRowDataBound="gvServiceCharges_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbClaimRequested" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested_N")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ser Prod ID">
                                  
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:UpdatePanel ID="up" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtServiceMaterial" runat="server" CssClass="TextBox" Width="120px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="txtServiceMaterial" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ser Prod Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Date","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtServiceDate" runat="server" CssClass="TextBox" onkeyup="return removeText('MainContent_gvServiceCharges_txtServiceDate');"></asp:TextBox>
                                        <asp:CalendarExtender ID="ceServiceDate" runat="server" TargetControlID="txtServiceDate" PopupButtonID="txtServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Worked Hours">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtWorkedHours" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base Price">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtBasePrice" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                      <HeaderTemplate>
                                          <table  >
                                              <tr><td style="border-width: 0px">Discount</td> 
                                                  <td style="border-width: 0px"> <asp:TextBox ID="txtTaxP" runat="server"  CssClass="TextBox" Width="30px"></asp:TextBox>%</td></tr>
                                          </table>
                                        
                                    </HeaderTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="TextBox" Text="0"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quotation Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pro. Inv. Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProformaInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ProformaInvoiceNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inv. Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim / Invoice Requested" Visible="false">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbIsClaimRequested" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Tsir Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblServiceRemove" runat="server" OnClick="lblServiceRemove_Click" Visible='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested_N")%>'>Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lblServiceAdd" runat="server" OnClick="lblServiceAdd_Click">Add</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                               
                                <%--       <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbTSIRCreate" runat="server" OnClick="lbTSIRCreate_Click">TSIR Create</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>


                        <asp:Button ID="btnGenerateQuotation" runat="server" Text="Generate Quotation" CssClass="InputButton" OnClick="btnGenerateQuotation_Click" OnClientClick="return ConfirmCreate();" />
                        <asp:Button ID="btnGenerateProfarmaInvoice" runat="server" Text="Generate Pro Invoice" CssClass="InputButton" OnClick="btnGenerateProfarmaInvoice_Click" OnClientClick="return ConfirmCreate();" />
                        <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate Invoice" CssClass="InputButton" OnClick="btnGenerateInvoice_Click" OnClientClick="return ConfirmCreate();" />

                        <asp:Button ID="btnRequestForClaim" runat="server" Text="Request For Claim" CssClass="InputButton" OnClick="btnRequestForClaim_Click" OnClientClick="return ConfirmCreate();" />
                        <asp:LinkButton ID="lbFocus" runat="server" OnClick="lblServiceAdd_Click"></asp:LinkButton> 
                    </div>
                </div>

            </asp:Panel>
        </td>
    </tr>
</table>

<style type="text/css">
    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }

    .modalPopup {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 99%;
        /*height: 140px;*/
    }
</style>




<script type="text/javascript">
    function collapseExpandServiceCharges(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketServiceCharges_pnlServiceCharges");
        var imageID = document.getElementById("MainContent_DMS_ICTicketServiceCharges_imgServiceCharges");

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }

    //$(document).ready(function () {
    //    var tablefixedWidthID = document.getElementById('tablefixedWidthID');
    //    var $width = $(window).width() - 28;
    //    tablefixedWidthID.style.width = $width + "px";
    //});

    function removeText(id) {
        var TheTextBox = document.getElementById(id);
        TheTextBox.value = "";
        return false;
    }

    function ConfirmCreate() {
        var x = confirm("No changes will be allowed after saving");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>


<script type="text/javascript">
    function InIEvent() {

    }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial').val() };
                    $.ajax({
                        url: "DMS_ICTicketProcess.aspx/SearchMaterials",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });
        });
    };
    $(function () {
        $("#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial').val() };
                $.ajax({
                    url: "DMS_ICTicketProcess.aspx/SearchMaterials",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");  
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox  
        });
    });
</script>

<script type="text/javascript">
    $(function () {
        $("#MainContent_DMS_ICTicketServiceCharges_txtMaterial").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketServiceCharges_txtMaterial').val() };

                $.ajax({
                    url: "DMS_ICTicketProcess.aspx/SearchSMaterial",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");  
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox  
        });
    });
</script>


<style>
    .footer {
        height: 15px;
        width: 100%;
    }

        .footer td {
            border: none;
        }

        .footer th {
            border: none;
        }
</style>
<style>
    .AutoExtender {
        font-family: Verdana, Helvetica, sans-serif;
        font-size: .8em;
        font-weight: normal;
        border: solid 1px #006 height: 25px;
        ing: 20px;


    {
        om: do ted 1p cursor: pointer;
        color: roon;
        ighlight;

    {
        color: White;
        background-color: #006699;
        cursor: pointer;
    }

    /*#divwidt
                 width: 150px !important;
        }

            #divwidth div {
                width: 150px !important;
            }*/
</style>


<script type="text/javascript">
    $(document).ready(function () {
        $("#MainContent_txtSearch").autocomplete('Search_CS.ashx');
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {
        var gvTickets = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvServiceCharges');
        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblItem = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_lblItem_' + i);

                if (lblItem != null) {
                    if (lblItem.innerHTML == "0") {
                        lblItem.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }

        var gvAttachedFile = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvAttachedFile');

        if (gvAttachedFile != null) {
            for (var i = 0; i < gvAttachedFile.rows.length - 1; i++) {
                var lblFileName = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvAttachedFile_lblFileName_' + i);
                if (lblFileName != null) {
                    if (lblFileName.innerHTML == "") {
                        lblFileName.parentNode.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
    });
</script>