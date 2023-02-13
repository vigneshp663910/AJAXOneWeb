<%@ Page Title="" Language="C#" MasterPageFile="~/DealerDashboard.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeBehind="EnquiryChart.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.EnquiryChart" %>

<%@ Register Src="~/UserControls/MultiSelectDropDown.ascx" TagPrefix="UC" TagName="UC_M_Dealer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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
                        <UC:UC_M_Dealer ID="ddlmDealer" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
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
                        <UC:UC_M_Dealer ID="ddlmProductType" runat="server"></UC:UC_M_Dealer>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>

    <div class="tab">
        <button class="tablinks active" onclick="openCity(event, 'London'); return false;">Conversion Ratio </button>
        <button class="tablinks" onclick="openCity(event, 'Paris'); return false;">Product Velocity</button>
        <button class="tablinks" onclick="openCity(event, 'Tokyo'); return false;">Source Velocity</button>
    </div>

    <div id="London" class="tabcontentEnq">
        <div class="div1">
            <div class="grid">
                <div class="tile-size-one grid-item">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label3" Text="Total Enquiry" runat="server" />
                            </div>
                            <div class="details-position">
                                <asp:Label ID="lblEnquiryCount" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-one grid-item">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label1" Text="Enquiry Conversion % " runat="server" />
                            </div>
                            <div class="details-position">
                                <asp:Label ID="lblTotalConversion" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label8" Text="Region Wise Enquiry" runat="server" />
                            </div>
                            <div id="divRegion" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label9" Text="Source Wise Enquiry" runat="server" />
                            </div>
                            <div id="divSource" style="height: 300px"></div>
                        </div>
                    </div>
                </div>

                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="content">
                            <div class="details">
                                <div class="desc">
                                    <asp:Label ID="Label2" Text="Conversion Ratio - Product wise" runat="server" />
                                </div>

                                <asp:GridView ID="gvEnquiryProductType" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                    <Columns>
                                        <asp:BoundField HeaderText="Product Type" DataField="ProductType" />

                                        <asp:BoundField HeaderText="Total Enq" DataField="TotalCount">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Won" DataField="ConvertedCount">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Conv Ratio" DataField="Conversion">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#ffffff" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-four grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="content">
                            <div class="details">
                                <div class="desc">
                                    <asp:Label ID="Label6" Text="Conversion Ratio - Source wise" runat="server" />
                                    <br />
                                </div>
                                <asp:GridView ID="gvEnquirySource" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                    <Columns>
                                        <asp:BoundField HeaderText="Source" DataField="LeadSource" />
                                        <asp:BoundField HeaderText="Total Enq" DataField="Total">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Contribution" DataField="Effectiveness">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Won" DataField="Won">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Conv Ratio" DataField="Conversion">
                                            <ItemStyle HorizontalAlign="Right" ForeColor="#bd0cbd" Font-Bold="true" />
                                        </asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#ffffff" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="Paris" class="tabcontentEnq">
        <div class="div1">
            <div class="grid">
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label7" Text="Enquiry Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label5" Text="Concrete Mixer Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity1" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label4" Text="Concrete Pump Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity2" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label10" Text="Batching Plant Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity3" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label11" Text="Transit Mixer Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity4" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label12" Text="Boom Pump Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity5" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label13" Text="Dumper Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity6" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label14" Text="Placing Equipment Velocity" runat="server" />
                            </div>
                            <div id="divProductVelocity7" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="Tokyo" class="tabcontentEnq">
        <div class="div1">
            <div class="grid">

                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label16" Text="Positive Referral Customer" runat="server" />
                            </div>
                            <div id="divSourceVelocity1" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label17" Text="AJAX Website" runat="server" />
                            </div>
                            <div id="divSourceVelocity2" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label18" Text="Magazine ADD" runat="server" />
                            </div>
                            <div id="divSourceVelocity3" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label19" Text="Existing Customer - Repeat Order" runat="server" />
                            </div>
                            <div id="divSourceVelocity4" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label20" Text="Social Media Campaign" runat="server" />
                            </div>
                            <div id="divSourceVelocity5" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label21" Text="Exhibition" runat="server" />
                            </div>
                            <div id="divSourceVelocity6" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label22" Text="Mktg Campaign" runat="server" />
                            </div>
                            <div id="divSourceVelocity7" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
             
                
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label28" Text="Indiamart Buy Lead" runat="server" />
                            </div>
                            <div id="divSourceVelocity8" style="height: 300px"></div>
                        </div>
                    </div>
                </div> 
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label29" Text="Indiamart Call" runat="server" />
                            </div>
                            <div id="divSourceVelocity9" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label30" Text="Direct Enquiry" runat="server" />
                            </div>
                            <div id="divSourceVelocity10" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label15" Text="Indiamart Email" runat="server" />
                            </div>
                            <div id="divSourceVelocity11" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label23" Text="MKTG Road Show" runat="server" />
                            </div>
                            <div id="divSourceVelocity12" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label24" Text="MKTG Customer Meet" runat="server" />
                            </div>
                            <div id="divSourceVelocity13" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label25" Text="Indiamart Catalogue View" runat="server" />
                            </div>
                            <div id="divSourceVelocity14" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label26" Text="Reference Operator" runat="server" />
                            </div>
                            <div id="divSourceVelocity15" style="height: 300px"></div>
                        </div>
                    </div>
                </div>
                <div class="tile-size-two grid-item" style="height: auto !important">
                    <div class="content">
                        <div class="details">
                            <div class="desc">
                                <asp:Label ID="Label27" Text="Reference  BC India" runat="server" />
                            </div>
                            <div id="divSourceVelocity16" style="height: 300px"></div>
                        </div>
                    </div>
                </div>



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
                , Country: $('#MainContent_ddlMCountry').val()
                , Region: Region
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
                        // title: 'Region Wise Enquiry',
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
                , Country: $('#MainContent_ddlMCountry').val()
                , Region: Region
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
                        // title: 'Source Wise Enquiry',
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
        function VelocityChart() {
            var Region = $('#MainContent_ddlMRegion').val();
            var param = {
                DateFrom: $('#MainContent_txtDateFrom').val()
                , DateTo: $('#MainContent_txtDateTo').val()
                , Country: $('#MainContent_ddlMCountry').val()
                , Region: Region
            }
            $.ajax({
                type: "POST",
                url: 'EnquiryChart.aspx/GetVelocity',
                //data: "{country: '  country '}",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                   
                    debugger;
                    var view0 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[0]));
                    var view1 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[1]));
                    var view2 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[2]));
                    var view3 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[3]));
                    var view4 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[4]));
                    var view5 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[5]));
                    var view6 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[6]));
                    var view7 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[7]));

                    var SourceView1 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[8]));
                    var SourceView2 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[9]));
                    var SourceView3 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[10]));
                    var SourceView4 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[11]));
                    var SourceView5 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[12]));
                    var SourceView6 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[13]));
                    var SourceView7 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[14]));
                    var SourceView8 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[15]));
                    var SourceView9 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[16]));
                    var SourceView10 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[17]));
                    var SourceView11 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[18]));
                    var SourceView12 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[19]));
                    var SourceView13 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[20]));
                    var SourceView14 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[21]));
                    var SourceView15 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[22]));                  
                    var SourceView16 = new google.visualization.DataView(google.visualization.arrayToDataTable(data.d[23]));
                  

                    //var data1 = google.visualization.arrayToDataTable(data.d);
                    //var view = new google.visualization.DataView(data1);
                    //view.setColumns([0, 1
                    //    //{
                    //    //    calc: "stringify",
                    //    //    sourceColumn: 1,
                    //    //    type: "string",
                    //    //    role: "annotation"
                    //    //}, 
                    //]);

                    view0.setColumns([0, 1]);
                    view1.setColumns([0, 1]);
                    view2.setColumns([0, 1]);
                    view3.setColumns([0, 1]);
                    view4.setColumns([0, 1]);
                    view5.setColumns([0, 1]);
                    view6.setColumns([0, 1]);
                    view7.setColumns([0, 1]);

                    SourceView1.setColumns([0, 1]);
                    SourceView2.setColumns([0, 1]);
                    SourceView3.setColumns([0, 1]);
                    SourceView4.setColumns([0, 1]);
                    SourceView5.setColumns([0, 1]);
                    SourceView6.setColumns([0, 1]);
                    SourceView7.setColumns([0, 1]);
                    SourceView8.setColumns([0, 1]);
                    SourceView9.setColumns([0, 1]);
                    SourceView10.setColumns([0, 1]);
                    SourceView11.setColumns([0, 1]);
                    SourceView12.setColumns([0, 1]);
                    SourceView13.setColumns([0, 1]);
                    SourceView14.setColumns([0, 1]);
                    SourceView15.setColumns([0, 1]);
                    SourceView16.setColumns([0, 1]);
                    
                    

                    var options = {
                        //width: '80%',
                        height: 300,
                        legend: { position: 'none' },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true
                    };
                    var chart0 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity"));
                    chart0.draw(view0, options);

                    var chart1 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity1"));
                    chart1.draw(view1, options);

                    var chart2 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity2"));
                    chart2.draw(view2, options);

                    var chart3 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity3"));
                    chart3.draw(view3, options);

                    var chart4 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity4"));
                    chart4.draw(view4, options);

                    var chart5 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity5"));
                    chart5.draw(view5, options);

                    var chart6 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity6"));
                    chart6.draw(view6, options);

                    var chart7 = new google.visualization.ColumnChart(document.getElementById("divProductVelocity7"));
                    chart7.draw(view7, options);


                    var SourceChart1 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity1"));
                    SourceChart1.draw(SourceView1, options);

                    var SourceChart2 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity2"));
                    SourceChart2.draw(SourceView2, options);

                    var SourceChart3 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity3"));
                    SourceChart3.draw(SourceView3, options);

                    var SourceChart4 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity4"));
                    SourceChart4.draw(SourceView4, options);

                    var SourceChart5 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity5"));
                    SourceChart5.draw(SourceView5, options);

                    var SourceChart6 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity6"));
                    SourceChart6.draw(SourceView6, options);

                    var SourceChart7 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity7"));
                    SourceChart7.draw(SourceView7, options);

                    var SourceChart8 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity8"));
                    SourceChart8.draw(SourceView8, options);

                    var SourceChart9 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity9"));
                    SourceChart9.draw(SourceView9, options);

                    var SourceChart10 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity10"));
                    SourceChart10.draw(SourceView10, options);

                    var SourceChart11 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity11"));
                    SourceChart11.draw(SourceView11, options);

                    var SourceChart12 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity12"));
                    SourceChart12.draw(SourceView12, options);

                    var SourceChart13 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity13"));
                    SourceChart13.draw(SourceView13, options);

                    var SourceChart14 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity14"));
                    SourceChart14.draw(SourceView14, options);

                    var SourceChart15 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity15"));
                    SourceChart15.draw(SourceView15, options);

                    var SourceChart16 = new google.visualization.ColumnChart(document.getElementById("divSourceVelocity16"));
                    SourceChart16.draw(SourceView16, options); 
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
        function openCity(evt, cityName) {
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
            evt.currentTarget.className += " active";
        }
        //$(window).bind("load", function () {

        //    var cityName = "Paris";
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
        //});
    </script>


</asp:Content>
