<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="MapTest2.aspx.cs" Inherits="DealerManagementSystem.MapTest2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <title>Show/Add multiple markers to Google Maps from database in asp.net website</title>
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
     <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk">    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <asp:Button ID="btnSearchCustChgHst" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true"  />
                  
      <div id="map_canvas" style="width: 500px; height: 400px"></div>
      <script type="text/javascript">
          const image = "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";
         
          /*function initialize() {*/

              var markers = JSON.parse('<%=ConvertDataTabletoString() %>');
              var mapOptions = {
                  center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                  zoom: 5,
                  mapTypeId: google.maps.MapTypeId.ROADMAP
                  //  marker:true
              }; 
              var infoWindow = new google.maps.InfoWindow();
              var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
              for (i = 0; i < markers.length; i++) {
                  var data = markers[i]
                  var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                  var marker = new google.maps.Marker({
                      position: myLatlng,
                      map: map,
                      title: data.title,
                      icon: image,
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
