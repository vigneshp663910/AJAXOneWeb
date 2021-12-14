<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketBasicInformation_N.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketBasicInformation_N" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<style type="text/css">
    .auto-style1 {
        height: 51px;
    }
</style>


<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>



<asp:HiddenField ID="hfBasicInformation" runat="server" Value="X"></asp:HiddenField>
<UC:UC_BasicInformation ID="UC_BasicInformation" runat="server"></UC:UC_BasicInformation>


<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />


<asp:Label ID="lblMessageTechnicianInformation" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<asp:HiddenField ID="hfTechnicianInformation" runat="server" Value="X"></asp:HiddenField>
<asp:Panel ID="pnlTechnician" runat="server">
    <table id="txnHistory1:panelGridid3" style="height: 100%; width: 100%" class="IC_materialInfo">
        <tr>
            <td>
                <div class="boxHead" id="DivTechnician">
                    <div class="logheading">Technician Information</div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpandTechnicianInformation();">
                            <img id="imgTechnicianInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="pnlTechnicianInformation" runat="server">
                    <div class="rf-p " id="txnHistory:inputFiltersPanel3">
                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body3">
                            <asp:GridView ID="gvTechnician" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" OnRowDataBound="gvTechnician_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Technician">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="gvddlTechnician" runat="server" CssClass="TextBox" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Technician Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbTechnicianAdd" runat="server" OnClick="lbTechnicianAdd_Click">Add</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assigned By">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assigned On">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbTechnicianRemove" runat="server" OnClick="lbTechnicianRemove_Click">Remove</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Label ID="lblMessageCallInformation" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<asp:CheckBox ID="CheckBox1" runat="server" Visible="False"></asp:CheckBox>
