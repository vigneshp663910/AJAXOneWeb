<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerEmployeeView.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.DealerEmployeeView" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
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
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Employee View</legend>
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed Grid">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeID")%>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="162px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployee.Name")%>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="162px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="62px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="250px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="250px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Of Joining">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFatherName" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfJoining")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="192px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Of Leaving">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateOfLeaving" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfLeaving")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="192px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDepartment.DealerDepartment")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignation.DealerDesignation")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporting To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ReportingTo.Name" )%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="75px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SAP Emp Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSAPEmpCode" Text='<%# DataBinder.Eval(Container.DataItem, "SAPEmpCode" )%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="75px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Login User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLoginUserName" Text='<%# DataBinder.Eval(Container.DataItem, "LoginUserName" )%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="75px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" Text='<%# DataBinder.Eval(Container.DataItem, "IsActiveString" )%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>
        </div>

        <div class="col-md-12 text-center">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton btn Back" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnBack_Click" />
        </div>
        <div class="col-md-12" id="pnlUpdateOffice" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Dealership Manpower Update</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <label>Dealer Office</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" Width="250px" />
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

                    <div class="col-md-3 text-right">
                        <label>SAP Emp Code</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtSAPEmpCode" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                    </div>
                    <div class="col-md-3 text-right">
                        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClick="btnSave_Click"   OnClientClick="return Confirm('Are you sure you want to Save');"/>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
    function Confirm(Message) {
        var x = confirm(Message);
        if (x) {
            return true;
        }
        else
            return false;
    } 
    </script>
</asp:Content>
