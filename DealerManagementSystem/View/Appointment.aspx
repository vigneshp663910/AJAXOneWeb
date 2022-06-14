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

   <%-- <style>
         .dropdown1 {
            position: relative;
            display: inline-block;
              background-color: darkturquoise;
    padding: 3px
        }

        .dropdown1-content {
            display: none;
            position: absolute;
            background-color: #f1f1f1;
            min-width: 195px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

            .dropdown1-content a {
                color: black;
                padding: 8px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown1-content a:hover {
                    background-color: #ddd;
                }

        .dropdown1:hover .dropdown1-content {
            display: block;
        }

        .dropdown1:hover .dropbtn { 
        }

        .DateH{

        }
    </style>--%>

  
      <style>
        .dropdown1 {
            position: relative;
            display: block;
            background-color: darkturquoise;
            padding: 3px;
            margin-top: 3px;
        }

        .dropdown1-content {
            display: none;
            position: absolute;
            background-color: #336699;
            min-width: 195px;
            box-shadow: 0px 8px 16px 0px rgb(0 0 0 / 20%);
            z-index: 1;
            bottom: 29px;
            right: -136px;
            border-radius: 10px;
            padding: 10px;
            color: #fff;
        }

            .dropdown1-content a {
                color: black;
                padding: 8px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown1-content a:hover {
                    background-color: #ddd;
                }

        .dropdown1:hover .dropdown1-content {
            display: block;
        }

        .dropdown1:hover .dropbtn {
            /*  background-color: #3e8e41;*/
        }

        .DateH {
            text-align: center;
        }

        .dropdown1-content:before {
            content: "";
            display: block;
            border: inset 6px;
            border-color: #336699 transparent transparent;
            border-top-style: solid;
            position: absolute;
            bottom: -12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
  
    <br />
     
  <%--  <asp:Calendar ID="caAppointment" runat="server" BackColor="White" BorderColor="Black"   Font-Names="Verdana" 
         Font-Size="9pt" ForeColor="Black"  NextPrevFormat="FullMonth" Width="95%" OnDayRender="caAppointment_DayRender" OnSelectionChanged="caAppointment_SelectionChanged" 
        OnVisibleMonthChanged="caAppointment_VisibleMonthChanged" DayNameFormat="Full">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333"   Wrap="True"   CssClass="DateH" />
                        <DayStyle BackColor="#CCCCCC" CssClass = "Cal" BorderWidth="1px" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White"  CssClass = "Hea"/> 
                        <TitleStyle BackColor="#333399"  Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" /> 
                    </asp:Calendar>--%>

      <asp:Calendar ID="caAppointment" runat="server" BackColor="White" BorderColor="Black"   Font-Names="Verdana" 
         Font-Size="9pt" ForeColor="Black"  NextPrevFormat="FullMonth" Width="95%" OnDayRender="caAppointment_DayRender" OnSelectionChanged="caAppointment_SelectionChanged" 
        OnVisibleMonthChanged="caAppointment_VisibleMonthChanged" DayNameFormat="Full">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333"   Wrap="True"   CssClass="DateH" />
                        <DayStyle BackColor="#CCCCCC" CssClass = "Cal" BorderWidth="1px" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White"  CssClass = "Hea"/>
                      <%--  <OtherMonthDayStyle ForeColor="#999999"  />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" BorderWidth="1px" />--%>
                        <TitleStyle BackColor="#333399"  Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                      <%--  <TodayDayStyle BackColor="#999999" ForeColor="White"    /> --%>
                    </asp:Calendar>
   
</asp:Content>
