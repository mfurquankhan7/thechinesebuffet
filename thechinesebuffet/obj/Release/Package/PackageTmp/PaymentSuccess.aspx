﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="thechinesebuffet.PaymentSuccess" %>

<!DOCTYPE html>

<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link rel='shortcut icon' type='image/x-icon' href='favicon.ico' />
    <title>The Chinese Buffet - Reservation successful</title>

    <link href='https://fonts.googleapis.com/css?family=Roboto:400,300,500,700' rel='stylesheet' type='text/css'>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/navigation.css" rel="stylesheet" />
    <link href="css/layout.css" rel="stylesheet" />
    <link href="css/stylesheet.css" rel="stylesheet" />
    <link href="css/others.css" rel="stylesheet" />
    <link href="css/home.css" rel="stylesheet" />
    <link href="css/forms.css" rel="stylesheet" />
    <link href="css/owl.carousel.css" rel="stylesheet" />
    <link href="css/owl.theme-home.css" rel="stylesheet" />
    <link href="source/jquery.fancybox.css?v=2.1.5" rel="stylesheet" type="text/css" media="screen" />

</head>

<body>

    <header>

        <div class="center-layout">
            <div class="logo">

                <a href="home.html" target="_self">
                    <img src="images/layout/the-chinese-buffet-logo.png" />
                </a>

            </div>
            <!--/.logo-->

            <nav id="main-nav">
                <ul>
                    <li><a href="home.html" target="_self" class="active">Home</a>
                    </li>
                    <li><a href="about-us.html" target="_self">About Us</a>
                    </li>
                    <li><a href="menu.html" target="_self">Menu</a>
                    </li>
                    <li><a href="special-offers.html" target="_self">Special Offers</a>
                    </li>
                    <li><a href="gallery.html" target="_self">Gallery</a>
                    </li>
                    <li><a href="media.html" target="_self">Media</a>
                    </li>
                    <li><a href="restaurants.html" target="_self">Restaurants</a>
                    </li>
                    <li><a href="contact-us.html" target="_self">Contact Us</a>
                    </li>
                </ul>

            </nav>
            <!--/#main-nav-->

        </div>
        <!--/.center-layout-->

    </header>
    <!--/header-->

    <div class="responsive-btn">
        <i class="fa fa-bars fa-lg" aria-hidden="true"></i>
    </div>

    <div class="responsive-menu">
        <div class="resclose"><i class="fa fa-times fa-2x" aria-hidden="true"></i></div>
        <ul>
            <div class="row"><a href="home.html" class="active" target="_self">Home</a></div>
            <div class="row"><a href="about-us.html" class="" target="_self">About Us</a></div>
            <div class="row"><a href="menu.html" class="" target="_self">Menu</a></div>
            <div class="row"><a href="special-offers.html" class="" target="_self">Special Offers</a></div>
            <div class="row"><a href="gallery.html" class="" target="_self">Gallery</a></div>
            <div class="row"><a href="media.html" class="" target="_self">Media</a></div>
            <div class="row"><a href="restaurants.html" class="" target="_self">Restaurants</a></div>
            <div class="row"><a href="contact-us.html" class="" target="_self">Contact Us</a></div>

        </ul>
    </div>
    <!--/.responsive-menu-->

    <div class="square-logo">

        <div class="center-layout right-text">
            <img src="images/layout/square-logo.jpg" />
        </div>
        <!--/.center-layout-->

    </div>
    <!--/.square-logo-->

    <div class="book-table-outer">

        <div class="form-outer-container">

            <div class="form-container">

                <div class="header" id="bookshow">
                    Book a Table
                </div>
                <!--/.header-->

                <div class="form">

                    <div id="book-table">

                        <div class="form-row default">
                            <select class="form-field-common location" required>
                                <option value="1">Blackpool </option>
                                <option value="2">Bolton</option>
                                <option value="3">Bury</option>
                                <option value="11">Darlington</option>
                                <option value="4">Halifax </option>
                                <option value="5">Huddersfield</option>
                                <option value="6">Preston</option>
                                <option value="7">St. Helens</option>
                                <option value="8">Wakefield</option>
                                <option value="9">Wigan</option>
                                <option value="10">Wrexham</option>
                            </select>
                        </div>
                        <!--/.form-row-->

                        <div class="form-row default">
                            <input type="text" name="date" id="datepicker" class="form-field-common" placeholder="Date of Reservation" required />
                        </div>
                        <!--/.form-row-->


                        <div class="form-row default">
                            <select class="form-field-common myguest" name="guest" required>
                                <option value="0" selected>No. of guest</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="30+">30+</option>
                            </select>
                        </div>
                        <!--/.form-row-->
                        <div class="form-row default">

                            <select class="form-field-common myslot" name="slot" required>
                                <option value="">Available Slots</option>
                            </select>

                        </div>

                        <div class="form-row default">
                            <select class="form-field-common myguesthighchairs" name="guest" required>
                                <option value="0" selected>No. of high chairs</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="30+">30+</option>
                            </select>
                        </div>
                        <!--/.form-row-->

                        <div class="form-row default">
                            <select class="form-field-common myguestwheelchairs" name="guest" required>
                                <option value="0" selected>No. of wheel chairs</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="30+">30+</option>
                            </select>
                        </div>
                        <!--/.form-row-->

                        <div class="form-row default">
                            <select class="form-field-common myguestpramsseats" name="guest" required>
                                <option value="0" selected>No. of pram seats</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                                <option value="24">24</option>
                                <option value="25">25</option>
                                <option value="26">26</option>
                                <option value="27">27</option>
                                <option value="28">28</option>
                                <option value="29">29</option>
                                <option value="30">30</option>
                                <option value="30+">30+</option>
                            </select>
                        </div>
                        <!--/.form-row-->

                        <!--/.form-row-->
                        <div class="form-row default">
                            <input type="hidden" name="getlocation" id="booklocation" value="Blackpool">
                            <input type="hidden" name="getguest" id="bookguest" value="">
                            <input type="hidden" name="getslot" id="bookslot" value="">
                            <input type="button" onclick="booktable();" class="form-field-common submit" value="Submit" />
                            <a class="fancybox" onclick="hideBookaTable();" href="#couldnotbook" title="">Could not Book?</a>

                        </div>
                        <!--/.form-row-->

                    </div>
                    <!--/#book-table-->

                    <div id="couldnotbook" style="width:400px;display: none;">

                        <div class="form-row default">
                            <select class="form-field-common pop-location" required>
                                <option value="1">Blackpool </option>
                                <option value="2">Bolton</option>
                                <option value="3">Bury</option>
                                <option value="11">Darlington</option>
                                <option value="4">Halifax </option>
                                <option value="5">Huddersfield</option>
                                <option value="6">Preston</option>
                                <option value="7">St. Helens</option>
                                <option value="8">Wakefield</option>
                                <option value="9">Wigan</option>
                                <option value="10">Wrexham</option>
                            </select>
                        </div>
                        <!--/.form-row-->
                        <div class="form-row default">
                            <input type="text" id="couldnotbook-name" name="name" class="form-field-common" placeholder="Name..." required />
                        </div>
                        <div class="form-row default">
                            <input type="number" id="couldnotbook-mobile" name="mobile" class="form-field-common" placeholder="Mobile..." pattern="[0-9.]*" required />
                        </div>
                        <!--/.form-row-->

                        <div class="form-row default">
                            <input type="submit" onclick="couldnotbook();" class="form-field-common submit" value="Submit" />
                        </div>
                        <!--/.form-row-->

                    </div>
                    <!--#couldnotbook-->

                </div>
                <!--/.form-->

            </div>
            <!--/.top-container-->

        </div>
        <!--/.form-inner-container-->

    </div>
    <!--/.book-table-outer-->

    <div class="top-container default">

        <div class="center-layout">

            <div class="center-layout">
                <div class="top-container-inner top-left">
                    <ul>
                        <li><a href="login.html" target="_self">Login</a></li>
                        <li><a href="signup.html" target="_self">Sign Up</a></li>
                        <li><a href="careers.html" target="_self">Careers</a></li>
                    </ul>

                    <div class="social-follow">
                        <div class="fb-like" data-href="https://www.facebook.com/TheChineseBuffet/" data-layout="button_count" data-action="like" data-size="small" data-show-faces="true" data-share="false"></div>
                        <div id="fb-root"></div>
                        <script>
                            (function (d, s, id) {
                                var js, fjs = d.getElementsByTagName(s)[0];
                                if (d.getElementById(id)) return;
                                js = d.createElement(s);
                                js.id = id;
                                js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.7";
                                fjs.parentNode.insertBefore(js, fjs);
                            }(document, 'script', 'facebook-jssdk'));

                        </script>
                    </div>
                    <div class="social-follow">
                        <a href="https://twitter.com/TChineseBuffet" class="twitter-follow-button" data-show-screen-name="false" data-show-count="true">Follow @TChineseBuffet</a>
                        <script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
                    </div>

                </div>
                <div class="top-container-inner1 top-right">
                    <ul>
                        <li><a href="https://www.facebook.com/TheChineseBuffet/" target="_blank" class="social-text"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
                        <li><a href="https://twitter.com/TChineseBuffet" target="_blank" class="social-text"><i class="fa fa-twitter" aria-hidden="true"></i></a></li>
                        <li class="last"><a href="https://www.instagram.com/the_chinese_buffet/" target="_blank" class="social-text"><i class="fa fa-instagram" aria-hidden="true"></i></a></li>
                        <!--<li class="last"><a href="#" target="_self" class="social-text"><i class="fa fa-tripadvisor" aria-hidden="true"></i></a></li>-->
                    </ul>
                </div>
            </div>
            <!--/.center-layout-->

        </div>
        <!--/.center-layout-->

    </div>
    <!--/.top-container-->

    <div class="main-container default">

        <div class="default careers-bg first-div">

            <div class="center-layout">

                <h1>Reservation</h1>
                <!--/h1-->
                <form id="form1" runat="server">
                    <div id="divProducts" runat="server" visible="True" >
                        <h2>Thank you for your order!</h2>
                    <div>
                        <asp:Panel runat="server" ID="pnlSuccess" Visible="false" ForeColor="black">
                <br />
                <br />
                    You will receive an email shortly confirming your order.
                <br />
                <br />
                Your order ID is <asp:Literal runat="server" ID="litVendorTXCode"></asp:Literal>
                            <br />
                <br />
                Your Reservation code is <asp:Literal runat="server" ID="litCode"></asp:Literal>
            </asp:Panel>
        </div>
    </div>

	<%--<table border="0" width="100%" class="formFooter">
		<tr>
			<td colspan="2">&nbsp;</td>
		</tr>
		<tr>
		    <td width="50%" align="left"></td>
			<td width="50%" align="right"><asp:ImageButton ID="proceed" ImageUrl="/images/proceed.gif"  runat="server" OnClick="proceed_Click" /></td>
		</tr>
	</table>--%>
    
    </form>
                


            </div>
            <!--/.center-layout-->

        </div>
        <!--/.aboutus-bg-->

    </div>
    <!--/.main-container-->

    <footer class="default">

        <div class="center-layout center-text">
            Copyright 2016 THE Chinese Buffet, All rights reserved. <a href="terms-conditions.html" target="_self">Terms &amp; Conditions</a> | <a href="http://ec.europa.eu/ipg/basics/legal/cookies/index_en.htm" target="_blank">Cookies</a>

        </div>
        <!--/.center-layout-->

    </footer>
    <!--/footer-->
    <div id="reservation-outer">
            <div data-role="popup" id="reservation-inner-grid" data-theme="b" class="ui-corner-all">
                
            </div>
            <div data-role="popup" id="reservation-inner-details" data-theme="b" class="ui-corner-all">
                <table id="customerDetails" style="width: 100%;">
                        <tbody><tr>
                            <td colspan="2">
                                    <h1 style="font-size:15px !important;line-height:40px !important;">Please enter your details below</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                First Name(s):
                            </td>
                            <td>
                                    <input name="txt-firstname" type="text" maxlength="20" class="form-field-common" placeholder="Firstname..." id="txt-firstname">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Surname:
                            </td>
                            <td>
                                <input name="txt-surname" type="text" maxlength="20" class="form-field-common" placeholder="Surname..." id="txt-surname">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address Line 1:
                            </td>
                            <td>
                                <input name="txt-addressline1" type="text" maxlength="100" class="form-field-common" placeholder="Address Line1..." id="txt-addressline1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address Line 2:
                            </td>
                            <td>
                                <input name="txt-addressline2" type="text" maxlength="100" class="form-field-common" placeholder="Address Line2..." id="txt-addressline2">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                City:
                            </td>
                            <td>
                                <input name="txt-city" type="text" class="form-field-common" placeholder="City..." maxlength="40" id="txt-city">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Postal Code:
                            </td>
                            <td>
                                <input name="txt-zipcode" class="form-field-common" placeholder="Postal code..." type="text" maxlength="10" id="txt-zipcode">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile:
                            </td>
                            <td>
                                <input name="txt-mobile" type="text" class="form-field-common" placeholder="Mobile number..." maxlength="20" id="txt-mobile">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email Address:
                            </td>
                            <td> 
                                <input name="txt-email" type="text" class="form-field-common" placeholder="Email..." maxlength="255" id="txt-email">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Notes:
                            </td>
                            <td>
                                <textarea style="width:168px;" name="txt-notesarea" rows="2" class="form-field-common" placeholder="Notes..." cols="20" id="txt-notes"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                                <br />
                                <button onclick="closeReservationBox();">Close</button>
                                <button type="button" name="next" value="Next" onclick="proceedToAskPayment()" id="btn-next" class="button">Next</button>
                            </td>
                        </tr>
                    </tbody></table>

            </div>


            <div data-role="popup" id="reservation-inner-askpayment" data-theme="b" class="ui-corner-all">
                <div style="text-align:center;">
                    <div style="font-size: 19px;font-weight: bold;padding: 15px;" >Do you want to pay a deposit to secure your booking?</div>
                    <br />
                    <button onclick="closeReservationBox();">Close</button>
                    <button id="pay-back" onclick="goto('reservation-inner-details');">Back</button>
                    <button id="pay-yes" onclick="payDeposit(true);">Yes</button>
                    <button id="pay-no" onclick="payDeposit(false);">No</button>
                </div>
            </div>
            <div data-role="popup" id="reservation-inner-dopayment" data-theme="b" class="ui-corner-all">
                <div id="BodyContent_pnlOrderConfirmation">
                    <table id="tblOrderConfirmation" style="width: 100%;" cellpadding="5" cellspacing="5">
                        <tbody><tr>
                            <td colspan="2">
                                <h1 style="font-size: 23px !important;line-height: 37px !important;">Confirm Your Details</h1>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Restaurant:</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblRestaurant">Bolton</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Date:</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblDate">03/08/2016</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Time:</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblTime">1200</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Number of guests:</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblNoOfGuests">8</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>First Name(s):</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblFirstName">Muhammed</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Surname:</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblSurname">Khan</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Deposit:</b>
                            </td>
                            <td>
                                <span id="BodyContent_lblDeposit">40.00</span>
                            </td>
                        </tr>
                    </tbody></table>
                    <div style="margin: 10px;" class="extraBookingInfo">
                        <a href="#" id="btnMoreInfo">Extra Booking Information..</a>
                    </div>
                    <div data-role="popup" id="reservation-inner-additionalinfo"  style="width: 100%; display: none;" data-theme="b" class="ui-corner-all">
                    <table id="tblExtraInfo" style="width: 100%;" cellpadding="5" cellspacing="5">
                        <tbody><tr>
                            <td>
                                Number of highchairs:
                            </td>
                            <td>
                                <span id="BodyContent_lblNoOfHighChairs">0</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Number of wheelchairs
                            </td>
                            <td>
                                <span id="BodyContent_lblNoOfWheelChairs">0</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Number of prams
                            </td>
                            <td>
                                <span id="BodyContent_lblNoOfPrams">0</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seats
                            </td>
                            <td>
                                <span id="BodyContent_lblConfirmationSeats">8</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address Line 1:
                            </td>
                            <td>
                                <span id="BodyContent_lblAddressLine1">A-201, Yogeshwar Apartment, market road, Near mumbra railway station, Mumbra, District- Thane</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address Line 2:
                            </td>
                            <td>
                                <span id="BodyContent_lblAddressLine2">jij</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                City:
                            </td>
                            <td>
                                <span id="BodyContent_lblCity">Mumbai</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Post/Zip Code:
                            </td>
                            <td>
                                <span id="BodyContent_lblPostCode">400612</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile:
                            </td>
                            <td>
                                <span id="BodyContent_lblMobile">07045242797</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email Address:
                            </td>
                            <td>
                                <span id="BodyContent_lblEmail">mfurquankhan7@gmail.com</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Notes:
                            </td>
                            <td>
                                <span id="BodyContent_lblNotes">jhgfjuyf</span>
                            </td>
                        </tr>
                    </tbody>
                    </table>
                    <button onclick="javascript:$('#reservation-inner-additionalinfo').hide();">Close</button>
                    </div>
                    <p><b>To complete this booking, you need to pay a deposit of <span class="pound"></span><span id="depositPerPerson">5</span> per person as the booking is for more than <span id="maxNonBookingPersonCount">4</span> people.</b> Note: you will be transfered back to The Chinese Buffet website after paying the deposit confirming your booking.</p>
                    <table style="width: 100%;" cellpadding="5" cellspacing="5">
                        <tbody><tr>
                            <td>
                                <button onclick="closeReservationBox();">Close</button>
                            </td>
                            <td>
                                <button type="submit" name="payment-back" onclick="goto('reservation-inner-askpayment')" value="Back" id="btn_confimationBack">Back</button>
                            </td>
                            <td>
                                <button type="submit" name="payment-back" onclick="paydeposittosagepay();" value="btn_confimationPay" id="btn_confimationPay">Pay Deposit</button>
                            </td>
                        </tr>
                    </tbody></table>
                
