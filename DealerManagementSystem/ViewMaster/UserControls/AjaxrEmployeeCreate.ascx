<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxrEmployeeCreate.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.AjaxrEmployeeCreate" %>

<fieldset class="fieldset-border" id="pnlRole" runat="server" visible="false">
    <legend style="background: none; color: #007bff; font-size: 17px;">Employee Role Assigning</legend>
    <div class="col-md-12">
        <div class="col-md-3 text-right">
            <label>Dealer Office</label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
        </div>
        <%-- <div class="col-md-3 text-right">
                            <label>Date of Joining</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                            <asp:CalendarExtender ID="caDateOfJoining" runat="server" TargetControlID="txtDateOfJoining" PopupButtonID="txtDateOfJoining" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateOfJoining" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-3 text-right">
                            <label>SAP Emp Code</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtSAPEmpCode" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                        </div>--%>
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
