<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LocationMap.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.LocationMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        html {
            height: 100%
        }

        body {
            height: 100%;
            margin: 0;
            padding: 0
        }

        #map_canvas {
            height: 100%
        }
    </style>
    <%-- <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">    </script>--%>

    <%--  <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk">    </script>--%>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>

    var geocoder;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Employee</label>
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-center">
                            <br />
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                        <div class="col-md-2 text-center;">
                            <table style="text-align: left; background-color: white; font-size: smaller; font-weight: normal;">
                                <tr style="text-align: left; background-color: black; color: white; font-weight: normal;">
                                    <td>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/DealerMainOffice.png" Width="18" Height="18" /></td>
                                    <td>Dealer Main Office</td>
                                    <td>
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/DealerBranchOffice.png" Width="18" Height="18" /></td>
                                    <td>Dealer Branch Office</td>
                                </tr>
                                <tr style="text-align: left; background-color: black; color: white; font-weight: normal;">
                                    <td>
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/SalesEngg.png" Width="20" Height="20" /></td>
                                    <td>Sales Executive</td>
                                    <td>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ServiceEngg.png" Width="20" Height="20" /></td>
                                    <td>Service Executive</td>
                                </tr>

                                <tr style="text-align: left; background-color: black; color: white; font-weight: normal;">
                                    <td>
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Officer.png" Width="20" Height="20" /></td>
                                    <td>Office Executive</td>
                                    <td>
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Officer.png" Width="20" Height="20" /></td>
                                    <td>Office Executive</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div id="map_canvas" style="width: 100%; height: 600px"></div>

        </div>

    </div>

    <script type="text/javascript">
        //  const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";
        // const image = "http://maps.google.com/mapfiles/kml/shapes/man.png";

        //var icon1 = {
        //   // url: "http://maps.google.com/mapfiles/kml/shapes/man.png", // url*/
        //    //url: "https://ajaxone.ajax-engg.com/Images/SalesEngg.png",
        //    url: "https://ajaxone.ajax-engg.com/Images/ServiceEngg.jpg",
        //    scaledSize: new google.maps.Size(25, 25) // size
        //};





        /*function initialize() {*/

        var markers = JSON.parse('<%=ConvertDataTabletoString() %>');
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
            //zoom: 12,
            zoom: 4.6,
            mapTypeId: google.maps.MapTypeId.ROADMAP
            //  marker:true
        };
        var infoWindow = new google.maps.InfoWindow();
        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        for (i = 0; i < markers.length; i++) {
            var data = markers[i]
            debugger;
            //var locationService = new GoogleLocationService();
            //var point = locationService.GetLatLongFromAddress(data.GeoLocation);
            //var latitude = data.Latitude;
            //var longitude = data.Longitude;
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: { url: data.image, scaledSize: new google.maps.Size(25, 25) },
                // icon: data.image,
            });

            (function (marker, data) {

                // Attaching a click event to the current marker
                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent(data.description);
                    infoWindow.open(map, marker);
                });
            })(marker, data);
        }
        /* }*/


    </script>


</asp:Content>
