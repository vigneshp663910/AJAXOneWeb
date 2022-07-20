<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="DealerEmployeeAssigningRole.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.DealerEmployeeAssigningRole" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>
    <script type="text/javascript">
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

        function AadhaarCardNo(event) {

            var val = document.getElementById("<%=txtAadhaarCardNo.ClientID %>").value;
            // val = val.replace("-", "");
            val = replaceAll(val, "-", "");
            //alert(t.value);
            if (val.length > 8) {
                document.getElementById('<%=txtAadhaarCardNo.ClientID %>').value = val.substring(0, 4) + "-" + val.substring(4, 8) + "-" + val.substring(8, 12);
            }
            else if (val.length > 4) {
                document.getElementById('<%=txtAadhaarCardNo.ClientID %>').value = val.substring(0, 4) + "-" + val.substring(4, 8);
            }
        }
        function replaceAll(str, term, replacement) {
            return str.replace(new RegExp(escapeRegExp(term), 'g'), replacement);
        }
        function escapeRegExp(string) {
            return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="pnlManage" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 text-left">
                            <label class="modal-label">Aadhaar Card No</label>
                            <asp:TextBox ID="txtAadhaarCardNo" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="14" onkeydown="return isNumber(event);" onkeyUp="AadhaarCardNo(event)"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtAadhaarCardNo" WatermarkText="XXXX-XXXX-XXXX"></asp:TextBoxWatermarkExtender>
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="InputButton btn Search" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                            <%-- <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" OnClick="btnExportExcel_Click" />--%>
                        </div>
                    </div>
                </fieldset>
            </div>


            <%--<div class="col-md-12 Report">
                <div class="col-md-12">--%>
            <%--<label>Dealer Employee Manage</label><asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />--%>
            <%--</div>--%>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>

                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Dealer Employee Manage:</td>

                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvDealerEmployee" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed Grid" AllowPaging="True" DataKeyNames="DealerEmployeeID" PageSize="20" OnPageIndexChanging="gvDealerEmployee_PageIndexChanging">
                            <columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <itemtemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <itemtemplate>
                                        <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" />
                                    </itemtemplate>
                                    <headerstyle width="162px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Father Name">
                                    <itemtemplate>
                                        <asp:Label ID="lblFatherName" Text='<%# DataBinder.Eval(Container.DataItem, "FatherName")%>' runat="server"></asp:Label>
                                    </itemtemplate>
                                    <headerstyle width="192px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Number">
                                    <itemtemplate>
                                        <asp:Label ID="lblContactNumber" runat="server">
                                            <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNumber")%></a>
                                        </asp:Label>
                                    </itemtemplate>
                                    <headerstyle width="150px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <itemtemplate>
                                        <asp:Label ID="lblEmail" runat="server">
                                            <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "Email")%>'><%# DataBinder.Eval(Container.DataItem, "Email")%></a>
                                        </asp:Label>
                                    </itemtemplate>
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <itemtemplate>
                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "State.State" )%>' runat="server"></asp:Label>
                                    </itemtemplate>
                                    <headerstyle width="75px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aadhaar Card No">
                                    <itemtemplate>
                                        <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "AadhaarCardNo" )%>' runat="server"></asp:Label>
                                    </itemtemplate>
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAN No">
                                    <itemtemplate>
                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "PANNo")%>' runat="server"></asp:Label>
                                    </itemtemplate>
                                    <headerstyle width="76px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add Role">
                                    <itemtemplate>
                                        <asp:LinkButton ID="lbEditRole" runat="server" OnClick="lbEditRole_Click">Click </asp:LinkButton>
                                    </itemtemplate>
                                    <headerstyle width="150px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                </asp:TemplateField>
                            </columns>
                            <alternatingrowstyle backcolor="#ffffff" />
                            <footerstyle forecolor="White" />
                            <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                            <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                        </asp:GridView>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12" id="pnlAssingn" runat="server" visible="false">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Employee View</legend>
            <div class="col-md-12">
                <div class="col-md-3 text-right">
                    <label>Name</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Father Name</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Photo</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbPhotoFileName" runat="server" OnClick="lbPhotoFileName_Click" Visible="false">
                        <asp:Label ID="lblPhotoFileName" runat="server" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>DOB</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDOB" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Contact Number</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblContactNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Email</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Equcational Qualification</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblEqucationalQualification" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Total Experience</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblTotalExperience" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Address</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>State</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblState" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>District</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Tehsil</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblTehsil" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Village</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblVillage" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Location</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Aadhaar Card No</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAadhaarCardNo" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Adhaar Card Copy Front Side</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbAdhaarCardCopyFrontSideFileName" runat="server" OnClick="lbfuAdhaarCardCopyFrontSide_Click" Visible="false">
                        <asp:Label ID="lblAdhaarCardCopyFrontSideFileName" runat="server" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>Adhaar Card Copy Back Side</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbAdhaarCardCopyBackSideFileName" runat="server" OnClick="lbAdhaarCardCopyBackSide_Click" Visible="false">
                        <asp:Label ID="lblAdhaarCardCopyBackSideFileName" runat="server" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>PANNo</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblPANNo" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>PAN Card Copy</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbPANCardCopyFileName" runat="server" OnClick="lbPANCardCopy_Click" Visible="false">
                        <asp:Label ID="lblPANCardCopyFileName" runat="server" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-3 text-right">
                    <label>BankName</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblBankName" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Account No</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>IFSC Code</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblIFSCCode" runat="server"></asp:Label>
                </div>
                <div class="col-md-3 text-right">
                    <label>Cheque Copy</label>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lbChequeCopyFileName" runat="server" OnClick="lbChequeCopy_Click" Visible="false">
                        <asp:Label ID="lblChequeCopyFileName" runat="server" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
            </div>
        </fieldset>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Employee Role</legend>
            <div class="col-md-12 Report">
                <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed Grid">
                    <columns>
                        <asp:TemplateField HeaderText="Employee ID" Visible="false">
                            <itemtemplate>
                                <asp:Label ID="lblDealerEmployeeID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployeeID")%>' runat="server" />
                            </itemtemplate>
                            <headerstyle width="162px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" Visible="false">
                            <itemtemplate>
                                <asp:Label ID="lblName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerEmployee.Name")%>' runat="server" />
                            </itemtemplate>
                            <headerstyle width="162px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Code">
                            <itemtemplate>
                                <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                            </itemtemplate>
                            <headerstyle width="62px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Name">
                            <itemtemplate>
                                <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <headerstyle width="250px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Joining">
                            <itemtemplate>
                                <asp:Label ID="lblFatherName" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfJoining","{0:d}")%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <headerstyle width="192px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Leaving">
                            <itemtemplate>
                                <asp:Label ID="lblDateOfLeaving" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfLeaving","{0:d}")%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <headerstyle width="192px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <itemtemplate>
                                <asp:Label ID="lblContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDepartment.DealerDepartment")%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <headerstyle width="150px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation">
                            <itemtemplate>
                                <asp:Label ID="lblEmail" Text='<%# DataBinder.Eval(Container.DataItem, "DealerDesignation.DealerDesignation")%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reporting To">
                            <itemtemplate>
                                <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ReportingTo.Name" )%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <headerstyle width="75px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SAP Emp Code">
                            <itemtemplate>
                                <asp:Label ID="lblSAPEmpCode" Text='<%# DataBinder.Eval(Container.DataItem, "SAPEmpCode" )%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <headerstyle width="75px" />
                            <itemstyle verticalalign="Middle" horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <itemtemplate>
                                <%--   <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "IsActive" )%>' runat="server"></asp:Label>--%>
                                <asp:Label ID="Label6" Text='<%# DataBinder.Eval(Container.DataItem, "IsActiveString" )%>' runat="server"></asp:Label>
                            </itemtemplate>
                            <itemstyle verticalalign="Middle" horizontalalign="Center" />
                        </asp:TemplateField>
                    </columns>
                    <alternatingrowstyle backcolor="#ffffff" />
                    <footerstyle forecolor="White" />
                    <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                    <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                    <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                </asp:GridView>
            </div>
        </fieldset>
        <div id="Panel1" runat="server">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Employee Role Assigning</legend>
                <div class="col-md-12">
                    <%--  <div class="col-md-3 text-right">
                            <label>Login User Name</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtLoginUserName" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>--%>
                    <div class="col-md-3 text-right">
                        <label>Dealer</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Dealer Office</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Date of Joining</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDateOfJoining" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtDOB');"></asp:TextBox>
                        <asp:CalendarExtender ID="caDateOfJoining" runat="server" TargetControlID="txtDateOfJoining" PopupButtonID="txtDateOfJoining" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDateOfJoining" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>SAP Emp Code</label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtSAPEmpCode" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
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
                        <label>-</label>
                    </div>
                    <div class="col-md-3">
                        <asp:GridView ID="GVAssignDistrict" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed Grid">
                            <columns>
                                <asp:TemplateField HeaderText="District ID" Visible="false">
                                    <itemtemplate>
                                        <asp:Label ID="lblDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" />
                                    </itemtemplate>
                                    <headerstyle width="162px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <itemtemplate>
                                        <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server" />
                                    </itemtemplate>
                                    <headerstyle width="162px" />
                                    <itemstyle verticalalign="Middle" horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <itemtemplate>
                                        <asp:LinkButton ID="LnkDistrict" runat="server" Text="Delete" OnClick="LnkDistrict_Click"></asp:LinkButton>
                                    </itemtemplate>
                                </asp:TemplateField>
                            </columns>
                            <alternatingrowstyle backcolor="#ffffff" />
                            <footerstyle forecolor="White" />
                            <headerstyle font-bold="True" forecolor="White" horizontalalign="Left" />
                            <pagerstyle font-bold="True" forecolor="White" horizontalalign="Center" />
                            <rowstyle backcolor="#fbfcfd" forecolor="Black" horizontalalign="Left" />
                        </asp:GridView>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>District</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                        <asp:HiddenField ID="HiddenDistrictID" runat="server" Visible="false" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnAssignRole" runat="server" Text="Assign Role" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnAssignRole_Click" Width="100px" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="InputButton btn Back" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnBack_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
