<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DealerCreate.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.DealerCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%--<fieldset class="fieldset-border" runat="server">--%>
<%--<legend style="background: none; color: #007bff; font-size: 17px;">Create Dealer</legend>--%>
<asp:UpdatePanel ID="updatepnlDealer" runat="server">
    <ContentTemplate>
        <div class="col-md-12">
            <div class="col-md-4">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer</legend>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Dealer Code</label>
                        <asp:TextBox ID="txtDealerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDealerCode" WatermarkText="Dealer Code" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Dealer Name</label>
                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtContactName" WatermarkText="Contact Name" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label>Dealer Short Name</label>
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDisplayName" WatermarkText="Display Name" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">GSTIN</label>
                        <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtGSTIN" WatermarkText="GSTIN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">PAN</label>
                        <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtPAN" WatermarkText="PAN" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Contact Person</label>
                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="35"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtContactPerson" WatermarkText="Contact Person" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtEmail" WatermarkText="Email" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Phone</label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtMobile" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Dealer Type</label>
                        <asp:DropDownList ID="ddlDealerType" runat="server" CssClass="form-control" DataTextField="DealerType" DataValueField="DealerTypeID" />
                    </div>
                </fieldset>
            </div>
            <div class="col-md-4">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Address</legend>

                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Address 1</label>
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtAddress1" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Address 2</label>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="txtAddress2" WatermarkText="Address 2" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Address 3</label>
                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="txtAddress3" WatermarkText="Address 3" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">PinCode</label>
                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="txtPincode" WatermarkText="Pincode" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">City</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="20"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtCity" WatermarkText="City" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                </fieldset>
            </div>
            <div class="col-md-4">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Bank</legend>

                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Bank</label>
                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender17" runat="server" TargetControlID="txtBank" WatermarkText="Bank" WatermarkCssClass="WatermarkCssClass" />

                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Branch</label>
                        <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender18" runat="server" TargetControlID="txtBranch" WatermarkText="Branch" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">IFSC Code</label>
                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" TargetControlID="txtIFSCCode" WatermarkText="IFSC Code" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Account Number</label>
                        <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender20" runat="server" TargetControlID="txtAccountNo" WatermarkText="Account No" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Office</legend>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">SAP Location Code</label>
                        <asp:TextBox ID="txtSapLocationCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" TargetControlID="txtSapLocationCode" WatermarkText="SAP Location Code" WatermarkCssClass="WatermarkCssClass" />
                    </div>

                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Office Code</label>
                        <asp:TextBox ID="txtOfficeCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtOfficeCode" WatermarkText="Office Code" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Office Name</label>
                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender23" runat="server" TargetControlID="txtOfficeName" WatermarkText="Office Name" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                </fieldset>
            </div>
            <div class="col-md-4">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">E Invoice</legend>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">E Invoice</label>
                        <asp:CheckBox ID="cbEInvoice" runat="server" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Service Paid E Invoice</label>
                        <asp:CheckBox ID="cbServicePaidEInvoice" runat="server" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label>E Invoice Date</label>
                        <asp:TextBox ID="txtEInvoiceDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp1:CalendarExtender ID="cxEInvoiceDate" runat="server" TargetControlID="txtEInvoiceDate" PopupButtonID="txtEInvoiceDate" Format="dd/MM/yyyy" />
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="txtEInvoiceDate" WatermarkText="DD/MM/YYYY" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">API Username</label>
                        <asp:TextBox ID="txtApiUserName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender15" runat="server" TargetControlID="txtApiUserName" WatermarkText="API Username" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">API Password</label>
                        <asp:TextBox ID="txtApiPassword" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender16" runat="server" TargetControlID="txtApiPassword" WatermarkText="API Password" WatermarkCssClass="WatermarkCssClass" />
                    </div>
                </fieldset>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<%--</fieldset>--%>
