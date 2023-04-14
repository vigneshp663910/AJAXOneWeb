<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="MachineOperatorRegister.aspx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.MachineOperatorRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
        function validateChar(event) {
            if (event.key == " ")
                return true;
            if (event.key.search(/^[a-zA-Z]+$/) === -1) {
                return false;
            }
            return true;
        }
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                if ((iKeyCode > 95 && iKeyCode < 106)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            return true;
        }
        function isNumberDigit(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode == 110 || iKeyCode == 190) {
                return true;
            }
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                if ((iKeyCode > 95 && iKeyCode < 106)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            return true;
        }
        function AadhaarCardNo(event) {
            var val = document.getElementById("<%=txtAadhaarCardNo.ClientID %>").value;
            val = replaceAll(val, "-", "");
            if (val.length > 8) {
                document.getElementById('<%=txtAadhaarCardNo.ClientID %>').value = val.substring(0, 4) + "-" + val.substring(4, 8) + "-" + val.substring(8, 12);
            }
            else if (val.length > 4) {
                document.getElementById('<%=txtAadhaarCardNo.ClientID %>').value = val.substring(0, 4) + "-" + val.substring(4, 8);
            }
        }
        function replaceAll(str, term, replacement) {
            return str.replace(new RegExp(escapeRegExp(term), 'g'), replacement);
        }
        function escapeRegExp(string) {
            return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
        }
        function PANNo(event) {
            if (event.key == " ")
                return false;
            var val = document.getElementById("<%=txtPANNo.ClientID %>").value;
            var v = 0;
            var sVal;
            var str = "";
            for (v = 0; v < val.length; v++) {
                sVal = val.substring(v, v + 1);
                if (((str + sVal).length < 6) || ((str + sVal).length == 10)) {
                    if (sVal.search(/^[a-zA-Z]+$/) === -1) {
                    }
                    else {
                        str = str + sVal;
                    }
                }
                else if ((str + sVal).length < 10) {
                    if (isNaN(sVal)) {

                    }
                    else {
                        str = str + sVal;
                    }
                }
            }
            document.getElementById("<%=txtPANNo.ClientID %>").value = str;
        }
    </script>
    <style>
        .uppercase {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Machine Operator Input</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <label>Aadhaar Card No</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAadhaarCardNo" runat="server" CssClass="form-control" MaxLength="14" onkeydown="return isNumber(event);" onkeyUp="AadhaarCardNo(event)" OnTextChanged="txtAadhaarCardNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtAadhaarCardNo" WatermarkText="XXXX-XXXX-XXXX"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Adhaar Card Copy Front Side</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuAdhaarCardCopyFrontSide" runat="server" CssClass="form-control custom-file" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Label ID="lblAdhaarCardCopyFrontSideFileName" runat="server" Text=""></asp:Label>
                        <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileRemove" runat="server" OnClick="lbAdhaarCardCopyFrontSideFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileDownload" runat="server" OnClick="lbAdhaarCardCopyFrontSideFileDownload_Click" CssClass="btn lDownload" Visible="false">Download</asp:LinkButton>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Adhaar Card Copy Back Side</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuAdhaarCardCopyBackSide" runat="server" CssClass="form-control custom-file" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Label ID="lblAdhaarCardCopyBackSideFileName" runat="server" Text=""></asp:Label>
                        <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileRemove" runat="server" OnClick="lbAdhaarCardCopyBackSideFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileDownload" runat="server" OnClick="lbAdhaarCardCopyBackSideFileDownload_Click" CssClass="btn lDownload" Visible="false">Download</asp:LinkButton>
                    </div>                    
                    <div class="col-md-3 text-right">
                        <label>Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="uppercase form-control" AutoComplete="Off" onkeydown="return validateChar(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Father Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="uppercase form-control" AutoComplete="SP" onkeydown="return validateChar(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Photo</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuPhoto" runat="server" CssClass="form-control custom-file" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" Style="display: none" />
                        <asp:Label ID="lblPhotoFileName" runat="server" CssClass="label" Text=""></asp:Label>
                        <asp:LinkButton ID="lbPhotoFileRemove" runat="server" OnClick="lbPhotoFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:ImageButton ID="ibtnPhoto" runat="server" OnClick="ibtnPhoto_Click" Width="65px" Height="75px" Visible="false" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DOB</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDOB" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Contact No 1</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" AutoComplete="SP" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Contact No 2</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtContactNumber1" runat="server" CssClass="form-control" AutoComplete="SP" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Email</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Equcational Qualification</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlEqucationalQualification" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Total Experience (Years)</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtTotalExperience" runat="server" CssClass="form-control" onkeydown="return isNumberDigit(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Address</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>State</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>District</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Tehsil</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Village</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtVillage" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Location</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>PANNo</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtPANNo" runat="server" CssClass="uppercase form-control" AutoComplete="SP" MaxLength="10" onkeyUp="return PANNo(event);"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPANNo" WatermarkText="WWWWW DDDD W"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>PAN Card Copy</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuPANCardCopy" runat="server" CssClass="form-control" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Label ID="lblPANCardCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                        <asp:LinkButton ID="lbPANCardCopyFileRemove" runat="server" OnClick="lbPANCardCopyFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:LinkButton ID="lbPANCardCopyFileDownload" runat="server" OnClick="lbPANCardCopyFileDownload_Click" CssClass="btn lDownload" Visible="false">Download</asp:LinkButton>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>BankName</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="uppercase form-control" AutoComplete="SP" onkeydown="return validateChar(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Account No</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" onkeydown="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>IFSC Code</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="uppercase form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Cheque Copy</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuChequeCopy" runat="server" CssClass="form-control" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Label ID="lblChequeCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                        <asp:LinkButton ID="lbChequeCopyFileRemove" runat="server" OnClick="lbChequeCopyFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:LinkButton ID="lbChequeCopyFileDownload" runat="server" OnClick="lbChequeCopyFileDownload_Click" CssClass="btn lDownload" Visible="false">Download</asp:LinkButton>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Emergency Contact</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtEmergencyContactNumber" runat="server" CssClass="form-control" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>BloodGroup</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </fieldset>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Operator For</legend>
                <div class="col-md-3 text-right">
                    <label>Select Operator</label>
                </div>
                <div class="col-md-9">
                    <asp:ListView ID="ListViewProductType" runat="server" DataKeyNames="ProductTypeID">
                        <ItemTemplate>
                            <div class="col-md-3 col-sm-12">
                                <asp:CheckBox ID="chkProductType" runat="server" AutoPostBack="true" OnCheckedChanged="chkProductType_CheckedChanged"/>
                                <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType")%>' runat="server" />
                                <asp:Label ID="lblProductTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ProductTypeID")%>' runat="server" Visible="false" />
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div class="col-md-12" runat="server" id="DivDLInfo" visible="false">
                    <div class="col-md-3 text-right">
                        <label>DL Number</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDLNumber" runat="server" CssClass="uppercase form-control" AutoComplete="off" MaxLength="20"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DL Issue Date</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDLIssueDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDLIssueDate');"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDLIssueDate" PopupButtonID="txtDLIssueDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDLIssueDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DL Issuing Office</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDLIssuingOffice" runat="server" CssClass="uppercase form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DL Expiry Date</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDLExpiryDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDLExpiryDate');"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDLExpiryDate" PopupButtonID="txtDLExpiryDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtDLExpiryDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DL Copy Front Side</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuDLCopyFrontSide" runat="server" CssClass="form-control custom-file" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Label ID="lblDLCopyFrontSideFileName" runat="server" Text=""></asp:Label>
                        <asp:LinkButton ID="lbDLCopyFrontSideFileRemove" runat="server" OnClick="lbDLCopyFrontSideFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:LinkButton ID="lbDLCopyFrontSideFileDownload" runat="server" OnClick="lbDLCopyFrontSideFileDownload_Click" CssClass="btn lDownload" Visible="false">Download</asp:LinkButton>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DL Copy Back Side</label>
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="fuDLCopyBackSide" runat="server" CssClass="form-control custom-file" ViewStateMode="Inherit" onchange="UploadFile(this)" />
                        <asp:Label ID="lblDLCopyBackSideFileName" runat="server" Text=""></asp:Label>
                        <asp:LinkButton ID="lbDLCopyBackSideFileRemove" runat="server" OnClick="lbDLCopyBackSideFileRemove_Click" CssClass="btn lRemove" Visible="false">Remove</asp:LinkButton>
                        <asp:LinkButton ID="lbDLCopyBackSideFileDownload" runat="server" OnClick="lbDLCopyBackSideFileDownload_Click" CssClass="btn lDownload" Visible="false">Download</asp:LinkButton>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>DL For</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDLFor" runat="server" CssClass="uppercase form-control" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton btn Back" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnBack_Click" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
