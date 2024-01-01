<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="DealerBusinessExcellenceReport.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Reports.DealerBusinessExcellenceReport" %>

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
            
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />
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
                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max Score" ItemStyle-Width="30px">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaxScore" Text='<%# DataBinder.Eval(Container.DataItem, "Max Score")%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblMaxScoreF" runat="server" ForeColor="Black" Font-Size="Large" />
                        </FooterTemplate>
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
                            <asp:Label ID="lblParameter" Text='<%# DataBinder.Eval(Container.DataItem, "Parameter")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Target" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                        <HeaderStyle BackColor="#ddebf7" />
                        <ItemTemplate>
                            <asp:Label ID="lblTarget" Text='<%# DataBinder.Eval(Container.DataItem, "Target")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                        <HeaderStyle BackColor="#c6e0b4" />
                        <ItemTemplate>
                            <asp:Label ID="lblActual" Text='<%# DataBinder.Eval(Container.DataItem, "Actual" )%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Achievement" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                        <HeaderStyle BackColor="#c6e0b4" />
                        <ItemTemplate>
                            <asp:Label ID="lblAchievement" Text='<%# DataBinder.Eval(Container.DataItem, "Achievement" )%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Minimum Qualifying" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                        <HeaderStyle BackColor="#c6e0b4" />
                        <ItemTemplate>
                            <asp:Label ID="lblMinimumQualifying" Text='<%# DataBinder.Eval(Container.DataItem, "Minimum Qualifying" )%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Final Score" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                        <HeaderStyle BackColor="#c6e0b4" />
                        <ItemTemplate>
                            <asp:Label ID="lblFinalScore" Text='<%# DataBinder.Eval(Container.DataItem, "Final Score" )%>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFinalScoreF" runat="server" ForeColor="Black" Font-Size="Large" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                        <HeaderStyle BackColor="#c6e0b4" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks" )%>' runat="server" />
                        </ItemTemplate>
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
</asp:Content>
