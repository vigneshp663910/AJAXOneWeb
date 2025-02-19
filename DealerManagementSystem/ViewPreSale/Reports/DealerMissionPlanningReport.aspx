<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerMissionPlanningReport.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.DealerMissionPlanningReport" %>

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
            <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Year</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Month</label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Product Type</label>
                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px"  />
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
                                    <td>Planning(s):</td>

                                    <td>
                                        <asp:Label ID="lblRowCountV" runat="server" CssClass="label"></asp:Label></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnVTArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVTArrowLeft_Click" /></td>
                                    <td>
                                        <asp:ImageButton ID="ibtnVTArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVTArrowRight_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


                <asp:GridView ID="gvMissionPlanning" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnDataBound="OnDataBound" 
                     ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Year" SortExpression="Year" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Month" SortExpression="Month" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblMonthName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dealer Code" ItemStyle-Width="30px">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Type" ItemStyle-Width="30px">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblProductTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ProductTypeID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#ddebf7" />
                            <ItemTemplate>
                                <asp:Label ID="lblLeadGenerationPlan" Text='<%# DataBinder.Eval(Container.DataItem, "New Lead Generation Plan")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblLeadGenerationPlanF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#c6e0b4" />
                            <ItemTemplate>
                                <%-- <asp:Label ID="lblLeadGenerationActual" Text='<%# DataBinder.Eval(Container.DataItem, "New Lead Generation Actual")%>' runat="server" />--%>
                                <asp:LinkButton ID="lblLeadGenerationActual" Text='<%# DataBinder.Eval(Container.DataItem, "New Lead Generation Actual" )%>' runat="server" OnClick="lblLinkButton_Click" />

                            </ItemTemplate>
                            <FooterTemplate>
                                <%--           <asp:Label ID="lblLeadGenerationActualF" runat="server" ForeColor="Black" Font-Size="Large" />--%>
                                <asp:LinkButton ID="lblLeadGenerationActualF" runat="server" OnClick="lblLinkButton_Click"  ForeColor="Black" Font-Size="Large" />

                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%Actual" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#a0e172" />
                            <ItemTemplate>
                                <asp:Label ID="lblLeadGenerationActualP" Text='<%# DataBinder.Eval(Container.DataItem, "New Lead Generation %Actual")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblLeadGenerationActualPF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#ddebf7" />
                            <ItemTemplate>
                                <asp:Label ID="lblLeadConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "Lead Conversion Plan")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblLeadConversionPlanF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#c6e0b4" />
                            <ItemTemplate>
                               <%-- <asp:Label ID="lblLeadConversionActual" Text='<%# DataBinder.Eval(Container.DataItem, "Lead Conversion Actual")%>' runat="server" />--%>
                                 <asp:LinkButton ID="lblLeadConversionActual" Text='<%# DataBinder.Eval(Container.DataItem,"Lead Conversion Actual"  )%>' runat="server" OnClick="lblLinkButton_Click" />
                                
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:Label ID="lblLeadConversionActualF" runat="server" ForeColor="Black" Font-Size="Large" />--%>
                                <asp:LinkButton ID="lblLeadConversionActualF" runat="server" OnClick="lblLinkButton_Click"  ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%Actual" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right">
                            <HeaderStyle BackColor="#a0e172" />
                            <ItemTemplate>
                                <asp:Label ID="lblLeadConversionActualP" Text='<%# DataBinder.Eval(Container.DataItem, "Lead Conversion %Actual")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblLeadConversionActualPF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#ddebf7" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuotationGeneratedPlan" Text='<%# DataBinder.Eval(Container.DataItem, "Quotation Generated Plan")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblQuotationGeneratedPlanF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#c6e0b4" />
                            <ItemTemplate>
                             <%--   <asp:Label ID="lblQuotationGeneratedActual" Text='<%# DataBinder.Eval(Container.DataItem, "Quotation Generated Actual")%>' runat="server" />--%>
                                <asp:LinkButton ID="lblQuotationGeneratedActual" Text='<%# DataBinder.Eval(Container.DataItem,"Quotation Generated Actual")%>' runat="server" OnClick="lblLinkButton_Click" />
                            </ItemTemplate>
                            <FooterTemplate>
                               <%-- <asp:Label ID="lblQuotationGeneratedActualF" runat="server" ForeColor="Black" Font-Size="Large" />--%>
                                <asp:LinkButton ID="lblQuotationGeneratedActualF" runat="server" OnClick="lblLinkButton_Click"  ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%Actual" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#a0e172" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuotationGeneratedActualP" Text='<%# DataBinder.Eval(Container.DataItem, "Quotation Generated %Actual")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblQuotationGeneratedActualPF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#ddebf7" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuotationConversionPlan" Text='<%# DataBinder.Eval(Container.DataItem, "Quotation Conversion Plan")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblQuotationConversionPlanF" runat="server" ForeColor="Black" Font-Size="Large" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#c6e0b4" />
                            <ItemTemplate>
                               <%-- <asp:Label ID="lblQuotationConversionActual" Text='<%# DataBinder.Eval(Container.DataItem, "Quotation Conversion Actual")%>' runat="server" />--%>
                                <asp:LinkButton ID="lblQuotationConversionActual" Text='<%# DataBinder.Eval(Container.DataItem,"Quotation Conversion Actual")%>' runat="server" OnClick="lblLinkButton_Click" />
                            </ItemTemplate>
                            <FooterTemplate>
<%--                                <asp:Label ID="lblQuotationConversionActualF" runat="server" ForeColor="Black" Font-Size="Large" />--%>
                                 <asp:LinkButton ID="lblQuotationConversionActualF" runat="server" OnClick="lblLinkButton_Click"  ForeColor="Black" Font-Size="Large" />
                               

                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="%Actual" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                            <HeaderStyle BackColor="#a0e172" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuotationConversionActualP" Text='<%# DataBinder.Eval(Container.DataItem, "Quotation Conversion %Actual")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblQuotationConversionActualPF" runat="server" ForeColor="Black" Font-Size="Large" />
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

