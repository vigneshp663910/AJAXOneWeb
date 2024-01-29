<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="ViewDealerBusinessExcellence.ascx.cs" Inherits="DealerManagementSystem.ViewBusinessExcellence.UserControls.ViewDealerBusinessExcellence" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnSubmit" runat="server" OnClick="lbActions_Click">Submit</asp:LinkButton>
                <asp:LinkButton ID="lbtnApproveL1" runat="server" OnClick="lbActions_Click">Approve L1</asp:LinkButton>
                <asp:LinkButton ID="lbtnApproveL2" runat="server" OnClick="lbActions_Click">Approve L2</asp:LinkButton>
                <asp:LinkButton ID="lbtnApproveL3" runat="server" OnClick="lbActions_Click">Approve L3</asp:LinkButton>
                <asp:LinkButton ID="lbtnApproveL4" runat="server" OnClick="lbActions_Click">Approve L4</asp:LinkButton> 
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border"> 
        <legend style="background: none; color: #007bff; font-size: 17px;">Dealer Business Excellence</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Year : </label>
                <asp:Label ID="lblYear" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Month : </label>
                <asp:Label ID="lblMonth" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Dealer : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Dealer Name : </label>
                <asp:Label ID="lblDealerName" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Requested By: </label>
                <asp:Label ID="lblRequestedBy" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Requested On : </label>
                <asp:Label ID="lblRequestedOn" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Submitted By : </label>
                <asp:Label ID="lblSubmittedBy" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Submitted On : </label>
                <asp:Label ID="lblSubmittedOn" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL1 By : </label>
                <asp:Label ID="lblApprovalL1By" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL1 On : </label>
                <asp:Label ID="lblApprovalL1On" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL2 By : </label>
                <asp:Label ID="lblApprovalL2By" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL2 On : </label>
                <asp:Label ID="lblApprovalL2On" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL3 By : </label>
                <asp:Label ID="lblApprovalL3By" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL3 On : </label>
                <asp:Label ID="lblApprovalL3On" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL4 By : </label>
                <asp:Label ID="lblApprovalL4By" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>ApprovalL4 On : </label>
                <asp:Label ID="lblApprovalL4On" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpCust" runat="server"   Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlSalesEngineer" runat="server" HeaderText="Function Area" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvDealer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found"   >
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Function Area" >
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFunctionArea" Text='<%# DataBinder.Eval(Container.DataItem, "FunctionArea.FunctionArea")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Function Sub Area"  >
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory2" Text='<%# DataBinder.Eval(Container.DataItem, "FunctionSubArea.FunctionSubArea")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parameter"  >
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblParameterID" Text='<%# DataBinder.Eval(Container.DataItem, "Parameter.DealerBusinessExcellenceCategory3ID")%>' runat="server" Visible="false" />
                                    <asp:Label ID="lblParameter" Text='<%# DataBinder.Eval(Container.DataItem, "Parameter.Parameter")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target"  ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTarget" Text='<%# DataBinder.Eval(Container.DataItem, "Target")%>' runat="server" />
                                    <asp:TextBox ID="txtTarget" Text='<%# DataBinder.Eval(Container.DataItem, "Target")%>'
                                        runat="server" CssClass="form-control" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual"   ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblActual" Text='<%# DataBinder.Eval(Container.DataItem, "Actual")%>' runat="server" />
                                    <asp:TextBox ID="txtActual" Text='<%# DataBinder.Eval(Container.DataItem, "Actual")%>' runat="server" CssClass="form-control" Visible="false" onblur="Calculation(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks"   ItemStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" />
                                    <asp:TextBox ID="txtRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server" CssClass="form-control" Visible="false" onblur="Calculation(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkBtnMissionPlanningEdit" runat="server" OnClick="btnEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                    <asp:Button ID="BtnUpdateMissionPlanning" runat="server" Text="Update" CssClass="btn Back" OnClick="BtnUpdateMissionPlanning_Click" Width="70px" Height="33px" Visible="false" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnUpdateMissionPlanning_Click" Width="70px" Height="33px" Visible="false" />
                                     
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
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>
