<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerSalesTarget.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.DealerSalesTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/UserControls/MultiSelectDropDown.ascx" TagPrefix="UC" TagName="UC_M_Dealer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Material Upload</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <asp:Label ID="lblMessageMaterialUpload" runat="server" Text="" CssClass="message" />
        <fieldset class="fieldset-border">
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Upload Material</label>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnUploadTarget" runat="server" Text="Add" CssClass="btn Save" OnClick="btnUploadTarget_Click" />
                </div>
            </div>
        </fieldset>

        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="col-md-12" id="div1" runat="server">
                <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label>Region</label>
                            <UC:UC_M_Dealer ID="ddlmRegion" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Dealer</label>
                            <UC:UC_M_Dealer ID="ddlmDealer" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Division</label>
                            <UC:UC_M_Dealer ID="ddlmDivision" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label>Year</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Month</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-1 text-left">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                        <%--<div class="col-md-1 text-left">
                        <asp:Button ID="BtnLineChartData" runat="server" CssClass="btn Search" Text="Line Chart Data" OnClick="BtnLineChartData_Click" Width="117px"></asp:Button>
                    </div>--%>
                        <div class="col-md-1 text-left">
                            <asp:Button ID="BtnDetailData" runat="server" CssClass="btn Search" Text="Detail Data" OnClick="BtnDetailData_Click" Width="90px"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>


        <div class="col-md-12 Report">
            <div class="table-responsive">
            </div>
        </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Incidence / 100 Mc</legend>
                    <div class="col-md-12 Report">
                        <div id="divRegionEast"></div>
                    </div>
                </fieldset>
            </div>
        </div>

        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Data
                        <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="BtnLineChartData_Click" ToolTip="Excel Download..." /></legend>
                    <div class="col-md-12 Report">

                        <!-- GridView -->
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="false" EmptyDataText="No Data Found"
                            OnPageIndexChanging="gvData_PageIndexChanging"
                            OnRowDataBound="gvData_RowDataBound">
                            <Columns>
                              
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle CssClass="FooterStyle" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>

                    </div>
                </fieldset>
            </div>
        </div>



        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
        <script type="text/javascript"> 
            function RegionEastChart() {
                //var param = {
                //    Dealer: $('#MainContent_ddlmDealer_btnView').val()
                //    , LeadDateFrom: $('#MainContent_txtMfgDateFrom').val()
                //    , LeadDateTo: $('#MainContent_txtMfgDateTo').val()
                //    , Country: ''
                //    , Region: $('#id="MainContent_ddlmRegion_btnView"').val()
                //    , ProductType: ''
                //}
                var param = {
                    
                }
                $.ajax({
                    type: "POST",
                    url: 'DealerSalesTarget.aspx/IncidentPer',
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    dataFilter: function (data) {
                        return data;
                    },
                    success: function (data) {

                        var data1 = google.visualization.arrayToDataTable(data.d);
                        var view = new google.visualization.DataView(data1);

                        //view.setColumns([0, 1,
                        //    //{
                        //    //    calc: "stringify",
                        //    //    sourceColumn: 1,
                        //    //    type: "string",
                        //    //    role: "annotation"
                        //    //},
                        //    2, 3, 4, 5
                        //]); 
                        var title1 = 'Mfg. ' + ' wise';
                        var options = {
                            title: title1,
                            hAxis: {
                                title: "fsgfrgfrtgh"
                            },
                            vAxis: {
                                title: 'Cost / Machine'
                            },
                            //width: '80%',
                            height: 400,
                            legend: { position: 'top', maxLines: 5 },
                            bar: { groupWidth: '80%' },
                            isStacked: false,
                            is3D: true,
                            trendlines: {
                                0: { type: 'exponential', color: '#333', opacity: 2 }
                            }
                        };
                        var chart = new google.visualization.ColumnChart(document.getElementById("divRegionEast"));
                        chart.draw(view, options);
                    },
                    failure: function (r) {
                        alert(r);
                    },
                    error: function (r) {
                        alert(r);
                    }
                });
            }
        </script>
    </div>
</asp:Content>
