<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpcAssemblyDView.ascx.cs" Inherits="DealerManagementSystem.ViewECatalogue.UserControls.SpcAssemblyDView" %>
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
              <%--  <asp:LinkButton ID="lbEditAllXYCoOrdinate" runat="server" OnClick="lbActions_Click">Edit All CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbEditXYCoOrdinate" runat="server" OnClick="lbActions_Click">Edit XY CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbSaveXYCoOrdinate" runat="server" OnClick="lbActions_Click">Save XY CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbCancelXYCoOrdinate" runat="server" OnClientClick="return Confirmation('Are you sure you want to cancel?');" OnClick="lbActions_Click">Cancel XY CoOrdinate</asp:LinkButton>
                <asp:LinkButton ID="lbChangeAssemblyDrawing" runat="server" OnClick="lbActions_Click">Change Assembly Drawing</asp:LinkButton>
                <asp:LinkButton ID="lbAddParts" runat="server" OnClick="lbActions_Click">Add Parts</asp:LinkButton>
                <asp:LinkButton ID="lbUploadParts" runat="server" OnClick="lbActions_Click">Upload Parts</asp:LinkButton>
                <asp:LinkButton ID="lbDownloadTemplate" runat="server" OnClick="lbActions_Click">Download Template</asp:LinkButton>--%>
                <asp:LinkButton ID="lbAddToCartTemp" runat="server" OnClick="lbActions_Click">Add To Cart</asp:LinkButton>
                <asp:LinkButton ID="lbCheckout" runat="server" OnClick="lbActions_Click">Checkout</asp:LinkButton>
                <%--<asp:LinkButton ID="lbDeleteSelectedParts" runat="server" OnClientClick="return Confirmation('Are you sure you want to Delete?');" OnClick="lbActions_Click">Delete Selected Parts</asp:LinkButton>--%>

            </div>
        </div>
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<style>
    .tooltip-box {
        position: absolute;
        background-color: white;
        color: #fff;
        padding: 5px 10px;
        border-radius: 4px;
        white-space: nowrap;
        pointer-events: none;
        opacity: 0;
        transition: opacity 0.2s;
        font-size: 13px;
        z-index: 9999;
    }

        .tooltip-box .testMatDesc {
            font-weight: bold;
            font-size: medium;
            color: blue;
            font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
        }

        .tooltip-box .Test {
            font-size: medium;
            color: black;
            font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
        }

        .tooltip-box .Value {
            font-size: medium;
            color: blue;
            font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
        }

        .tooltip-box .PriceTest {
            font-size: medium;
            color: black;
            font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
        }
</style>

<div id="tooltip" class="tooltip-box" style="border: 2px solid green;">Dynamic Tooltip</div>

