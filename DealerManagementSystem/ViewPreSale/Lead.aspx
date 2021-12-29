<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Lead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="fldCountry" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Controls</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <label>Category</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="Category" DataValueField="CategoryID" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Progress Status</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" DataTextField="ProgressStatus" DataValueField="ProgressStatusID" />
                    </div>


                    <div class="col-md-3 text-right">
                        <label>Qualification</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control" DataTextField="Qualification" DataValueField="QualificationID" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Source</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" DataTextField="Source" DataValueField="SourceID" />
                    </div>


                    <div class="col-md-3 text-right">
                        <label>Status</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataTextField="Status" DataValueField="StatusID" />
                    </div>
                    <div class="col-md-3 text-right">
                    </div>
                    <div class="col-md-3">
                    </div>

                    <div class="col-md-3 text-right">
                        <label>Contact Person</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>



                    <div class="col-md-3 text-right">
                        <label>Contact Number</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Customer Code</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>



                    <div class="col-md-3 text-right">
                        <label>Lead Type</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlLeadType" runat="server" CssClass="form-control" DataTextField="Status" DataValueField="StatusID" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>


                     <div class="col-md-3 text-right">
                        <label>Address 1</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Address 2</label>
                    </div>
                    <div class="col-md-3">
                         <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>


                     <div class="col-md-3 text-right">
                        <label>Country</label>
                    </div>
                    <div class="col-md-3">
                       <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>State</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                    </div>


                    
                     <div class="col-md-3 text-right">
                        <label>District</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Tehsil</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlTehsil" runat="server" CssClass="form-control" DataTextField="Tehsil" DataValueField="TehsilID" />
                    </div>


                </div>
            </fieldset>
        </div>
          <div class="col-md-12 text-center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
        </div>
    </div>
     
</asp:Content>
