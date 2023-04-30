<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUserManual.aspx.cs" Inherits="DealerManagementSystem.View.ViewUserManual" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <style>
        #download{
            visibility:hidden;
        }
        #more{
            visibility:hidden;
        }
        #start{
            visibility:hidden;
        }
        #center{
            visibility:hidden;
        }
        iframe { overflow:hidden; }
    </style>
    <script type="text/javascript">
        jQuery('#ifrm_dcbform').load(function () {
            jQuery('#ifrm_dcbform').contents().find("#start").hide();
            jQuery('#ifrm_dcbform').contents().find("#center").hide();
            jQuery('#ifrm_dcbform').contents().find("#end").hide();
        });
    </script>
    <iframe id="ifrm_dcbform" runat="server" class="sdd" style="width: 100%" frameborder="0" border="0" allowtransparency="true" scrolling="no"></iframe>

    </form>
</body>
</html>
