<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcAssemblyMaster.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcAssemblyMaster" %>

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

        input[type=checkbox], input[type=radio] {
            margin: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset id="fsCriteria" class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Filter<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/filter1.png" Width="30" Height="30" /></legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Product Group</label>
                        <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Model / PM Code</label>
                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Assembly Code</label>
                        <asp:TextBox ID="txtAssemblyCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Active</label>
                        <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1">Select</asp:ListItem>
                            <asp:ListItem Value="1">true</asp:ListItem>
                            <asp:ListItem Value="0">false</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-8">
                        <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Width="95px" />
                        <asp:Button ID="btnCreateAssembly" runat="server" CssClass="btn Save" Text="Create Assembly" Width="120px" OnClick="btnCreateAssembly_Click"></asp:Button>
                        <asp:Button ID="BtnUpload" runat="server" Text="Upload" CssClass="btn Save" OnClick="BtnUpload_Click" />
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
                                                <td>Assemblies:</td>
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
                                </div>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvAssembly" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15"
                                            EmptyDataText="No Data Found">
                                            <Columns>
                                                <%--  <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="SlNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlNo" Text='<%# DataBinder.Eval(Container.DataItem, "SlNo")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PG Code">
                                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpcAssemblyID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcAssemblyID")%>' runat="server" Visible="false" />
                                                        <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "SpcModel.ProductGroup.PGCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model / PM Code">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpcModelCode" Text='<%# DataBinder.Eval(Container.DataItem, "SpcModel.SpcModelCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "SpcModel.SpcModel")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assembly Code">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssemblyCode" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyCode")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assembly Description">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpcAssemblyDescription" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyDescription")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsActive")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assembly Image File Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assembly Type">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssemblyType" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyType")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEditAssembly" runat="server" OnClick="lblEditAssembly_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblDeleteAssembly" runat="server" OnClick="lnkBtnDeleteAssembly_Click"
                                                            OnClientClick='<%# "return Confirmation(\"" +"Are you sure you want to delete : " + Eval("AssemblyCode") +" ?"  + "\");" %>'>
                                                            <i class="fa fa-fw fa-times" style="font-size: 18px"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblViewImage" runat="server" Text="View" OnClick="lblViewImage_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbChangeAssemblyDrawing" runat="server" OnClick="lbChangeAssemblyDrawing_Click">Change Drawing</asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibPDF" runat="server" Width="20px" ImageUrl="../Images/pdf_dload.png" OnClick="ibPDF_Click" Style="height: 30px; width: 40px;" />
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="#ffffff" />
                                            <FooterStyle ForeColor="White" />
                                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                    <td style="vertical-align: top"></td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
            </div>

        </div>
    </div>

    <asp:Panel ID="pnlAssemblyCreate" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Create / Update Assembly</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <asp:Label ID="lblSpcAssemblyID" runat="server" Text="0" Visible="false" />
        <asp:Label ID="lblAssemblyMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="model-scroll">

                <div class="col-md-5 col-sm-12">
                    <label>Product Group</label>
                    <asp:DropDownList ID="ddlProductGroupC" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductGroupC_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Model / PM Code</label>
                    <asp:DropDownList ID="ddlModelC" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>SlNo</label>
                    <asp:TextBox ID="txtSlNoC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Assembly Code</label>
                    <asp:TextBox ID="txtAssemblyCodeC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-10 col-sm-12">
                    <label>Assembly Description</label>
                    <asp:TextBox ID="txtAssemblyDescriptionC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-10 col-sm-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarksC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Assembly Type</label>
                    <asp:DropDownList ID="ddlAssemblyTypeC" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Common">Common</asp:ListItem>
                        <asp:ListItem Value="Make Specific">Make Specific</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Active</label>
                    <div>
                        <asp:CheckBox ID="cbIsActiveC" runat="server" Checked="true" />
                    </div>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAssemblySave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAssemblySave_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlAssemblyDrawing" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Upload Assembly Drawing</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblDrawingMessage" runat="server" Text="" CssClass="message" />
                <asp:Label ID="lblDSpcAssemblyID" runat="server" Visible="false"></asp:Label>
                <div class="col-md-12">
                    <div class="col-md-5 col-sm-12">
                        <label>Model Code</label>
                        <asp:Label ID="lblDSpcModelCode" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="col-md-5 col-sm-12">
                        <label>Assembly Code</label>
                        <asp:Label ID="lblDSpcAssemblyCode" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Upload</label>
                        <asp:FileUpload ID="fuAssemblyDrawing" runat="server" />
                    </div>
                    <div class="col-md-12 col-sm-12 text-center">
                        <asp:Button ID="btnAssemblyDrawingSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAssemblyDrawingSave_Click" Width="100px" OnClientClick="return Confirmation('Are you sure you want to save?');" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyDrawing" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyDrawing" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


    <asp:Panel ID="pnlAssemblyImage" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Upload Assembly Drawing</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <div class="col-md-12">

                <div class="col-md-5 col-sm-12">
                    <label>PM Code</label>
                    <asp:Label ID="lblDPmCode" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-5 col-sm-12">
                    <label>Assembly Code</label>
                    <asp:Label ID="lblDAssemblyCode" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-10 col-sm-12">
                    <label>Assembly Description</label>
                    <asp:Label ID="lblDAssemblyDescription" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-10 col-sm-12">
                    <asp:Image ID="imgAssemblyImage" runat="server" Height="355" Width="650" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyImage" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyImage" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <asp:Panel ID="pnlAssemblyUpload" runat="server" CssClass="Popup" Style="display: none;">
        <div class="PopupHeader clearfix">
            <span id="PopupDialogue">Upload Assembly Master</span>
            <a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
        </div>
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="FldUpload" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Upload Excel File</legend>
                <asp:Label ID="lblMessageAssemblyUpload" runat="server" Text="" CssClass="message" />
                <div class="col-md-12">
                    <div class="col-md-2" runat="server">
                        <asp:FileUpload ID="fileUpload" runat="server" />
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btnDownload" runat="server" Text="Download Template" CssClass="btn Search" OnClick="btnDownload_Click" Width="150px" />
                        <asp:Button ID="btnViewExcel" runat="server" Text="View" CssClass="btn Save" OnClick="btnViewExcel_Click" Width="100px" />
                        <asp:Button ID="BtnSaveExcel" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSaveExcel_Click" Width="100px" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBack_Click" Width="100px" />
                    </div>
                </div>
                <div class="col-md-12 Report" style="height: 370px">
                    <asp:GridView ID="GVUpload" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                        EmptyDataText="No Data Found" AutoGenerateColumns="true" Width="100%">
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyUpload" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyUpload" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

    <div style="display: none">
        <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>

    <script type="text/javascript"> 

        function Confirmation(Message) {
            var x = confirm(Message);
            if (x) {
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
