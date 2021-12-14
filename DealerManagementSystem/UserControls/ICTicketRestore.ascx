<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketRestore.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketRestore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DMS_U_AvailabilityOfOtherMachine.ascx" TagPrefix="UC" TagName="UC_AvailabilityOfOtherMachine" %>

<script type="text/javascript">

    function collapseExpandRestore(obj) {
        var gvObject = document.getElementById("MainContent_UC_DMS_ICTicketRestore_pnlRestore");
        var imageID = document.getElementById("MainContent_UC_DMS_ICTicketRestore_imgRestore");
        var hfCallInformation = document.getElementById('<%= hfRestore.ClientID %>');
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
</script> 
<asp:HiddenField ID="hfRestore" runat="server" Value=""></asp:HiddenField>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<table id="txnHistory5:panelGridid2" style="height: 100%; width: 100%" class="IC_basicInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">IC Ticket Restore</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandRestore();">
                        <img id="imgRestore" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlRestore" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel2">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body2">
                        <table class="labeltxt fullWidth">
                            <tr>

                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label17" runat="server" CssClass="label" Text="Restore Date and Time"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtRestoreDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtRestoreDate');"  Enabled="false"></asp:TextBox>
                                            <asp:CalendarExtender ID="ceRestoreDate" runat="server" TargetControlID="txtRestoreDate" PopupButtonID="txtRestoreDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRestoreDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                            <asp:DropDownList ID="ddlRestoreHH" runat="server" CssClass="TextBox" Width="60px"  Enabled="false">
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
                                            <asp:DropDownList ID="ddlRestoreMM" runat="server" CssClass="TextBox" Width="65px" Enabled="false">
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
                                            <asp:Label ID="Label22" runat="server" CssClass="label" Text="Customer Remarks"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtCustomerRemarks" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label33" runat="server" CssClass="label" Text="Customer Satisfaction Level"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:DropDownList ID="ddlCustomerSatisfactionLevel" runat="server" CssClass="TextBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="tbl-row-right">
                                        <div class="tbl-col-left">
                                            <asp:Label ID="Label26" runat="server" CssClass="label" Text="Arrival Back Date and Time"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:TextBox ID="txtArrivalBackDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtRestoreDate');"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtArrivalBackDate" PopupButtonID="txtArrivalBackDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtArrivalBackDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                            <asp:DropDownList ID="ddlArrivalBackHH" runat="server" CssClass="TextBox" Width="60px">
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
                                            </asp:DropDownList><asp:DropDownList ID="ddlArrivalBackMM" runat="server" CssClass="TextBox" Width="65px">
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
                                            <asp:Label ID="Label24" runat="server" CssClass="label" Text="Complaint Status"></asp:Label>
                                        </div>
                                        <div class="tbl-col-right">
                                            <asp:DropDownList ID="ddlComplaintStatus" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlComplaintStatus_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>Open</asp:ListItem>
                                                <asp:ListItem>Close</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
