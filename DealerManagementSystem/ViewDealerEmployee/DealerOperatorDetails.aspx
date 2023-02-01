<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerOperatorDetails.aspx.cs" Inherits="DealerManagementSystem.ViewDealerEmployee.DealerOperatorDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12 Report" id="divDealerOperatorDetailsList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border" id="FldSearch" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Region</label>
                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="BtnSearch_Click" />
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="110px" />
                            <asp:Button ID="BtnFUpload" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnFUpload_Click" />
                        </div>
                    </div>
                </fieldset>
                <fieldset class="fieldset-border" id="FldUpload" runat="server" visible="false">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Upload File</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12" id="DivUpload" runat="server">
                            <asp:FileUpload ID="fileUpload" runat="server" />
                        </div>
                        <div class="col-md-4 col-sm-12">
                            <asp:Button ID="BtnUpload" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnUpload_Click" Width="100px" />
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
                                                <td>Dealer Operator(s):</td>

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
                            <asp:HiddenField ID="HiddenDealerOperatorDetailsID" runat="server" />
                            <asp:GridView ID="gvDealerOperatorDetails" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvDealerOperatorDetails_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="State" DataField="State"></asp:BoundField>
                                    <asp:BoundField HeaderText="Region" DataField="Region"></asp:BoundField>
                                    <asp:BoundField HeaderText="Dealer Code" DataField="DealerCode"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Dealer Name" ControlStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "DealerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Operator Name" DataField="OperatorName"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                        <a href='tel:<%# DataBinder.Eval(Container.DataItem, "ContactNo")%>'><%# DataBinder.Eval(Container.DataItem, "ContactNo")%></a>
                                            </asp:Label>
                                            <br />
                                            <asp:Label ID="lblEMail" runat="server">
                                                        <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EmailID")%>'><%# DataBinder.Eval(Container.DataItem, "EmailID")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Years Of Experience" DataField="YearsOfExperience"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DealerOperatorDetailsID")%>' CssClass="btn Back" OnClick="BtnView_Click" Width="75px" Height="25px" />
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
        <div class="col-md-12" id="divDealerOperatorDetailsView" runat="server" visible="false">
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
                                <%--<asp:LinkButton ID="lbEditDealerOperatorDetails" runat="server" OnClick="lbActions_Click">Edit Dealer Operator</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 field-margin-top">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Operator Details</legend>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Dealer Code : </label>
                                    <asp:Label ID="lblDealerCode" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>State : </label>
                                    <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Email ID : </label>
                                    <asp:Label ID="lblEmailID" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Dealer Name : </label>
                                    <asp:Label ID="lblDealerName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Region : </label>
                                    <asp:Label ID="lblRegion" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Years Of Experience : </label>
                                    <asp:Label ID="lblYearsOfExperience" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Dealer Operator Name : </label>
                                    <asp:Label ID="lblDealerOperatorName" runat="server" CssClass="label"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Contact Number : </label>
                                    <asp:Label ID="lblContactNumber" runat="server" CssClass="label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
