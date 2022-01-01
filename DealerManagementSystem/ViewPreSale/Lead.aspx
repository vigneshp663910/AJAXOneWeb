<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Lead.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Lead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
     .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 170px;
            height:50px;
            font:20px;
        } 
.ajax__tab_xp .ajax__tab_header {
    background-position: bottom;
    background-repeat: repeat-x;
    font-family: verdana,tahoma,helvetica;
    font-size: 12px; 
    font-weight: bold;
}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />


    <asp1:TabContainer ID="tbpOrgChart" runat="server"   >
        <asp1:TabPanel ID="tbpnlAjaxOrg" runat="server" HeaderText="Lead List"   ToolTip="Lead List">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpDealerOrg" runat="server" HeaderText="Lead Create">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12">
                        <fieldset class="fieldset-border">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <fieldset class="fieldset-border" id="fldCountry" runat="server">
                                        <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                                        <div class="col-md-12">
                                                  <div class="col-md-3 text-right">
                                                <label>Lead Date</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtLeadDate" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                                <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtLeadDate" PopupButtonID="txtLeadDate" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <label>Category</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="Category" DataValueField="CategoryID" />
                                            </div>
                                            <%-- <div class="col-md-3 text-right">
                        <label>Progress Status</label>
                    </div>
                   <div class="col-md-3">
                        <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" DataTextField="ProgressStatus" DataValueField="ProgressStatusID" />
                    </div>--%>


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


                                            <%--    <div class="col-md-3 text-right">
                        <label>Status</label>
                    </div>
                 <div class="col-md-3">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataTextField="Status" DataValueField="StatusID" />
                    </div>--%>

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
                                                <label>Remarks</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-12">
                                    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                                        <legend style="background: none; color: #007bff; font-size: 17px;">Address</legend>
                                        <div class="col-md-12">
                                            <div class="col-md-3 text-right">
                                                <label>Name</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <label>Person Name</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtContactPersonName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                            </div>



                                            <div class="col-md-3 text-right">
                                                <label>Person Number</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 text-right">
                                                <label>Person Mail</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtPersonMail" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
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
                        </fieldset>
                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tplSalesOrg" runat="server" HeaderText="Lead Assign">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/ProcessFlow/Sales_Org1.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpPartsOrg" runat="server" HeaderText="Parts">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/ProcessFlow/Parts_Org1.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpServiceOrg" runat="server" HeaderText="Service">
            <ContentTemplate>
                <fieldset class="fieldset-border">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/ProcessFlow/Service_Org1.png" />
                </fieldset>
            </ContentTemplate>
        </asp1:TabPanel>

    </asp1:TabContainer>

</asp:Content>
