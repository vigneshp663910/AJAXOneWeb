<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="IncidentPer100.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.IncidentPer100" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/UserControls/MultiSelectDropDown.ascx" TagPrefix="UC" TagName="UC_M_Dealer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="div1" runat="server">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-1 col-sm-12">
                        <label class="modal-label">Grapg Type</label>
                        <asp:DropDownList ID="ddlGrapgType" runat="server"   CssClass="form-control">
                            <asp:ListItem Value="1">Month</asp:ListItem>
                            <asp:ListItem Value="2">Quarter</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Mfg Date From</label>
                        <asp:TextBox ID="txtMfgDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtMfgDateFrom" PopupButtonID="txtMfgDateFrom" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                        <%-- <asp1:TextBoxWatermarkExtender ID="Text1" runat="server" TargetControlID="txtMfgDateFrom" WatermarkText="DD/MM/YYYY"></asp1:TextBoxWatermarkExtender>
                        --%>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">MfgDate To</label>
                        <asp:TextBox ID="txtMfgDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMfgDateTo" PopupButtonID="txtMfgDateTo" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                        <%-- <asp1:TextBoxWatermarkExtender ID="Text2" runat="server" TargetControlID="txtMfgDateTo" WatermarkText="DD/MM/YYYY"></asp1:TextBoxWatermarkExtender>
                        --%>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">As on Date</label>
                        <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        <asp1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAsOnDate" PopupButtonID="txtAsOnDate" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                        <%-- <asp1:TextBoxWatermarkExtender ID="Text1" runat="server" TargetControlID="txtMfgDateFrom" WatermarkText="DD/MM/YYYY"></asp1:TextBoxWatermarkExtender>
                        --%>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Dealer</label>
                        <UC:UC_M_Dealer ID="ddlmDealer" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Region</label>
                        <UC:UC_M_Dealer ID="ddlmRegion" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Service Type</label>
                        <UC:UC_M_Dealer ID="ddlmServiceType" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Division</label>
                        <UC:UC_M_Dealer ID="ddlmDivision" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Model</label>
                        <UC:UC_M_Dealer ID="ddlmModel" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                    </div>

                    <div class="col-md-2 text-left">
                        <label>HMR</label>
                        <UC:UC_M_Dealer ID="ddlmHMR" runat="server" CssClass="form-control"></UC:UC_M_Dealer>
                    </div>
                    <div class="col-md-1 text-left">
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    </div>
                    <div class="col-md-1 text-left">
                        <asp:Button ID="BtnLineChartData" runat="server" CssClass="btn Search" Text="Line Chart Data" OnClick="BtnLineChartData_Click" Width="117px"></asp:Button>
                    </div>
                    <div class="col-md-1 text-left">
                        <asp:Button ID="BtnDetailData" runat="server" CssClass="btn Search" Text="Detail data" OnClick="BtnDetailData_Click" Width="90px"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>


    <div class="col-md-12 Report">
        <div class="table-responsive">
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Incidence / 100 Mc</legend>
                <div class="col-md-12 Report">
                <div id="divRegionEast"></div>
                    </div>
            </fieldset>
        </div>
    </div>

    <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Data</legend>
                <div class="col-md-12 Report">

                    <!-- GridView -->
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"  ShowFooter="false" EmptyDataText="No Data Found" OnPageIndexChanging="gvData_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="FinYear">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "FinYear")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month/Quarter">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Quarter")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-15">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-15")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-250">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-250")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-500">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-500")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-750">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-750")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-1000">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-1000")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-1500">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-1500")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR 0-2000">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR 0-2000")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HMR All">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "HMR All")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle CssClass="FooterStyle" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>

                </div>
            </fieldset>
        </div>
    </div>



    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript"> 
        function RegionEastChart() {
            //var param = {
            //    Dealer: $('#MainContent_ddlmDealer_btnView').val()
            //    , LeadDateFrom: $('#MainContent_txtMfgDateFrom').val()
            //    , LeadDateTo: $('#MainContent_txtMfgDateTo').val()
            //    , Country: ''
            //    , Region: $('#id="MainContent_ddlmRegion_btnView"').val()
            //    , ProductType: ''
            //}
            var param = {
                DateFrom: $('#MainContent_txtMfgDateFrom').val()
                , DateTo: $('#MainContent_txtMfgDateTo').val()
                , AsOnDate: $('#MainContent_txtAsOnDate').val()
            }
            $.ajax({
                type: "POST",
                url: 'IncidentPer100.aspx/IncidentPer',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {

                    var data1 = google.visualization.arrayToDataTable(data.d);
                    var view = new google.visualization.DataView(data1);

                    //view.setColumns([0, 1,
                    //    //{
                    //    //    calc: "stringify",
                    //    //    sourceColumn: 1,
                    //    //    type: "string",
                    //    //    role: "annotation"
                    //    //},
                    //    2, 3, 4, 5
                    //]);
                    var GraphType = document.querySelector('#MainContent_ddlGrapgType')
                    var title1 = 'Incident Per 100 Machine - Mfg. ' + GraphType.selectedOptions[0].text + ' wise';
                    var options = {
                        title: title1,
                        hAxis: {
                            title: GraphType.selectedOptions[0].text
                        },
                        vAxis: {
                            title: 'Cost / Machine'
                        },
                        //width: '80%',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true,
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        }
                    };
                    var chart = new google.visualization.LineChart(document.getElementById("divRegionEast"));
                    chart.draw(view, options);
                },
                failure: function (r) {
                    alert(r);
                },
                error: function (r) {
                    alert(r);
                }
            });
        }
    </script>

</asp:Content>
