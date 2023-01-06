<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreate.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<style> 
    .WatermarkCssClass {
        color: #aaa;
    }
</style>


<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>

        <div id="divCustomerViewID" style="display: none">
            <fieldset class="fieldset-border">
                <div class="col-md-12 customerAuto-View">

                    <div class="col-md-2 text-right col-6">
                        <label>Customer Name : </label>
                    </div>
                    <div class="col-md-10 col-6">
                        <label id="lblCustomerName"></label>

                    </div>
                    <div class="col-md-2 text-right col-6">
                        <label>Contact Person : </label>
                    </div>
                    <div class="col-md-10 col-6">
                        <label id="lblContactPerson"></label>

                    </div>
                    <div class="col-md-2 text-right col-6">
                        <label>Mobile : </label>
                    </div>
                    <div class="col-md-10 col-6">
                        <label id="lblMobile"></label>
                    </div>
                </div>
                <div id="divChangeCustomer"> 
                    <asp:Button ID="BtnChangeCustomer" runat="server" CssClass="btn Search" Text="Change Customer" OnClick="BtnChangeCustomer_Click"></asp:Button>
                </div>
                
            </fieldset>
        </div>
        <div id="divCustomerCreateID">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">

                    <div class="col-md-4 col-sm-12">
                        <label class="modal-label">Is Draft</label>
                        <asp:CheckBox ID="cbIsDraft" runat="server" />
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <label class="modal-label">
                            Title
                        <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">
                            Customer Name ( Search by customer Code(6 char.)/Name(min 4 Char.)/Mobile(10 digits))
                        <samp style="color: red">*</samp></label>
                        <asp:HiddenField ID="hdfCustomerID" runat="server" />
                        <asp:HiddenField ID="hdfCustomerName" runat="server" />
                        <asp:HiddenField ID="hdfContactPerson" runat="server" />
                        <asp:HiddenField ID="hdfMobile" runat="server" />
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" MaxLength="80" BorderColor="Silver"
                             onKeyUp="GetCustomerAuto()"></asp:TextBox>

                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCustomerName" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
                    </div>

                    <div class="col-md-6 col-sm-12" style="display: none">
                        <label class="modal-label">Customer Name2</label>
                        <asp:TextBox ID="txtCustomerName2" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtCustomerName2" WatermarkText="Customer Name2" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            GSTIN
                     <%--   <samp style="color: red">*</samp>--%></label>
                        <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtGSTIN" WatermarkText="GSTIN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            PAN
                        <%--<samp style="color: red">*</samp>--%></label>
                        <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtPAN" WatermarkText="PAN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            Contact Person
                        <samp style="color: red">*</samp>
                        </label>
                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="35"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtContactPerson" WatermarkText="Contact Person" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            Mobile
                        <samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtMobile" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            Alternative Mobile
                        <%--<samp style="color: red">*</samp>--%></label>
                        <asp:TextBox ID="txtAlternativeMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtAlternativeMobile" WatermarkText="Alternative Mobile" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            Email
                        <%--<samp style="color: red">*</samp>--%></label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Email" MaxLength="40"></asp:TextBox>
                        <%-- <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID ="txtEmail" WatermarkText="Email"  />--%>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            Address 1
                        <%--<samp style="color: red">*</samp>--%></label>
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
                    <div class="col-md-6 col-sm-12" id="divDealer" runat="server">
                        <label>Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            Country
                        <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            State
                        <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            District
                        <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Tehsil</label>
                        <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" DataTextField="Tehsil" DataValueField="TehsilID" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">
                            PinCode
                        <%--<samp style="color: red">*</samp>--%></label>
                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtPincode" WatermarkText="Pincode" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">City</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtCity" WatermarkText="City" WatermarkCssClass="WatermarkCssClass" />
                    </div>

                </div>
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Birth Date</label>
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                        <asp1:CalendarExtender ID="cxDOB" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="txtDOB" WatermarkText="DD/MM/YYYY" />
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Anniversary Date</label>

                        <asp:TextBox ID="txtDOAnniversary" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
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
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Latitude</label>
                        <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Longitude</label>
                        <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>

                    </div>
                </div>
            </fieldset>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">

    function FormatAutocompleteList(item) {

        var inner_html = '<a class="customer">';
        inner_html += '<p class="customer-name-info"><label>' + item.value + '</label></p>';
        inner_html += '<div class=customer-info><label class="contact-number">Contact :' + item.ContactPerson + '(' + item.Mobile + ') </label>';
        inner_html += '<label class="customer-type">' + item.CustomerType + '</label></div>';
        inner_html += '<p class="customer-address"><label>' + item.Address + '</label></p>';
        inner_html += '</a>';
        return inner_html;
    }
</script>

