<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreate.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerCreate" %>

<script type="text/javascript"> 
    $(function () {
        $('#UCdiv1').click(function () {
            debugger
            var  CustomerID = document.getElementById('lblCustomerID1')
            var  CustomerName = document.getElementById('lblCustomerName1')
            var  ContactPerson = document.getElementById('lblContactPerson1')
            var  Mobile = document.getElementById('lblMobile1')
            UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
        });
    });
    $(function () {
        $('#UCdiv2').click(function () {
            debugger
            var CustomerID = document.getElementById('lblCustomerID2')
            var CustomerName = document.getElementById('lblCustomerName2')
            var ContactPerson = document.getElementById('lblContactPerson2')
            var Mobile = document.getElementById('lblMobile2')
            UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
        });
    });
    $(function () {
        $('#UCdiv3').click(function () {
            var CustomerID = document.getElementById('lblCustomerID3')
            var CustomerName = document.getElementById('lblCustomerName3')
            var ContactPerson = document.getElementById('lblContactPerson3')
            var Mobile = document.getElementById('lblMobile3')
            UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
        });
    });
    $(function () {
        $('#UCdiv4').click(function () {
            var CustomerID = document.getElementById('lblCustomerID4')
            var CustomerName = document.getElementById('lblCustomerName4')
            var ContactPerson = document.getElementById('lblContactPerson4')
            var Mobile = document.getElementById('lblMobile4')
            UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
        });
    });
    $(function () {
        $('#UCdiv5').click(function () {
            var CustomerID = document.getElementById('lblCustomerID5')
            var CustomerName = document.getElementById('lblCustomerName5')
            var ContactPerson = document.getElementById('lblContactPerson5')
            var Mobile = document.getElementById('lblMobile5')
            UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
        });
    });

</script>
<style>
    .fieldset-borderAuto {
        border: solid 1px #cacaca;
        margin: 1px 0;
        border-radius: 5px;
        padding: 10px;
        background-color: #b4b4b4;
    }

        .fieldset-borderAuto tr {
            /* background-color: #000084; */
            background-color: inherit;
            font-weight: bold;
            color: white;
        }

        .fieldset-borderAuto:hover {
            background-color: blue;
        }
</style>


<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-2 text-right">
                    <label>Customer Name</label>
                </div>
                <div class="col-md-4">
                   
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    <div id="UCdivAuto" style="position: absolute; background-color: red; display: none; z-index: 1;">
                        <div id="UCdiv1" class="fieldset-border">
                        </div>
                        <div id="UCdiv2" class="fieldset-border">
                        </div>
                        <div id="UCdiv3" class="fieldset-border">
                        </div>
                        <div id="UCdiv4" class="fieldset-border">
                        </div>
                        <div id="UCdiv5" class="fieldset-border">
                        </div>
                    </div>
                </div>
                <div class="col-md-2 text-right">
                    <label>GSTIN</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>PAN</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Contact Person</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Mobile</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Alternative Mobile</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtAlternativeMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Email</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Email"></asp:TextBox>
                </div>

                <div class="col-md-2 text-right">
                    <label>Address 1</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Address 2</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Address 3</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>Country</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-2 text-right">
                    <label>State</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-2 text-right">
                    <label>District</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-2 text-right">
                    <label>Tehsil</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" DataTextField="Tehsil" DataValueField="TehsilID" />
                </div>
                <div class="col-md-2 text-right">
                    <label>PinCode</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-md-2 text-right">
                    <label>City</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>


                <div class="col-md-2 text-right">
                    <label>Birth Date</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                </div>

                <div class="col-md-2 text-right">
                    <label>Anniversary Date</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDOAnniversary" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                </div>

                <div class="col-md-2 text-right">
                    <label>Send SMS</label>
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="cbSendSMS" runat="server" />
                </div>

                <div class="col-md-2 text-right">
                    <label>Send Email</label>
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="cbSendEmail" runat="server" />
                </div>

            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
