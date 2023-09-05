<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityInvoice.aspx.cs" Inherits="DealerManagementSystem.ViewMarketing.ActivityInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMSStyles.css" rel="stylesheet" />
    <link rel="stylesheet" href="invoicestyle.css">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <style type="text/css">
        .heading-txt {
            width: 100%;
            padding: 10px;
        }

            .heading-txt h1 {
                margin: 0px !important;
                font-size: 24px;
                color: #000;
                font-weight: 600;
                text-transform: uppercase;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
        <div runat="server" id="divPrint">
            <header>
                <div class="container">

                    <div class="row pt-40">
                        <div class="col-lg-12 header-txt">
                            <table style="width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td style="width: 40%; text-align: left">
                                        <p>
                                            <asp:Label ID="lblDealerCode" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            <asp:Label ID="lblDealerNameHdr" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            <asp:Label ID="lblDealerAddress" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            PAN No:
                                        <asp:Label ID="lblDealerPAN" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            GST NO:
                                        <asp:Label ID="lblDealerGSTIN" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                    <td style="width: 20%; text-align: center">
                                        <div style="border: none">
                                            <h2 style="border: none">Tax Invoice</h2>
                                        </div>
                                    </td>
                                    <td style="width: 40%; text-align: right">
                                        <asp:Image ID="imgLogo" Width="200px" runat="server" ImageUrl="ajaxlogo.png" Visible="false" />
                                        <img src="logo.svg" class="logo" alt="" style="width: 200px">
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>

            </header>
            <section class="tax-invoice-outer section-gap">
                <div class="container">
                    <div class="row">
                        <div style="text-align: center; width: 100%; background-color: black; color: white; font-family: Arial; font-size: 11pt">
                            <h2>Self-Loaders | Batching Plants | Pumps</h2>
                        </div>
                    </div>
                    <div class="row pt-40">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 40%; padding-left: 10px;">
                                    <p><span><strong>Bill to Party:</strong></span> Ajax Engineering Pvt Ltd</p>
                                    <p>
                                         
                                        No.253/1, 11th Main Road, 3rd Phase, Peenya<br>
                                        Industrial Area, Bangalore-560058 KA
                                    </p>
                                    <p>GSTIN: 29AABCA2035K1ZT</p>
                                    <p>PAN No.: AABCA2035K</p>

                                </td>
                                <td style="width: 30%">
                                    <asp:Image ID="imgQRCodeImg" Width="250" runat="server" ImageUrl="E:\\DotNet\\TicketingSys\\ATS\\ATSVendor\\QRCode\\90242021W000081.png" />

                                </td>
                                <td style="width: 30%">
                                    <div>
                                        <p>
                                            <span>Invoice No:</span>
                                            <asp:Label ID="lblInvNo" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            <span>Invoice Date:</span>
                                            <asp:Label ID="lblInvDate" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            <span>Activity:<asp:Label ID="lblActivity" runat="server"></asp:Label>
                                            </span>
                                        </p>
                                        <p>
                                            <span>Control No:<asp:Label ID="lblControlNo" runat="server"></asp:Label>
                                            </span>
                                        </p>
                                        <p>
                                            <span>Location:<asp:Label ID="lblLocation" runat="server"></asp:Label>
                                            </span>
                                        </p>
                                        <p>
                                            <span>Event Date:</span>
                                            <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"><asp:Label ID="lblIRN" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <!-- end row -->

                    <div class="row pt-40">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Material</th>
                                            <th>Description</th>
                                            <th>SAC</th>
                                            <th>Tax %</th>
                                            <th>Taxable Value</th>
                                            <th>
                                                <asp:Label ID="lblGSTHdr" runat="server"></asp:Label></th>
                                            <th>Total Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>
                                                <asp:Label ID="lblActivityCode" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblActivityDesc" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSAC" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblGST" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblTaxableValue" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblGSTValue" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="text-align: center">Total</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblTaxableValue_Total" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblTax_Total" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <p class="amount-word">
                                    <span>Amount in words:</span> INR
                       <asp:Label ID="lblAmountinWords" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->

                    <div class="row pt-40">
                        <div class="col-lg-12">
                            <div class="content">
                                <p>We declare that, If TDS is applicable as per Income Tax Act 1961 & its related rules, then TDS deduction is our responsibility while making payment to service provider/supplier for all cost sharing marketing expenses. Ajax is not required to deduct TDS while making payment to us. We hereby inform that any information in this regard required/called by for Ajax – same will be shared by us. We further inform that in case of any default of TDS, we will be liable to reimburse the interest/penalty levied on Ajax.</p>
                                <br>
                                <br>
                                <br>
                                <p>For, </p>
                                <p>
                                    <asp:Label ID="lblDealerName" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->
                    <br>
                    <br>
                    <br>
                    <div class="row pt-40">
                        <div class="col-lg-12">
                            <div class="a-sign">
                                <p>Authorised Signatory</p>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end row -->
                <div class="row pt-40" style="padding-top: 40px">
                    <div class="col-lg-12">
                        <div class="a-sign">
                            <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <!-- end row -->

                <!-- end container -->
            </section>
        </div>
    </form>
</body>
</html>

