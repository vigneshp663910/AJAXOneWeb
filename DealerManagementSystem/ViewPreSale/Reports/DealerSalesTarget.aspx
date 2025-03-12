<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerSalesTarget.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.DealerSalesTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/UserControls/MultiSelectDropDown.ascx" TagPrefix="UC" TagName="UC_M_Dealer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sapMNCValueScr {
            overflow: hidden;
            color: #e78c07;
            font-size: 1.5rem;
            float: left;
            margin-top: 43px;
        }
    </style>

    <script src="https://unpkg.com/masonry-layout@4/dist/masonry.pkgd.min.js"></script>
    <style>
        /*#div1 {*/
        /*height: 91.7vh;*/
        /*display: flex;
            flex-direction: column;
            overflow: hidden;
            margin-left: 1px;
            background: skyblue;
            background: linear-gradient(to right, #4e97d5, #30526f );*/
        /* background-image:url('https://localhost:44343/Ajax/Images/bg01.jpg');*/
        /* background-image:url('https://localhost:44343/Ajax/Images/bg05q.png');*/
        /* background-image:url('https://localhost:44343/Ajax/Images/bg04r.png');*/
        /*background-image: url('https://ajaxapps.ajax-engg.com:1444/Ajax/Images/bg05qr.png');*/
        /* scroll*/
        /*}*/




        .div1 {
            /*height: 91.7vh;*/
            /* height: 100vh;*/
            display: inline;
            flex-direction: column;
            overflow: hidden;
            margin-left: 1px;
            background: skyblue;
            background: linear-gradient(to right, #4e97d5, #30526f );
            /* background-image:url('https://localhost:44343/Ajax/Images/bg01.jpg');*/
            /* background-image:url('https://localhost:44343/Ajax/Images/bg05q.png');*/
            /* background-image:url('https://localhost:44343/Ajax/Images/bg04r.png');*/
            background-image: url('https://ajaxapps.ajax-engg.com:1444/Ajax/Images/bg05qr.png');
            /* scroll*/
        }

        .home-search-panel {
            padding: 15px;
        }

            .home-search-panel .tbl-col-right {
                margin-bottom: 10px;
            }

        .tbl-col-right .textBox.form-control {
            height: 35px;
            padding: 0px 7px;
        }

        .tbl-col-right input.form-control {
            height: 35px;
            padding: 0px 10px;
        }

        .home-search-bar {
            position: absolute;
            top: 0;
            right: 0;
            background: #2f516e;
        }

            .home-search-bar:hover {
                background: #336699;
            }

            .home-search-bar a {
                padding: 5px;
            }

        .home-history-body {
            position: relative;
            width: 100%;
            overflow: hidden;
        }

        .navbar-home-content {
            width: 300px;
        }

        .home-search-main {
            transition: width .4s;
            /*width: 300px;*/
            float: right;
            margin-top: 21px;
            margin-bottom: 20px;
            position: absolute;
            right: 0;
            z-index: 9;
            border: 1px solid #3C4C5B;
        }

        .details {
            position: relative;
        }

            .details .desc {
                clear: both;
                font-size: larger;
            }

            .details #donut_single {
                float: right;
            }

        .details-position {
            position: absolute;
            width: 100%;
            /* bottom: 0; */
            top: 47px;
        }
        /*Home page tiles*/
        /*.container .tblcontrols12 {
            position: relative;
        }
        .container .tblcontrols12 div.cell {

        }
        .container .tblcontrols12 #Div1 {
            float:left;
            width:48%;
        }
        .container .tblcontrols12 #Div1 .modbox, .container .tblcontrols12 #Div2 .modbox  {
            margin-right:11px;
            margin-bottom:15px;
        }
        .container .tblcontrols12 #Div3 .modbox {
            margin-right:11px;
        }
        .container .tblcontrols12 #Div2 {
            float:right;
            width:52%;
        }
        .container .tblcontrols12 #Div2 .modbox, .container .tblcontrols12 #Div5 .modbox, .container .tblcontrols12 #Div6 .modbox, 
        .container .tblcontrols12 #Div7 .modbox {
            margin-left:22px;
        }
        .container .tblcontrols12 #Div1 .dashboardGrid, .container .tblcontrols12 #Div2 .dashboardGrid {
            padding:15px;
            background:#d8d8d8;
        }
        .container .tblcontrols12 #Div3 {
            width:70%;
            float:left;
        }
        .container .tblcontrols12 #Div3 .dashboardGrid img  {
        }
        .container .tblcontrols12 #Div4 {
            clear:both;
        }
        .container .tblcontrols12 #Div4, .container .tblcontrols12 #Div5,.container .tblcontrols12 #Div6 {
            width:33.33%;
            float:left;
        }*/
        /* .container .tblcontrols12 #Div6 {
            width:30%;
            float:right;
        }
            
        .container .tblcontrols12 #Div7 {
            width:30%;
            float:right;
        }*/
        /*.container .tblcontrols12 div.cell table {
            width:100%;
        }
        .container .tblcontrols12 div.cell table tr td {
            
        }
        .modbox .row {
            margin: 0;
        }
        .modbox .wide_thumbnail {
            margin: 0!important;
            padding-bottom: 10px;
        }
        .modbox .portlet-body {
            padding: 0!important;
        }
        .modbox .portlet-body .details span {
            font-size:100px;
        }
        .container .tblcontrols12 div.cell {
            margin-bottom: 15px;
        }
        .cell .modbox .modtitle {
            font-size: 18px;
            text-shadow: 1px 2px 3px #bac4cf;
            font-weight: 500;
        }
        @media screen and (max-width: 767px) {
            .container .tblcontrols12 div.cell {
                width:100%!important;
                float:none!important;
            }
            .container .tblcontrols12 #Div1 .modbox, .container .tblcontrols12 #Div3 .modbox, .container .tblcontrols12 #Div2 .modbox, .container .tblcontrols12 #Div4 .modbox, .container .tblcontrols12 #Div5 .modbox, .container .tblcontrols12 #Div6 .modbox, 
            .container .tblcontrols12 #Div7 .modbox {
                margin:0;
            }
        }
        @media screen and (min-device-width: 320px) and (max-device-width: 720px) {

            #div1 {
                /*height: 93.2vh;
                 margin-left: 0px;*/
        /* }
        }*/
    </style>
    <style>
        .grid-item {
            background: #fff;
            box-shadow: 0 2px 4px rgb(51 51 51 / 20%);
            color: #666666;
            border-radius: 0.25rem 0.25rem 0.25rem 0.25rem;
            margin: 10px;
            padding: 15px;
            width: 160px;
            float: left;
        }

            .grid-item:hover {
                background-color: #F0F0F0;
            }

        .tile-size-one {
            width: 160px;
            height: 169px;
        }

        .tile-size-two {
            width: 340px;
            height: 169px;
        } 

        .tile-size-three {
            width: 340px;
            height: 360px;
        }
         

        .tile-size-four {
            width: 640px;
            height: 180px;
        }

        .dashboardGrid img {
            width: 270px !important;
            height: 100px !important;
        }

        #MainContent_tblDashboard .grid {
            /*  display: flex;
    flex-wrap: wrap;*/
            /*-webkit-column-width: 19em;
    -webkit-column-gap: 1rem;*/
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.grid').masonry({
                // options
                itemSelector: '.grid-item',
                columnWidth: 10
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>


    <style>
        /* Style the tab */
        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

        .tablinks {
            height: auto;
            padding-bottom: 33px
        }
        /* Style the buttons inside the tab */
        .tab button {
            background-color: inherit;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 16px;
            transition: 0.3s;
            font-size: 17px;
        }

            /* Change background color of buttons on hover */
            .tab button:hover {
                background-color: #ddd;
            }

            /* Create an active/current tablink class */
            .tab button.active {
                background-color: #ccc;
            }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
        }
    </style>
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
                    <legend style="background: none; color: #007bff; font-size: 17px;">Sales Target vs Actual</legend>
                    <div id="London" class="tabcontentEnq">
                        <div class="div1">
                            <div class="grid">

                                <div class="tile-size-four grid-item" style="height: auto !important ; width: 100%;  height: 180px;">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label8" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divSlcmSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-size-three grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label1" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divSlcmState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tile-size-four grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label2" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divBpSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-size-three grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label3" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divBpState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tile-size-four grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label4" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divCpSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-size-three grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label5" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divCpState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tile-size-four grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label6" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divBoomPumpSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-size-three grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:Label ID="Label7" Text="Region Wise Enquiry" runat="server" />
                                            </div>
                                            <div id="divBoomPumpState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div id=""></div>
                                <div id=""></div>
                            </div>
                        </div>

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
                        var DataSlcmSalesPerson = google.visualization.arrayToDataTable(data.d[0]);
                        var DataSlcmState = google.visualization.arrayToDataTable(data.d[1]);

                        var DataBpSalesPerson = google.visualization.arrayToDataTable(data.d[2]);
                        var DataBpState = google.visualization.arrayToDataTable(data.d[3]);

                        var DataCpSalesPerson = google.visualization.arrayToDataTable(data.d[4]);
                        var DataCpState = google.visualization.arrayToDataTable(data.d[5]);

                        var DataBoomPumpSalesPerson = google.visualization.arrayToDataTable(data.d[6]);
                        var DataBoomPumpState = google.visualization.arrayToDataTable(data.d[7]);

                        var viewSlcmSalesPerson = new google.visualization.DataView(DataSlcmSalesPerson);
                        var viewSlcmState = new google.visualization.DataView(DataSlcmState);
                        var viewBpSalesPerson = new google.visualization.DataView(DataBpSalesPerson);
                        var viewBpState = new google.visualization.DataView(DataBpState);
                        var viewCpSalesPerson = new google.visualization.DataView(DataCpSalesPerson);
                        var viewCpState = new google.visualization.DataView(DataCpState);
                        var viewBoomPumpSalesPerson = new google.visualization.DataView(DataBoomPumpSalesPerson);
                        var viewBoomPumpState = new google.visualization.DataView(DataBoomPumpState);

                        //view.setColumns([0, 1,
                        //    //{
                        //    //    calc: "stringify",
                        //    //    sourceColumn: 1,
                        //    //    type: "string",
                        //    //    role: "annotation"
                        //    //},
                        //    2, 3, 4, 5
                        //]);  
                        var options = {

                            //hAxis: {
                            //    title: "fsgfrgfrtgh"
                            //},
                            //vAxis: {
                            //    title: 'Cost / Machine'
                            //}, 
                            height: 300,
                            legend: { position: 'top', maxLines: 5 },
                            bar: { groupWidth: '80%' },
                            isStacked: false,
                            is3D: true,
                            trendlines: {
                                0: { type: 'exponential', color: '#333', opacity: 2 }
                            },
                            seriesType: 'bars',
                            series: { 2: { type: 'line' } }
                        };
                        var chartSlcmSalesPerso = new google.visualization.ComboChart(document.getElementById("divSlcmSalesPerson"));
                        var chartSlcmState = new google.visualization.ComboChart(document.getElementById("divSlcmState"));
                        var chartBpSalesPerso = new google.visualization.ComboChart(document.getElementById("divBpSalesPerson"));
                        var chartBpState = new google.visualization.ComboChart(document.getElementById("divBpState"));
                        var chartCpSalesPerso = new google.visualization.ComboChart(document.getElementById("divCpSalesPerson"));
                        var chartCpState = new google.visualization.ComboChart(document.getElementById("divCpState"));
                        var chartBoomPumpSalesPerso = new google.visualization.ComboChart(document.getElementById("divBoomPumpSalesPerson"));
                        var chartBoomPumpState = new google.visualization.ComboChart(document.getElementById("divBoomPumpState"));

                        chartSlcmSalesPerso.draw(viewSlcmSalesPerson, options);
                        chartSlcmState.draw(viewSlcmState, options);

                        chartBpSalesPerso.draw(viewBpSalesPerson, options);
                        chartBpState.draw(viewBpState, options);

                        chartCpSalesPerso.draw(viewCpSalesPerson, options);
                        chartCpState.draw(viewCpState, options);

                        chartBoomPumpSalesPerso.draw(viewBoomPumpSalesPerson, options);
                        chartBoomPumpState.draw(viewBoomPumpState, options);
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
