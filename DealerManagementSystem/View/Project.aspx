<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DealerManagementSystem.View.Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <asp:Panel ID="pnlProject" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Create Project</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
            </div>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Project Master</legend>
                    <div class="model-scroll">
                        <asp:Label ID="lblAddProjectMessage" runat="server" Text="" CssClass="message" />
                        <div class="col-md-12">
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Project Name<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Email Date<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtEmailDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp1:CalendarExtender ID="calendarextender1" runat="server" TargetControlID="txtEmailDate" PopupButtonID="txtEmailDate" Format="dd/MM/yyyy" />
                                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEmailDate" WatermarkText="DD/MM/YYYY" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Tender Number</label>
                                <asp:TextBox ID="txtTenderNumber" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">State<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">District<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Value<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtValue" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">L1 Contractor Name<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtL1ContractorName" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Address 1</label>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40" autocomplete="off"></asp:TextBox>
                                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtAddress1" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">L2 Bidder</label>
                                <asp:TextBox ID="txtL2Bidder" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">L3 Bidder</label>
                                <asp:TextBox ID="txtL3Bidder" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Contract Award Date<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtContractAwardDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp1:CalendarExtender ID="calendarextender2" runat="server" TargetControlID="txtContractAwardDate" PopupButtonID="txtContractAwardDate" Format="dd/MM/yyyy" />
                                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtContractAwardDate" WatermarkText="DD/MM/YYYY" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Contract End Date<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtContractEndDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtContractEndDate" PopupButtonID="txtContractEndDate" Format="dd/MM/yyyy" />
                                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtContractEndDate" WatermarkText="DD/MM/YYYY" />
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">Remarks</label>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" />
                                <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnBack_Click" />
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="MPE_Project" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlProject" BackgroundCssClass="modalBackground" />
        <div style="display: none">
            <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
        <div class="col-md-12" id="divProjectView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12">
                <div class="col-md-12">
                    <div class="action-btn">
                        <div class="" id="boxHere"></div>
                        <div class="dropdown btnactions" id="customerAction">
                            <%--<asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />--%>
                            <div class="btn Approval">Actions</div>
                            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                                <asp:LinkButton ID="lbEditProject" runat="server" OnClick="lbActions_Click">Edit Project</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 field-margin-top">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Project Details</legend>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Project Name : </label>
                                    <asp:Label ID="lblProjectName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>State : </label>
                                    <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>L1Contractor Name : </label>
                                    <asp:Label ID="lblL1ContractorName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>L3Bidder : </label>
                                    <asp:Label ID="lblL3Bidder" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Remarks : </label>
                                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Email Date : </label>
                                    <asp:Label ID="lblEmailDate" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>District : </label>
                                    <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Address 1 : </label>
                                    <asp:Label ID="lblAddress1" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>ContractAwardDate : </label>
                                    <asp:Label ID="lblContractAwardDate" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Tender Number : </label>
                                    <asp:Label ID="lblTenderNumber" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Value : </label>
                                    <asp:Label ID="lblValue" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>L2Bidder : </label>
                                    <asp:Label ID="lblL2Bidder" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>ContractEndDate : </label>
                                    <asp:Label ID="lblContractEndDate" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="col-md-12 Report" id="divProjectList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">From Date</label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="calendarextender4" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtFromDate" WatermarkText="DD/MM/YYYY" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">To Date</label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="calendarextender5" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtToDate" WatermarkText="DD/MM/YYYY" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Project Number</label>
                            <asp:TextBox ID="txtProjectNumber" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Project Name</label>
                            <asp:TextBox ID="txtSProjectName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="ddlSState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlSState_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">District</label>
                            <asp:DropDownList ID="ddlSDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                        </div>
                       <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="BtnSearch_Click" />
                            <asp:Button ID="BtnAdd" runat="server" Text="Add Project" CssClass="btn Save" OnClick="BtnAdd_Click" Width="100px" />
                           <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Project(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnPjtArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnPjtArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnPjtArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnPjtArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenProjectID" runat="server" />
                            <asp:GridView ID="gvProject" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="5" runat="server" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvProject_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Project Number" DataField="ProjectNumber"></asp:BoundField>
                                    <asp:BoundField HeaderText="Project Name" DataField="ProjectName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Email Date" DataField="EmailDate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Tender Number" DataField="TenderNumber"></asp:BoundField>
                                    <asp:BoundField HeaderText="State" DataField="State.State"></asp:BoundField>
                                    <asp:BoundField HeaderText="District" DataField="District.District"></asp:BoundField>
                                    <asp:BoundField HeaderText="Value" DataField="Value"></asp:BoundField>
                                    <asp:BoundField HeaderText="L1 Contractor Name" DataField="L1ContractorName"></asp:BoundField>
                                    <asp:BoundField HeaderText="L1 Contractor Address" DataField="L1ContractorAddress"></asp:BoundField>
                                    <asp:BoundField HeaderText="L2 Bidder" DataField="L2Bidder"></asp:BoundField>
                                    <asp:BoundField HeaderText="L3 Bidder" DataField="L3Bidder"></asp:BoundField>
                                    <asp:BoundField HeaderText="Contract Award Date" DataField="ContractAwardDate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Contract End Date" DataField="ContractEndDate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProjectID")%>' CssClass="btn Back" OnClick="BtnView_Click" Width="75px" Height="25px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
