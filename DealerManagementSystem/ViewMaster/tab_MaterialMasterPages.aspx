<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="tab_MaterialMasterPages.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.tab_MaterialMasterPages" %>

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
        <button class="tablinks" onclick="openCity(event, 'List')" id="defaultOpen">List</button>
        <button class="tablinks" onclick="openCity(event, 'Price')">Price</button>
        <button class="tablinks" onclick="openCity(event, 'Supersede')">Supersede</button>
        <button class="tablinks" onclick="openCity(event, 'Re-Order')">Re-Order Level</button>
        <button class="tablinks" onclick="openCity(event, 'Equipment')">Equipment</button>

    </div>

    <div id="List" class="tabcontent">
        <h3>Material Master</h3>
        <p>Material Master from SAP</p>
    </div>

    <div id="Price" class="tabcontent">
        <h3>Price Master</h3>
        <p>Price Master from SAP</p>
    </div>

    <div id="Supersede" class="tabcontent">
        <h3>Supersede Master</h3>
        <p>Supersede Master from SAP</p>

    </div>

    <div id="Re-Order" class="tabcontent">
        <h3>Re-OrderLevel</h3>
        <p>Re-Order Level Master</p>
    </div>

    <div id="Equipment" class="tabcontent">
         <h3>Equipment Master</h3>
        <p>Equipment Master</p>
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

