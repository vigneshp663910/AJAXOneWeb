<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BudgetPlanningMonth.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.BudgetPlanningMonth" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control"   />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Year</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                </div>
                  <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Month</label>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button> 
                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
                </div>
            </div>
        </fieldset>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                <div class="col-md-12 Report">

                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Budget Planning :</td>

                                        <td>
                                            <asp:Label ID="lblRowCountVTP" runat="server" CssClass="label"></asp:Label></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTPArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVTPArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTPArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVTPArrowRight_Click" /></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <asp:GridView ID="gvVisitTargetPlanning" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" OnPageIndexChanging="gvVisitTargetPlanning_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:Label ID="lblBudgetPYWiseID" Text='<%# DataBinder.Eval(Container.DataItem, "BudgetPlanningYear.BudgetPYWiseID")%>' runat="server" Visible="false" /> 
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "BudgetPlanningYear.Dealer.DealerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "BudgetPlanningYear.Dealer.DealerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "BudgetPlanningYear.Model.Model")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "BudgetPlanningYear.Year")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Planed"  ItemStyle-HorizontalAlign="right">
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtPlaned" Text='<%# DataBinder.Eval(Container.DataItem, "Planed")%>' runat="server" CssClass="form-control" TextMode="Number" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActual" Text='<%# DataBinder.Eval(Container.DataItem, "Actual")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn Save" Text="Update" OnClick="btnUpdate_Click" Width="150px"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