<div class="col-md-12 field-margin-top">
    <div class="col-md-12">
        <div class="col-md-12">
            <div class="col-md-3">
                <label>PG Code : </label>
                <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-3">
                <label>PM Desc : </label>
                <asp:Label ID="lblModel" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-3">
                <label>PM Code : </label>
                <asp:Label ID="lblModelCode" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-3">
                <label>Assembly : </label>
                <asp:Label ID="lblAssembly" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-6">
                <label>Assembly Des : </label>
                <asp:Label ID="lblAssemblyDes" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-3">
                <label>Assembly Type : </label>
                <asp:Label ID="lblAssemblyType" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-3">
                <label>Remarks : </label>
                <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
            </div>

        </div>
    </div>

    <div class="col-md-12">
        <div class="col-md-12 View">
            <%-- <asp:ImageButton ID="imgClickMe" runat="server" ImageUrl="ImageHandlerECatalogue.ashx?file=example.jpg" OnClick="imgClickMe_Click"  onmousemove="showCoords(event)" onmouseout="clearCoor()" Height="510" Width="900" GFG="250"/>--%>

            <div id="container">
                <asp:Image ID="imgAssemblyImage" runat="server" ImageUrl="ImageHandlerECatalogue.ashx?file=example.jpg" onmousemove="showCoords(event)" onmouseout="clearCoor()" onclick="javascript:ClickOnImage();" Height="510" Width="900" GFG="250" />

                <img id="CallOut" src="" alt="" height="50" width="275" style="visibility: hidden" />
                <p id="copno" style="font-family: Tahoma; font-size: xx-small; color: #0000FF"></p>
                <p id="copdesc" style="font-family: Tahoma; font-size: xx-small; color: #0000FF; background-color: #D6D6D6;"></p>
            </div>
            <script type="text/javascript"> 
                var tooltip = document.getElementById('tooltip');
                var target = document.getElementById('<%= imgAssemblyImage.ClientID %>');

                target.addEventListener('mousemove', function (e) {
                    mousemove(e);
                });

                function mousemove(e) {
                    var rect = target.getBoundingClientRect();

                    var x = Math.round(event.clientX - rect.left);
                    var y = Math.round(event.clientY - rect.top);

                    // tooltip.style.left = (e.pageX - 220) + 'px';
                    // tooltip.style.top = (e.pageY - 100) + 'px';


                    //tooltip.style.left = (e.pageX + 15) + 'px';
                    //tooltip.style.top = (e.pageY + 15) + 'px';


                    let tooltip1 = document.querySelector(".tooltip-box"); // select your tooltip
                    let rect1 = tooltip1.getBoundingClientRect();

                    if (e.pageX < 500) {
                        tooltip.style.left = (e.pageX + 15) + 'px';
                    }
                    else {
                        tooltip.style.left = (e.pageX - 15 - rect1.width) + 'px';
                    }

                    if (e.pageY < 300) {
                        tooltip.style.top = (e.pageY + 15) + 'px';
                    }
                    else {
                        tooltip.style.top = (e.pageY - 15 - rect1.height) + 'px';
                    }


                    // if (e.pageX < 500 && e.pageY < 300) {
                    //    tooltip.style.left = (e.pageX + 15) + 'px';
                    //    tooltip.style.top = (e.pageY + 15) + 'px';
                    //}
                    //else {


                    //    let tooltip1 = document.querySelector(".tooltip-box"); // select your tooltip
                    //    let rect1 = tooltip1.getBoundingClientRect(); 

                    //    tooltip.style.left = (e.pageX - 15 - rect1.width) + 'px';
                    //    tooltip.style.top = (e.pageY - 15 - rect1.height) + 'px';
                    //}

                    tooltip.style.opacity = 1;

                    var grid = document.getElementById('UC_SpcAssemblyDView_gvParts');
                    var rows = grid.getElementsByTagName('tr');
                    var rowCount = rows.length - 1;
                    var TootText = '';

                    var xyBulkUpdate = <%=this.xyBulkUpdate%>;

                    if (xyBulkUpdate == 0) {
                        for (var i = 0; i < rowCount; i++) {
                            var lblX_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblX_CoOrdinate_' + i);
                            var lblY_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblY_CoOrdinate_' + i);
                            var X_CoOrdinate = parseInt(lblX_CoOrdinate.innerText);
                            var Y_CoOrdinate = parseInt(lblY_CoOrdinate.innerText);
                            if (X_CoOrdinate >= x - 10 && X_CoOrdinate <= x + 10 && Y_CoOrdinate >= y - 10 && Y_CoOrdinate <= y + 10) {

                                var lblNumber = document.getElementById('UC_SpcAssemblyDView_gvParts_lblNumber_' + i);
                                var lblFlag = document.getElementById('UC_SpcAssemblyDView_gvParts_lblFlag_' + i);
                                var lblMaterial = document.getElementById('UC_SpcAssemblyDView_gvParts_lblMaterial_' + i);
                                var lblMaterialDescription = document.getElementById('UC_SpcAssemblyDView_gvParts_lblMaterialDescription_' + i);
                                var lblQty = document.getElementById('UC_SpcAssemblyDView_gvParts_lblQty_' + i);
                                TootText = "<br /><span class='testMatDesc'>" + lblMaterialDescription.innerText + "</span> <br />"
                                    + "<br /> <span class='Test'> SP Number : </span><span class='Value'>" + lblMaterial.innerText + "</span>"
                                    + "<br /> <span class='Test' >Position :</span><span class='Value'>" + lblNumber.innerText + "</span>"
                                    + "<br /> <span class='Test'> Alt :</span> <span class='Value'>" + lblFlag.innerText + "</span>"

                                    //+ "<br />Mat Des : " + 

                                    + "<br /> <span class='PriceTest'> Qty :</span><span class='Value'>" + lblQty.innerText
                                    ;

                                break;
                            }
                        }

                        tooltip.innerHTML = TootText
                            + "<br /><span class='Test'>X: </span><span class='Value'>" + x + "</span>"
                            + "<br /><span class='Test'>Y: </span><span class='Value'>" + y + "</span>";
                    }
                    else {
                        for (var i = 0; i < rowCount; i++) {
                            var rbParts = document.getElementById('UC_SpcAssemblyDView_gvParts_rbParts_' + i);
                            if (rbParts.checked == false) {
                                var lblNumber = document.getElementById('UC_SpcAssemblyDView_gvParts_lblNumber_' + i);
                                var lblFlag = document.getElementById('UC_SpcAssemblyDView_gvParts_lblFlag_' + i);
                                var lblMaterial = document.getElementById('UC_SpcAssemblyDView_gvParts_lblMaterial_' + i);
                                var lblMaterialDescription = document.getElementById('UC_SpcAssemblyDView_gvParts_lblMaterialDescription_' + i);
                                var lblQty = document.getElementById('UC_SpcAssemblyDView_gvParts_lblQty_' + i);
                                TootText = "<br /> <span class='Test' >Position :</span><span  style='font-size: 50px; color: blue;'>" + lblNumber.innerText + "</span>"
                                    + "<br /><span class='testMatDesc'>" + lblMaterialDescription.innerText + "</span> <br />"
                                    + "<br /> <span class='Test'> SP Number : </span><span class='Value'>" + lblMaterial.innerText + "</span>"
                                    + "<br /> <span class='Test'> Alt :</span> <span class='Value'>" + lblFlag.innerText + "</span>"

                                    //  + "<br />Mat Des : " + lblMaterialDescription.innerText

                                    + "<br /> <span class='PriceTest'> Qty :</span><span class='Value'>" + lblQty.innerText
                                    ;

                                tooltip.innerHTML = TootText
                                    + "<br /><span class='Test'>X: </span><span class='Value'>" + x + "</span>"
                                    + "<br /><span class='Test'>Y: </span><span class='Value'>" + y + "</span>";
                                break;
                            }
                        }
                    }

                };

                target.addEventListener('mouseleave', function () {
                    tooltip.style.opacity = 0;
                });



                //CallOut(120, 80, "Part No: 0740.000040\nDescription: CIRCLIP EXTERNAL");
                function CallOut(x, y, ls_pno, ls_pdesc) {
                    var imgCallOut = document.getElementById("CallOut");
                    imgCallOut.style.visibility = 'visible';
                    imgCallOut.src = '../Images/PopsPartDetails.png';

                    var rect = target.getBoundingClientRect();
                    //x = Math.round(x - rect.left);
                    //y = Math.round(y - rect.top);

                    //xCoor = x;
                    //yCoor = y + 18;
                    xCoor = x - 10;
                    yCoor = y + 15;

                    var newImg1 = $(imgCallOut);
                    newImg1.css({ position: "absolute", left: xCoor, top: yCoor });

                    copono = document.getElementById("copno");
                    copono.innerHTML = ls_pno;

                    copodesc = document.getElementById("copdesc");
                    copodesc.innerHTML = ls_pdesc;

                    var newTxtP = $(copono);
                    newTxtP.css({ position: "absolute", left: xCoor + 90, top: yCoor + 15 });
                    $("#container").append(newTxtP);

                    var newTxtPd = $(copodesc);
                    newTxtPd.css({ position: "absolute", left: xCoor + 90, top: yCoor + 30 });
                    $("#container").append(newTxtPd);

                }
            </script>

        </div>
    </div>

    <div class="col-md-12">
        <%--<div class="col-md-12">
            <label>XY Coordinate : </label>
            <asp:Label ID="lblXY" runat="server" CssClass="LabelValue"></asp:Label>
        </div>--%>
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
                    <asp:TemplateField ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" HeaderText="++ To Cart">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgAddToCart" runat="server" ImageUrl="~/Images/AddToCart.gif" Height="20px" Width="22px" ImageAlign="AbsMiddle" OnClick="imgAddToCart_Click" />
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PART NO">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblSpcMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.SpcMaterialID")%>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.Material")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPTION">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WHERE">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgShow" runat="server" ImageUrl="~/Images/Where1.png" Height="20px" Width="20px" ImageAlign="AbsMiddle" OnClick="link_Show" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="X ± 10">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblX_CoOrdinate" Text='<%# DataBinder.Eval(Container.DataItem, "X_CoOrdinate")%>' runat="server" ForeColor="#8080809e"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Y ± 10">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblY_CoOrdinate" Text='<%# DataBinder.Eval(Container.DataItem, "Y_CoOrdinate" )%>' runat="server" ForeColor="#8080809e"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblEditParts" runat="server" OnClick="lnkBtnItemAction_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnItemAction_Click" OnClientClick="return Confirmation('Are you sure you want to delete?');"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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

