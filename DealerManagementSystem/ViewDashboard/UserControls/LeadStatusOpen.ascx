<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusOpen.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusOpen" %>
<div class="modbox"> 
    <div class="modboxin">
        <div class="portlet-body" style="padding: 5px;">
            <div id="divLeadStatistics" class="row no-margin" style="font-size: medium; text-align: right;">
                <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                    <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                        <div class="visual"><i class="fa fa-ticket"></i></div>
                        <div class="details" style="color: white;">
                            <asp:Label ID="lblOpen" runat="server" Text="0"></asp:Label>
                            <div class="desc">
                                <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Open</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
        </div>
    </div>
</div>
