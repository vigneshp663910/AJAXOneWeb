<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="tab_ASNPages.aspx.cs" Inherits="DealerManagementSystem.ViewSales.tab_ASNPages" %>

<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="../CSS/mystyle.css">
    <style>
       .body {
            font-family: Calibri;
            /* font-size: x-large;
            font-weight: bolder;*/
        }

        /* Style the tab */
        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 5px 16px;
                transition: 0.3s;
                font-size: 14px;
                font-weight: bold;
                width: 150px;
                border: thick;
                border-color: black;
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
</head>
<body>

    <%-- <p>In this example, we use JavaScript to "click" on the London button, to open the tab on page load.</p>--%>

    <div class="tab">
        <button class="tablinks" onclick="openCity(event, 'List')" id="defaultOpen">ASN List</button>
        <button class="tablinks" onclick="openCity(event, 'View')">ASN View</button>
        <button class="tablinks" onclick="openCity(event, 'OEM')">OEM Invoice</button>
        <button class="tablinks" onclick="openCity(event, 'GR')">Goods Receipt</button>
        <button class="tablinks" onclick="openCity(event, 'Report')">Reports</button>

    </div>

    <div id="List" class="tabcontent">
        <h3>ASN List</h3>
        <p>London is the capital city of England.</p>

    </div>

    <div id="View" class="tabcontent">
        <h3>View ASN</h3>
        <p>Paris is the capital of France.</p>
    </div>

    <div id="OEM" class="tabcontent">
        <h3>Paris</h3>
        <p>Paris is the capital of France.</p>
    </div>

    <div id="GR" class="tabcontent">
        <h3>Paris</h3>
        <p>Paris is the capital of France.</p>
    </div>

    <div id="Report" class="tabcontent">
        <h3>Tokyo</h3>
        <p>Tokyo is the capital of Japan.</p>
    </div>

    <script>
        function openCity(evt, cityName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
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

        // Get the element with id="defaultOpen" and click on it
        document.getElementById("defaultOpen").click();
    </script>

</body>
</html>
