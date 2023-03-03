<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketMarginWarrantyChange.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketMarginWarrantyChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <div class="col-md-12">
        <div class="col-md-12"> 
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Ticket Reached Date Change</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Requested Date</label>
                        <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtRequestedDate');" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender ID="ceRequestedDate" runat="server" TargetControlID="txtRequestedDate" PopupButtonID="txtRequestedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtRequestedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-1 col-sm-12">
                        <label class="modal-label">-</label>
                        <asp:DropDownList ID="ddlRequestedHH" runat="server" CssClass="form-control" Width="70px">
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
                    </div>
                    <div class="col-md-1 col-sm-12">
                        <label class="modal-label">-</label>
                        <asp:DropDownList ID="ddlRequestedMM" runat="server" CssClass="form-control" Width="75px">
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
            </fieldset>
        </div>
    </div>
</asp:Content>
