<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="LoginAs.aspx.cs" Inherits="DealerManagementSystem.Account.LoginAs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        /* function geoFindMe() { */

        //const status = document.querySelector('#status');
        //const mapLink = document.querySelector('#map-link');

        //mapLink.href = '';
        //mapLink.textContent = '';

        function success(position) {

            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            document.getElementById('MainContent_hfLatitude').value = latitude;
            document.getElementById('MainContent_hfLongitude').value = longitude;
            status.textContent = '';

            /*alert(latitude);*/
            //  mapLink.href = `https://www.openstreetmap.org/#map=18/${latitude}/${longitude}`;
            //  mapLink.textContent = `Latitude: ${latitude} °, Longitude: ${longitude} °`; 
        }
        function error() {
            status.textContent = 'Unable to retrieve your location';
        }

        if (!navigator.geolocation) {
            status.textContent = 'Geolocation is not supported by your browser';

        } else {
            status.textContent = 'Locating…';
            navigator.geolocation.getCurrentPosition(success, error);
        }

        /*  } */
     //   document.querySelector('#find-me').addEventListener('click', geoFindMe);
    </script>
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>

    <body>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
        <asp:Panel ID="pnlUser" runat="server">
            <div>
                <div class="diveField">
                    <div class="diveFieldLabel">
                        <asp:Label ID="lblEmp" runat="server" Text="User ID" CssClass="label"></asp:Label>
                    </div>
                    <div class="diveFieldText">
                        <asp:TextBox ID="txtEmp" runat="server" CssClass="TextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="diveField">
                    <div class="diveFieldLabel">
                        <asp:Label ID="Label2" runat="server" Text="Contact Name" CssClass="label"></asp:Label>
                    </div>
                    <div class="diveFieldText">
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="TextBox"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" OnClick="btnSearch_Click" />
            </div>


            <div style="width: 100%; overflow-x: auto; overflow-y: auto; padding-bottom: 10px;">
                <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false"
                    CssClass="TableView" BorderStyle="None" AllowPaging="true" OnPageIndexChanging="gvEmployee_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="User Id">
                            <ItemStyle BorderStyle="None" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lbUserID" runat="server" OnClick="lbEmpId_Click">
                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' Visible="false" />
                                    <asp:TextBox ID="txtVendor" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:TextBox>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" BorderStyle="None" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtEmployeeNameBy" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </asp:Panel>
    </body>
    </html>
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfLatitude" runat="server" />
    <asp:HiddenField ID="hfLongitude" runat="server" />
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-left">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">User ID</label>
                            <asp:TextBox ID="txtEmp" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">Name</label>
                            <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">IsEnabled</label>
                            <asp:DropDownList ID="ddlIsEnabled" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="2">InActive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        
                        <div class="col-md-2 text-left">
                            <label>Department</label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Designation</label>
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearch_Click"></asp:Button>
                        </div>

                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                <div class="col-md-12 Report">

                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>User(s):</td>

                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnUserArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnUserArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnUserArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnUserArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvEmployee_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ExternalReferenceID")%>' runat="server" />
                                </ItemTemplate>
                                <%--<HeaderStyle Width="62px" />--%>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Dealer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeRole.Dealer.DealerName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="250px" />
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="User ID" SortExpression="UserID">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkUserID" runat="server" OnClick="lbEmpId_Click">
                                        <asp:TextBox ID="txtUserID" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' Width="100%" Visible="false" />
                                        <asp:Label ID="lblUserName" runat="server" CssClass="TextBox" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' Width="100%" Height="22px"></asp:Label>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemStyle VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:Label ID="txtEmployeeNameBy" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server" Width="300px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Enabled?">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lbIsLocked" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "IsLocked")%>'></asp:Label>--%>
                                    <asp:CheckBox ID="cbIsEnabled" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsEnabled")%>' Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemStyle VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <%--<asp:Label ID="txtEmployeeNameBy" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server" Width="500px"></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>

</asp:Content>
