<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EquipmentMissing.aspx.cs" Inherits="DealerManagementSystem.ViewEquipment.EquipmentMissing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <div class="col-md-12">
                <div class="col-md-12">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 col-sm-12">
                                <label class="modal-label">Equipment</label>
                                <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true"   OnClick="btnSearch_Click" />
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
                            <asp:GridView ID="gvEquipment" runat="server" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false" >
                                <Columns> 
                                    <asp:TemplateField HeaderText="Model">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentModel.Model")%>' runat="server" />
                                            <asp:Label ID="lblEquipmentHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentHeaderID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Engine SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEngineSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EngineSerialNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipment SerialNo">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentSerialNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model Description">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblModelDescription" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentModel.ModelDescription")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Code">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dispatched On">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDispatchedOn" Text='<%# DataBinder.Eval(Container.DataItem, "DispatchedOn", "{0:d}")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warranty ExpiryDate">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyExpiryDate", "{0:d}")%>' runat="server" />
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
    </div>
</asp:Content>
