<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesIncentive.aspx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.SalesIncentive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12 Report" id="divSalesIncentiveList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border" id="FldSearch" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
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
                            <label class="modal-label">Invoice No</label>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer Employee</label>
                            <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="BtnSearch_Click" />
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="110px" />
                            <asp:Button ID="BtnFUpload" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnFUpload_Click" />
                            <asp:Button ID="btnDownload" runat="server" Text="Download Template" CssClass="btn Search" OnClick="btnDownload_Click" Width="150px"/>
                        </div>
                    </div>
                </fieldset>

            </div>
            <div class="col-md-12" id="DivReport" runat="server">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Sales Incentive(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenSalesIncentiveID" runat="server" />
                            <asp:GridView ID="gvSalesIncentive" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvSalesIncentive_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="DCode" DataField="DealerCode"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Dealer Name" ControlStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Sales Person Name" DataField="SalesPersonName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Sales Person Aadhaar No" DataField="SalesPersonAadhaarNo"></asp:BoundField>
                                    <%--<asp:BoundField HeaderText="Month" DataField="Month"></asp:BoundField>
                                                <asp:BoundField HeaderText="Year" DataField="Year"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="Month&Year" DataField="Month&Year"></asp:BoundField>
                                    <asp:BoundField HeaderText="Sales Level 1/2" DataField="SalesLevel" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgInvoiceNo" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblgInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:dd-MM-yyyy}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Model" DataField="Model"></asp:BoundField>
                                    <asp:BoundField HeaderText="Incentive Amount" DataField="IncentiveAmount" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SalesIncentiveID")%>' CssClass="btn Back" OnClick="BtnView_Click" Width="75px" Height="25px" />
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
        </div>
        <div class="col-md-12" id="divSalesIncentiveUpload" runat="server" visible="false">
            <fieldset class="fieldset-border" id="FldUpload" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Upload File</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12" id="Div1" runat="server">
                        <asp:FileUpload ID="fileUpload" runat="server" />
                    </div>
                    <div class="col-md-4 col-sm-12">
                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn Save" OnClick="btnView_Click1" Width="100px" />
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" Width="100px" />
                        <asp:Button ID="BtnFBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnFBack_Click" />
                    </div>
                </div>
                <asp:GridView ID="GVUpload" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                    EmptyDataText="No Data Found" AutoGenerateColumns="true" Width="100%">
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </fieldset>
        </div>
        <div class="col-md-12" id="divSalesIncentiveView" runat="server" visible="false">
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
                                <%--<asp:LinkButton ID="lbEditSalesIncentive" runat="server" OnClick="lbActions_Click">Edit Sales Incentive</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 field-margin-top">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Sales Incentive Details</legend>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Dealer Code : </label>
                                    <asp:Label ID="lblDealerCode" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Sales Person Aadhaar No : </label>
                                    <asp:Label ID="lblSPAadhaarNo" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Invoice No : </label>
                                    <asp:Label ID="lblInvoiceNo" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Incentive Amount : </label>
                                    <asp:Label ID="lblIncentiveAmount" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Dealer Name : </label>
                                    <asp:Label ID="lblDealerName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Sales Level 1/2 : </label>
                                    <asp:Label ID="lblSalesLevel" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Invoice Date : </label>
                                    <asp:Label ID="lblInvoiceDate" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Sales Person Name : </label>
                                    <asp:Label ID="lblSPName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Month&Year : </label>
                                    <asp:Label ID="lblMonthandYear" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Model : </label>
                                    <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
