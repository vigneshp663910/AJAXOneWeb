<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreate.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.CustomerCreate" %>

<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-2 text-right">
                    <label>Customer Name</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
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
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" ></asp:TextBox>
                </div>


                <div class="col-md-2 text-right">
                    <label>Birth Date</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" ></asp:TextBox>
                </div>

                <div class="col-md-2 text-right">
                    <label>Anniversary Date</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDOAnniversary" runat="server" CssClass="form-control" BorderColor="Silver"  TextMode="Date"></asp:TextBox>
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
                     <asp:CheckBox ID="cbSendEmail"  runat="server" /> 
                </div>

            </div> 
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
