<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="CreateAjaxEmployee.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.CreateAjaxEmployee" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
       
    </script>
    <style>
        .uppercase {
            text-transform: uppercase;
        }
    </style>     
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Details Input</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <label>Aadhaar Card No</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAadhaarCardNo" runat="server" CssClass="form-control" MaxLength="14" onkeydown="return isNumber(event);" onkeyUp="AadhaarCardNo(event)" OnTextChanged="txtAadhaarCardNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtAadhaarCardNo" WatermarkText="XXXX-XXXX-XXXX"></asp:TextBoxWatermarkExtender>
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
                    <legend style="background: none; color: #007bff; font-size: 17px;">Employee Role Assigning</legend>
                    <div class="col-md-12">
                      
                        <div class="col-md-3 text-right">
                            <label>Dealer Office</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-3 text-right">
                            <label>Date of Joining</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                            <asp:CalendarExtender ID="caDateOfJoining" runat="server" TargetControlID="txtDateOfJoining" PopupButtonID="txtDateOfJoining" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateOfJoining" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        
                        <div class="col-md-3 text-right">
                            <label>Department</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-3 text-right">
                            <label>Designation</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-3 text-right">
                            <label>Reporting To</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlReportingTo" runat="server" CssClass="form-control" />
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