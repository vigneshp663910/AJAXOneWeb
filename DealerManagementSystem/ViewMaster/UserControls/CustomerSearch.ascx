<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSearch.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
<link rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />--%>

<script src="../../autocomplete/jquery-ui.js"></script>
<link rel="Stylesheet" href="../../autocomplete/jquery-ui.css" />
<script type="text/javascript">    
    $(function () {
        $("#MainContent_UC_Expense_txtCustomer").autocomplete({
            source: function (request, response) {
                var param = { empName: $('#MainContent_UC_Expense_txtCustomer').val() };
                $.ajax({
                    url: "CustomerSearch.ascx/GetSearch",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST", contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                   
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");    
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox    
        });
    });
</script>

<asp:TextBox ID="txtCustomer" runat="server" AutoCompleteType="Disabled"></asp:TextBox>

<asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
    <Columns>
        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Customer Code">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
            <ItemTemplate>
                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle BackColor="#f2f2f2" />
    <FooterStyle ForeColor="White" />
    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
</asp:GridView>
