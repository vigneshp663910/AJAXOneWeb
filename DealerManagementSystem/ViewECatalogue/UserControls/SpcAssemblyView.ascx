<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpcAssemblyView.ascx.cs" Inherits="DealerManagementSystem.ViewECatalogue.UserControls.SpcAssemblyView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:HiddenField ID="hdnUpdatedIDs" runat="server" />
<asp:HiddenField ID="hdnX" runat="server" />
<asp:HiddenField ID="hdnY" runat="server" />
<style>
    .Popup {
        width: 95%;
        height: 95%;
        top: 128px;
        left: 283px;
    }

        .Popup .model-scroll {
            height: 80vh;
            overflow: auto;
        }

    input[type=checkbox], input[type=radio] {
        margin: 0px;
    }
</style>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbEditXYCoOrdinate" runat="server" OnClick="lbActions_Click">Edit XY CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbSaveXYCoOrdinate" runat="server" OnClick="lbActions_Click">Save XY CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbCancelXYCoOrdinate" runat="server" OnClientClick="return Confirmation('Are you sure you want to cancel?');" OnClick="lbActions_Click">Cancel XY CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbSaveToCart" runat="server" OnClick="lbActions_Click">Save To Cart</asp:LinkButton>
                <asp:LinkButton ID="lbUploadParts" runat="server" OnClick="lbActions_Click">Upload Parts</asp:LinkButton>
                <asp:LinkButton ID="lbDownloadTemplate" runat="server" OnClick="lbActions_Click">Download Template</asp:LinkButton>
                <asp:LinkButton ID="lbEditAssembly" runat="server" OnClick="lbActions_Click">Edit Assembly</asp:LinkButton>
                <asp:LinkButton ID="lbChangeAssemblyDrawing" runat="server" OnClick="lbActions_Click">Change Assembly Drawing</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Stock Transfer Order</legend>
            <div class="col-md-12 View">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label>Division : </label>
                        <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <label>Model : </label>
                        <asp:Label ID="lblModel" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <label>Model Code : </label>
                        <asp:Label ID="lblModelCode" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <label>Assembly : </label>
                        <asp:Label ID="lblAssembly" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <label>Assembly Des : </label>
                        <asp:Label ID="lblAssemblyDes" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <label>Assembly Type : </label>
                        <asp:Label ID="lblAssemblyType" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <div class="col-md-9">
        <div class="col-md-12 View">
            <div class="col-md-12">
                <asp:Panel ID="Panel1" runat="server" GroupingText="" Height="50px" Width="100%" BackColor="#c0c0c0" Visible="true" Font-Names="Calibri">
                    <table border="2" cellpadding="0" cellspacing="0" width="100%" style="border-style: hidden hidden solid hidden; height: 50px; border-bottom-color: #CC0000;">
                        <tr>
                            <td align="left" style="border-right-style: hidden">
                                <p id="demo" style="font-family: Calibri; font-size: medium; color: #0000FF"></p>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%-- <asp:ImageButton ID="imgClickMe" runat="server" ImageUrl="ImageHandlerECatalogue.ashx?file=example.jpg" OnClick="imgClickMe_Click"  onmousemove="showCoords(event)" onmouseout="clearCoor()" Height="510" Width="900" GFG="250"/>--%>
                <asp:Image ID="imgAssemblyImage" runat="server" ImageUrl="ImageHandlerECatalogue.ashx?file=example.jpg" onmousemove="showCoords(event)" onmouseout="clearCoor()" onclick="javascript:ClickOnImage();" Height="510" Width="900" GFG="250" />

                <br />
                <asp:Label ID="lblCoordinates" runat="server" Font-Bold="true" />

            </div>
        </div>
    </div>
    <div class="col-md-3">
        <div class="col-md-12 Report">
            <asp:GridView ID="gvParts" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Check">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:CheckBox ID="cbParts" runat="server" />
                            <asp:RadioButton ID="rbParts" runat="server" GroupName="GroupRadio" onclick="selectOnlyOne(this)" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POS">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSpcAssemblyPartsCoOrdinateID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcAssemblyPartsCoOrdinateID")%>' runat="server" Style="display: none"></asp:Label>
                            <asp:Label ID="lblNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Number")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alt">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblFlag" Text='<%# DataBinder.Eval(Container.DataItem, "Flag")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.Material")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Where">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" Text="HH" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="X 10">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblX_CoOrdinate" Text='<%# DataBinder.Eval(Container.DataItem, "X_CoOrdinate")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Y 10">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblY_CoOrdinate" Text='<%# DataBinder.Eval(Container.DataItem, "Y_CoOrdinate" )%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnItemAction_Click" OnClientClick="return ConfirmItemDelete();"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp:Panel ID="pnlSaveCoOrdinate" runat="server" CssClass="Popup" Style="display: none" Height="500px">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Material</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblCoOrdinateMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <asp:GridView ID="gvPartsCoordinats" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="POS">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSpcAssemblyPartsCoOrdinateID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcAssemblyPartsCoOrdinateID")%>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Number")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alt">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblFlag" Text='<%# DataBinder.Eval(Container.DataItem, "Flag")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.Material")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="X 10">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblX_CoOrdinate" Text='<%# DataBinder.Eval(Container.DataItem, "X_CoOrdinate")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Y 10">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblY_CoOrdinate" Text='<%# DataBinder.Eval(Container.DataItem, "Y_CoOrdinate" )%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
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
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveCoOrdinate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveCoOrdinate_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_SaveCoOrdinate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSaveCoOrdinate" BackgroundCssClass="modalBackground" />

