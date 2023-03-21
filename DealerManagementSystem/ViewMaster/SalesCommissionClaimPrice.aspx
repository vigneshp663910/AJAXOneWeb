<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesCommissionClaimPrice.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.SalesCommissionClaimPrice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <%--<asp:TabContainer ID="tabConMaterial" runat="server" ToolTip="Material" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
            <asp:TabPanel ID="tabPnlSalCommClaimPrice" runat="server" HeaderText="Sales Commission Claim Price" Font-Bold="True" ToolTip="Sales Commission Claim Price...">--%>
        <contenttemplate>
            <div class="col-md-12">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                           <%-- <div class="col-md-2 text-left">
                                <label>Plant</label>
                                <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                <%--OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="true"--%>
                            <%--</div>--%>
                            <div class="col-md-2 text-left">
                                <label>Material</label>
                                <%--<asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            </div>
                            <div class="col-md-1 text-right">
                                <br />
                                <asp:Button ID="btnSalCommClaimPriceSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSalCommClaimPriceSearch_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <tr>
                                <td>
                                    <span id="txnHistory3:refreshDataGroup">
                                        <div class="boxHead">
                                            <div class="logheading">
                                                <div style="float: left">
                                                    <table>
                                                        <tr>
                                                            <td>Sales Commission Claim Price(s):</td>

                                                            <td>
                                                                <asp:Label ID="lblRowCountSalCommClaimPrice" runat="server" CssClass="label"></asp:Label></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnSalCommClaimPriceArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnSalCommClaimPriceArrowLeft_Click" /></td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnSalCommClaimPriceArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnSalCommClaimPriceArrowRight_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="background-color: white">
                                            <asp:GridView ID="gvSalCommClaimPrice" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                                OnPageIndexChanging="gvSalCommClaimPrice_PageIndexChanging" Width="100%" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lblSalesCommissionClaimPriceID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimPriceID")%>' runat="server" Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Material Code">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Materail.MaterialCode")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Materail.MaterialID")%>' runat="server" Enabled="false" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                             <asp:TextBox ID="txtMaterailCode" runat="server" placeholder="Materail Code" CssClass="form-control"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Description">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Materail.MaterialDescription")%>' runat="server"></asp:Label>
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPercentage" Text='<%# DataBinder.Eval(Container.DataItem, "Percentage")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtPercentage" runat="server" placeholder="Percentage" CssClass="form-control"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount (INR)">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnSalCommClaimPriceEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimPriceID")%>' OnClick="lnkBtnSalCommClaimPriceEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkBtnSalCommClaimPriceDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimPriceID")%>' OnClick="lnkBtnSalCommClaimPriceDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Button ID="BtnAddOrUpdateSalCommClaimPrice" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddOrUpdateSalCommClaimPrice_Click" Width="70px" Height="33px" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#ffffff" />
                                                <FooterStyle ForeColor="White" />
                                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </div>
                                    </span>
                                </td>
                            </tr>
                        </div>
                    </fieldset>
                </div>
            </div>
        </contenttemplate>
        <%--</asp:TabPanel>
        </asp:TabContainer>--%>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            GridRowDisable('MainContent_gvSalCommClaimPrice', 'MainContent_gvSalCommClaimPrice_lblMaterialCode_')
            //var gvSalCommClaimPrice = document.getElementById('MainContent_tbpLocation_tbpnlCountry_gvCountry');

            //if (gvSalCommClaimPrice != null) {
            //    for (var i = 0; i < gvSalCommClaimPrice.rows.length - 1; i++) {
            //        var lblMaterialCode = document.getElementById('MainContent_gvSalCommClaimPrice_lblMaterialCode_' + i);
            //        if (lblMaterialCode != null) {
            //            if (lblMaterialCode.innerHTML == "") {
            //                lblMaterialCode.parentNode.parentNode.style.display = "none";
            //            }
            //        }
            //    }
            //}
        });

        function GridRowDisable(gv, lbl) {
            var gvCountry = document.getElementById(gv);
            if (gvCountry != null) {
                for (var i = 0; i < gvCountry.rows.length - 1; i++) {
                    var lblMaterialCode = document.getElementById(lbl + i);
                    if (lblMaterialCode != null) {
                        if (lblMaterialCode.innerHTML == "") {
                            lblMaterialCode.parentNode.parentNode.style.display = "none";
                        }
                    }
                }
            }
        }
    </script>
</asp:Content>