</div>
            </div>
        <div data-role="popup" id="reservation-inner-confirmpayment" data-theme="b" class="ui-corner-all">
            <div id="no-deposit-paid"><h2>Thank you for making a reservation at THE Chinese Buffet!</h2>
                <div id="nodepositsuccessmsg">Due to no deposit being paid, your reservation is not guaranteed, Please contact THE Chinese Buffet on THE Chinese Buffet <span id="booked-restName">Bolton</span> on <span id="booked-restNumber">01204 388222</span> to confirm your reservation. 
                    Your reservation code is: <span id="booked-id">241273</span> 
                </div>
            </div>
                <br />
                <button onclick="closeReservationBox();">Close</button>
            </div>
        <!--<button onclick="closeReservationBox();">Close</button>-->
    </div>
    <div id="loading-screen" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%; background-color: rgba(0,0,0,0.5); clear: both; text-align: center; display: none; vertical-align: middle;">
        <div style="padding:10px;width:130px;display: inline-block;margin: 0 auto;position:relative;top:300px;">
            <img src="images/bgs/loading.gif" /><br />
            <p style="color: #fff;">Please wait...</p>
        </div>
    </div>
</body>

<script src="js/jquery-1.10.2.js"></script>
<script src="js/jquery-ui.js"></script>
<script src="source/jquery.fancybox.js?v=2.1.5"></script>
<script src="js/others.js"></script>
<script src="js/common.js"></script>
<script src="js/SmartBox.js"></script>
<script src="js/main.js"></script>
<script src="js/jspostcode.js"></script>
<script src="js/jstelnumbers.js"></script>
<script>
    jQuery(document).ready(function () {
        populateRestaurantDropdown(false, false);
        $("#datepicker").datepicker({
            dateFormat: 'dd-mm-yy'
        });
        $('.fancybox').fancybox({
            padding: 20,
            helpers: {
                overlay: {
                    locked: false
                }
            }
        });
        var status = getParameterByName("status");
        var reasonCode = getParameterByName("reasonCode");
        var VendorTxCode = getParameterByName("VendorTxCode");
        var o = getParameterByName("o");
        if (status == "fail") {
            if (reasonCode == "001")
            { }
            else
            { }
        }
        else if (status == "success") {
            if (o == p)
            { }
            else
            { }
        }
        //$('#apply-form').submit(function() {
        //    $.ajax({
        //        type: "POST",
        //        url: "sources/apply-form.php",
        //        data: $('form').serialize(),
        //        success: function(data) {
        //            alert(data);
        //        },
        //        error: function(data) {
        //            console.log(data);
        //        }
        //    });
        //    return false;
        //});
    });
    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
</script>

</html>
