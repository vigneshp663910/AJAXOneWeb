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
    --%>

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
                            $('#grd').empty();
                            $('#grd').append("<tr><th>Recognition_Type </th><th>Recognition_Number </th></tr>")
                            for (var i = 0; i < 5; i++) {

                                $('#grd').append("<tr><td>" + "john" + "</td><td>" + "Peter" + "</td></tr>")
                            };


                        },
                        error: function () {

                            alert("Error");
                        }
                    });
                },
                minLength: 1 //This is the Char length of inputTextBox    
            });
        });
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

        $(function () {
            $('#div').click(function () {
                var trid = $(this).attr('id'); // table row ID

                var gg = $('#div').children('txtEmpName');
                alert(trid);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
       

        <div id="div">
            EmpName :
            <asp:TextBox ID="txtEmpName" runat="server"   Text="john"></asp:TextBox>
            <br />
            <br />
            <br />
        </div>

          <asp:GridView ID="grd" runat="server"> 
        </asp:GridView>
            <asp:GridView ID="gvRelation" runat="server"  >
             
            <AlternatingRowStyle BackColor="#f2f2f2" />
            <FooterStyle ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
        </asp:GridView>
        <div class="ui-widget">
            <label for="tags">Tags: </label>
            <input id="tags">
        </div>

      
    </form>
</body>
</html>
