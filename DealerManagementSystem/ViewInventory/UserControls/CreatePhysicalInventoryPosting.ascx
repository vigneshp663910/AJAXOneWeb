<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreatePhysicalInventoryPosting.ascx.cs" Inherits="DealerManagementSystem.ViewInventory.UserControls.CreatePhysicalInventoryPosting" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />

<fieldset class="fieldset-border" id="Fieldset2" runat="server">
    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
    <div class="col-md-12">
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Dealer</label>
            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
        </div>
        <div class="col-md-2 text-left">
            <label>Dealer Office</label>
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Document Number</label>
            <asp:TextBox ID="txtDocumentNumber" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Document Date</label>
            <asp:TextBox ID="txtDocumentDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
        </div>
        <div class="col-md-2 text-left">
            <label>Posting Inventory Type</label>
            <asp:DropDownList ID="ddlPostingInventoryType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Reason Of Posting</label>
            <asp:TextBox ID="txtReasonOfPosting" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
        </div>
         <div class="col-md-2 col-sm-12">
            <label class="modal-label">Total Material</label>
            <asp:label ID="lblTotalMaterial" runat="server" CssClass="form-control"></asp:label>
        </div>
        <div class="col-md-12">
            <div class="col-md-2 col-sm-12" id="Div1" runat="server">
            </div>
            <div class="col-md-12 col-sm-12">
                <asp:FileUpload ID="fileUpload" runat="server" />
                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn Save" OnClick="btnView_Click" Width="100px" />
                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" Width="100px" />
                <asp:Button ID="btnDownload" runat="server" Text="Download Template" CssClass="btn Search" OnClick="btnDownload_Click" Width="150px" />
            </div>
        </div>
    </div>
</fieldset>
<fieldset class="fieldset-border">
    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
    <div class="boxHead">
        <div class="logheading">
            <div style="float: left">
                <table>
                    <tr>
                        <td>Warehouse Stock : </td>
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
    <asp:GridView ID="GVUpload" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" PageSize="10" AllowPaging="true" EmptyDataText="No Data Found">
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
            <asp:TemplateField HeaderText="Material Description">
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Physical Stock">
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblPhysicalStock" Text='<%# DataBinder.Eval(Container.DataItem, "PhysicalStock")%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="System Stock">
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblSystemStock" Text='<%# DataBinder.Eval(Container.DataItem, "SystemStock")%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deference Quantity">
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblSystemStock" Text='<%# DataBinder.Eval(Container.DataItem, "DeferenceQuantity")%>' runat="server"></asp:Label>
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

