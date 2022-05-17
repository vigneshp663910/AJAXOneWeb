<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Geolocation.aspx.cs" Inherits="DealerManagementSystem.Geolocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function geoFindMe() {
            alert(0);
            const status = document.querySelector('#status');
            const mapLink = document.querySelector('#map-link');

            mapLink.href = '';
            mapLink.textContent = '';

            function success(position) {
                const latitude = position.coords.latitude;
                const longitude = position.coords.longitude;

                status.textContent = '';
              //  mapLink.href = `https://www.openstreetmap.org/#map=18/${latitude}/${longitude}`;
              //  mapLink.textContent = `Latitude: ${latitude} °, Longitude: ${longitude} °`;
                alert(latitude);
                alert(longitude);
            }

            function error() {
               
                alert(1); 
                status.textContent = 'Unable to retrieve your location';
            }

            if (!navigator.geolocation) {
                alert(2);
                status.textContent = 'Geolocation is not supported by your browser';
                
            } else {
                alert(2);
                status.textContent = 'Locating…';
                navigator.geolocation.getCurrentPosition(success, error);
            }

        }

     //   document.querySelector('#find-me').addEventListener('click', geoFindMe);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <button id = "find-me" onclick="geoFindMe()">Show my location</button><br/>
<p id = "status"></p>
<a id = "map-link" target="_blank"></a>
    </form>
</body>
</html>
