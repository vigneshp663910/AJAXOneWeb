<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxEmployeeCreate.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.AjaxEmployeeCreate" %>

<fieldset class="fieldset-border" id="pnlRole" runat="server">
    <legend style="background: none; color: #007bff; font-size: 17px;">Employee Role Assigning</legend>
    <div class="col-md-12">
         <div class="col-md-3 text-right">
            <label>Name</label>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblName" runat="server" ></asp:Label> 
             <asp:Label ID="lblEID" runat="server"  Visible="false"></asp:Label>  
             <asp:Label ID="lblDealerEmployeeRoleID" runat="server" Visible="false"></asp:Label>  
        </div>
        
         <div class="col-md-3 text-right">
            <label>SAP Emp Code</label>
        </div>
         <div class="col-md-3">
              <asp:Label ID="lblSAPEmpCode" runat="server" Text="Label"></asp:Label>  
        </div>
          <div class="col-md-3 text-right">
            <label>User Name</label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
        </div>
       

        <div class="col-md-3 text-right">
            <label>Dealer Office</label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
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
          <div class="col-md-12 text-center">
                           <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" /> 
                    </div>
    </div>

</fieldset>


