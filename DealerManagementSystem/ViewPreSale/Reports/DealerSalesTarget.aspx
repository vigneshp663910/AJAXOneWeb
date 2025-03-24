<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DealerSalesTarget.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.DealerSalesTarget" %>

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

        .tile-Dasboard {
            width: 100%;
            height: 180px;
            margin: 0px;
            padding: 0px
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
        .Popup {
            width: 95%;
            height: 95%;
            top: 128px;
            left: 283px;
        }

            .Popup .model-scroll {
                height: 80vh;
                overflow: auto;
            }
    </style>

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

    <style>
        .google-visualization-chart {
            overflow: visible !important;
            white-space: normal !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="col-md-12">


        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="col-md-12" id="div1" runat="server">
                <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Filter</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label>Region</label>
                            <UC:UC_M_Dealer ID="ddlmRegion" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Dealer</label>
                            <UC:UC_M_Dealer ID="ddlmDealer" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                        </div>

                        <div class="col-md-1 col-sm-12">
                            <label>Year</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-1 col-sm-12">
                            <label>Month</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-1 col-sm-12">
                            <label>Purpose</label>
                            <asp:DropDownList ID="ddlPurpose" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Top 5</asp:ListItem>
                                <asp:ListItem Value="2">Bottom 5</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1 text-left">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>

                        <div class="col-md-1 text-left">
                            <asp:Button ID="btnUploadData" runat="server" CssClass="btn Search" Text="Upload Data" Width="120px"></asp:Button>
                        </div>
                        <div class="col-md-1 text-left">
                            <asp:Button ID="BtnDetailData" runat="server" CssClass="btn Search" Text="Detail Data" OnClick="BtnDetailData_Click" Width="90px" Visible="false"></asp:Button>
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
                    <div class="tab">
                        <button id="btnLondon" class="tablinks" onclick="openCityFixName('London');">SLCM</button>
                        <button id="btnParis" class="tablinks" onclick="openCityFixName('Paris');">Non SLCM</button>
                        <button id="btnTokyo" class="tablinks" onclick="openCityFixName('Tokyo');">Velocity</button>
                        <asp:HiddenField ID="hfTab" Value="London" runat="server" />

                    </div>
                    <div id="London" class="tabcontentEnq">
                        <div class="div1">
                            <div class="grid">

                                <div class="tile-Dasboard grid-item" style="height: auto !important;">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnSlcmSalesPerson" runat="server" Text="SLCM Sales Person Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divSlcmSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnSlcmState" runat="server" Text="SLCM State Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divSlcmState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnSlcmRegion" runat="server" Text="SLCM Region Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divSlcmRegion" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div id="Paris" class="tabcontentEnq">
                        <div class="div1">
                            <div class="grid">
                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnBpSalesPerson" runat="server" Text="BP Sales Person Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divBpSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnBpState" runat="server" Text="BP State Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divBpState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnCpSalesPerson" runat="server" Text="CP Sales Person Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divCpSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnCpState" runat="server" Text="CP State Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divCpState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnBoomPumpSalesPerson" runat="server" Text="Boom Pump Sales Person Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true" />
                                            </div>
                                            <div id="divBoomPumpSalesPerson" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnBoomPumpState" runat="server" Text="Boom Pump State Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true" />
                                            </div>
                                            <div id="divBoomPumpState" style="height: 300px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="Tokyo" class="tabcontentEnq">
                        <div class="div1">
                            <div class="grid">
                                <div class="tile-Dasboard grid-item" style="height: auto !important">
                                    <div class="content">
                                        <div class="details">
                                            <div class="desc">
                                                <asp:LinkButton ID="lbtnVelocity" runat="server" Text="Velocity Wise" OnClick="lbtnChartDataView_Click" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:LinkButton>
                                            </div>
                                            <div id="divVelocity" style="height: 500px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

        </div>
    </div>
    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <asp:Panel ID="pnlChartData" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
         <asp:ImageButton ID="btnChartDataExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnChartDataExcel_Click" ToolTip="Excel Download..." Width="23" Height="23" />
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <!-- GridView -->
                <div class="model-scroll">
                    <asp:GridView ID="gvChartData" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">

                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle CssClass="FooterStyle" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </div>

    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_chartDetails" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlChartData" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlLeadDetails" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Data
                        <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="BtnLineChartData_Click" ToolTip="Excel Download..." /></legend>
                    <div class="col-md-12 Report">

                        <!-- GridView -->
                        <asp:GridView ID="gvData" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" ShowFooter="false" EmptyDataText="No Data Found"
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

    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_LeadDetails" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLeadDetails" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Label ID="lblMessageMaterialUpload" runat="server" Text="" CssClass="message" />
    <asp:Panel ID="pnlUploadData" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Material Upload</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">
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
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnUploadData" PopupControlID="pnlUploadData" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript"> 
        function SalesTargetActualAllChart() {
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
                url: 'DealerSalesTarget.aspx/SalesTargetActualAll',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                    var DataSlcmSalesPerson = google.visualization.arrayToDataTable(data.d[0]);
                    var DataBpSalesPerson = google.visualization.arrayToDataTable(data.d[1]);
                    var DataCpSalesPerson = google.visualization.arrayToDataTable(data.d[2]);
                    var DataBoomPumpSalesPerson = google.visualization.arrayToDataTable(data.d[3]);

                    var DataSlcmState = google.visualization.arrayToDataTable(data.d[4]);
                    var DataBpState = google.visualization.arrayToDataTable(data.d[5]);
                    var DataCpState = google.visualization.arrayToDataTable(data.d[6]);
                    var DataBoomPumpState = google.visualization.arrayToDataTable(data.d[7]);

                    var DataSlcmRegion = google.visualization.arrayToDataTable(data.d[8]);

                    var viewSlcmSalesPerson = new google.visualization.DataView(DataSlcmSalesPerson);
                    viewSlcmSalesPerson.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewSlcmState = new google.visualization.DataView(DataSlcmState);
                    viewSlcmState.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewBpSalesPerson = new google.visualization.DataView(DataBpSalesPerson);
                    viewBpSalesPerson.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewBpState = new google.visualization.DataView(DataBpState);
                    viewBpState.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewCpSalesPerson = new google.visualization.DataView(DataCpSalesPerson);
                    viewCpSalesPerson.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewCpState = new google.visualization.DataView(DataCpState);
                    viewCpState.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewBoomPumpSalesPerson = new google.visualization.DataView(DataBoomPumpSalesPerson);
                    viewBoomPumpSalesPerson.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewBoomPumpState = new google.visualization.DataView(DataBoomPumpState);
                    viewBoomPumpState.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);

                    var viewSlcmRegion = new google.visualization.DataView(DataSlcmRegion);
                    viewSlcmRegion.setColumns([0,
                        1, { calc: "stringify", sourceColumn: 1, type: "string", role: "annotation" },
                        2, { calc: "stringify", sourceColumn: 2, type: "string", role: "annotation" },
                        3, { calc: "stringify", sourceColumn: 3, type: "string", role: "annotation" }
                    ]);


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
                        vAxis: {
                            y: {
                                distance: { label: '' },
                            }
                        },
                        height: 300,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: false,
                        is3D: true,

                        annotations: {
                            alwaysOutside: true,
                            textStyle: {
                                fontSize: 12,
                                color: 'black'
                            }
                        },
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        },
                        seriesType: 'bars',
                        series: { 2: { type: 'line' } },
                        colors: ['blue', 'green', '#ec8f6e']
                    };
                    var chartSlcmSalesPerso = new google.visualization.ComboChart(document.getElementById("divSlcmSalesPerson"));
                    var chartSlcmState = new google.visualization.ComboChart(document.getElementById("divSlcmState"));
                    var chartBpSalesPerso = new google.visualization.ComboChart(document.getElementById("divBpSalesPerson"));
                    var chartBpState = new google.visualization.ComboChart(document.getElementById("divBpState"));
                    var chartCpSalesPerso = new google.visualization.ComboChart(document.getElementById("divCpSalesPerson"));
                    var chartCpState = new google.visualization.ComboChart(document.getElementById("divCpState"));
                    var chartBoomPumpSalesPerso = new google.visualization.ComboChart(document.getElementById("divBoomPumpSalesPerson"));
                    var chartBoomPumpState = new google.visualization.ComboChart(document.getElementById("divBoomPumpState"));

                    var chartSlcmRegion = new google.visualization.ComboChart(document.getElementById("divSlcmRegion"));

                    chartSlcmSalesPerso.draw(viewSlcmSalesPerson, options);
                    chartSlcmState.draw(viewSlcmState, options);

                    chartBpSalesPerso.draw(viewBpSalesPerson, options);
                    chartBpState.draw(viewBpState, options);

                    chartCpSalesPerso.draw(viewCpSalesPerson, options);
                    chartCpState.draw(viewCpState, options);

                    chartBoomPumpSalesPerso.draw(viewBoomPumpSalesPerson, options);
                    chartBoomPumpState.draw(viewBoomPumpState, options);

                    chartSlcmRegion.draw(viewSlcmRegion, options);
                },
                failure: function (r) {
                    alert(r);
                },
                error: function (r) {
                    alert(r);
                }
            });
        }
        function VelocityChart() {
            var param = {}
            $.ajax({
                type: "POST",
                url: 'DealerSalesTarget.aspx/Velocity',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                    var DataVelocity = google.visualization.arrayToDataTable(data.d[0]);
                    var viewVelocity = new google.visualization.DataView(DataVelocity);
                    //]);  
                    var options = {
                        vAxis: { y: { distance: { label: '' }, } },
                        height: 500,
                        legend: { position: 'top', maxLines: 2, },
                        bar: { groupWidth: '80%' },
                        isStacked: false,
                        is3D: true,
                        annotations: { alwaysOutside: true, textStyle: { fontSize: 12, color: 'black' } },
                        trendlines: { 0: { type: 'exponential', color: '#333', opacity: 2 } },
                        seriesType: 'bars',
                       // colors: ['blue', 'green', '#ec8f6e']
                    };
                    var chartVelocity = new google.visualization.ComboChart(document.getElementById("divVelocity"));
                    chartVelocity.draw(viewVelocity, options);
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
    <script>
        //function openCity(evt, cityName) {
        //    var i, tabcontent, tablinks;
        //    tabcontent = document.getElementsByClassName("tabcontentEnq");
        //    for (i = 0; i < tabcontent.length; i++) {
        //        tabcontent[i].style.display = "none";
        //    }
        //    tablinks = document.getElementsByClassName("tablinks");
        //    for (i = 0; i < tablinks.length; i++) {
        //        tablinks[i].className = tablinks[i].className.replace(" active", "");
        //    }
        //    document.getElementById(cityName).style.display = "block";
        //    evt.currentTarget.className += " active";
        //}

        function openCityFixName(cityName) {
            $("#MainContent_hfTab").val(cityName);
            // return true;
        }

        function openCity(cityName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontentEnq");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";
            document.getElementById('btn' + cityName).className += " active";
        }

    </script>

</asp:Content>