<asp:Panel ID="pnlSaveCoOrdinate" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Update X Y Co-Ordinate</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblCoOrdinateMessage" runat="server" Text="" CssClass="message" />
        <div class="model-scroll">
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

<asp:Panel ID="pnlAssemblyDrawing" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Upload Assembly Drawing</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblDrawingMessage" runat="server" Text="" CssClass="message" />
            <div class="col-md-12">
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

<asp:Panel ID="pnlCheckout" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Cart Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="model-scroll">
        <asp:Label ID="lblSaveToCart" runat="server" Text="" CssClass="message" />
        <div class="col-md-6 col-sm-12">
            <label>Dealer</label>
            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Office</label>
            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Remarks</label>
            <asp:TextBox ID="txtCartRemarks" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
        </div>

        <div class="col-md-12">
            <asp:GridView ID="gvToCart" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Check">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:CheckBox ID="cbParts" runat="server" Checked="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POS">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
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
                            <asp:Label ID="lblSpcAssemblyID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcAssemblyID")%>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblSpcMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcMaterialID")%>' runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "PartQty")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnDeleteCartTemp" runat="server" OnClick="lnkBtnDeleteCartTemp_Click" OnClientClick="return Confirmation('Are you sure you want to delete?');"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
            <asp:Button ID="btnSaveToCart" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveToCart_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Checkout" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCheckout" BackgroundCssClass="modalBackground" />

