<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcAssembly.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcAssembly" %>

<%@ Register Src="~/ViewECatalogue/UserControls/SpcAssemblyView.ascx" TagPrefix="UC" TagName="UC_SpcAssemblyView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .Popup {
            transition: initial;
        }
    </style>
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");
            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
    </script>
    <style>
        .Back {
            float: right;
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset id="fsCriteria" class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Division</label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Model</label>
                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                    <label>Assembly Code</label>
                    <asp:TextBox ID="txtAssemblyCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                    <div class="col-md-8">
                        <label class="modal-label">Action</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                        <asp:Button ID="btnCreateAssembly" runat="server" CssClass="btn Save" Text="Create Assembly" Width="120px" OnClick="btnCreateAssembly_Click"></asp:Button>
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
                                                <td>PO(s):</td>
                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                                <td>
                                                    <asp:ImageButton ID="imgBtnExportExcel" runat="server" ImageUrl="~/Images/Excel.jfif" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" ToolTip="Excel Download..." />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: right; overflow: auto;">
                                        <%--<div style="float :left">
                                             
                                        </div>--%>
                                        <div style="float: right">
                                            <img id="fs" alt="" src="../Images/NormalScreen.png" onclick="ScreenControl(2)" width="23" height="23" style="display: none;" />
                                            <img id="rs" alt="" src="../Images/FullScreen.jpg" onclick="ScreenControl(1)" width="23" height="23" style="display: block;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvAssembly" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15"
                                EmptyDataText="No Data Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White" HeaderStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="15px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="<i class='fa fa-eye fa-1x' aria-hidden='true'></i>" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnViewPO" ImageUrl="~/Images/Preview.png" runat="server" ToolTip="View..." Height="20px" Width="20px" ImageAlign="Middle" OnClick="btnViewPO_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpcAssemblyImageID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcAssemblyID")%>' runat="server" Visible="false" />

                                            <asp:ImageButton ID="imgDivision" runat="server" ImageUrl="~/Images/SpareParts.png" Width="25" Height="25" />
                                            <%--                                            <asp:Label ID="lblf_division" Text='<%# DataBinder.Eval(Container.DataItem, "Model.Division.DivisionCode")%>' runat="server"></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Model">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Model.Model")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ModelCode">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVendorCode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.ModelCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assembly Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssemblyCode" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AssemblyDescription">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AssemblyType">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssemblyType" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyType")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
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
        <div class="col-md-12" id="divDetailsView" runat="server" visible="false" style="padding: 5px 15px">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton">
                    <asp:Button ID="btnPurchaseOrderViewBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderViewBack_Click" />
                </div>
            </div>
            <UC:UC_SpcAssemblyView ID="UC_SpcAssemblyView" runat="server"></UC:UC_SpcAssemblyView>
        </div>
        <div class="col-md-12" id="divPurchaseOrderCreate" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere1"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnPurchaseOrderCreateBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnPurchaseOrderCreateBack_Click" />
                </div>
            </div>

        </div>
    </div>

    <asp:Panel ID="pnlAssemblyCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Assembly Create</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>

        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblAssemblyEditMessage" runat="server" Text="" CssClass="message" Visible="false" />
                <div class="col-md-6 col-sm-12">
                    <label>Model</label>
                    <asp:DropDownList ID="ddlModelAssemblyC" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label>AssemblyCode</label>
                    <asp:TextBox ID="txtAssemblyCodeC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label>AssemblyDescription</label>
                    <asp:TextBox ID="txtAssemblyDescription" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label>Model</label>
                    <asp:DropDownList ID="ddlAssemblyType" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Common">Common</asp:ListItem>
                        <asp:ListItem Value="Make Specific">Make Specific</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAssemblySave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAssemblySave_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
</asp:Content>
