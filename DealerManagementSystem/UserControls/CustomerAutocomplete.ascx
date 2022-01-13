<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAutocomplete.ascx.cs" Inherits="DealerManagementSystem.UserControls.CustomerAutocomplete" %>
 

<script src="../JSAutocomplete/ajax/jquery-1.8.0.js"></script>
<script src="../JSAutocomplete/ajax/ui1.8.22jquery-ui.js"></script>
<link rel="Stylesheet" href="../JSAutocomplete/ajax/jquery-ui.css" />
<script type="text/javascript">  
    $(function () {
        $("#MainContent_txtCustomerAutocomplete_txtCustomerAuto").autocomplete({ 
            source: function (request, response) {
                debugger
                var param = { CustS: $('#MainContent_txtCustomerAutocomplete_txtCustomerAuto').val() };
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                  /*  url: "TestAutocomplete.aspx/GetEmpNames",*/
                    url: "CustomerAutocomplete.ascx/GetCustomer", 
                    data: JSON.stringify(param),
                    dataType: 'JSON',
                    success: function (data) {
                        debugger
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
            minLength: 3 //This is the Char length of inputTextBox    
        });
    });
</script>

<script type="text/javascript" src="../JSAutocomplete/ajax/1.8.3jquery.min.js"></script>
<script type="text/javascript"> 
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

<asp:TextBox ID="txtCustomerAuto" runat="server"></asp:TextBox>
<div id="divAuto" style="position: absolute; background-color: red; display: none">
    <div id="div1" class="fieldset-border">
    </div>
    <div id="div2" class="fieldset-border">
    </div>
    <div id="div3" class="fieldset-border">
    </div>
    <div id="div4" class="fieldset-border">
    </div>
    <div id="div5" class="fieldset-border">
    </div>
</div>
