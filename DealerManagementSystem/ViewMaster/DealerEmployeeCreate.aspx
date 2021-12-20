<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerEmployeeCreate.aspx.cs" Inherits="DealerManagementSystem.MasterScreenView.DealerEmployeeCreate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                //alert("Only characters");
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
            // val = val.replace("-", "");
            val = replaceAll(val, "-", "");
            //alert(t.value);
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

            // alert(sVal);
        }


        document.getElementById("<%=txtPANNo.ClientID %>").value = str;
        }
    </script>
    <style>
        .uppercase {
            text-transform: uppercase;
        }
    </style>

    <div class="container">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid2" style="height: 100%; width: 100%" class="IC_basicInfo">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Dealership Manpower Registration</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandCallInformation();">
                                            <img id="imgCallInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlCallInformation" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel2">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body2">
                                            <table class="labeltxt fullWidth">
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-left">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Aadhaar Card No"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtAadhaarCardNo" runat="server" CssClass="TextBox" BorderColor="Silver" MaxLength="14" onkeydown="return isNumber(event);" onkeyUp="AadhaarCardNo(event)" OnTextChanged="txtAadhaarCardNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtAadhaarCardNo" WatermarkText="XXXX-XXXX-XXXX"></asp:TextBoxWatermarkExtender>

                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label24" runat="server" CssClass="label" Text="Adhaar Card Copy Front Side"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:FileUpload ID="fuAdhaarCardCopyFrontSide" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                                                <asp:Label ID="lblAdhaarCardCopyFrontSideFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                                <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileRemove" runat="server" OnClick="lbAdhaarCardCopyFrontSideFileRemove_Click" Visible="false">Remove</asp:LinkButton>
                                                                <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileDownload" runat="server" OnClick="lbAdhaarCardCopyFrontSideFileDownload_Click" Visible="false">Download</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label35" runat="server" CssClass="label" Text="Adhaar Card Copy Back Side"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:FileUpload ID="fuAdhaarCardCopyBackSide" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                                                <asp:Label ID="lblAdhaarCardCopyBackSideFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                                <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileRemove" runat="server" OnClick="lbAdhaarCardCopyBackSideFileRemove_Click" Visible="false">Remove</asp:LinkButton>
                                                                <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileDownload" runat="server" OnClick="lbAdhaarCardCopyBackSideFileDownload_Click" Visible="false">Download</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label104" runat="server" CssClass="label" Text="Name"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtName" runat="server" CssClass="uppercase TextBox" AutoComplete="Off" onkeydown="return validateChar(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>

                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label26" runat="server" CssClass="label" Text="Father Name"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="uppercase TextBox" AutoComplete="SP" onkeydown="return validateChar(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="Photo"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:FileUpload ID="fuPhoto" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                                                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" Style="display: none" />
                                                                <asp:Label ID="lblPhotoFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                                <asp:LinkButton ID="lbPhotoFileRemove" runat="server" OnClick="lbPhotoFileRemove_Click" Visible="false">Remove</asp:LinkButton>
                                                                <asp:ImageButton ID="ibtnPhoto" runat="server" OnClick="ibtnPhoto_Click" Width="65px" Height="75px" Visible="false" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label11" runat="server" CssClass="label" Text="DOB"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDOB" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                            </div>

                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label28" runat="server" CssClass="label" Text="Contact No 1 "></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtContactNumber" runat="server" CssClass="input" AutoComplete="SP" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Contact No 2"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtContactNumber1" runat="server" CssClass="input" AutoComplete="SP" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label22" runat="server" CssClass="label" Text="Email"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="input" AutoComplete="SP"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="auto-style1">
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="lblHMRValue" runat="server" CssClass="label" Text="Equcational Qualification"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlEqucationalQualification" runat="server" CssClass="TextBox" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td class="auto-style1">
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label23" runat="server" CssClass="label" Text="Total Experience (Years)"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtTotalExperience" runat="server" CssClass="TextBox" onkeydown="return isNumberDigit(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label12" runat="server" CssClass="label" Text="Address"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="uppercase TextBox" AutoComplete="SP" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label30" runat="server" CssClass="label" Text="State"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label21" runat="server" CssClass="label" Text="District"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label106" runat="server" CssClass="label" Text="Tehsil"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="TextBox" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-left">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label32" runat="server" CssClass="label" Text="Village"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtVillage" runat="server" CssClass="TextBox" AutoComplete="SP"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label36" runat="server" CssClass="label" Text="Location"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtLocation" runat="server" CssClass="input" AutoComplete="SP"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label16" runat="server" CssClass="label" Text="PANNo"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtPANNo" runat="server" CssClass="uppercase TextBox" AutoComplete="SP" MaxLength="10" onkeyUp="return PANNo(event);"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPANNo" WatermarkText="WWWWW DDDD W"></asp:TextBoxWatermarkExtender>

                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label25" runat="server" CssClass="label" Text="PAN Card Copy"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:FileUpload ID="fuPANCardCopy" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                                                <asp:Label ID="lblPANCardCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                                <asp:LinkButton ID="lbPANCardCopyFileRemove" runat="server" OnClick="lbPANCardCopyFileRemove_Click" Visible="false">Remove</asp:LinkButton>
                                                                <asp:LinkButton ID="lbPANCardCopyFileDownload" runat="server" OnClick="lbPANCardCopyFileDownload_Click" Visible="false">Download</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label27" runat="server" CssClass="label" Text="BankN ame"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="uppercase TextBox" AutoComplete="SP" onkeydown="return validateChar(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label29" runat="server" CssClass="label" Text="Account No"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" onkeydown="return isNumber(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label17" runat="server" CssClass="label" Text="Cheque Copy"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:FileUpload ID="fuChequeCopy" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" onchange="UploadFile(this)" />
                                                                <asp:Label ID="lblChequeCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                                <asp:LinkButton ID="lbChequeCopyFileRemove" runat="server" OnClick="lbChequeCopyFileRemove_Click" Visible="false">Remove</asp:LinkButton>
                                                                <asp:LinkButton ID="lbChequeCopyFileDownload" runat="server" OnClick="lbChequeCopyFileDownload_Click" Visible="false">Download</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="IFSC Code"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="uppercase TextBox"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Emergency Contact"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtEmergencyContactNumber" runat="server" CssClass="TextBox"  MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="BloodGroup"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="TextBox" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnBack_Click" Visible="false" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
