<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Material" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp:HiddenField ID="HiddenID" runat="server" Visible="false" />
    <div class="col-md-12">
        <asp:TabContainer ID="tabConMaterial" runat="server" ToolTip="Material" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
            <asp:TabPanel ID="tabbPnlDivision" runat="server" HeaderText="Division" Font-Bold="True" ToolTip="<Division...">
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
                                                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division Code" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionCode" Text='<%# DataBinder.Eval(Container.DataItem, "DivisionCode")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division Description" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionDescription" Text='<%# DataBinder.Eval(Container.DataItem, "DivisionDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
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
                                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
            <asp:TabPanel ID="tabPnlModel" runat="server" HeaderText="Model" Font-Bold="True" ToolTip="<Division...">
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
                                                                        <asp:ImageButton ID="ibtnModelArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnModelArrowLeft_Click" /></td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ibtnModelArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnModelArrowRight_Click" /></td>
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
                                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
            <asp:TabPanel ID="tabPnlMaterial" runat="server" HeaderText="Material" Font-Bold="True" ToolTip="<Material...">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <fieldset class="fieldset-border">
                                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                                <div class="col-md-12">
                                    
                                    <div class="col-md-2 text-right">
                                        <label>Division</label>
                                        <asp:DropDownList ID="ddlDivisionMC" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Material Model</label>
                                        <asp:DropDownList ID="ddlMaterialModel" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <label>Material Code</label>
                                         <asp:TextBox ID="txtMaterialCode" runat="server" placeholder="Material Code" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2 text-right">
                                        <label>Material Status</label>
                                       <%--<asp:CheckBox ID="cbActive" runat="server" Checked="true" />--%>
                                        <asp:DropDownList ID="ddlMaterialStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true">
                                            <asp:ListItem Value=""> Select</asp:ListItem>
                                            <asp:ListItem Value="1"> Active</asp:ListItem>
                                            <asp:ListItem Value="0"> Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                 <%--   <div class="col-md-12 text-center">--%>
                                     <div class="col-md-2 text-right">
                                      <br />
                                        <asp:Button ID="btnMaterialSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialSearch_Click" OnClientClick="return dateValidation();" />
                                        &nbsp;<asp:Button ID="btnMaterialExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialExportExcel_Click" Width="125px" />
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
                                                                    <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "CST","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SST %">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "SST","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GST %">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "GST","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#ffffff" />
                                                        <FooterStyle ForeColor="White" />
                                                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
            <asp:TabPanel ID="tablPnlMaterialPrice" runat="server" HeaderText="Material Price" Font-Bold="True" ToolTip="<Material Price...">
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
                                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
            <asp:TabPanel ID="tablPnlMaterSupersede" runat="server" HeaderText="Material Supersede" Font-Bold="True" ToolTip="<Material supersede...">
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

                                    <div class="col-md-2">
                                        <asp:Button ID="btnMaterailSupersedeSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterailSupersedeSearch_Click" OnClientClick="return dateValidation();" />
                                        &nbsp;<asp:Button ID="btnMaterailSupersedeExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialSupersedeExportExcel_Click" Width="125px" />
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
                                                                    <td>Material Supersede Report</td>

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
                                                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
            <asp:TabPanel ID="tabPnlReOrderLevel" runat="server" HeaderText="Re-Order Level" Font-Bold="True" ToolTip="<Re-Order Level...">
                <ContentTemplate>
                    <h3>Re-OrderLevel</h3>
                    <p>Re-Order Level Master</p>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabPnlEquipment" runat="server" HeaderText="Equipment" Font-Bold="True" ToolTip="<Equipment...">
                <ContentTemplate>
                    <h3>Equipment Master</h3>
                    <p>Equipment Master</p>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
