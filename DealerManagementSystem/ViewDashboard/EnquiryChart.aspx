<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EnquiryChart.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.EnquiryChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #divRegionEast {
            position: relative;
            width: 550px;
            height: 400px;
        }

        #divRegionNorth {
            position: relative;
            width: 550px;
            height: 400px;
        }

        #divRegionSouth {
            position: relative;
            width: 550px;
            height: 400px;
        }

        #divRegionWest {
            position: relative;
            width: 550px;
            height: 400px;
        }
    </style>

    <style>
        .sapMNCValueScr {
            overflow: hidden;
            color: #e78c07;
            font-size: 2.25rem;
            float: left;
            margin-top: 43px;
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


        #div1 {
            /*height: 91.7vh;*/
            min-height: 91.7vh; 
            display: flex;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    

        <div class="col-md-12">
            <div class="col-md-12" id="divList" runat="server">
                <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="col-md-2 text-left">
                            <label>Dealer</label>
                            <asp:DropDownList ID="ddlMDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Country</label>
                            <asp:DropDownList ID="ddlMCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label>Region</label>
                            <asp:DropDownList ID="ddlMRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label>ProductType</label>
                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <div class="col-md-12 Report">
            <div class="table-responsive">
            </div>
        </div>
        <div id="div1">
        <div class="tile-size-two grid-item">
            <div class="content">
                <div class="details">
                    <div class="desc">
                        <asp:Label ID="Label3" Text="Sum of Enquiry" runat="server" />
                    </div>
                    <div class="details-position">
                        <asp:Label ID="lblEnquiryCount" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="col-md-5">
                <div id="divRegion"></div>
            </div>
            <div class="col-md-5">
                <div id="divSource"></div>
            </div>
        </div>

    </div>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript"> 
        function RegionChart() {
            var Region = $('#MainContent_ddlMRegion').val();
            var param = {
                DateFrom: $('#MainContent_txtDateFrom').val()
                , DateTo: $('#MainContent_txtDateTo').val()
                , Dealer: $('#MainContent_ddlMDealer').val()
                , Country: $('#MainContent_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'EnquiryChart.aspx/EnquiryRegionWiseCount',
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
                    var options = {
                        title: 'Region Wise Enquiry',
                        ////width: '80%',
                        height: 300,
                        legend: { position: 'bottom' },
                        //legend: { position: 'top', maxLines: 5 },
                        //bar: { groupWidth: '80%' },
                        //isStacked: true,
                        //is3D: true,
                        //trendlines: {
                        //    0: { type: 'exponential', color: '#333', opacity: 2 }
                        //}
                    };
                    var chart = new google.visualization.PieChart(document.getElementById("divRegion"));
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
        function SourceChart() {
            var Region = $('#MainContent_ddlMRegion').val();
            var param = {
                DateFrom: $('#MainContent_txtDateFrom').val()
                , DateTo: $('#MainContent_txtDateTo').val()
                , Dealer: $('#MainContent_ddlMDealer').val()
                , Country: $('#MainContent_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'EnquiryChart.aspx/EnquirySourceWiseCount',
                //data: "{country: '  country '}",
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
                    var options = {
                        title: 'Source Wise Enquiry',
                        pieHole: 0.5,
                        ////width: '80%',
                        height: 300, 
                         legend: { position: 'bottom' },
                        //bar: { groupWidth: '80%' },
                        //isStacked: true,
                        //is3D: true,
                        //trendlines: {
                        //    0: { type: 'exponential', color: '#333', opacity: 2 }
                        //}
                    };
                    var chart = new google.visualization.PieChart(document.getElementById("divSource"));
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


</asp:Content>
