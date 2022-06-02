<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketView.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddTechnician.ascx" TagPrefix="UC" TagName="UC_ICTicketAddTechnician" %>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnAddTechnician" runat="server" OnClick="lbActions_Click">Add Technician</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditCallInformation" runat="server" OnClick="lbActions_Click">Edit Call Information</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditFSR" runat="server" OnClick="lbActions_Click">Edit FSR</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFSRAttachments" runat="server" OnClick="lbActions_Click">Add FSR Attachments</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddOtherMachine" runat="server" OnClick="lbActions_Click">Add Other Machine</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddServiceCharges" runat="server" OnClick="lbActions_Click">Add Service Charges</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddTSIR" runat="server" OnClick="lbActions_Click">Add TSIR</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddMaterialCharges" runat="server" OnClick="lbActions_Click">Add Material Charges</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddNotes" runat="server" OnClick="lbActions_Click">Add Notes</asp:LinkButton>
                <asp:LinkButton ID="lbtAddTechnicianWork" runat="server" OnClick="lbActions_Click">Add Technician Work</asp:LinkButton>
                <asp:LinkButton ID="lbtnRestore" runat="server" OnClick="lbActions_Click">Restore</asp:LinkButton> 
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>IC Ticket : </label>
                <asp:Label ID="lblICTicket" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Requested Date : </label>
                <asp:Label ID="lblRequestedDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>District : </label>
                <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Complaint Description : </label>
                <asp:Label ID="lblComplaintDescription" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Information : </label>
                <asp:Label ID="lblInformation" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Dealer : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer Category : </label>
                <asp:Label ID="lblCustomerCategory" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Contact Person Name & No : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Old IC Ticket Number : </label>
                <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Warranty : </label>
                <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>Is Margin Warranty : </label>
                <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>Equipment : </label>
                <asp:Label ID="lblEquipment" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Model : </label>
                <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Warranty Expiry : </label>
                <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Last HMR Date & Value : </label>
                <asp:Label ID="lblLastHMRValue" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Refurbished Expiry : </label>
                <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>AMC Expiry : </label>
                <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>: </label>

            </div>
            <div class="col-md-4">
                <label>: </label>

            </div>
            <div class="col-md-4">
                <label>: </label>

            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlTechnician" runat="server" HeaderText="Technician" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvTechnician" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Technician">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Technician Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTechnicianRemove" runat="server" OnClick="lbTechnicianDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlCallInformation" runat="server" HeaderText="Call Information">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
                <div class="col-md-12 View">
                    <div class="col-md-4">
                        <label>Departure Date and Time : </label>
                        <asp:Label ID="lblDepartureDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Reached Date and Time : </label>
                        <asp:Label ID="lblReachedDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Location : </label>
                        <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Service Type : </label>
                        <asp:Label ID="lblServiceType" runat="server" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlServiceTypeOverhaul" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceTypeOverhaul" DataValueField="ServiceTypeOverhaulID" />
                        <asp:DropDownList ID="ddlServiceSubType" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceSubType" DataValueField="ServiceSubTypeID" />

                    </div>
                    <div class="col-md-4">
                        <label>Service Priority : </label>
                        <asp:Label ID="lblServicePriority" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Delivery Location : </label>
                        <asp:Label ID="lblDealerOffice" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Current HMR Value : </label>
                        <asp:Label ID="lblHMRValue" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Current HMR Date : </label>
                        <asp:Label ID="lblHMRDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Cess : </label>
                        <asp:CheckBox ID="cbCess" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <label>Type Of Warranty : </label>
                        <asp:Label ID="lblTypeOfWarranty" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Main Application : </label>
                        <asp:Label ID="lblMainApplication" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Sub Application Manual : </label>
                        <asp:Label ID="lblSubApplication" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Site Contact Person’s Name : </label>
                        <asp:Label ID="lblOperatorName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Site Contact Person’s Number : </label>
                        <asp:Label ID="lblSiteContactPersonNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Site Contact Person’s Number 2 : </label>
                        <asp:Label ID="lblSiteContactPersonNumber2" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Designation : </label>
                        <asp:Label ID="lblDesignation" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Scope of Work : </label>
                        <asp:Label ID="lblScopeOfWork" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>No Claim : </label>
                        <asp:CheckBox ID="cbNoClaim" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <label>No Claim Reason : </label>
                        <asp:Label ID="lblNoClaimReason" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Mc Entered Service Date : </label>
                        <asp:Label ID="lblMcEnteredServiceDate" runat="server" CssClass="label"></asp:Label>

                    </div>
                    <div class="col-md-4">
                        <label>Service Started Date : </label>
                        <asp:Label ID="lblServiceStartedDate" runat="server" CssClass="label"></asp:Label>

                    </div>
                    <div class="col-md-4">
                        <label>Service Ended Date : </label>
                        <asp:Label ID="lblServiceEndedDate" runat="server" CssClass="label" Text=""></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Kind Attn : </label>
                        <asp:Label ID="lblKindAttn" runat="server" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txt" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>Remarks : </label>
                        <asp:Label ID="lblRemarks" runat="server" CssClass="label" Text=""></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Is Machine Active: </label>
                        <asp:CheckBox ID="cbIsMachineActive" runat="server" Checked="true" />
                    </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFSR" runat="server" HeaderText="FSR">
        <ContentTemplate>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
                <div class="col-md-12 View">
                    <div class="col-md-4">
                        <label>Mode Of Payment : </label>
                        <asp:Label ID="lblModeOfPayment" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Operator Name : </label>
                        <asp:Label ID="lblOperatorNameFSR" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Operator Contact No : </label>
                        <asp:Label ID="lblOperatorNumber" runat="server" CssClass="label"></asp:Label>
                        >
                    </div>
                    <div class="col-md-4">
                        <label>Machine Maintenance Level : </label>
                        <asp:Label ID="lblMachineMaintenanceLevel" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>SIs Rental : </label>
                        <asp:CheckBox ID="cbIsRental" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <label>Rental Contractor Name : </label>
                        <asp:Label ID="lblRentalName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Rental Contractor Contact No : </label>
                        <asp:Label ID="lblRentalNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Nature Of Complaint : </label>
                        <asp:Label ID="lblNatureOfComplaint" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Observation : </label>
                        <asp:Label ID="lblObservation" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>Work Carried Out : </label>
                        <asp:Label ID="lblWorkCarriedOut" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <label>SE Suggestion : </label>
                        <asp:Label ID="lblReport" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </fieldset>

                <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%"   DataKeyNames="AttachedFileID" OnRowDataBound="gvAttachedFile_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Attachment Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFSRAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FSRAttachedName.FSRAttachedName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lbFSRAttachedFileRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlAvailabilityOfOtherMachine" runat="server" HeaderText="Other Machine">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvAvailabilityOfOtherMachine" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%"  DataKeyNames="AvailabilityOfOtherMachineID" >
                        <Columns>
                            <asp:TemplateField HeaderText="Type Of Machine">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfMachine" Text='<%# DataBinder.Eval(Container.DataItem, "TypeOfMachine.TypeOfMachine")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Make.Make")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbAvailabilityOfOtherMachineRemove" runat="server" OnClick="lbAvailabilityOfOtherMachineRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    
    <asp1:TabPanel ID="tpnlServiceCharges" runat="server" HeaderText="Service Charges">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvServiceCharges" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%"   DataKeyNames="ServiceChargeID">
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbClaimRequested" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested_N")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ser Prod ID">

                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ser Prod Desc">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Date","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked Hours">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base Price">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td style="border-width: 0px">Discount</td>
                                            <td style="border-width: 0px">
                                                <asp:TextBox ID="txtTaxP" runat="server" CssClass="TextBox" Width="30px"></asp:TextBox>%</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quotation Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pro. Inv. Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProformaInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ProformaInvoiceNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inv. Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim / Invoice Requested" Visible="false">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbIsClaimRequested" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested")%>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tsir Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblServiceRemove" runat="server" OnClick="lblServiceRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlTSIR" runat="server" HeaderText="TSIR">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvTSIR" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="TsirID" OnRowDataBound="gvTSIR_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="javascript:collapseExpand('TsirID-<%# Eval("TsirID") %>');">
                                        <img id="imageTsirID-<%# Eval("TsirID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbCheck" runat="server" OnCheckedChanged="cbCheck_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="TSIR">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TsirNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tsir Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "TsirDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tsir Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "Status.StatusID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SRO Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceCharge.Material.MaterialCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SRO Code Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceCharge.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblCancelTSIR" runat="server" OnClick="lblCancelTSIR_Click">Cancel</asp:LinkButton>
                                    <tr>
                                        <td colspan="100%" style="padding-left: 96px">
                                            <div id="TsirID-<%# Eval("TsirID") %>" style="display: none; position: relative;">
                                                <table>
                                                    <tr>
                                                        <td colspan="100%" style="border-bottom-width: 0px; border-right-width: 0px;">
                                                            <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="AttachedFileID" ShowFooter="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Note Type">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFSRAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FSRAttachedName.FSRAttachedName")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                                            <asp:UpdatePanel ID="upManage" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownloadR_Click" Text="Download">
                                                                                    </asp:LinkButton>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lblAttachedFileRemoveR_Click">Remove</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                            <asp:DropDownList ID="ddlFSRAttachedName" runat="server" CssClass="TextBox" /></td>
                                                        <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                            <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" /></td>
                                                        <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                            <asp:UpdatePanel ID="upManage" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lblAttachedFileAdd" runat="server" OnClick="lblAttachedFileAddR_Click">Add</asp:LinkButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="lblAttachedFileAdd" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlMaterialCharges" runat="server" HeaderText="Material Charges">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <div id="divWarrantyDistribution" runat="server">
                            <table class="labeltxt fullWidth">
                                <tr>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="Customer Pay %"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtCustomerPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Pay %"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtDealerPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="AE Pay %"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtAEPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSaveWarrantyDistribution" runat="server" Text="Save Warranty Distribution" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSaveWarrantyDistribution_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%"   DataKeyNames="ServiceMaterialID">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbEdit" runat="server" OnCheckedChanged="cbEdit_CheckedChanged" AutoPostBack="true" />
                                        <asp:LinkButton ID="lbUpdate" runat="server" Text="Update" Visible="false" OnClick="lbUpdate_Click"></asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:LinkButton ID="lbEditCancel" runat="server" Text="Back" Visible="false" OnClick="lbEditCancel_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" HeaderStyle-Width="30px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="TextBox" Width="100px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material S/N">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtMaterialSN" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avl Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAvailableQty" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prime Faulty Part">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbIsFaultyPart" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsFaultyPart")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLD Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialCode")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtDefectiveMaterial" runat="server" CssClass="TextBox" Width="100px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLD Material S/N">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefectiveMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtDefectiveMaterialSN" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recomened Parts">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecomenedParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsRecomenedParts")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quotation  Parts">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbQuotationParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuotationParts")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialSource" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSource")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblMaterialSourceID" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSourceID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlMaterialSource" runat="server" CssClass="TextBox" Width="80px" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblTsirID" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlTSIRNumber" runat="server" CssClass="TextBox" Width="122px" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qtn No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPONumber" Text='<%# DataBinder.Eval(Container.DataItem, "PONumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parts Invoice">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOldInvoice" Text='<%# DataBinder.Eval(Container.DataItem, "OldInvoice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCancel" Text="Canceled" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsDeleted")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click">
                                            <i class="fa fa-fw fa-times" style="font-size:18px"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlNotes" runat="server" HeaderText="Notes" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvNotes" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%"   DataKeyNames="ServiceNoteID">
                        <Columns>
                            <asp:TemplateField HeaderText="Note Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNoteType" Text='<%# DataBinder.Eval(Container.DataItem, "NoteType.NoteType")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblComments" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtComments" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblNoteRemove" runat="server" OnClick="lblNoteRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="footer" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabechnicianWorkHours" runat="server" HeaderText="Technician Work Hours" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvTechnicianWorkDays" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Technician">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName_ContactName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName_ContactName")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblServiceTechnicianWorkDateID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceTechnicianWorkDateID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked Day">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkedDay" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked Hours">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbWorkedDayRemove" runat="server" OnClick="lbWorkedDayRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="IC Ticket Restore" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">

                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <label>Restore Date and Time : </label>
                                <asp:Label ID="lblRestoreDate" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <label>Customer Remarks : </label>
                                <asp:Label ID="lblCustomerRemarks" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <label>Customer Satisfaction Level : </label>
                                <asp:Label ID="lblCustomerSatisfactionLevel" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <label>Arrival Back Date and Time : </label>
                                <asp:Label ID="lblArrivalBackDate" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <label>Complaint Status : </label>
                                <asp:Label ID="lblComplaintStatus" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </div>
        </ContentTemplate>
    </asp1:TabPanel>

</asp1:TabContainer>


<asp:Panel ID="pnlAddTechnician" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Technician</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAssignEngineer" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddTechnician ID="UC_ICTicketAddTechnician" runat="server"></UC:UC_ICTicketAddTechnician>
        </div>
        <div class="col-md-12 text-center">
            <%--<asp:Button ID="btnSaveAssignSE" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveAssignSE_Click" />--%>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddTechnician" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddTechnician" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>


