<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="LeadReportForDefinedPeriod.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.LeadReportForDefinedPeriod" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
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


    <%-- <asp:Panel ID="pnlSpecifyCriteria" runat="server" CssClass="Popup" Style="display: none">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Specify Criteria</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>--%>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
            <fieldset class="fieldset-border">
                <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Employee</label>
                            <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Lead Date From</label>
                            <asp:TextBox ID="txtLeadDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Lead Date To</label>
                            <asp:TextBox ID="txtLeadDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-2 text-left">
                            <label>Country</label>
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSCountry_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label>State</label>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>

                        </div>
                    </div>
                </fieldset>
            </fieldset>
        </div>
    </div>
    <%-- </asp:Panel>--%>
    <%--  <ajaxToolkit:ModalPopupExtender ID="MPE_SpecifyCriteria" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSpecifyCriteria" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
    --%>

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List All Product</legend>
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Lead(s):</td>
                                        <td>
                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnLeadArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeadArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnLeadArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnLeadArrowRight_Click" /></td>
                                        <td>
                                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" Height="25px" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvLead" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLead_PageIndexChanging" EmptyDataText="No Data Found" OnDataBound="OnDataBound" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <%-- <HeaderTemplate>
                                    <th colspan="3">Report Period</th>
                                    <th colspan="3">Ratio</th>
                                    <th colspan="3">Opening Lead</th>
                                    <th colspan="3">Lead Generated</th>
                                    <th colspan="3">Win to Ajax</th>
                                    <th colspan="3">Lead Lost</th>
                                    <th colspan="3">Lead Drop</th>
                                    <th colspan="3">Closing Lead</th>
                                    <th colspan="5">Ageing - Closing Lead</th>
                                    <tr class="header2">
                                        <th></th>
                                        <th>Serial No.</th>
                                        <th>Employee Name</th>
                                        <th>Employee Adress</th>
                                        <th>Employee designation </th>
                                        <th>Salary</th>
                                    </tr>
                                </HeaderTemplate>--%>
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dealer Code">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Code")%>' runat="server" />
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblEnggUserID" Text='<%# DataBinder.Eval(Container.DataItem, "EnggUserID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Name")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Salesman">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerSalesman" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Salesman")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hit">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHitRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Hit Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblHitRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lost">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLostRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblLostRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drop">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDropRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblDropRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenHot" Text='<%# DataBinder.Eval(Container.DataItem, "Open Hot")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenHotF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Open Warm")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenWarmF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenCold" Text='<%# DataBinder.Eval(Container.DataItem, "Open Cold")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenColdF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedHot" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Hot")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedHotF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Warm")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedWarmF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedCold" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Cold")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedColdF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinHot" Text='<%# DataBinder.Eval(Container.DataItem, "Win Hot")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinHotF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Win Warm")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinWarmF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinCold" Text='<%# DataBinder.Eval(Container.DataItem, "Win Cold")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinColdF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostHot" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Hot")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostHotF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Warm")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostWarmF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostCold" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Cold")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostColdF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropHot" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Hot")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropHotF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Warm")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropWarmF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropCold" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Cold")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropColdF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingHot" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Hot")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingHotF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Warm")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingWarmF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingCold" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Cold")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingColdF" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="0-30">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge30" Text='<%# DataBinder.Eval(Container.DataItem, "Age 0 - 30")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge30F" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="31-60">
                                <HeaderStyle BackColor="#f8cbad" />
                                <FooterStyle BackColor="#f8cbad" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge60" Text='<%# DataBinder.Eval(Container.DataItem, "Age 31 - 60")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge60F" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="61-90">
                                <HeaderStyle BackColor="#f4b084" />
                                <FooterStyle BackColor="#f4b084" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge90" Text='<%# DataBinder.Eval(Container.DataItem, "Age 61 - 90")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge90F" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="91-180">
                                <HeaderStyle BackColor="#f0904e" />
                                <FooterStyle BackColor="#f0904e" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge180" Text='<%# DataBinder.Eval(Container.DataItem, "Age 91 - 180")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge180F" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="> 180">
                                <HeaderStyle BackColor="#de6614" />
                                <FooterStyle BackColor="#de6614" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAgeA180" Text='<%# DataBinder.Eval(Container.DataItem, "Age > 180")%>' runat="server" OnClick="lblLinkButton_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAgeA180F" runat="server" OnClick="lblLinkButtonF_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </fieldset>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List SLCM</legend>
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Lead(s):</td>
                                        <td>
                                            <asp:Label ID="lblRowCountSLCM" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnLeadArrowLeftSLCM" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeadArrowLeftSLCM_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnLeadArrowRightSLCM" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnLeadArrowRightSLCM_Click" /></td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelSLCM_Click" Width="100px" Height="25px" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvLeadSLCM" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLeadSLCM_PageIndexChanging" EmptyDataText="No Data Found" OnDataBound="gvLeadSLCM_OnDataBound" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dealer Code">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Code")%>' runat="server" />
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblEnggUserID" Text='<%# DataBinder.Eval(Container.DataItem, "EnggUserID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Name")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Salesman">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerSalesman" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Salesman")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hit">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHitRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Hit Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblHitRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lost">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLostRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblLostRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drop">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDropRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblDropRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenHot" Text='<%# DataBinder.Eval(Container.DataItem, "Open Hot")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenHotF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Open Warm")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenWarmF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenCold" Text='<%# DataBinder.Eval(Container.DataItem, "Open Cold")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenColdF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedHot" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Hot")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedHotF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Warm")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedWarmF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedCold" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Cold")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedColdF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinHot" Text='<%# DataBinder.Eval(Container.DataItem, "Win Hot")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinHotF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Win Warm")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinWarmF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinCold" Text='<%# DataBinder.Eval(Container.DataItem, "Win Cold")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinColdF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostHot" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Hot")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostHotF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Warm")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostWarmF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostCold" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Cold")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostColdF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropHot" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Hot")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropHotF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Warm")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropWarmF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropCold" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Cold")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropColdF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingHot" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Hot")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingHotF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Warm")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingWarmF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingCold" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Cold")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingColdF" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="0-30">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge30" Text='<%# DataBinder.Eval(Container.DataItem, "Age 0 - 30")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge30F" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="31-60">
                                <HeaderStyle BackColor="#f8cbad" />
                                <FooterStyle BackColor="#f8cbad" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge60" Text='<%# DataBinder.Eval(Container.DataItem, "Age 31 - 60")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge60F" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="61-90">
                                <HeaderStyle BackColor="#f4b084" />
                                <FooterStyle BackColor="#f4b084" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge90" Text='<%# DataBinder.Eval(Container.DataItem, "Age 61 - 90")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge90F" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="91-180">
                                <HeaderStyle BackColor="#f0904e" />
                                <FooterStyle BackColor="#f0904e" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge180" Text='<%# DataBinder.Eval(Container.DataItem, "Age 91 - 180")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge180F" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="> 180">
                                <HeaderStyle BackColor="#de6614" />
                                <FooterStyle BackColor="#de6614" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAgeA180" Text='<%# DataBinder.Eval(Container.DataItem, "Age > 180")%>' runat="server" OnClick="lblLinkButtonSLCM_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAgeA180F" runat="server" OnClick="lblLinkButtonFSLCM_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </fieldset>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List Focus Product</legend>
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Lead(s):</td>
                                        <td>
                                            <asp:Label ID="lblRowCountFocusProduct" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnLeadArrowLeftFocusProduct" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnLeadArrowLeftFocusProduct_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnLeadArrowRightFocusProduct" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnLeadArrowRightFocusProduct_Click" /></td>
                                        <td>
                                            <asp:Button ID="Button3" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelFocusProduct_Click" Width="100px" Height="25px" /></td>
                                    </tr>
                                </table>



                            </div>
                        </div>
                    </div>

                    <asp:GridView ID="gvLeadFocusProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                        PageSize="10" AllowPaging="true" OnPageIndexChanging="gvLeadFocusProduct_PageIndexChanging" EmptyDataText="No Data Found" OnDataBound="gvLeadFocusProduct_OnDataBound" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dealer Code">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Code")%>' runat="server" />
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblEnggUserID" Text='<%# DataBinder.Eval(Container.DataItem, "EnggUserID")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Name")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Salesman">
                                <HeaderStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerSalesman" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Salesman")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hit">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHitRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Hit Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblHitRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lost">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLostRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblLostRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drop">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDropRatio" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Ratio")%>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblDropRatioF" runat="server" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenHot" Text='<%# DataBinder.Eval(Container.DataItem, "Open Hot")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenHotF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Open Warm")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenWarmF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblOpenCold" Text='<%# DataBinder.Eval(Container.DataItem, "Open Cold")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblOpenColdF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedHot" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Hot")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedHotF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Warm")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedWarmF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblGeneratedCold" Text='<%# DataBinder.Eval(Container.DataItem, "Generated Cold")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblGeneratedColdF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinHot" Text='<%# DataBinder.Eval(Container.DataItem, "Win Hot")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinHotF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Win Warm")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinWarmF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblWinCold" Text='<%# DataBinder.Eval(Container.DataItem, "Win Cold")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblWinColdF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostHot" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Hot")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostHotF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Warm")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostWarmF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblLostCold" Text='<%# DataBinder.Eval(Container.DataItem, "Lost Cold")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblLostColdF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropHot" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Hot")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropHotF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Warm")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropWarmF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDropCold" Text='<%# DataBinder.Eval(Container.DataItem, "Drop Cold")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblDropColdF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hot">
                                <HeaderStyle BackColor="#c6e0b4" />
                                <FooterStyle BackColor="#c6e0b4" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingHot" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Hot")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingHotF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warm">
                                <HeaderStyle BackColor="#ddebf7" />
                                <FooterStyle BackColor="#ddebf7" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingWarm" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Warm")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingWarmF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cold">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblClosingCold" Text='<%# DataBinder.Eval(Container.DataItem, "Closing Cold")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblClosingColdF" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="0-30">
                                <HeaderStyle BackColor="#fce4d6" />
                                <FooterStyle BackColor="#fce4d6" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge30" Text='<%# DataBinder.Eval(Container.DataItem, "Age 0 - 30")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge30F" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="31-60">
                                <HeaderStyle BackColor="#f8cbad" />
                                <FooterStyle BackColor="#f8cbad" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge60" Text='<%# DataBinder.Eval(Container.DataItem, "Age 31 - 60")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge60F" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="61-90">
                                <HeaderStyle BackColor="#f4b084" />
                                <FooterStyle BackColor="#f4b084" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge90" Text='<%# DataBinder.Eval(Container.DataItem, "Age 61 - 90")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge90F" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="91-180">
                                <HeaderStyle BackColor="#f0904e" />
                                <FooterStyle BackColor="#f0904e" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAge180" Text='<%# DataBinder.Eval(Container.DataItem, "Age 91 - 180")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAge180F" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="> 180">
                                <HeaderStyle BackColor="#de6614" />
                                <FooterStyle BackColor="#de6614" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAgeA180" Text='<%# DataBinder.Eval(Container.DataItem, "Age > 180")%>' runat="server" OnClick="lblLinkButtonFocusProduct_Click" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lblAgeA180F" runat="server" OnClick="lblLinkButtonFFocusProduct_Click" ForeColor="Black" Font-Size="Large" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </fieldset>
            </div>
        </div>
    </div>

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
                <asp:GridView ID="gvLeadDetails" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid"
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
