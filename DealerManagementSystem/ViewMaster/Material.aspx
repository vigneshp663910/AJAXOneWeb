<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Material" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewMaster/UserControls/MaterialView.ascx" TagPrefix="UC" TagName="UC_MaterialView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12" id="divList" runat="server">
        <asp:TabContainer ID="tabConMaterial" runat="server" ToolTip="Material" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
            <asp:TabPanel ID="tabbPnlDivision" runat="server" HeaderText="Division" Font-Bold="True" ToolTip="Division...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                                <div class="col-md-12 Report">
                                    <tr>
                                        <td>
                                            <span id="txnHistory1:refreshDataGroup">
                                                <div style="background-color: white">
                                                    <asp:GridView ID="gvDivision" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                                        OnPageIndexChanging="gvDivision_PageIndexChanging" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionCode" Text='<%# DataBinder.Eval(Container.DataItem, "DivisionCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division Description" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionDescription" Text='<%# DataBinder.Eval(Container.DataItem, "DivisionDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitOfMeasurement" Text='<%# DataBinder.Eval(Container.DataItem, "UOM")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warranty Delivery Hours" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWarrantyDeliveryHours" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyDeliveryHours")%>' runat="server"></asp:Label>
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
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabPnlModel" runat="server" HeaderText="Model" Font-Bold="True" ToolTip="Model...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Division</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
                                            <span id="txnHistory2:refreshDataGroup">
                                                <div class="boxHead">
                                                    <div class="logheading">
                                                        <div style="float: left">
                                                            <table>
                                                                <tr>
                                                                    <td>SAP Material Group | Model(s):</td>

                                                                    <td>
                                                                        <asp:Label ID="lblRowCountM" runat="server" CssClass="label"></asp:Label></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnModelArrowLeft1" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnModelArrowLeft_Click" /></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnModelArrowRight1" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnModelArrowRight_Click" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div style="background-color: white">
                                                    <asp:GridView ID="gvMaterailModel" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="10" EmptyDataText="No Data Found"
                                                        OnPageIndexChanging="gvMaterailModel_PageIndexChanging" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division" HeaderStyle-Width="80px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "Division.DivisionCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model" HeaderStyle-Width="160px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model Code" HeaderStyle-Width="160px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModelCode" Text='<%# DataBinder.Eval(Container.DataItem, "ModelCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model Description">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModelDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ModelDescription")%>' runat="server"></asp:Label>
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
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabPnlMaterial" runat="server" HeaderText="Material" Font-Bold="True" ToolTip="Material...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">

                                    <div class="col-md-2 text-left">
                                        <label>Division</label>
                                        <asp:DropDownList ID="ddlDivisionMC" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label>Material Model</label>
                                        <asp:DropDownList ID="ddlMaterialModel" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label>Material Status</label>
                                        <%--<asp:CheckBox ID="cbActive" runat="server" Checked="true" />--%>
                                        <asp:DropDownList ID="ddlMaterialStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true">
                                            <asp:ListItem Value=""> Select</asp:ListItem>
                                            <asp:ListItem Value="1"> Active</asp:ListItem>
                                            <asp:ListItem Value="0"> Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label>Material Code</label>
                                        <asp:TextBox ID="txtMaterialCode" runat="server" placeholder="Material Code" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 text-right">
                                        <br />
                                        <asp:Button ID="btnMaterialSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialSearch_Click" OnClientClick="return dateValidation();" />
                                        <%-- &nbsp;<asp:Button ID="btnMaterialExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialExportExcel_Click" Width="125px" />--%>
                                    </div>

                                    <div class="col-md-1 text-left">
                                        <br />
                                        <asp:Button ID="btnMaterialExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialExportExcel_Click" Width="125px" />
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
                                                                    <td>Material(s):</td>

                                                                    <td>
                                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnMaterialArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnMaterialArrowLeft_Click" /></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnMaterialArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnMaterialArrowRight_Click" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="background-color: white">

                                                    <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                                        OnPageIndexChanging="gvMaterial_PageIndexChanging" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material" HeaderStyle-Width="120px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Material Desc">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Reso, BaseUnit%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBaseUnit" Text='<%# DataBinder.Eval(Container.DataItem, "BaseUnit")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialType%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialType" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialType")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Division Code">
                                                                <%--<ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialGroup" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialGroup")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>--%>
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionCode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.Division.DivisionCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mode Code">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModeCode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.ModelCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mode">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.Model")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model Description">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblModelDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Model.ModelDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Reso, GrossWeight%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrossWeight" Text='<%# DataBinder.Eval(Container.DataItem, "GrossWeight","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Reso, NetWeight%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "NetWeight","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="<%$ Resources:Reso, WeightUnit%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWeightUnit" Text='<%# DataBinder.Eval(Container.DataItem, "WeightUnit")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialDivision%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDivision" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDivision")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HSN">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "HSN")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="CST %">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCST" Text='<%# DataBinder.Eval(Container.DataItem, "CST","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SST %">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSST" Text='<%# DataBinder.Eval(Container.DataItem, "SST","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GST %">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGST" Text='<%# DataBinder.Eval(Container.DataItem, "GST","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnViewMaterial" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MaterialID")%>' Text="View" CssClass="btn Back" OnClick="btnViewMaterial_Click" Width="75px" Height="25px" />
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
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tablPnlMaterialPrice" runat="server" HeaderText="Material Price" Font-Bold="True" ToolTip="Material Price...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Material Code</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtMaterialCodePrice" runat="server" placeholder="Material Code" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Button ID="btnMaterialPriceSerach" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialPriceSerach_Click" OnClientClick="return dateValidation();" />
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
                                            <%--<span id="txnHistory2:refreshDataGroup">--%>
                                            <%--<div class="boxHead">
                                                <div class="logheading">
                                                    <div style="float: left">
                                                        <table>
                                                            <tr>
                                                                <td>Material Price Report</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div style="background-color: white">

                                                <asp:GridView ID="gvMaterialPrice" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"
                                                    Width="100%">
                                                    <Columns>
                                                        <%--<asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Material" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Material Desc">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax%">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaxPercentage" Text='<%# DataBinder.Eval(Container.DataItem, "TaxPercentage","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurrentPrice" Text='<%# DataBinder.Eval(Container.DataItem, "CurrentPrice","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HSN">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "HSN")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, BaseUnit%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBaseUnit" Text='<%# DataBinder.Eval(Container.DataItem, "BaseUnit")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialType%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialType" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialType")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialGroup%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialGroup" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialGroup")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, GrossWeight%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrossWeight" Text='<%# DataBinder.Eval(Container.DataItem, "GrossWeight","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, NetWeight%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "NetWeight","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, WeightUnit%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWeightUnit" Text='<%# DataBinder.Eval(Container.DataItem, "WeightUnit")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialDivision%>">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialDivision" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDivision")%>' runat="server"></asp:Label>
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
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tablPnlMaterSupersede" runat="server" HeaderText="Material Supersede" Font-Bold="True" ToolTip="Material Supersede...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 text-right">
                                        <label>Material Code</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtMaterialCodeSupersede" runat="server" placeholder="Material Code" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-1 text-right">
                                        <asp:Button ID="btnMaterailSupersedeSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterailSupersedeSearch_Click" OnClientClick="return dateValidation();" />
                                    </div>
                                    <div class="col-md-1 text-left">
                                        <asp:Button ID="btnMaterailSupersedeExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialSupersedeExportExcel_Click" Width="125px" />
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
                                            <span id="txnHistory4:refreshDataGroup">
                                                <div class="boxHead">
                                                    <div class="logheading">
                                                        <div style="float: left">
                                                            <table>
                                                                <tr>
                                                                    <td>Material Supersede :</td>

                                                                    <td>
                                                                        <asp:Label ID="lblMaterialSupersedeCount" runat="server" CssClass="label"></asp:Label></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnMaterialSupersedeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnMaterialSupersedeArrowLeft_Click" /></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnMaterialSupersedeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnMaterialSupersedeArrowRight_Click" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="background-color: white">

                                                    <asp:GridView ID="gvMaterialSupersede" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                                        OnPageIndexChanging="gvMaterialSupersede_PageIndexChanging" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Material" HeaderStyle-Width="120px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Desc">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supersede" HeaderStyle-Width="120px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Supersede.Material")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supersede Mat Desc">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Supersede.MaterialDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supersede Remarks">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "Supersede.Description")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid From">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialValidFrom" Text='<%# DataBinder.Eval(Container.DataItem, "Supersede.ValidFrom","{0:d}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valid To">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValidTo" Text='<%# DataBinder.Eval(Container.DataItem, "Supersede.ValidTo","{0:d}")%>' runat="server"></asp:Label>
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
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tblPnlMaterialVariant" runat="server" HeaderText="Material Category Type" Font-Bold="True" ToolTip="Material Category...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label>Product Type</label>
                                        <asp:DropDownList ID="ddlSProductType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label class="modal-label">-</label>
                                        <asp:Button ID="btnMatVariantTypeSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="btnMatVariantTypeSearch_Click" />
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
                                            <div class="boxHead">
                                                <div class="logheading">
                                                    <div style="float: left">
                                                        <table>
                                                            <tr>
                                                                <td>Material Category(s) :</td>

                                                                <td>
                                                                    <asp:Label ID="lblMatVariantTypeRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnMatVariantTypeArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnMatVariantTypeArrowLeft_Click" /></td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnMatVariantTypeArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnMatVariantTypeArrowRight_Click" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="background-color: white">
                                                <asp:GridView ID="GVMatVariantType" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                                    OnPageIndexChanging="GVMatVariantType_PageIndexChanging" ShowFooter="true" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductType" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductType")%>' runat="server" />
                                                                <asp:Label ID="lblProductTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType.ProductTypeID")%>' runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariantName" Text='<%# DataBinder.Eval(Container.DataItem, "VariantName")%>' runat="server" />
                                                                <asp:Label ID="lblVariantTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "VariantTypeID")%>' runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtVariantName" runat="server" placeholder="Material Category" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Max To Select">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaxToSelect" Text='<%# DataBinder.Eval(Container.DataItem, "MaxToSelect")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtMaxToSelect" runat="server" placeholder="Max To Select" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lblMatVariantTypeEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VariantTypeID")%>' OnClick="lblMatVariantTypeEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblMatVariantTypeDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "VariantTypeID")%>' OnClientClick="return ConfirmDeleteMaterialVariantType();" OnClick="lblMatVariantTypeDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="BtnAddMatVariantType" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddMatVariantType_Click" Width="70px" Height="33px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="#ffffff" />
                                                    <FooterStyle ForeColor="White" />
                                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="HidMatVariantType" runat="server" Visible="false" />
                                            </div>
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tblPnlMaterialVariantTypeMapping" runat="server" HeaderText="Material Category Mapping" Font-Bold="True" ToolTip="Material Category Mapping...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    <div class="col-md-2 col-sm-12">
                                        <label>Product Type</label>
                                        <asp:DropDownList ID="ddlSMProductType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSMProductType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label>Product</label>
                                        <asp:DropDownList ID="ddlSMProduct" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-12">
                                        <label>Material Category</label>
                                        <asp:DropDownList ID="ddlSMVariantName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div> 
                                    <div class="col-md-2 col-sm-12">
                                        <label class="modal-label">Material Code</label>
                                        <asp:TextBox ID="txtSMMaterial" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 text-left">
                                        <label class="modal-label">-</label>
                                        <asp:Button ID="btnMatVariantTypeMappingSearch" runat="server" Text="Retrieve" CssClass="btn Search" OnClick="btnMatVariantTypeMappingSearch_Click" />
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
                                            <div class="boxHead">
                                                <div class="logheading">
                                                    <div style="float: left">
                                                        <table>
                                                            <tr>
                                                                <td>Material Category Mapping(s) :</td>

                                                                <td>
                                                                    <asp:Label ID="lblMatVariantTypeMappingRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnMatVariantTypeMappingArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnMatVariantTypeMappingArrowLeft_Click" /></td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnMatVariantTypeMappingArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnMatVariantTypeMappingArrowRight_Click" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="background-color: white">
                                                <asp:GridView ID="GVMatVariantTypeMapping" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" EmptyDataText="No Data Found"
                                                    OnPageIndexChanging="GVMatVariantTypeMapping_PageIndexChanging" ShowFooter="true" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMappingProductType" Text='<%# DataBinder.Eval(Container.DataItem, "VariantType.ProductType.ProductType")%>' runat="server" />
                                                                <asp:Label ID="lblMappingProductTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "VariantType.ProductType.ProductTypeID")%>' runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddlAddProductType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAddProductType_SelectedIndexChanged"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariantName" Text='<%# DataBinder.Eval(Container.DataItem, "VariantType.VariantName")%>' runat="server" />
                                                                <asp:Label ID="lblVariantTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "VariantType.VariantTypeID")%>' runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddlAddVariantType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProduct" Text='<%# DataBinder.Eval(Container.DataItem,"Product") == null ? "ALL" : DataBinder.Eval(Container.DataItem,"Product.Product") %>' runat="server" />
                                                                <asp:Label ID="lblProductID" Text='<%# DataBinder.Eval(Container.DataItem,"Product") == null ? null : DataBinder.Eval(Container.DataItem,"Product.ProductID") %>' runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddlAddProduct" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode") + " " + DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddMaterial" runat="server" placeholder="Material" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lblMatVariantTypeMappingEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MaterialVariantTypeMappingID")%>' OnClick="lblMatVariantTypeMappingEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblMatVariantTypeMappingDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MaterialVariantTypeMappingID")%>' OnClientClick="return ConfirmDeleteMaterialVariantTypeMapping();" OnClick="lblMatVariantTypeMappingDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="BtnAddMatVariantTypeMapping" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAddMatVariantTypeMapping_Click" Width="70px" Height="33px" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="#ffffff" />
                                                    <FooterStyle ForeColor="White" />
                                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="HidMatVariantTypeMapping" runat="server" Visible="false" />
                                            </div>
                                            </span>
                                        </td>
                                    </tr>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    <div class="col-md-12" id="divMaterialView" runat="server" visible="false">
        <div class="" id="boxHere"></div>
        <div class="back-buttton" id="backBtn">
            <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
        </div>
        <div class="col-md-12" runat="server" id="tblDashboard">
            <UC:UC_MaterialView ID="UC_MaterialView" runat="server"></UC:UC_MaterialView>
            <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>            
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $("#MainContent_tabConMaterial_tblPnlMaterialVariantTypeMapping_GVMatVariantTypeMapping_txtAddMaterial").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_tabConMaterial_tblPnlMaterialVariantTypeMapping_GVMatVariantTypeMapping_txtAddMaterial').val() };
                    $.ajax({
                        url: "Material.aspx/SearchMaterial",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                        }
                    });
                },
                minLength: 2
            });
        });
        function ConfirmDeleteMaterialVariantType() {
            var x = confirm("Are you sure you want to Delete Material Category?");
            if (x) {
                return true;
            }
            else
                return false;
        }
        function ConfirmDeleteMaterialVariantTypeMapping() {
            var x = confirm("Are you sure you want to Delete Material Category Mapping?");
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
