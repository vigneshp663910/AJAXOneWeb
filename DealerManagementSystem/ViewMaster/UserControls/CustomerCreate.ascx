<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreate.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<script type="text/javascript"> 
    $(function () {
        $('#UCdiv1').click(function () {
            var CustomerID = document.getElementById('lblCustomerID1')
            var CustomerName = document.getElementById('lblCustomerName1')
            var ContactPerson = document.getElementById('lblContactPerson1')
            var Mobile = document.getElementById('lblMobile1')
            UCAutoCustomer(CustomerID, CustomerName, ContactPerson, Mobile);
        });
    });
    $(function () {
        $('#UCdiv2').click(function () {
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
    $('#txtEmail').watermark('Required information');

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

    .WatermarkCssClass {
        color: #aaa;
    }
</style>


<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Title</label>
                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <%--<div id="UCdivAuto" style="position: absolute; background-color: red; display: none; z-index: 1;">--%>
                    <div id="UCdivAuto" class="custom-auto-complete"> 
                        <div id="UCdiv1" class="auto-item"  style="display: none"> 
                        </div> 
                        <div id="UCdiv2" class="auto-item"  style="display: none">
                        </div>
                        <div id="UCdiv3" class="auto-item"  style="display: none">
                        </div>
                        <div id="UCdiv4" class="auto-item"  style="display: none">
                        </div>
                        <div id="UCdiv5" class="auto-item"  style="display: none">
                        </div>
                    </div>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCustomerName" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer Name2</label>
                    <asp:TextBox ID="txtCustomerName2" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtCustomerName2" WatermarkText="Customer Name2" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">GSTIN</label>
                    <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtGSTIN" WatermarkText="GSTIN" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">PAN</label>
                    <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtPAN" WatermarkText="PAN" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Contact Person</label>

                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtContactPerson" WatermarkText="Contact Person" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Mobile</label>

                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtMobile" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Alternative Mobile</label>

                    <asp:TextBox ID="txtAlternativeMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtAlternativeMobile" WatermarkText="Alternative Mobile" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Email</label>

                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Email" MaxLength="40"></asp:TextBox>
                    <%-- <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID ="txtEmail" WatermarkText="Email"  />--%>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Address 1</label>
                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtAddress1" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Address 2</label>

                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtAddress2" WatermarkText="Address 2" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Address 3</label>

                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtAddress3" WatermarkText="Address 3" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Country</label>

                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">State</label>

                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">District</label>

                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Tehsil</label>

                    <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" DataTextField="Tehsil" DataValueField="TehsilID" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">PinCode</label>

                    <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtPincode" WatermarkText="Pincode" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">City</label>

                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtCity" WatermarkText="City" WatermarkCssClass="WatermarkCssClass" />
                </div>


                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Birth Date</label>

                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" BorderColor="Silver"  WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                    <asp1:CalendarExtender ID="cxDOB" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="txtDOB" WatermarkText="DD/MM/YYYY" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Anniversary Date</label>

                    <asp:TextBox ID="txtDOAnniversary" runat="server" CssClass="form-control" BorderColor="Silver"  WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                    <asp1:CalendarExtender ID="cxDOAnniversary" runat="server" TargetControlID="txtDOAnniversary" PopupButtonID="txtDOAnniversary" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtDOAnniversary" WatermarkText="DD/MM/YYYY" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Send SMS</label> 
                    <asp:CheckBox ID="cbSendSMS" runat="server" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Send Email</label> 
                    <asp:CheckBox ID="cbSendEmail" runat="server" />
                </div>
                
            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
