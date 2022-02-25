<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="tab_AdminModulePages.aspx.cs" Inherits="DealerManagementSystem.ViewAdmin.tab_AdminModulePages" %>

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
        <button class="tablinks" onclick="openCity(event, 'Module')" id="defaultOpen">Module</button>
        <button class="tablinks" onclick="openCity(event, 'Roles')">Roles</button>
        <button class="tablinks" onclick="openCity(event, 'Users')">Users</button>
        <button class="tablinks" onclick="openCity(event, 'UserRoles')">User Roles</button>
        <button class="tablinks" onclick="openCity(event, 'LoginLogs')">LoginLogs</button>
        <button class="tablinks" onclick="openCity(event, 'TxnLogs')">Transaction Logs</button>

    </div>

    <div id="Module" class="tabcontent">
        <h3>Module</h3>
        <p>DMS Module Master</p>
        <div style="text-align: center;">
            <img src='/images/UnderDev.png' width="202" height="202">
        </div>
    </div>

    <div id="Roles" class="tabcontent">
        <h3>Roles Master</h3>
        <p>DMS Roles Master</p>
    </div>

    <div id="Users" class="tabcontent">
        <h3>User Master</h3>
        <p>DMS User Master</p>

    </div>

    <div id="UserRoles" class="tabcontent">
        <h3>User Roles</h3>
        <p>DMS User Roles Authorisation</p>
    </div>

    <div id="LoginLogs" class="tabcontent">
        <h3>Login Logs</h3>
        <p>DMS Login Logs</p>
    </div>

    <div id="TxnLogs" class="tabcontent">
        <h3>Txn Logs</h3>
        <p>DMS Transaction Logs</p>
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