<asp:Panel ID="pnlPatrsListUpload" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Patrs List Upload</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblPatrsListUploadMessage" runat="server" Text="" CssClass="message" />
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Upload</label>
                    <asp:FileUpload ID="fileUploadBinLocationConfig" runat="server" />
                </div>
                <div class="col-md-12 col-sm-12 text-center">
                    <asp:Button ID="btnViewPatrsList" runat="server" Text="View" CssClass="btn Save" OnClick="btnViewPatrsList_Click" Width="100px" />
                    <asp:Button ID="BtnSavePatrsList" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSavePatrsList_Click" Width="100px" />
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="GgUploadPartsList" CssClass="table table-bordered table-condensed Grid" runat="server" ShowHeaderWhenEmpty="true"
                                EmptyDataText="No Data Found" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="POS" DataField="Number"></asp:BoundField>
                                    <asp:BoundField HeaderText="Alt" DataField="Flag"></asp:BoundField>
                                    <asp:BoundField HeaderText="Material" DataField="Material.Material"></asp:BoundField>
                                    <asp:BoundField HeaderText="Material Desc" DataField="Material.MaterialDescription"></asp:BoundField>
                                    <asp:BoundField HeaderText="Qty" DataField="Qty"></asp:BoundField>
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks"></asp:BoundField>
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
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_PatrsListUpload" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlPatrsListUpload" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />




<asp:Panel ID="pnlAssemblyEdit" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">AssemblyEdit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAssemblyEditMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-6 col-sm-12">
                <label>Model</label>
                <asp:DropDownList ID="ddlModelAssemblyEdit" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label>AssemblyCode</label>
                <asp:TextBox ID="txtAssemblyCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
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
                <asp:Button ID="btnAssemblyEditSave" runat="server" Text="Update" CssClass="btn Save" OnClick="btnAssemblyEditSave_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyEdit" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlAssemblyDrawing" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Upload Assembly Drawing</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAssemblyDrawingMessage" runat="server" Text="" CssClass="message" />
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Upload</label>
                    <asp:FileUpload ID="fuAssemblyDrawing" runat="server" />
                </div>
                <div class="col-md-12 col-sm-12 text-center">
                    <asp:Button ID="btnAssemblyDrawingSave" runat="server" Text="View" CssClass="btn Save" OnClick="btnAssemblyDrawingSave_Click" Width="100px" /> 
                </div>
            </div> 
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AssemblyDrawing" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAssemblyDrawing" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
<script type="text/javascript">
    function showCoords(event) {
        var img = document.getElementById("MainContent_UC_SpcAssemblyView_imgAssemblyImage");
        var rect = img.getBoundingClientRect();

        var x = Math.round(event.clientX - rect.left);
        var y = Math.round(event.clientY - rect.top);


        var coords = "X : " + x + ", Y : " + y;
        document.getElementById("demo").innerHTML = coords;
    }
    function clearCoor() {
        document.getElementById("demo").innerHTML = "";
    }

    function selectOnlyOne(radio) {
        var grid = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts');
        var rows = grid.getElementsByTagName('tr');
        var rowCount = rows.length - 1;
        debugger;

        for (var i = 0; i < rowCount; i++) {
            var radios = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_rbParts_' + i);
            radios.checked = false;
        }
        radio.checked = true;
    }

    function ClickOnImage() {

        var img = document.getElementById("MainContent_UC_SpcAssemblyView_imgAssemblyImage");
        var rect = img.getBoundingClientRect();
        var x = Math.round(event.clientX - rect.left);
        var y = Math.round(event.clientY - rect.top);

        var xyUpdate = <%=this.xyUpdate%>;

        var grid = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts');
        var rows = grid.getElementsByTagName('tr');
        var rowCount = rows.length - 1;
        debugger;
        if (xyUpdate == 1) {
            for (var i = 0; i < rowCount; i++) {
                var rbParts = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_rbParts_' + i);
                if (rbParts.checked) {

                    rbParts.disabled = true;
                    var lblX_CoOrdinate = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_lblX_CoOrdinate_' + i);
                    var lblY_CoOrdinate = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_lblY_CoOrdinate_' + i);
                    var lblID = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_lblSpcAssemblyPartsCoOrdinateID_' + i);

                    var hdnUpdatedIDs = document.getElementById('<%= hdnUpdatedIDs.ClientID %>');
                    var hdnX = document.getElementById('<%= hdnX.ClientID %>');
                    var hdnY = document.getElementById('<%= hdnY.ClientID %>');

                    hdnUpdatedIDs.value = hdnUpdatedIDs.value + ',' + lblID.innerText;
                    hdnX.value = hdnX.value + ',' + x;
                    hdnY.value = hdnY.value + ',' + y;

                    lblX_CoOrdinate.innerText = x;
                    lblY_CoOrdinate.innerText = y;
                }
            }
        }
        else {
            for (var i = 0; i < rowCount; i++) {
                var lblX_CoOrdinate = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_lblX_CoOrdinate_' + i);
                var lblY_CoOrdinate = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_lblY_CoOrdinate_' + i);
                var X_CoOrdinate = parseInt(lblX_CoOrdinate.innerText);
                var Y_CoOrdinate = parseInt(lblY_CoOrdinate.innerText);

                if (X_CoOrdinate >= x - 10 && X_CoOrdinate <= x + 10 && Y_CoOrdinate >= y - 10 && Y_CoOrdinate <= y + 10) {
                    var cbPart = document.getElementById('MainContent_UC_SpcAssemblyView_gvParts_cbParts_' + i);
                    cbPart.checked = true;
                    return;
                }
            }
        }
    }

    function Confirmation(Message) {
        var x = confirm(Message);
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>