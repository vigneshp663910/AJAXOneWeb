<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).on("load", function () {
            var hfTapActive = document.getElementById("MainContent_hfTapActive").value;
            $('#LICountry').removeClass("active");
            $('#LIState').removeClass("active");
            $('#LIDistrict').removeClass("active");
            $('#LICity').removeClass("active");

            $('#country').addClass("tab-pane fade");
            $('#state').addClass("tab-pane fade");
            $('#district').addClass("tab-pane fade");
            $('#city').addClass("tab-pane fade");

            if (hfTapActive == 1) {
                $('#LICountry').addClass("active");
                $('#country').addClass("tab - pane fade in active");
            }
            else if (hfTapActive == 2) {
                $('#LIState').addClass("active");
                $('#state').addClass("tab - pane fade in active");
            }
            else if (hfTapActive == 3) {
                $('#LIDistrict').addClass("active");
                $('#district').addClass("tab - pane fade in active");
            }
            else if (hfTapActive == 4) {
                $('#LICity').addClass("active");
                $('#city').addClass("tab - pane fade in active");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <br />
            <div>
                <h2><span class="MainHeader">Location</span></h2>
                <div style="height: 5px;" class="ProjectHeadingLine"></div>
            </div>
            <br />
        </div>
        <div class="col-md-12">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li id="LICountry"><a data-toggle="tab" href="#country"><i class="fa fa-address-card fa-fw" aria-hidden="true"></i>Country</a></li>
                <li id="LIState"><a data-toggle="tab" href="#state"><i class="fa fa-address-card fa-fw" aria-hidden="true"></i>State</a></li>
                <li id="LIDistrict"><a data-toggle="tab" href="#district"><i class="fa fa-address-card fa-fw" aria-hidden="true"></i>District</a></li>
                <li id="LICity"><a data-toggle="tab" href="#city"><i class="fa fa-address-card fa-fw" aria-hidden="true"></i>City</a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div id="country">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Country</legend>
                        <div class="w3-panel w3-pale-blue w3-border w3-display-container">
                            <span onclick="this.parentElement.style.display='none'"
                                class="w3-button w3-large w3-display-topright">&times;</span>
                            <h4>Instructions!</h4>
                            <p>Maximum functional skills to be assined to an employee is 4</p>
                        </div>
                        <label for="lblQuestions">Select Functional Skills:</label>
                        <div class="Report">
                        </div>
                        <asp:Button ID="btnFunctionalSkillSubmit" runat="server" CssClass="btn w3-blue" Text="Submit"></asp:Button>
                    </fieldset>
                </div>
                <div id="state">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">State</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-right">
                                <label>Country</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-right">
                                <label>State</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                            </div>                            
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2 text-right">
                                <label>State Code</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnSaveState" runat="server" CssClass="btn Save" Text="Submit"></asp:Button>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-right">
                                <label>Country</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSSCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-right">
                                <label>State</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSSState" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnSearchState" runat="server" CssClass="btn Search" Text="Search"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="District ID">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#f2f2f2" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
                <div id="district">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">District</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-right">
                                <label>State</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlDState" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-right">
                                <label>District</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDDistrict" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnSaveDistrict" runat="server" CssClass="btn Save" Text="Submit"></asp:Button>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Selection</legend>
                        <div class="col-md-12">
                            <div class="col-md-2 text-right">
                                <label>State</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSDState" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-right">
                                <label>District</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSDDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnSearchDistrict" runat="server" CssClass="btn Search" Text="Search"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvDistrict" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Select">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="District ID">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictID" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictID")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTicketStatus" Text='<%# DataBinder.Eval(Container.DataItem, "District")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#f2f2f2" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
                <div id="city">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">City</legend>
                        <div class="w3-panel w3-pale-blue w3-border w3-display-container">
                            <span onclick="this.parentElement.style.display='none'"
                                class="w3-button w3-large w3-display-topright">&times;</span>
                            <h4>Instructions!</h4>
                            <div runat="server" id="div1" visible="false">
                                <p>Maximum Technical skills to be assined to an employee is 4</p>
                            </div>
                            <div runat="server" id="div2" visible="false">
                                <p>Maximum Technical skills to be assined to an employee is 7</p>
                            </div>
                        </div>
                        <label for="lblQuestions">Select Technical Skills:</label>
                        <div class="Report">
                        </div>
                        <asp:Button ID="Button1" runat="server" CssClass="btn w3-blue" Text="Submit"></asp:Button>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfTapActive" runat="server" Value="1"></asp:HiddenField>
</asp:Content>
