<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAutocomplete.aspx.cs" Inherits="DealerManagementSystem.TestAutocomplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Autocomplete - Default functionality</title>
    <%-- <link rel="stylesheet" href="JSAutocomplete/query-ui.css"> 
    <script src="/JSAutocomplete/jquery-3.6.0.js"></script>   
    <script src="/JSAutocomplete/ui1.13.0jquery-ui.js"></script>

      <%--     
    <script>
        $(function () {
            var availableTags = [
                "ActionScript",
                "AppleScript",
                "Asp",
                "BASIC",
                "C",
                "C++",
                "Clojure",
                "COBOL",
                "ColdFusion",
                "Erlang",
                "Fortran",
                "Groovy",
                "Haskell",
                "Java",
                "JavaScript",
                "Lisp",
                "Perl",
                "PHP",
                "Python",
                "Ruby",
                "Scala",
                "Scheme"
            ];
            $("#tags").autocomplete({
                source: availableTags
            });
        });
    </script>--%>



    <script src="/JSAutocomplete/ajax/jquery-1.8.0.js"></script>

    <script src="JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
    <link rel="Stylesheet" href="JSAutocomplete/ajax/jquery-ui.css" />
    <script type="text/javascript">    
        //$(function () {
        //    $("#txtEmpName").autocomplete({
        //        source: function (request, response) {
        //            var param = { empName: $('#txtEmpName').val() };
        //            $.ajax({
        //                url: "TestAutocomplete.aspx/GetEmpNames",
        //                data: JSON.stringify(param),
        //                dataType: "json",
        //                type: "POST",
        //                contentType: "application/json; charset=utf-8",
        //                dataFilter: function (data) { return data; },
        //                success: function (data) {
        //                    response($.map(data.d, function (item) {
        //                        return {
        //                            value: item
        //                        }
        //                    }))
        //                },
        //                error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                    var err = eval("(" + XMLHttpRequest.responseText + ")");
        //                    alert(err.Message)
        //                    // console.log("Ajax Error!");    
        //                }
        //            });
        //        },
        //        minLength: 1 //This is the Char length of inputTextBox    
        //    });
        //});

        //$(function () {
        //    $("#txtEmpName").autocomplete({
        //        source: function (request, response) {
        //            var param = { empName: $('#txtEmpName').val() };

        //            $.ajax({
        //                type: 'POST',
        //                contentType: "application/json; charset=utf-8",
        //                url: "TestAutocomplete.aspx/GetEmpNames",
        //                data: JSON.stringify(param),
        //                dataType: 'JSON',
        //                success: function (data) {
        //                    $('#grd').empty();
        //                    $('#grd').append("<tr><th>Recognition_Type </th><th>Recognition_Number </th></tr>")
        //                    for (var i = 0; i < 5; i++) {

        //                        $('#grd').append("<tr><td>" + "john" + "</td><td>" + "Peter" + "</td></tr>")
        //                    };


        //                },
        //                error: function () {

        //                    alert("Error");
        //                }
        //            });
        //        },
        //        minLength: 1 //This is the Char length of inputTextBox    
        //    });
        //});




        $(function () {
            $("#txtEmpName").autocomplete({
                source: function (request, response) {
                    var param = { empName: $('#txtEmpName').val() };

                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        url: "TestAutocomplete.aspx/GetEmpNames",
                        data: JSON.stringify(param),
                        dataType: 'JSON',
                        success: function (data) {
                            debugger
                            //$('#divAuto').empty();
                            //document.getElementById("divAuto").innerHTML = data.d;
                            document.getElementById('divAuto').style.display = "block";
                            var n = 0;
                            for (var i = 1; i <= 5; i++) {
                                $(('#div' + i)).empty();
                                document.getElementById('div' + i).style.display = "none";
                                 
                            }

                            $.map(data.d, function (item) {
                                n = n + 1;
                                document.getElementById('div' + n).style.display = "block"; 
                                document.getElementById("div" + n).innerHTML = item;
                            })
                        },
                        error: function () {

                            alert("Error");
                        }
                    });
                },
                minLength: 1 //This is the Char length of inputTextBox    
            });
        });


         
        //$(function () {
        //    $("#txtEmpName").autocomplete({
        //        source: function (request, response) {
        //            debugger
        //            var param = { empName: $('#txtEmpName').val() };
        //            $.get("https://localhost:44302/api/Customer/CustomerAutocomplete?Customer=" + $('#txtEmpName').val(), function (data) {
        //                debugger
        //                alert("Data: " + data );
        //                document.getElementById('divAuto').style.display = "block";
        //                var n = 0;
        //                for (var i = 1; i <= 5; i++) {
        //                    $(('#div' + i)).empty();
        //                }

        //                $.map(data.d, function (item) {
        //                    n = n + 1;
        //                    $(('#div' + n)).empty();
        //                    document.getElementById("div" + n).innerHTML = item;
        //                });
        //            });
        //        },
        //        minLength: 3 //This is the Char length of inputTextBox    
        //    });
        //});
    </script>


    <script type="text/javascript" src="JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=GridView] td").click(function () {
                DisplayDetails($(this).closest("tr"));
            });
        });
        function DisplayDetails(row) {
            var message = "";
            message += "Id: " + $("td", row).eq(0).html();
            message += "\nName: " + $("td", row).eq(1).html();
            message += "\nDescription: " + $("td", row).eq(2).html();
            alert(message);
        }
        function D1() {
            alert("frtgbhfgbh");
        }

        //$(function () {
        //    $('#div').click(function () {
        //        var trid = $(this).attr('id'); // table row ID

        //        var gg = $('#div').children('txtEmpName');
        //        alert(trid);
        //    });
        //});

        $(function () {
            $('#div1').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID1'), document.getElementById('lblCustomerName1'));
            });
        });
        $(function () {
            $('#div2').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID2'), document.getElementById('lblCustomerName2'));
            });
        });
        $(function () {
            $('#div3').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID3'), document.getElementById('lblCustomerName3'));
            });
        });
        $(function () {
            $('#div4').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID4'), document.getElementById('lblCustomerName4'));
            });
        });
        $(function () {
            $('#div5').click(function () {
                AutoCustomer(document.getElementById('lblCustomerID5'), document.getElementById('lblCustomerName5'));
            });
        });
        function AutoCustomer(lblCustomerID, lblCustomerName) {
            var txtEmpName = document.getElementById('txtEmpName');
            txtEmpName.value = lblCustomerName.innerText;
             document.getElementById('divAuto').style.display = "none";
            // alert(lblCustomerName.innerText);
        }
    </script>
    <style>
        .fieldset-border {
            border: solid 1px #cacaca;
            margin: 1px 0;
            border-radius: 5px;
            padding: 10px;
            background-color: #b4b4b4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
        <div id="divAuto" style="position: absolute; background-color: red; display:none">
            <div id="div1"  class="fieldset-border">
            </div>
            <div id="div2"  class="fieldset-border">
            </div>
            <div id="div3"  class="fieldset-border">
            </div>
            <div id="div4"  class="fieldset-border">
            </div>
            <div id="div5"  class="fieldset-border">
            </div>
        </div>


<%--        <div id="divAuto1" style="background-color: red ; position:absolute">
            <div id="div10" class="fieldset-border">
                <label id="lblCustomerID11" style="display: none">John1</label>
                <table>
                    <tr>
                        <td>
                            <label id="lblCustomerName11">John1</label>
                        </td>
                        <td>Prospect</td>
                    </tr>
                    <tr>
                        <td>Peter</td>
                        <td>900067670</td>
                    </tr>
                </table>
            </div>
            <div id="div12" class="fieldset-border">
                <label id="lblCustomerID12" style="display: none">John2</label>
                <table>
                    <tr>
                        <td>
                            <label id="lblCustomerName12">John2</label></td>
                        <td>Prospect</td>
                    </tr>
                    <tr>
                        <td>Peter</td>
                        <td>900000000</td>
                    </tr>
                </table>
            </div>
        </div>--%>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </form>
</body>
</html>
