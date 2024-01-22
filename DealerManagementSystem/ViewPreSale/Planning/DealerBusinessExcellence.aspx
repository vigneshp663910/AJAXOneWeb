<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="DealerBusinessExcellence.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Planning.DealerBusinessExcellence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />

    <div class="col-md-12" id="divList" runat="server">
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
                    <label class="modal-label">Dealer</label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Function Area</label>
                    <asp:DropDownList ID="ddlFunctionArea" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFunctionArea_SelectedIndexChanged" />
                </div>
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Function Sub Area</label>
                    <asp:DropDownList ID="ddlFunctionSubArea" runat="server" CssClass="form-control" />
                </div>
                 
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button> 
                     <asp:Button ID="btnSubmit" runat="server" CssClass="btn Search" Text="Submit" OnClick="btnSubmit_Click"></asp:Button> 
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
                                        <%--<td>
                                            <asp:ImageButton ID="ibtnVTArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnVTArrowLeft_Click" /></td>
                                        <td>
                                            <asp:ImageButton ID="ibtnVTArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnVTArrowRight_Click" /></td>--%>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>


                    <asp:GridView ID="gvMissionPlanning" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found" OnDataBound="OnDataBound" OnPageIndexChanging="gvVisitTarget_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Year" SortExpression="Year" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lblDealerBusinessExcellenceID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBusinessExcellenceID")%>' runat="server" Visible="false" />--%>
                                    <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month" SortExpression="Month" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code" ItemStyle-Width="30px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Function Area" ItemStyle-Width="30px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFunctionArea" Text='<%# DataBinder.Eval(Container.DataItem, "FunctionArea")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Function Sub Area" ItemStyle-Width="30px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                     <asp:Label ID="lblCategory2" Text='<%# DataBinder.Eval(Container.DataItem, "Category2")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Parameter" ItemStyle-Width="30px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblParameterID" Text='<%# DataBinder.Eval(Container.DataItem, "DealerBusinessExcellenceCategory3ID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblParameter" Text='<%# DataBinder.Eval(Container.DataItem, "Parameter")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTarget" Text='<%# DataBinder.Eval(Container.DataItem, "Target")%>' runat="server" />
                                    <asp:TextBox ID="txtTarget" Text='<%# DataBinder.Eval(Container.DataItem, "Target")%>'
                                        runat="server" CssClass="form-control"   Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblActual" Text='<%# DataBinder.Eval(Container.DataItem, "Actual")%>' runat="server" />
                                    <asp:TextBox ID="txtActual" Text='<%# DataBinder.Eval(Container.DataItem, "Actual")%>' runat="server" CssClass="form-control"   Visible="false" onblur="Calculation(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" />
                                    <asp:TextBox ID="txtRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" CssClass="form-control"  Visible="false" onblur="Calculation(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnMissionPlanningEdit" runat="server" OnClick="btnEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                    <asp:Button ID="BtnUpdateMissionPlanning" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnUpdateMissionPlanning_Click" Width="70px" Height="33px" Visible="false" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnUpdateMissionPlanning_Click" Width="70px" Height="33px" Visible="false" />
                                    <asp:CheckBox ID="cbIsSubmitted" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSubmitted")%>'  Visible="false"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset> 
    </div>

</asp:Content>
