<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="DealerManagementSystem.Location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).on("load", function () {
            var hfTapActive = document.getElementById("ContentPlaceHolder1_hfTapActive").value;
            $('#LIhome').removeClass("active");
            $('#LImenu1').removeClass("active");
            $('#LImenu2').removeClass("active");

            $('#home').addClass("tab-pane fade");
            $('#menu1').addClass("tab-pane fade");
            $('#menu2').addClass("tab-pane fade");

            if (hfTapActive == 1) {
                $('#LIhome').addClass("active");
                $('#home').addClass("tab - pane fade in active");
            }
            else if (hfTapActive == 2) {
                $('#LImenu1').addClass("active");
                $('#menu1').addClass("tab - pane fade in active");
            }
            else if (hfTapActive == 3) {
                $('#LImenu2').addClass("active");
                $('#menu2').addClass("tab - pane fade in active");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <br />
            <div>
                <h2><span class="MainHeader">IT Service Request Form</span></h2>
                <div style="height: 5px;" class="ProjectHeadingLine"></div>
            </div>
            <br />
        </div>
        <div class="col-md-12 Report">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li id="LICountry"><a data-toggle="tab" href="#country"><i class="fa fa-question-circle fa-fw" aria-hidden="true"></i>Country</a></li>
                <li id="LIState"><a data-toggle="tab" href="#state"><i class="fa fa-question-circle fa-fw" aria-hidden="true"></i>State</a></li>
                <li id="LIDistrict"><a data-toggle="tab" href="#district"><i class="fa fa-question-circle fa-fw" aria-hidden="true"></i>District</a></li>
                <li id="LICity"><a data-toggle="tab" href="#city"><i class="fa fa-question-circle fa-fw" aria-hidden="true"></i>City</a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div id="country">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Functional Skills</legend>
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
                        <legend style="background: none; color: #007bff; font-size: 17px;">Soft Skills</legend>
                        <div class="w3-panel w3-pale-blue w3-border w3-display-container">
                            <span onclick="this.parentElement.style.display='none'"
                                class="w3-button w3-large w3-display-topright">&times;</span>
                            <h4>Instructions!</h4>
                            <p>Maximum Soft skills to be assined to an employee is 2</p>
                        </div>
                        <label for="lblQuestions">Select Soft Skills:</label>
                        <div class="Report">
                            
                        </div>
                        <asp:Button ID="btnSoftSkillSubmit" runat="server" CssClass="btn w3-blue" Text="Submit"></asp:Button>
                    </fieldset>
                </div>
                <div id="district">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Technical Skills</legend>
                        <div class="w3-panel w3-pale-blue w3-border w3-display-container">
                            <span onclick="this.parentElement.style.display='none'"
                                class="w3-button w3-large w3-display-topright">&times;</span>
                            <h4>Instructions!</h4>
                            <div runat="server" id="divInstructionforall" visible="false">
                                <p>Maximum Technical skills to be assined to an employee is 4</p>
                            </div>
                            <div runat="server" id="divInstructionforTraining" visible="false">
                                <p>Maximum Technical skills to be assined to an employee is 7</p>
                            </div>
                        </div>
                        <label for="lblQuestions">Select Technical Skills:</label>
                        <div class="Report">
                            
                        </div>
                        <asp:Button ID="btnTechnicalSkillSubmit" runat="server" CssClass="btn w3-blue" Text="Submit"></asp:Button>
                    </fieldset>
                </div>
                <div id="city">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Technical Skills</legend>
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
</asp:Content>
