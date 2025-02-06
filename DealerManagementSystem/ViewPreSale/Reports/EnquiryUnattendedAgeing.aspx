<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="EnquiryUnattendedAgeing.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.EnquiryUnattendedAgeing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Popup {
            width: 95%;
            height: 95%;
            top: 128px;
            left: 283px;
        }

            .Popup .model-scroll {
                height: 80vh;
                overflow: auto;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

    <fieldset class="fieldset-border" id="Fieldset2" runat="server">
        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
        <div class="col-md-12">
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Region</label>
                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Dealer</label>
                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
            </div>

            <div class="col-md-12 text-center">
                <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                <%--  <asp:Button ID="btnEdit" runat="server" CssClass="btn Save" Text="Edit" OnClick="btnEdit_Click" Width="150px" Visible="false"></asp:Button>--%>
            </div>
        </div>
    </fieldset>
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
        <div class="col-md-12 Report">
            <div class="boxHead">
                <div class="logheading">
                    <div style="float: left">
                        <table>
                            <tr>
                                <td>Ageing Enquiries:</td>
                                <td>
                                    <asp:Label ID="lblRowCountV" runat="server" CssClass="label"></asp:Label></td>
                                <td>
                                    <asp:ImageButton ID="ibtnLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeft_Click" /></td>
                                <td>
                                    <asp:ImageButton ID="ibtnRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnRight_Click" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>


            <asp:GridView ID="gvEnquiry" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnDataBound="OnDataBound"
                ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White" HeaderStyle-Width="10px" ItemStyle-Backcolor ="#039caf">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            <itemstyle width="10px" horizontalalign="Right"></itemstyle>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Region" ItemStyle-Width="30px">
                        <HeaderStyle BackColor="#039caf" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblRegionID" Text='<%# DataBinder.Eval(Container.DataItem, "RegionID")%>' runat="server" Visible="false" />
                            <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dealer Code" ItemStyle-Width="30px">
                        <HeaderStyle BackColor="#039caf" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dealer Name" ItemStyle-Width="30px">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#039caf" />
                        <ItemTemplate>
                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Days 0 to 3" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <HeaderStyle BackColor="#ff8080" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDays0To3" Text='<%# DataBinder.Eval(Container.DataItem, "Days 0 to 3" )%>' runat="server" OnClick="lblLinkButton_Click" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblDays0To3F" runat="server" OnClick="lblLinkButton_Click" ForeColor="Black" Font-Size="Large" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Days 4 to 6" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <HeaderStyle BackColor="#ff4d4d" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDays4To6" Text='<%# DataBinder.Eval(Container.DataItem, "Days 4 To 6" )%>' runat="server" OnClick="lblLinkButton_Click" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblDays4To6F" runat="server" OnClick="lblLinkButton_Click" ForeColor="Black" Font-Size="Large" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Days > 6" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <HeaderStyle BackColor="#ff0000" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDaysGr6" Text='<%# DataBinder.Eval(Container.DataItem, "Days > 6" )%>' runat="server" OnClick="lblLinkButton_Click" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblDaysGr6F" runat="server" OnClick="lblLinkButton_Click" ForeColor="Black" Font-Size="Large" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enquiry Total" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                        <HeaderStyle BackColor="#b30000" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lblEnquiryTotal" Text='<%# DataBinder.Eval(Container.DataItem, "Enquiry Total" )%>' runat="server" OnClick="lblLinkButton_Click" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lblEnquiryTotalF" runat="server" OnClick="lblLinkButton_Click" ForeColor="Black" Font-Size="Large" />
                        </FooterTemplate>
                    </asp:TemplateField>

                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle ForeColor="White" BackColor="#fce4d6" />
                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" BackColor="#fce4d6" />
                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>
        </div>
    </fieldset>

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>


    <asp:Panel ID="pnlLeadDetails" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <asp:Button ID="btnExcelDetails" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExcelDetails_Click" Width="100px" />

            <div class="model-scroll">
                <asp:GridView ID="gvDetails" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
                    EmptyDataText="No Data Found">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />

                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </ItemTemplate>
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
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_LeadDetails" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlLeadDetails" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


</asp:Content>

