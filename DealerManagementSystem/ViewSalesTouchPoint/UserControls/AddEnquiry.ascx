<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEnquiry.ascx.cs" Inherits="DealerManagementSystem.ViewSalesTouchPoint.UserControls.AddEnquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <script type="text/javascript">
        function IsNumbericOnlyCheck(name) {
            var regEx = /^\d+$/;
            if (name.value.match(regEx)) {
                return true;
            }
            else {
                name.value = '';
                return false;
            }
        }
        </script>
        <fieldset class="fieldset-border"> 
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer Name<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Contact Person Name<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtPersonName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Contact Person Mobile<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"  TextMode="Phone" MaxLength="10" onchange="return IsNumbericOnlyCheck(this);"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">EMail</label>
                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Country<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">State<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">District<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Address 1<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40" autocomplete="off"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtAddress" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Address 2</label>
                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40" autocomplete="off"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtAddress2" WatermarkText="Address 2" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Address 3</label>
                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40" autocomplete="off"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtAddress3" WatermarkText="Address 3" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Pincode<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="6" autocomplete="off" onchange="return IsNumbericOnlyCheck(this);"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>