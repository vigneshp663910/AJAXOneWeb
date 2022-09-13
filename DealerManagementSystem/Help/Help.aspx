<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="DealerManagementSystem.Help.Help" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">



            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Help List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Help(s):</td>

                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px"/></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px"/></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" ShowFooter="true" DataKeyNames="DocumentAttachmentID" OnPageIndexChanging="gvDocument_PageIndexChanging">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="30px" ItemStyle-Width="30px" ControlStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsno" Text='<%# DataBinder.Eval(Container.DataItem, "Sno")%>' runat="server"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtsno" runat="server" placeholder="sno" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PDF" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLinkpdf" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "PDFAttachment")%>'>
                                                <asp:Image ID="Imagepdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:FileUpload ID="fileUploadPDF" runat="server" Height="30px" Width="220px"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PPS" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLinkpps" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "PPSAttachment")%>'>
                                                <asp:Image ID="Imagepps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:FileUpload ID="fileUploadPPS" runat="server" Height="30px" Width="220px"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Video" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLinklink" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "VideoLink")%>'>
                                                <asp:Image ID="Imagelink" runat="server" ImageUrl="~/Images/vplay.png" Width="40px" Height="25px" />
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtVideoLink" runat="server" placeholder="VideoLink" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OrderNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderNo" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNo")%>' runat="server" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtOrderNo" runat="server" placeholder="OrderNo" CssClass="form-control"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibedit" runat="server" Width="20px" ImageUrl="~/Images/Edit.png" OnClick="ibedit_Click" Style="height: 20px; width: 20px;" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DocumentAttachmentID")%>' />
                                            <asp:ImageButton ID="ibdelete" runat="server" Width="20px" ImageUrl="~/Images/delete.png" OnClick="ibdelete_Click" Style="height: 20px; width: 20px;" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DocumentAttachmentID")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btn Back" OnClick="BtnAdd_Click" Width="70px" Height="33px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:HiddenField ID="HiddenID" runat="server" />
                            <asp:HiddenField ID="HiddenFieldpdf" runat="server" />
                            <asp:HiddenField ID="HiddenFieldpps" runat="server" />
                        </div>
                    </fieldset>
                </div>
            </div>






        </div>
    </div>











    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
    </head>
    <body>

        <table style="border-color: #CCCCCC; width: 1000px; font-family: Calibri; font-size: large; border-collapse: collapse;" border="2">
            <tr>
                <td colspan="5" id="PageHeading" align="center" class="auto-style1"><b>INDEX
                </b></td>
            </tr>
            <tr align="center" style="color: #FFFFFF">
                <th width="30px">SN</th>
                <th width="600px">Module</th>
                <th width="150px">PDF</th>
                <th width="150px">PPS</th>
                <th width="150px">Video</th>

            </tr>
            <%--<tr style="background-color: #FFFFFF">
                <td align="right">1</td>
                <td>Preface</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne_Preface.pdf"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Preview.png" width="25px" Height="25px"/></asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1a" runat="server" NavigateUrl="https://youtu.be/w_2OvF-iq70"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1c" runat="server" NavigateUrl="Files/31_AJAXOne_Preface.pps"><asp:Image ID="Image1c" runat="server" ImageUrl="~/Images/save.png"  width="25px" Height="25px" /></asp:HyperLink></td>
            </tr>--%>
            <tr style="background-color: #FFFFFF">
                <td align="right">1</td>
                <td>Get Started</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Getting Started.pdf">
                        <asp:Image ID="Image1pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1pps" runat="server" NavigateUrl="Files/Getting Started.pps">
                        <asp:Image ID="Image1pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--                    <asp:HyperLink ID="HyperLink2b" runat="server" NavigateUrl="https://youtu.be/RB5xdVw5kAY"><asp:Image ID="Image5a" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">2</td>
                <td>Master Data</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink2pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Master Data.pdf">
                        <asp:Image ID="Image2pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink2pps" runat="server" NavigateUrl="Files/Master Data.pps">
                        <asp:Image ID="Image2pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink2d" runat="server" NavigateUrl="https://youtu.be/vkfaxFTIDN4"><asp:Image ID="Image5d" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">3</td>
                <td>Dealer Employee Management</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink3pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Employee.pdf">
                        <asp:Image ID="Image3pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink3pps" runat="server" NavigateUrl="Files/Employee.pps">
                        <asp:Image ID="Image3pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="https://youtu.be/LUWU2OgrFns"><asp:Image ID="Image7" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">4</td>
                <td>Organisation</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink4pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Organisation.pdf">
                        <asp:Image ID="Image4pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink4pps" runat="server" NavigateUrl="Files/Organisation.pps">
                        <asp:Image ID="Image4pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="https://youtu.be/LUWU2OgrFns"><asp:Image ID="Image7" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">5</td>
                <td>Activities</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink5pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Activities.pdf">
                        <asp:Image ID="Image5pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink5pps" runat="server" NavigateUrl="Files/Activities.pps">
                        <asp:Image ID="Image5pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="https://youtu.be/LUWU2OgrFns"><asp:Image ID="Image7" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">6</td>
                <td>Project</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink6pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Project.pdf">
                        <asp:Image ID="Image6pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink6pps" runat="server" NavigateUrl="Files/Project.pps">
                        <asp:Image ID="Image6pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="https://youtu.be/LUWU2OgrFns"><asp:Image ID="Image7" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">7</td>
                <td>Pre-Sales</td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Preview.png" width="25px" Height="25px"/></asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="Files/34_AJAXOne_Pre-Sales.pps"><asp:Image ID="Image3" runat="server" ImageUrl="~/Images/save.png"  width="25px" Height="25px" /></asp:HyperLink>--%></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right"></td>
                <td>a) Customer Visit</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7apdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/PreSales- Customer Visit.pdf">
                        <asp:Image ID="Image7apdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7apps" runat="server" NavigateUrl="Files/PreSales- Customer Visit.pps">
                        <asp:Image ID="Image7apps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image12" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right"></td>
                <td>b) Enquiry</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7bpdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/PreSales-Enquiry.pdf">
                        <asp:Image ID="Image7bpdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7bpps" runat="server" NavigateUrl="Files/PreSales-Enquiry.pps">
                        <asp:Image ID="Image7bpps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image15" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right"></td>
                <td>c) Lead</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7cpdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/PreSales-Lead.pdf">
                        <asp:Image ID="Image7cpdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7cpps" runat="server" NavigateUrl="Files/PreSales-Lead.pps">
                        <asp:Image ID="Image7cpps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image18" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right"></td>
                <td>d) Followup</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7dpdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/PreSales-Manage followup.pdf">
                        <asp:Image ID="Image7dpdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7dpps" runat="server" NavigateUrl="Files/PreSales-Manage followup.pps">
                        <asp:Image ID="Image7dpps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image18" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right"></td>
                <td>e) Quotation</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7epdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/PreSales-Quotation.pdf">
                        <asp:Image ID="Image7epdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7epps" runat="server" NavigateUrl="Files/PreSales-Quotation.pps">
                        <asp:Image ID="Image7epps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image18" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right"></td>
                <td>f) Report</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7fpdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/PreSales-Report.pdf">
                        <asp:Image ID="Image7fpdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink7fpps" runat="server" NavigateUrl="Files/PreSales-Report.pps">
                        <asp:Image ID="Image7fpps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="https://youtu.be/TD8BX5yNyR0"><asp:Image ID="Image18" runat="server" ImageUrl="~/Images/vplay.png"  width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">8</td>
                <td>Task</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink8pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Task.pdf">
                        <asp:Image ID="Image8pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink8pps" runat="server" NavigateUrl="Files/Task.pps">
                        <asp:Image ID="Image8pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="https://youtu.be/qxj2HgGuDRY">
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/vplay.png" Width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">9</td>
                <td>Dashboard</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink9pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Dashboard.pdf">
                        <asp:Image ID="Image9pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink9pps" runat="server" NavigateUrl="Files/Dashboard.pps">
                        <asp:Image ID="Image9pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="https://youtu.be/qxj2HgGuDRY">
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/vplay.png" Width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">10</td>
                <td>Pre-Sales Implementation</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink10pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/Implementation.pdf">
                        <asp:Image ID="Image10pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink10pps" runat="server" NavigateUrl="Files/Implementation.pps">
                        <asp:Image ID="Image10pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" />
                    </asp:HyperLink></td>
                <td align="center">
                    <%--<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="https://youtu.be/qxj2HgGuDRY">
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/vplay.png" Width="40px" Height="25px" /></asp:HyperLink>--%></td>
            </tr>




            <%--<tr style="background-color: #FFFFFF">
                <td align="right">7</td>
                <td>Parts</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">View</asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="UserGuide.aspx">Play</asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="Files/V1_AJAXOne.pps">Download</asp:HyperLink></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">8</td>
                <td>Service</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">View</asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="UserGuide.aspx">Play</asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="Files/V1_AJAXOne.pps">Download</asp:HyperLink></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">9</td>
                <td>e-Catalogue</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">View</asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="UserGuide.aspx">Play</asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="Files/V1_AJAXOne.pps">Download</asp:HyperLink></td>
            </tr>
            <tr style="background-color: #FFFFFF">
                <td align="right">11</td>
                <td>Presentation</td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink11pdf" runat="server" NavigateUrl="~/Help/HelpDoc.aspx?aFileName=../Help/Files/V1_AJAXOne.pdf">
                        <asp:Image ID="Image11pdf" runat="server" ImageUrl="~/Images/Preview.png" Width="25px" Height="25px" /></asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="UserGuide.aspx">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/vplay.png" Width="40px" Height="25px" /></asp:HyperLink></td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink311pps" runat="server" NavigateUrl="Files/V1_AJAXOne.pps">
                        <asp:Image ID="Image11pps" runat="server" ImageUrl="~/Images/save.png" Width="25px" Height="25px" /></asp:HyperLink></td>
            </tr>--%>
        </table>
    </body>
    </html>

</asp:Content>
