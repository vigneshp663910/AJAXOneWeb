<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MachineOperatorView.ascx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.UserControls.MachineOperatorView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<div class="col-md-12">
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Operator Details</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Name</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblName" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Father Name</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblFatherName" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Photo</label>
                </div>
                <div class="col-md-3">
                    <asp:ImageButton ID="ibtnPhoto" runat="server" OnClick="ibtnPhoto_Click" Width="65px" Height="75px" />
                </div>
                <div class="col-md-3 text-right">
                    <label>DOB</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDOB" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Contact No 1</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblContactNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Contact No 2</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblContactNumber1" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Email</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Equcational Qualification</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEqucationalQualification" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Total Experience</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblTotalExperience" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Address</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>State</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>District</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Tehsil</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblTehsil" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Village</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblVillage" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Location</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Aadhaar Card No</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAadhaarCardNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Adhaar Card Copy Front Side</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileName" runat="server" OnClick="lbfuAdhaarCardCopyFrontSide_Click" Visible="false">
                        <asp:Label ID="lblAdhaarCardCopyFrontSideFileName" runat="server" CssClass="label" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>Adhaar Card Copy Back Side</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileName" runat="server" OnClick="lbAdhaarCardCopyBackSide_Click" Visible="false">
                        <asp:Label ID="lblAdhaarCardCopyBackSideFileName" runat="server" CssClass="label" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>PANNo</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblPANNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>PAN Card Copy</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbPANCardCopyFileName" runat="server" OnClick="lbPANCardCopy_Click" Visible="false">
                        <asp:Label ID="lblPANCardCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>BankName</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblBankName" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Account No</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAccountNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>IFSC Code</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblIFSCCode" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Cheque Copy</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbChequeCopyFileName" runat="server" OnClick="lbChequeCopy_Click" Visible="false">
                        <asp:Label ID="lblChequeCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>Emergency Contact</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEmergencyContact" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>BloodGroup</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblBloodGroup" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Operator For</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Operator Divisions</label>
                </div>
                <div class="col-md-3">
                    <asp:ListBox ID="lbProductTypes" runat="server" CssClass="TextBox form-control" Enabled="false"></asp:ListBox>
                </div>

            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">DL Info</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>DL Info</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDLInfo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL Number</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDLNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL Issue Date</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDLIssueDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL Issuing Office</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDLIssueingOffice" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL Expiry Date</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDLExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL Copy Front Side</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbDLCopyFrontSide" runat="server" OnClick="lbDLCopyFrontSide_Click" Visible="false">
                        <asp:Label ID="lblDLCopyFrontSide" runat="server" CssClass="label" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL Copy Back Side</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbDLCopyBackSide" runat="server" OnClick="lbDLCopyBackSide_Click" Visible="false">
                        <asp:Label ID="lblDLCopyBackSide" runat="server" CssClass="label" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>DL For</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDLFor" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </fieldset>
    </div>
</div>
