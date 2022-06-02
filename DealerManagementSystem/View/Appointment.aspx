<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="DealerManagementSystem.View.Appointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Hea{
            padding-left:50px;
            padding-right:50px
        }
         .Cal{
            background-color:white!important;
        }
    </style>
    <style>
        .fieldset-borderAuto {
            border: solid 1px #cacaca;
            margin: 1px 0;
            border-radius: 5px;
            padding: 10px;
            background-color: #b4b4b4;
        }

            .fieldset-borderAuto tr {
                /* background-color: #000084; */
                background-color: inherit;
                font-weight: bold;
                color: white;
            }

            .fieldset-borderAuto:hover {
                background-color: blue;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    
    <asp:Calendar ID="caAppointment" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" 
         Font-Size="9pt" ForeColor="Black" Height="700px" NextPrevFormat="ShortMonth" Width="95%" OnDayRender="caAppointment_DayRender" OnSelectionChanged="caAppointment_SelectionChanged" OnVisibleMonthChanged="caAppointment_VisibleMonthChanged">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt"  />
                        <DayStyle BackColor="#CCCCCC" CssClass = "Cal" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White"  CssClass = "Hea"/>
                        <OtherMonthDayStyle ForeColor="#999999"  />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                        <TodayDayStyle BackColor="#999999" ForeColor="White" /> 
                    </asp:Calendar>
   
</asp:Content>
