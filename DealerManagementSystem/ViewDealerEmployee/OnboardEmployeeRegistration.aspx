<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnboardEmployeeRegistration.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DealerManagementSystem.ViewDealerEmployee.OnboardEmployeeRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Onboard Employee</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
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
    </script>
    <style>
        .uppercase {
            text-transform: uppercase;
        }

        .jumbotron {
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .panel {
            /*background: #424242cc;*/
            background: #fff;
            border-radius: 10px;
            padding: 35px;
        }

        fieldset {
            /*color: white;*/
            color: black;
        }

        legend {
            /*color: white;*/
            color: black;
        }

        .btn-success {
            padding: 5px 20px;
            font-size: 18px;
            border : 5px solid green;
            border-radius : 10px;
            background-color : green;
        }
        /*.fieldset-border {
            border: 1px solid #007bff;
            border-radius: 30px;
        }

        legend {
            border-bottom: 1px solid #007bff;
        }*/
    </style>
</head>
<body>
    <div class="jumbotron text-center">
        <h2>User Account Registration</h2>
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div>
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <fieldset>
                                <legend>Personal Information</legend>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Emp Code<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control" MaxLength="14" OnTextChanged="txtEmpCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Name<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="uppercase form-control" AutoComplete="Off" onkeydown="return validateChar(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Father Name</label>
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="uppercase form-control" AutoComplete="SP" onkeydown="return validateChar(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>DOB<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDOB" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Contact No 1<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" AutoComplete="SP" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Contact No 2</label>
                                            <asp:TextBox ID="txtContactNumber1" runat="server" CssClass="form-control" AutoComplete="SP" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Email<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Educational Qualification</label>
                                            <asp:DropDownList ID="ddlEducationalQualification" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total Experience (Years)</label>
                                            <asp:TextBox ID="txtTotalExperience" runat="server" CssClass="form-control" onkeydown="return isNumberDigit(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Emergency Contact</label>
                                            <asp:TextBox ID="txtEmergencyContactNumber" runat="server" CssClass="form-control" MaxLength="10" onkeydown="return isNumber(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>BloodGroup</label>
                                            <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Contact Information</legend>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Address<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>State<samp style="color: red">*</samp></label>
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>District<samp style="color: red">*</samp></label>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Tehsil</label>
                                                    <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Village</label>
                                                    <asp:TextBox ID="txtVillage" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Location</label>
                                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Role Information</legend>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dealer</label>
                                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" Enabled="false">
                                                <asp:ListItem Value="53" Selected="True">Ajax</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dealer Office</label>
                                            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" Enabled="false">
                                                <asp:ListItem Value="143" Selected="True">BANGALURU 2000</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date of Joining<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                                            <asp:CalendarExtender ID="caDateOfJoining" runat="server" TargetControlID="txtDateOfJoining" PopupButtonID="txtDateOfJoining" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateOfJoining" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Department<samp style="color: red">*</samp></label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Designation<samp style="color: red">*</samp></label>
                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Reporting To<samp style="color: red">*</samp></label>
                                            <asp:DropDownList ID="ddlReportingTo" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Permission Information</legend>
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Module Permission<samp style="color: red">*</samp></label>
                                            <asp:TextBox ID="txtModulePermission" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Dealer Permission<samp style="color: red">*</samp></label>
                                            <br />
                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" /><asp:Label ID="lblSelect" runat="server" Text="Select All Dealer"></asp:Label>
                                            <br />
                                            <asp:ListView ID="ListViewDealer" runat="server" DataKeyNames="DealerID">
                                                <ItemTemplate>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkDealer" runat="server" OnCheckedChanged="chkDealer_CheckedChanged" AutoPostBack="true" />
                                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")+"-"+DataBinder.Eval(Container.DataItem, "DisplayName")%>' runat="server" />
                                                        <asp:Label ID="lblDID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-success" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>