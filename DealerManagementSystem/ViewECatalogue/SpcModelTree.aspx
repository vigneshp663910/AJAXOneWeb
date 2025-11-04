<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpcModelTree.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcModelTree" MaintainScrollPositionOnPostback="true" %>


<!DOCTYPE html>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" style="background-color: #808080; color: #FFFFFF">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

    <script>
        function openModal(optn) {

            var tab1 = document.getElementById("toptn1");
            var tab2 = document.getElementById("toptn2");

            if (optn == 1) {

                tab1.style.display = "block";
                tab2.style.display = "none";

            }
            else {
                tab1.style.display = "none";
                tab2.style.display = "block";
            }

            $('#myModal').modal('show')
        };
    </script>
</head>

<body id="models">
    <form id="form1" runat="server">
        <div>
            <label class="modal-label" style="font-size:22px">Machine No</label>
            <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control" AutoComplete="Off" OnTextChanged="txtEquipment_TextChanged" AutoPostBack="true"></asp:TextBox>
            <hr style="border: 1px solid #CC0000; width: 1000px;" />
            <asp:TreeView ID="TreeView1" runat="server" ImageSet="BulletedList4" NodeIndent="15" ExpandDepth="0" Font-Names="Calibri" Font-Size="Large" onclick="javascript:GetSelectedNode();" Font-Bold="False">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <NodeStyle Font-Names="Tahoma" Font-Size="13pt" ForeColor="Black" HorizontalPadding="2px"
                    NodeSpacing="0px" VerticalPadding="0px"></NodeStyle>
                <ParentNodeStyle Font-Bold="True" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="2px"
                    VerticalPadding="2px" />
            </asp:TreeView>
        </div>

    </form>

    <script type="text/javascript">

        function GetSelectedNode() {
            debugger;
            var treeViewData = window["<%=TreeView1.ClientID%>" + "_Data"];
            if (treeViewData.selectedNodeID.value != "") {
                var selectedNode = document.getElementById(treeViewData.selectedNodeID.value);
                var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);

                let parts = value.split("\\");

                var text = selectedNode.innerHTML;


                if (parts.length == 5) {
                    window.parent.RefreshIframe(parts[parts.length - 1]);
                }

            } else {
                // alert("No node selected.")
            }
            return false;
        }

        function txtSearch() {

            alert("Hi");
            document.getElementById('model').style.color('lightred')
            //would be like this in jQuery: $('#someId').css('color', 'lightred')
        }

    </script>
</body>
</html>