<asp:HiddenField ID="hfCallInformation" runat="server" Value="X"></asp:HiddenField>
<asp:Panel ID="pnlCallInformationSH" runat="server">
    <table id="txnHistory1:panelGridid2" style="height: 100%; width: 100%" class="IC_basicInfo">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Call Information</div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpandCallInformation();">
                            <img id="imgCallInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="pnlCallInformation" runat="server">
                    <div class="rf-p " id="txnHistory:inputFiltersPanel2">
                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body2">
                            <table class="labeltxt fullWidth">
                                <tr>
                                    <td>
                                        <table class="labeltxt fullWidth">
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-left">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label47" runat="server" CssClass="label" Text="Departure Date and Time"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtDepartureDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtDepartureDate');" BorderColor="Silver"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDepartureDate" PopupButtonID="txtDepartureDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDepartureDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                            <asp:DropDownList ID="ddlDepartureHH" runat="server" CssClass="TextBox" Width="60px">
                                                                <asp:ListItem Value="-1">HH</asp:ListItem>
                                                                <asp:ListItem>0</asp:ListItem>
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>7</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                                <asp:ListItem>9</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlDepartureMM" runat="server" CssClass="TextBox" Width="65px">
                                                                <asp:ListItem Value="0">MM</asp:ListItem>
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>35</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>55</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-left">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Reached Date and Time"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtReachedDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtReachedDate');" OnTextChanged="txtReachedDate_TextChanged" AutoPostBack="true" BorderColor="Silver"></asp:TextBox>
                                                            <asp:CalendarExtender ID="ceReachedDate" runat="server" TargetControlID="txtReachedDate" PopupButtonID="txtReachedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtReachedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                            <asp:DropDownList ID="ddlReachedHH" runat="server" CssClass="TextBox" Width="60px">
                                                                <asp:ListItem Value="-1">HH</asp:ListItem>
                                                                <asp:ListItem>0</asp:ListItem>
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>7</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                                <asp:ListItem>9</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlReachedMM" runat="server" CssClass="TextBox" Width="65px">
                                                                <asp:ListItem Value="0">MM</asp:ListItem>
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>35</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>55</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label104" runat="server" CssClass="label" Text="Location"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label11" runat="server" CssClass="label" Text="Service Type"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="TextBox" OnTextChanged="ddlServiceType_TextChanged" AutoPostBack="true" />
                                                            <asp:DropDownList ID="ddlServiceTypeOverhaul" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceTypeOverhaul" DataValueField="ServiceTypeOverhaulID" />
                                                            <asp:DropDownList ID="ddlServiceSubType" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceSubType" DataValueField="ServiceSubTypeID" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Service Priority"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlServicePriority" runat="server" CssClass="TextBox" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label106" runat="server" CssClass="label" Text="Delivery Location"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="TextBox" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="lblHMRValue" runat="server" CssClass="label" Text="Current HMR Value"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtHMRValue" runat="server" CssClass="input" AutoComplete="SP" OnTextChanged="txtHMRValue_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label16" runat="server" CssClass="label" Text="Current HMR Date"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtHMRDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                            <asp:CalendarExtender ID="ceHMRDate" runat="server" TargetControlID="txtHMRDate" PopupButtonID="txtHMRDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtHMRDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="lblCess" runat="server" CssClass="label" Text="Cess"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:CheckBox ID="cbCess" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="labeltxt fullWidth">
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Type Of Warranty"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlTypeOfWarranty" runat="server" CssClass="TextBox" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label36" runat="server" CssClass="label" Text="Main Application"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlMainApplication" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlMainApplication_SelectedIndexChanged" AutoPostBack="true" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label14" runat="server" CssClass="label" Text="Sub Application Manual"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlSubApplication" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlSubApplication_SelectedIndexChanged" AutoPostBack="true" />
                                                            <asp:TextBox ID="txtSubApplicationEntry" runat="server" CssClass="TextBox" AutoComplete="Off" Visible="false" OnTextChanged="txtSubApplicationEntry_TextChanged"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label27" runat="server" CssClass="label" Text="Site Contact Person’s Name"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtOperatorName" runat="server" CssClass="input" AutoComplete="SP"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label29" runat="server" CssClass="label" Text="Site Contact Person’s Number"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtSiteContactPersonNumber" runat="server" CssClass="input"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Site Contact Person’s Number 2"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtSiteContactPersonNumber2" runat="server" CssClass="input"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label45" runat="server" CssClass="label" Text="Designation"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="TextBox" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label20" runat="server" CssClass="label" Text="Scope of Work"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtScopeOfWork" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="labeltxt fullWidth">
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="No Claim"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:CheckBox ID="cbNoClaim" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label4" runat="server" CssClass="label" Text="No Claim Reason"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtNoClaimReason" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Mc Entered Service Date"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtMcEnteredServiceDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtMcEnteredServiceDate" PopupButtonID="txtMcEnteredServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtMcEnteredServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Service Started Date"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtServiceStartedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtServiceStartedDate" PopupButtonID="txtServiceStartedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtServiceStartedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Service Ended Date"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtServiceEndedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtServiceEndedDate" PopupButtonID="txtServiceEndedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtServiceEndedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label23" runat="server" CssClass="label" Text="Kind Attn"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtKindAttn" runat="server" CssClass="input"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label25" runat="server" CssClass="label" Text="Remarks"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="tbl-row-right">
                                                        <div class="tbl-col-left">
                                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Is Machine Active"></asp:Label>
                                                        </div>
                                                        <div class="tbl-col-right">
                                                            <asp:CheckBox ID="cbIsMachineActive" runat="server" Checked="true" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                            <table class="labeltxt fullWidth">



                                <tr>
                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label24" runat="server" CssClass="label" Text="Category 1"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory1" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory1_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label26" runat="server" CssClass="label" Text="Category 2"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory2" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory2_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label28" runat="server" CssClass="label" Text="Category 3"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory3" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory3_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>


                                </tr>
                                <tr>

                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label30" runat="server" CssClass="label" Text="Category 4"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory4" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory4_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>


                                </tr>
                                <tr>

                                    <td style="display: none">
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label32" runat="server" CssClass="label" Text="Category 5"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory5" runat="server" CssClass="TextBox" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <%--<table class="labeltxt fullWidth">
                                <tr>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label47" runat="server" CssClass="label" Text="Departure Date and Time"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtDepartureDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtDepartureDate');" BorderColor="Silver"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDepartureDate" PopupButtonID="txtDepartureDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDepartureDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                <asp:DropDownList ID="ddlDepartureHH" runat="server" CssClass="TextBox" Width="60px">
                                                    <asp:ListItem Value="-1">HH</asp:ListItem>
                                                    <asp:ListItem>0</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlDepartureMM" runat="server" CssClass="TextBox" Width="65px">
                                                    <asp:ListItem Value="0">MM</asp:ListItem>
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Type Of Warranty"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlTypeOfWarranty" runat="server" CssClass="TextBox" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="No Claim"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:CheckBox ID="cbNoClaim" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Reached Date and Time"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtReachedDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtReachedDate');" OnTextChanged="txtReachedDate_TextChanged" AutoPostBack="true" BorderColor="Silver"></asp:TextBox>
                                                <asp:CalendarExtender ID="ceReachedDate" runat="server" TargetControlID="txtReachedDate" PopupButtonID="txtReachedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtReachedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                <asp:DropDownList ID="ddlReachedHH" runat="server" CssClass="TextBox" Width="60px">
                                                    <asp:ListItem Value="-1">HH</asp:ListItem>
                                                    <asp:ListItem>0</asp:ListItem>
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem>4</asp:ListItem>
                                                    <asp:ListItem>5</asp:ListItem>
                                                    <asp:ListItem>6</asp:ListItem>
                                                    <asp:ListItem>7</asp:ListItem>
                                                    <asp:ListItem>8</asp:ListItem>
                                                    <asp:ListItem>9</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlReachedMM" runat="server" CssClass="TextBox" Width="65px">
                                                    <asp:ListItem Value="0">MM</asp:ListItem>
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label24" runat="server" CssClass="label" Text="Category 1"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory1" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory1_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label36" runat="server" CssClass="label" Text="Main Application"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlMainApplication" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlMainApplication_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="No Claim Reason"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtNoClaimReason" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label104" runat="server" CssClass="label" Text="Location"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtLocation" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label26" runat="server" CssClass="label" Text="Category 2"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory2" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory2_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td class="auto-style1">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label14" runat="server" CssClass="label" Text="Sub Application Manual"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlSubApplication" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlSubApplication_SelectedIndexChanged" AutoPostBack="true" />
                                                <asp:TextBox ID="txtSubApplicationEntry" runat="server" CssClass="TextBox" AutoComplete="Off" Visible="false" OnTextChanged="txtSubApplicationEntry_TextChanged"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Mc Entered Service Date"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtMcEnteredServiceDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtMcEnteredServiceDate" PopupButtonID="txtMcEnteredServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtMcEnteredServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label11" runat="server" CssClass="label" Text="Service Type"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="TextBox" OnTextChanged="ddlServiceType_TextChanged" AutoPostBack="true" />
                                                <asp:DropDownList ID="ddlServiceTypeOverhaul" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceTypeOverhaul" DataValueField="ServiceTypeOverhaulID" />
                                                <asp:DropDownList ID="ddlServiceSubType" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceSubType" DataValueField="ServiceSubTypeID" />

                                            </div>
                                        </div>
                                    </td>
                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label28" runat="server" CssClass="label" Text="Category 3"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory3" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory3_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label27" runat="server" CssClass="label" Text="Site Contact Person’s Name"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtOperatorName" runat="server" CssClass="input" AutoComplete="SP"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Service Started Date"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtServiceStartedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtServiceStartedDate" PopupButtonID="txtServiceStartedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtServiceStartedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label12" runat="server" CssClass="label" Text="Service Priority"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlServicePriority" runat="server" CssClass="TextBox" />
                                            </div>
                                        </div>
                                    </td>
                                    <td style="display: none">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label30" runat="server" CssClass="label" Text="Category 4"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory4" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlCategory4_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label29" runat="server" CssClass="label" Text="Site Contact Person’s Number"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtSiteContactPersonNumber" runat="server" CssClass="input"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Service Ended Date"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtServiceEndedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtServiceEndedDate" PopupButtonID="txtServiceEndedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtServiceEndedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label106" runat="server" CssClass="label" Text="Delivery Location"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="TextBox" />
                                            </div>
                                        </div>
                                    </td>
                                    <td style="display: none">
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label32" runat="server" CssClass="label" Text="Category 5"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlCategory5" runat="server" CssClass="TextBox" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="Site Contact Person’s Number 2"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtSiteContactPersonNumber2" runat="server" CssClass="input"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>
                                    <td class="auto-style1">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label23" runat="server" CssClass="label" Text="Kind Attn"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtKindAttn" runat="server" CssClass="input"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td class="auto-style1">
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="lblHMRValue" runat="server" CssClass="label" Text="Current HMR Value"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtHMRValue" runat="server" CssClass="input" AutoComplete="SP" OnTextChanged="txtHMRValue_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label45" runat="server" CssClass="label" Text="Designation"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="TextBox" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label25" runat="server" CssClass="label" Text="Remarks"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label16" runat="server" CssClass="label" Text="Current HMR Date"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtHMRDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                                                <asp:CalendarExtender ID="ceHMRDate" runat="server" TargetControlID="txtHMRDate" PopupButtonID="txtHMRDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtHMRDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label20" runat="server" CssClass="label" Text="Scope of Work"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtScopeOfWork" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Is Machine Active"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:CheckBox ID="cbIsMachineActive" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="tbl-row-right">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="lblCess" runat="server" CssClass="label" Text="Cess"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:CheckBox ID="cbCess" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                            </table>--%>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<script type="text/javascript">
    function collapseExpandBasicInformation(obj) {
        var gvObject = document.getElementById("MainContent_UC_BasicInformation_N_UC_BasicInformation_pnlBasicInformation");
        var imageID = document.getElementById("MainContent_UC_BasicInformation_N_UC_BasicInformation_imgBasicInformation");
        var hfBasicInformation = document.getElementById('<%= hfBasicInformation.ClientID %>');
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfBasicInformation.value = 'X';
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfBasicInformation.value = ' ';
        }
    }
    function collapseExpandTechnicianInformation(obj) {
        var gvObject = document.getElementById("MainContent_UC_BasicInformation_N_pnlTechnicianInformation");
        var imageID = document.getElementById("MainContent_UC_BasicInformation_N_imgTechnicianInformation");
        var hfTechnicianInformation = document.getElementById('<%= hfTechnicianInformation.ClientID %>');
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfTechnicianInformation.value = 'X';
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfTechnicianInformation.value = ' ';
        }
    }
    $(document).ready(function () {

        MainContent_UC_BasicInformation_N_UC_BasicInformation_pnlBasicInformation
        //var gvObject = document.getElementById("MainContent_UC_BasicInformation_pnlBasicInformation");
        //var imageID = document.getElementById("MainContent_UC_BasicInformation_imgBasicInformation");

        var gvObject = document.getElementById("MainContent_UC_BasicInformation_N_UC_BasicInformation_pnlBasicInformation");
        var imageID = document.getElementById("MainContent_UC_BasicInformation_N_UC_BasicInformation_imgBasicInformation");
        var hfBasicInformation = document.getElementById('<%= hfBasicInformation.ClientID %>');
        if (hfBasicInformation.value == "X") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
        var gvTickets = document.getElementById('MainContent_UC_BasicInformation_gvTechnician');
        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblItem = document.getElementById('MainContent_UC_BasicInformation_gvTechnician_lblUserName_' + i);
                if (lblItem != null) {
                    if (lblItem.innerHTML == "") {
                        lblItem.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
        var pnlTechnicianInformation = document.getElementById("MainContent_UC_BasicInformation_pnlTechnicianInformation");
        var imgTechnicianInformation = document.getElementById("MainContent_UC_BasicInformation_imgTechnicianInformation");
        var hfTechnicianInformation = document.getElementById('<%= hfTechnicianInformation.ClientID %>');
        if (hfTechnicianInformation.value == "X") {
            pnlTechnicianInformation.style.display = "inline";
            imgTechnicianInformation.src = "Images/grid_collapse.png";
        }
        else {
            pnlTechnicianInformation.style.display = "none";
            imgTechnicianInformation.src = "Images/grid_expand.png";
        }
        var pnlCallInformation = document.getElementById("MainContent_UC_BasicInformation_pnlCallInformation");
        var imgCallInformation = document.getElementById("MainContent_UC_BasicInformation_imgCallInformation");
        var hfCallInformation = document.getElementById('<%= hfCallInformation.ClientID %>');
        if (pnlCallInformation != null) {
            if (hfCallInformation.value == "X") {
                pnlCallInformation.style.display = "inline";
                imgCallInformation.src = "Images/grid_collapse.png";
            }
            else {
                pnlCallInformation.style.display = "none";
                imgCallInformation.src = "Images/grid_expand.png";
            }
        }
    });

    function collapseExpandCallInformation(obj) {
        var gvObject = document.getElementById("MainContent_UC_BasicInformation_N_pnlCallInformation");
        var imageID = document.getElementById("MainContent_UC_BasicInformation_N_imgCallInformation");
        var hfCallInformation = document.getElementById('<%= hfCallInformation.ClientID %>');
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
            hfCallInformation.value = 'X';
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
            hfCallInformation.value = ' ';
        }
    }

    function removeText(id) {
        var TheTextBox = document.getElementById(id);
        TheTextBox.value = "";
        return false;
    }
</script>