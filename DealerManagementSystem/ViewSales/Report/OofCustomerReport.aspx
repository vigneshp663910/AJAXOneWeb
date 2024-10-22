<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="OofCustomerReport.aspx.cs" Inherits="DealerManagementSystem.ViewSales.Report.OofCustomerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <%--<div class="col-md-12" id="divList" runat="server">--%>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Date</label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDate" WatermarkText="Date"></asp:TextBoxWatermarkExtender>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="65px" />
                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" Width="100px" OnClick="btnExportExcel_Click"/>
                </div>
            </div>
        </fieldset>
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
                                            <td>OOF Customer(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCountOOFCustomer" runat="server" CssClass="label"> </asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowLeftOOFCustomer" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeftOOFCustomer_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnArrowRightOOFCustomer" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRightOOFCustomer_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvOOFCustomer" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" PageSize="10" AllowPaging="true" EmptyDataText="No Data Found" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server">
                                    <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                        </asp:Label>
                                        <br />
                                        <asp:Label ID="lblEMail" runat="server">
                                    <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EMail")%>'><%# DataBinder.Eval(Container.DataItem, "EMail")%></a>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Address1">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address1")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address2">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address3">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" Text='<%# DataBinder.Eval(Container.DataItem, "City")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pin Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPinCode" Text='<%# DataBinder.Eval(Container.DataItem, "PinCode")%>' runat="server"></asp:Label>
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
                </fieldset>
            </div>
        </div>
        <%--</div>--%>
    </div>
</asp:Content>