<asp:Panel ID="pnlCartTemp" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Save To Cart</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="model-scroll">
        <asp:Label ID="lblAddToCartTempMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <asp:GridView ID="gvCartTemp" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Assembly Code">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblSpcAssemblyPartsCoOrdinateID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcAssemblyPartsCoOrdinateID")%>' runat="server" Style="display: none"></asp:Label>
                            <asp:Label ID="lblAssemblyCode" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyCode")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assembly Description">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssemblyDescription" Text='<%# DataBinder.Eval(Container.DataItem, "AssemblyDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POS">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
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
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                            <asp:Label ID="lblSpcMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "SpcMaterialID")%>' runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtQty" Text='<%# DataBinder.Eval(Container.DataItem, "PartQty")%>' CssClass="form-control" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' CssClass="form-control" runat="server"></asp:TextBox>
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
            <asp:Button ID="btnAddToCartemp" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddToCartemp_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddToCartemp" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCartTemp" BackgroundCssClass="modalBackground" />


<asp:Panel ID="pnlAddPart" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Parts</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblPartAddMessage" runat="server" Text="" CssClass="message" />
    <asp:Label ID="lblSpcAssemblyPartsCoOrdinateID" runat="server" Text="0" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <div class="col-md-5 col-sm-12">
                <label>POS</label>
                <asp:TextBox ID="txtNumberC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-5 col-sm-12">
                <label>Alt</label>
                <asp:TextBox ID="txtFlagC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-5 col-sm-12">
                <label>Part</label>
                <asp:TextBox ID="txtPartC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-5 col-sm-12">
                <label>Part Description</label>
                <asp:TextBox ID="txtPartDescription" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-5 col-sm-12">
                <label>Qty</label>
                <asp:TextBox ID="txtQtyC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-10 col-sm-12">
                <label>Remarks</label>
                <asp:TextBox ID="txtRemarksC" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnPartsSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnPartsSave_Click" />
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddPart" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddPart" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
<script type="text/javascript">
    function showCoords(event) {
        var img = document.getElementById("UC_SpcAssemblyDView_imgAssemblyImage");
        var rect = img.getBoundingClientRect();

        var x = Math.round(event.clientX - rect.left);
        var y = Math.round(event.clientY - rect.top);


        var coords = "X : " + x + ", Y : " + y;
        // document.getElementById("UC_SpcAssemblyDView_lblXY").innerHTML = coords;
    }
    function clearCoor() {
        // document.getElementById("UC_SpcAssemblyDView_lblXY").innerHTML = "";
    }

    function selectOnlyOne(radio) {
        var grid = document.getElementById('UC_SpcAssemblyDView_gvParts');
        var rows = grid.getElementsByTagName('tr');
        var rowCount = rows.length - 1;
        debugger;

        for (var i = 0; i < rowCount; i++) {
            var radios = document.getElementById('UC_SpcAssemblyDView_gvParts_rbParts_' + i);
            radios.checked = false;
        }
        radio.checked = true;
    }

    function ClickOnImage() {
        debugger;
        var img = document.getElementById("UC_SpcAssemblyDView_imgAssemblyImage");
        var rect = img.getBoundingClientRect();
        var x = Math.round(event.clientX - rect.left);
        var y = Math.round(event.clientY - rect.top);

        var xyUpdate = <%=this.xyUpdate%>;
        var xyBulkUpdate = <%=this.xyBulkUpdate%>;

        var grid = document.getElementById('UC_SpcAssemblyDView_gvParts');
        var rows = grid.getElementsByTagName('tr');
        var rowCount = rows.length - 1;
        if (xyUpdate == 1) {
            for (var i = 0; i < rowCount; i++) {
                var rbParts = document.getElementById('UC_SpcAssemblyDView_gvParts_rbParts_' + i);
                if (rbParts.checked) {

                    rbParts.disabled = true;
                    var lblX_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblX_CoOrdinate_' + i);
                    var lblY_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblY_CoOrdinate_' + i);
                    var lblID = document.getElementById('UC_SpcAssemblyDView_gvParts_lblSpcAssemblyPartsCoOrdinateID_' + i);

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
        else if (xyBulkUpdate == 1) {
            var hdnUpdatedIDs = document.getElementById('<%= hdnUpdatedIDs.ClientID %>');
            var hdnX = document.getElementById('<%= hdnX.ClientID %>');
            var hdnY = document.getElementById('<%= hdnY.ClientID %>');
            if (hdnX.value !== "") {
                debugger;
                let lastX = hdnX.value.split(",").pop();
                let lastY = hdnY.value.split(",").pop();
                if (lastX >= x - 5 && lastX <= x + 5 && lastY >= y - 5 && lastY <= y + 5) {
                    alert("Kindly verify whether the last XY coordinate aligns with the current point.");
                    return;
                }
            }
            for (var i = 0; i < rowCount; i++) {
                var rbParts = document.getElementById('UC_SpcAssemblyDView_gvParts_rbParts_' + i);
                if (rbParts.checked == false) {
                    rbParts.checked = true;
                    var lblX_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblX_CoOrdinate_' + i);
                    var lblY_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblY_CoOrdinate_' + i);
                    var lblID = document.getElementById('UC_SpcAssemblyDView_gvParts_lblSpcAssemblyPartsCoOrdinateID_' + i);

                    hdnUpdatedIDs.value = hdnUpdatedIDs.value + ',' + lblID.innerText;
                    hdnX.value = hdnX.value + ',' + x;
                    hdnY.value = hdnY.value + ',' + y;

                    lblX_CoOrdinate.innerText = x;
                    lblY_CoOrdinate.innerText = y;
                    mousemove(event);
                    break;
                }
            }
        }
        else {
            for (var i = 0; i < rowCount; i++) {
                var lblX_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblX_CoOrdinate_' + i);
                var lblY_CoOrdinate = document.getElementById('UC_SpcAssemblyDView_gvParts_lblY_CoOrdinate_' + i);
                var X_CoOrdinate = parseInt(lblX_CoOrdinate.innerText);
                var Y_CoOrdinate = parseInt(lblY_CoOrdinate.innerText);

                if (X_CoOrdinate >= x - 10 && X_CoOrdinate <= x + 10 && Y_CoOrdinate >= y - 10 && Y_CoOrdinate <= y + 10) {
                    var cbPart = document.getElementById('UC_SpcAssemblyDView_gvParts_cbParts_' + i);
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

