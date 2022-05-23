<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="UserLocationTrackMap.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.UserLocationTrackMap" %>

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
     <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
    <%-- <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false">    </script>--%>
    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk" >    </script>--%>
      <%--  <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg&callback=initMap&v=weekly" defer ></script>--%>
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
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div id="map" style="width: 100%; height: 600px"></div>
            <%--  <script  type="text/javascript" 
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg&callback=initMap&v=weekly"
     
    ></script>--%>
             <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk" >    </script>
        </div>

    </div>

    <%--<script type="text/javascript">
         
        debugger;
        const waypts = [];
      //  function initMap() {
        const directionsService = new google.maps.DirectionsService();
        const directionsRenderer = new google.maps.DirectionsRenderer();
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 6,
            center: { lat: 13.06667100, lng: 77.50631200 },
        }); 
              waypts.push({

                  location: new google.maps.LatLng(13.06667100, 77.50631200),
            stopover: true,
        });

        directionsService
            .route({
                origin: new google.maps.LatLng(12.95680000, 77.58330000),
                destination: new google.maps.LatLng(13.02240000, 77.52821001),
                waypoints: waypts,
                optimizeWaypoints: true,
                travelMode: google.maps.TravelMode.DRIVING,
            }).then((response) => {
                directionsRenderer.setDirections(response);

            })
            .catch((e) => window.alert("Directions request failed due to " + status));
     //   }
         //  window.initMap = initMap;
    </script>--%>

    <script>
     //   function initMap() {
            const directionsService = new google.maps.DirectionsService();
            const directionsRenderer = new google.maps.DirectionsRenderer();
            const map = new google.maps.Map(document.getElementById("map"), {
                zoom: 6,
                center: { lat: 13.06667100, lng: 77.50631200 },
            });

            directionsRenderer.setMap(map);
         
        directionsRenderer.setMap(map);
        calculateAndDisplayRoute(directionsService, directionsRenderer);
            //document.getElementById("submit").addEventListener("click", () => {
            //    calculateAndDisplayRoute(directionsService, directionsRenderer);
            //});
       // }
             

        function calculateAndDisplayRoute(directionsService, directionsRenderer) {
            debugger;
            const waypts = [];
            waypts.push({
                location: new google.maps.LatLng(13.06667100, 77.50631200),
                stopover: true,
            });  
               
            directionsService
                .route({
                    origin: new google.maps.LatLng(12.95680000, 77.58330000),
                    destination: new google.maps.LatLng(13.02240000, 77.52821001),
                    waypoints: waypts,
                    optimizeWaypoints: true,
                    travelMode: google.maps.TravelMode.DRIVING,
                })
                .then((response) => {
                    directionsRenderer.setDirections(response) ;

                    alert(response);
                    // For each route, display summary information.

                })
                .catch((e) => window.alert("Directions request failed due to " + e));
        }

     //   window.initMap = initMap;
    </script>

</asp:Content>

