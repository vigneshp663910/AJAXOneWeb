<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnboardEmployeeView.ascx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.UserControls.OnboardEmployeeView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div class="col-md-12">
    <script type="text/javascript">
        function ConfirmReject() {
            var x = confirm("Are you sure you want to Reject Employee?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmApprove() {
            var x = confirm("Are you sure you want to Approve Employee?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmGenerate() {
            var x = confirm("Are you sure you want to Generate User?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbApprove" runat="server" OnClientClick="return ConfirmApprove();" OnClick="lbActions_Click">Approve</asp:LinkButton>
                <asp:LinkButton ID="lbReject" runat="server" OnClientClick="return ConfirmReject();" OnClick="lbActions_Click">Reject</asp:LinkButton>
                <asp:LinkButton ID="lbGenerate" runat="server" OnClientClick="return ConfirmGenerate();" OnClick="lbActions_Click">Generate User</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
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
                <div class="col-md-3 text-right">
                    <label>UserName</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>User Created On</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblUserCreatedOn" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border" id="DivApproverInfo" runat="server" visible="false">
            <legend style="background: none; color: #007bff; font-size: 17px;">Approver Information</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Approved By</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Approved On</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblApprovedOn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Status</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Remarks</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblApproverRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Module Permission</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblModulePermission" runat="server" CssClass="label"></asp:Label>
                </div>
                <br />
                <div class="col-md-3 text-right">
                    <label>Dealer Permission</label>
                </div>
                <div class="col-md-9">
                    <asp:ListView ID="ListViewDealerList" runat="server" DataKeyNames="DealerID">
                        <ItemTemplate>
                            <div class="col-md-3 col-sm-12">
                                <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")+"-"+DataBinder.Eval(Container.DataItem, "DisplayName")%>' runat="server" />
                                <asp:Label ID="lblDID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>

            </div>
        </fieldset>
        <fieldset class="fieldset-border" id="DivApprover" runat="server" visible="false">
            <legend style="background: none; color: #007bff; font-size: 17px;">Permission</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Module Permission<samp style="color: red">*</samp></label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtModulePermission" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
                <div class="col-md-3 text-right">
                    <label>Remarks<samp style="color: red">*</samp></label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="uppercase form-control" AutoComplete="SP" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
                <div class="col-md-3 text-right">
                    <label>Dealer Permission</label>
                </div>
                <br />
                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" /><asp:Label ID="lblSelect" runat="server" Text="Select All Dealer"></asp:Label>
                <br />
                <asp:ListView ID="ListViewDealer" runat="server" DataKeyNames="DealerID">
                    <ItemTemplate>
                        <div class="col-md-3 col-sm-12">
                            <asp:CheckBox ID="chkDealer" runat="server" OnCheckedChanged="chkDealer_CheckedChanged" AutoPostBack="true" />
                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")+"-"+DataBinder.Eval(Container.DataItem, "DisplayName")%>' runat="server" />
                            <asp:Label ID="lblDID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </fieldset>
    </div>
</div>