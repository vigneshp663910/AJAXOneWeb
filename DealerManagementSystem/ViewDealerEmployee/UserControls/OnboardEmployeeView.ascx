<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnboardEmployeeView.ascx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.UserControls.OnboardEmployeeView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<div class="col-md-12">
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Personal Information</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Emp Code</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEmpCode" runat="server" CssClass="label"></asp:Label>
                </div>
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
                    <label>DOB</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDOB" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Contact No 1</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblContactNumber1" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Contact No 2</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblContactNumber2" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Email</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Educational Qualification</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEducationalQualification" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Total Experience</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblTotalExperience" runat="server" CssClass="label"></asp:Label>
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
            <legend style="background: none; color: #007bff; font-size: 17px;">Contact Information</legend>
            <div class="col-md-12">
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
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Role Information</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Dealer</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Dealer Office</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDealerOffice" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Date of Joining</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDOJ" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Department</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDepartment" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Designation</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDesignation" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Reporting To</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblReportingTo" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </fieldset>
        <div class="col-md-12" id="DivApprover" runat="server" visible="false">
            <div class="col-md-3 text-right">
                <label>Module Permission</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtModulePermission" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
            <div class="col-md-3 text-right">
                <label>Dealer Permission</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtDealerPermission" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
            <div class="col-md-3 text-right">
                <label>Approver Remarks</label>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnApprove_Click" />
                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnReject_Click" />
            </div>
        </div>
    </div>
</div>
