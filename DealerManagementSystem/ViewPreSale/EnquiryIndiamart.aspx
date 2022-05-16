<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dealer.Master" CodeBehind="EnquiryIndiamart.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.EnquiryIndiamart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 text-left">
                    <%--<asp:Label ID="Label7" runat="server" Text="Date From "></asp:Label>--%>
                    <label>Date From</label>
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    <%--<asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
                    <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>--%>
                </div>
                <div class="col-md-2 text-left">
                    <%--<asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>--%>
                    <label>Date To</label>
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" AutoComplete="Off" TextMode="Date"></asp:TextBox>
                    <%--<asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
                    <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>--%>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnEnquiryIndiamart_Click" OnClientClick="return dateValidation();" />


                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="125px" />
                </div>
            </div>
        </fieldset>


    </div>  <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Enquiry Indiamart:</td>
                                        <td>
                                            <asp:Label ID="lblRowCountEnquiryIM" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnEnquiryIMArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnEnquiryIMArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnEnquiryIMArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnEnquiryIMArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvEnquiry" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvEnquiry_PageIndexChanging">
                        <columns>
                                <asp:templatefield headertext="RId" itemstyle-horizontalalign="Center" itemstyle-width="25px">
                                    <itemtemplate>
                                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                                        <asp:label id="lblRowNumber" text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </itemtemplate>
                                </asp:templatefield>
                            </columns>
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

</asp:Content>
