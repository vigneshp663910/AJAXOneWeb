<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerEmployeeView.aspx.cs" Inherits="DealerManagementSystem.MasterScreenView.DealerEmployeeView" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid2" style="height: 100%; width: 100%" class="IC_basicInfo">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Dealership Manpower View</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandCallInformation();">
                                            <img id="imgCallInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <div class="rf-p " id="txnHistory:inputFiltersPanel2">
                                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body2">
                                        <table class="labeltxt fullWidth">
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label104" runat="server" CssClass="label" Text="Name"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblName" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label26" runat="server" CssClass="label" Text="Father Name"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblFatherName" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label10" runat="server" CssClass="label" Text="Photo"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">


                                                            <asp:ImageButton ID="ibtnPhoto" runat="server" OnClick="ibtnPhoto_Click" Width="65px" Height="75px" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label11" runat="server" CssClass="label" Text="DOB"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblDOB" runat="server" CssClass="label"></asp:Label>
                                                        </div>

                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label28" runat="server" CssClass="label" Text="Contact No 1"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblContactNumber" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Contact No 2"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblContactNumber1" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label22" runat="server" CssClass="label" Text="Email"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="auto-style1">
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="lblHMRValue" runat="server" CssClass="label" Text="Equcational Qualification"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblEqucationalQualification" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="auto-style1">
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label23" runat="server" CssClass="label" Text="Total Experience"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblTotalExperience" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Address"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label30" runat="server" CssClass="label" Text="State"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label21" runat="server" CssClass="label" Text="District"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label106" runat="server" CssClass="label" Text="Tehsil"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblTehsil" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-left">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label32" runat="server" CssClass="label" Text="Village"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblVillage" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label36" runat="server" CssClass="label" Text="Location"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-left">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Aadhaar Card No"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblAadhaarCardNo" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label24" runat="server" CssClass="label" Text="Adhaar Card Copy Front Side"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">

                                                            <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileName" runat="server" OnClick="lbfuAdhaarCardCopyFrontSide_Click" Visible="false">
                                                                <asp:Label ID="lblAdhaarCardCopyFrontSideFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label35" runat="server" CssClass="label" Text="Adhaar Card Copy Back Side"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">

                                                            <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileName" runat="server" OnClick="lbAdhaarCardCopyBackSide_Click" Visible="false">
                                                                <asp:Label ID="lblAdhaarCardCopyBackSideFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label16" runat="server" CssClass="label" Text="PANNo"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblPANNo" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label25" runat="server" CssClass="label" Text="PAN Card Copy"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">

                                                            <asp:LinkButton ID="lbPANCardCopyFileName" runat="server" OnClick="lbPANCardCopy_Click" Visible="false">
                                                                <asp:Label ID="lblPANCardCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label27" runat="server" CssClass="label" Text="BankN ame"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblBankName" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label29" runat="server" CssClass="label" Text="Account No"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblAccountNo" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="IFSC Code"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblIFSCCode" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label17" runat="server" CssClass="label" Text="Cheque Copy"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">

                                                            <asp:LinkButton ID="lbChequeCopyFileName" runat="server" OnClick="lbChequeCopy_Click" Visible="false">
                                                                <asp:Label ID="lblChequeCopyFileName" runat="server" CssClass="label" Text=""></asp:Label>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Department"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblDepartment" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label4" runat="server" CssClass="label" Text="Designation"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblDesignation" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Reporting To"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:Label ID="lblReportingTo" runat="server" CssClass="label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table id="txnHistory3:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td><span id="txnHistory3:refreshDataGroup1">
                                <div class="boxHead">
                                    <div class="logheading">
                                        <div style="float: left">
                                            <table>
                                                <tr>
                                                    <td>Employee Role</td>

                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="InputButtonRight-contain">
                                    <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" OnClick="btnExportExcel_Click" />--%>
                                </div>
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID3">
                                    <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False" CssClass="TableGrid"  >
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
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="62px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="250px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Dealer Office">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerOffice.OfficeName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="250px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignation.DealerDesignation")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ReportingTo.Name" )%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="75px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="SAP Emp Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSAPEmpCode" Text='<%# DataBinder.Eval(Container.DataItem, "SAPEmpCode" )%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="75px" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <%--   <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "IsActive" )%>' runat="server"></asp:Label>--%>
                                                    <asp:Label ID="Label6" Text='<%# DataBinder.Eval(Container.DataItem, "IsActiveString" )%>' runat="server"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </span>
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlUpdateOffice" runat="server">
                        <table id="txnHistory3:panelGridid2" style="height: 100%; width: 100%" class="IC_basicInfo">
                            <tr>
                                <td>
                                    <div class="boxHead">
                                        <div class="logheading">Dealership Manpower Update</div>
                                        <div style="float: right; padding-top: 0px">
                                            <a href="javascript:collapseExpandCallInformation();">
                                                <img id="img1" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                        </div>
                                    </div>
                                    <div class="rf-p " id="txnHistory3:inputFiltersPanel2">
                                        <div class="rf-p-b " id="txnHistory3:inputFiltersPanel_body2">
                                            <table class="labeltxt fullWidth">
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Dealer Office"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="TextBox" Width="250px" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label58" runat="server" CssClass="label" Text="Reporting To"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:DropDownList ID="ddlReportingTo" runat="server" CssClass="TextBox" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                            <div class="tbl-row-right">
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="SAP Emp Code"></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:TextBox ID="txtSAPEmpCode" runat="server" CssClass="input" AutoComplete="SP"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSave_Click" />

                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
