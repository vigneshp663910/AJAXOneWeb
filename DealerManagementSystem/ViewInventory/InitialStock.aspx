<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="InitialStock.aspx.cs" Inherits="DealerManagementSystem.ViewInventory.InitialStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12 Report" id="divList" runat="server">
            <div class="col-md-12">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Dealer</label>
                                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2 text-left">
                                <label>Division</label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-left">
                                <label>Material Model</label>
                                <asp:DropDownList ID="ddlMaterialModel" runat="server" CssClass="form-control" OnSelectedIndexChanged="btnMaterialSearch_Click" AutoPostBack="true"></asp:DropDownList>
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
                                <asp:Button ID="btnMaterialUpload" runat="server" Text="Material Upload" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnMaterialUpload_Click" Width="125px" />
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
                                                OnPageIndexChanging="gvMaterial_PageIndexChanging"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dealer Name">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Name")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dealer">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer Code")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Office Name">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOfficeName" Text='<%# DataBinder.Eval(Container.DataItem, "OfficeName")%>' runat="server"></asp:Label>
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

                                                    <asp:TemplateField HeaderText="Per Unit Price">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPerUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "PerUnitPrice")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Open Stock">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOpenStock" Text='<%# DataBinder.Eval(Container.DataItem, "OpenStock")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Posted By">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostedBy" Text='<%# DataBinder.Eval(Container.DataItem, "Posted By")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Posted On">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPostedBy" Text='<%# DataBinder.Eval(Container.DataItem, "Posted On")%>' runat="server"></asp:Label>
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
        </div>
        <div class="col-md-12 Report" id="divUpload" runat="server" visible="false">
            <fieldset class="fieldset-border" id="FldUpload" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Upload File</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealerO" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerO_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Office</label>
                        <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" AutoPostBack="true" />
                    </div>

                </div>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12" id="Div1" runat="server">
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <asp:FileUpload ID="fileUpload" runat="server" />
                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn Save" OnClick="btnView_Click" Width="100px" />
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" Width="100px" />
                        <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnBack_Click" />
                        <asp:Button ID="btnDownload" runat="server" Text="Download Template" CssClass="btn Search" OnClick="btnDownload_Click" Width="150px" />
                    </div>
                </div>
                <asp:GridView ID="GVUpload" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                    EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Code">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Per Unit Price">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblPerUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "PerUnitPrice")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </fieldset>
        </div>
    </div>
</asp:Content>
