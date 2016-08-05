
var SELECTEDLOCID = 0;
var SLOTSELECTED = 0;
var CLIENTIP = 0;
var customerDetailsJson = {};
var toProceedwithotherservices = true;
var toProceedwithotherrestaurant = true;
var custPaidDeposit = false;
var jobApplyLocation = null;
var jobApplyTitle = null;
var maxNonBookingPersonCount = 4;
var depositPerPerson = 5;
$(function () {
    $(".pound").html("&pound;");
    $("#reservation-outer").hide();
    $('#bookshow').click(function () {
        if ($('.book-table-outer').css('bottom') == '-460px') {
            $('.book-table-outer').css('bottom', 0);
        } else {
            $('.book-table-outer').css('bottom', -460);
        }
    });
    $("#btnMoreInfo").click(function () {
        fillFields();
        $('#reservation-inner-additionalinfo').show();
    });
    $.getJSON("http://jsonip.com/?callback=?", function (data) {
        console.log(data);
        CLIENTIP = data.ip;
    });
    //$('#book-table').submit(function () {
    //    var restID = $('.location').val();
    //    var data = "{'restID':'" + restID + "'}";
    //    service.get("bookTable",data,bookTablesuccess,bookTableerror)
    //    //$.ajax({
    //    //    type: "POST",
    //    //    url: "sources/book-table.php",
    //    //    data: $('form').serialize(),
    //    //    success: function (data) {
    //    //        alert(data);
    //    //        $('.book-table-outer').find('form')[0].reset();
    //    //        $('.book-table-outer').css('bottom', -510);
    //    //    },
    //    //    error: function (data) {
    //    //        console.log(data);
    //    //    }
    //    //});
    //    return false;
    //});
    $('.myguest').change(function () {
        var moreoption = '<option  value="1200">12:00 PM</option><option  value="1215">12:15 PM</option><option  value="1230">12:30 PM</option><option  value="1245">12:45 PM</option><option value="1300">13:00 PM</option><option value="1315">13:15 PM</option><option value="1330">13:30 PM</option><option value="1345">13:45 PM</option><option value="1400">14:00 PM</option><option value="1415">14:15 PM</option><option value="1430">14:30 PM</option><option value="1445">14:45 PM</option><option value="1500">15:00 PM</option><option value="1515">15:15 PM</option><option value="1530">15:30 PM</option><option value="1545">15:45 PM</option><option value="1600">16:00 PM</option><option value="1615">16:15 PM</option><option value="1630">16:30 PM</option><option value="1645">16:45 PM</option><option value="1700">17:00 PM</option><option value="1715">17:15 PM</option><option value="1730">17:30 PM</option><option value="1745">17:45 PM</option><option value="1800">18:00 PM</option><option value="1815">18:15 PM</option><option value="1830">18:30 PM</option><option value="1845">18:45 PM</option><option value="1900">19:00 PM</option><option value="1915">19:15 PM</option><option value="1930">19:30 PM</option><option value="1945">19:45 PM</option><option value="2000">20:00 PM</option><option value="2015">20:15 PM</option><option value="2030">20:30 PM</option><option value="2045">20:45 PM</option><option value="2100">21:00 PM</option><option value="2115">21:15 PM</option><option value="2130">21:30 PM</option><option value="2145">21:45 PM</option><option value="2200">22:00 PM</option><option value="2215">22:15 PM</option><option value="2230">22:30 PM</option><option value="2245">22:45 PM</option>';
        var lessoption = '<option  value="1200">12:00 PM</option><option  value="1215">12:15 PM</option><option  value="1230">12:30 PM</option><option  value="1245">12:45 PM</option><option value="1300">13:00 PM</option><option value="1315">13:15 PM</option><option value="1330">13:30 PM</option><option value="1345">13:45 PM</option><option value="2000">20:00 PM</option><option value="2015">20:15 PM</option><option value="2030">20:30 PM</option><option value="2045">20:45 PM</option><option value="2100">21:00 PM</option><option value="2115">21:15 PM</option><option value="2130">21:30 PM</option><option value="2145">21:45 PM</option><option value="2200">22:00 PM</option><option value="2215">22:15 PM</option><option value="2230">22:30 PM</option><option value="2245">22:45 PM</option>';
        var guest = parseInt($(this).val());
        if (guest == 0) {
            $('.myslot').html('<option value="">Available Slots</option>');
        } else if (guest <= 20) {
            $('.myslot').html(moreoption);
        } else {
            $('.myslot').html(lessoption);
        }
    });
    $('.location').change(function () {
        $('#booklocation').attr('value', $(this).val());
    });
    $('.myguest').change(function () {
        $('#bookguest').attr('value', $(this).val());
    });
    $('.myslot').change(function () {
        $('#bookslot').attr('value', $(this).val());
    });
});
function booktable() {
    if (validateBookTable()) {
        var restID = $('.location').val();
        var datetime = $('#datepicker').val();
        var arr = datetime.split('-');
        datetime = arr[2] + "-" + arr[1] + "-" + arr[0];
        var data = "{'restID':'" + restID + "','bookingDateTime':'" + datetime + "','ipaddress':'" + CLIENTIP + "'}";
        services.get("bookTable", data, bookTablesuccess, bookTableerror);
    }
}
function validateBookTable() {
    var totalguest = parseInt($(".myguest").val());
    var highchair = parseInt($(".myguesthighchairs").val());
    var wheelchair = parseInt($(".myguestwheelchairs").val());
    var pramseats = parseInt($(".myguestpramsseats").val());
    var restID = $('.location').val();
    var datetime = $('#datepicker').val();
    if (restID == "0") {
        showmessage("Please select a Restaurant for booking", $('.location'));
        return false;
    }
    if (datetime == "") {
        showmessage("Please select a date for booking", $('#datepicker'));
        return false;
    }
    else {
        var arr = datetime.split('-');
        var slot = $('.myslot option:selected').text();
        slot = slot.split(" ")[0];
        datetime = arr[2] + "-" + arr[1] + "-" + arr[0];
        datetime = new Date(datetime);
        datetime.setHours(slot.split(":")[0]);
        datetime.setMinutes(slot.split(":")[1]);
        var now = new Date();
        if (now > datetime) {
            showmessage("Please select a date in future", $('#datepicker'));
            return false;
        }

    }
    if (totalguest == 0) {
        showmessage("Please select number of guests", $(".myguest"));
        return false;
    }
    if (highchair + wheelchair + pramseats > totalguest) {
        showmessage("Total guests cannot be less than sum of high, wheel and prams seats",null);
        return false;
    }
    
    return true;
}
function bookTablesuccess(data)
{
    if (data[0].status == "-1") {
        showmessage("Sorry you can't make more than two bookings per day.",null);
        return;
    }
    var recs = data[0].records;
    depositPerPerson = data[0].depositPerPerson;
    maxNonBookingPersonCount = data[0].maxNonBookingPersonCount;
    $("#depositPerPerson").html(depositPerPerson);
    $("#maxNonBookingPersonCount").html(maxNonBookingPersonCount);
    var slot = $('.myslot').val();
    SLOTSELECTED = slot;
    var noofguest = $(".myguest").val();
    var dateofreservation = $("#datepicker").val();
    var arr = dateofreservation.split('-');
    dateofreservation = arr[2] + "-" + arr[1] + "-" + arr[0];
    dateofreservation = new Date(dateofreservation);
    var isSeatAvailable = true;
    var isSunday = dateofreservation.getDay() == 0 ? true : false;
    for (var i = 0; i <= recs.length - 1; i++)
    {
        if (slot == recs[i].Time)
        {
            if (parseInt(recs[i].NoOfSpaces) > 0) {
                if (parseInt(recs[i].MaxReservationsAtThisTime) < noofguest) {
                    isSeatAvailable = false;
                    break;
                }
            }
            else {
                isSeatAvailable = false;
                break;
            }
        }
    }
    if (!isSeatAvailable) {
        //show the grid
        $("#reservation-outer > div").hide();
        $("#reservation-outer").show();
        $("#reservation-inner-grid").show();
        if (isSunday) {
            var html = "<div class='gridouter'>";
            html += "<div class='gridouter-postlunch'><br/><div><b>Sunday Buffet</b></div><br/>";
            for (var i = 0; i <= recs.length - 1; i++) {
                var _class = "red";
                var _disabled = "true";
                if (parseInt(recs[i].NoOfSpaces) > 0) {
                    _class = "green";
                    _disabled = "false;"
                }
                if(_disabled == "true")
                    html += "<input type='button' id='reservation-" + recs[i].Time + "' onclick='reserveTimeSlot(this)' class='" + _class + " spangrid' disabled='" + _disabled + "' value='" + recs[i].Time + "'/>";
                else
                    html += "<input type='button' id='reservation-" + recs[i].Time + "' onclick='reserveTimeSlot(this)' class='" + _class + " spangrid' value='" + recs[i].Time + "'/>";

            }
            html += "</div><br/>";
            html += "<br/><button onclick='closeReservationBox();'>Close</button></div>";
            $("#reservation-inner-grid").html(html);
        }
        else {
            var html = "<div class='gridouter'>";
            var prelunch = "<div class='gridouter-prelunch'><div><b>Lunch Menu</b></div>";
            var postlunch = "<div class='gridouter-postlunch'><div><b>Grand Buffet</b></div>";
            for (var i = 0; i <= recs.length - 1; i++) {
                var _class = "red";
                var _disabled = "true";
                if (parseInt(recs[i].NoOfSpaces) > 0) {
                    _class = "green";
                    _disabled = "false;"
                }
                if (parseInt(recs[i].Time) < 1700) {
                    prelunch += "<input type='button' id='reservation-" + recs[i].Time + "' onclick='reserveTimeSlot(this)' class='" + _class + " spangrid' disabled='" + _disabled + "' value='" + recs[i].Time + "'/>";
                }
                else
                    postlunch += "<input type='button' id='reservation-" + recs[i].Time + "' onclick='reserveTimeSlot(this)' class='" + _class + " spangrid' disabled='" + _disabled + "' value='" + recs[i].Time + "'/>";
            }
            prelunch += "</div><br/>";
            postlunch += "</div><br/>";
            html += "<br/>"+prelunch+postlunch+"<button onclick='closeReservationBox();'>Close</button></div>";
            $("#reservation-inner-grid").html(html);
        }
    }
    else
        gotocustomerdetails();

}
function bookTableerror(data) {
    showmessage("Some error found while booking the table",null);
}
function gotocustomerdetails()
{
    $("#reservation-outer > div").hide();
    $("#reservation-outer").show();
    $("#reservation-inner-details").show();
}
function proceedToAskPayment() {
    if (validateCustomerDetails())
    {
        customerDetailsJson["BookingRestaurantID"] = $('.location').val();
        customerDetailsJson["BookingRestaurantName"] = $('.location option:selected').html();
        var date = $('#datepicker').val();
        var arr = date.split("-");
        date = arr[2] + "-" + arr[1] + "-" + arr[0];
        customerDetailsJson["BookingDate"] = date;
        customerDetailsJson["BookingNoOfGuests"] = $(".myguest").val();
        customerDetailsJson["BookingNoOfHighChairs"] = $(".myguesthighchairs").val();
        customerDetailsJson["BookingNoOfWheelChairs"] = $(".myguestwheelchairs").val();
        customerDetailsJson["BookingNoOfPramsSeat"] = $(".myguestpramsseats").val();
        customerDetailsJson["BookingTimeSlot"] = SLOTSELECTED;
        customerDetailsJson["BookingFirstName"] = $("#txt-firstname").val();
        customerDetailsJson["BookingSurName"] = $("#txt-surname").val();
        customerDetailsJson["BookingAddressLine1"] = $("#txt-addressline1").val();
        customerDetailsJson["BookingAddressLine2"] = $("#txt-addressline2").val();
        customerDetailsJson["BookingCity"] = $("#txt-city").val();
        customerDetailsJson["BookingPostalCode"] = $("#txt-zipcode").val();
        customerDetailsJson["BookingMobile"] = $("#txt-mobile").val();
        customerDetailsJson["BookingEmail"] = $("#txt-email").val();
        customerDetailsJson["BookingNotes"] = $("#txt-notes").val();
        customerDetailsJson["BookingIpAddress"] = CLIENTIP;
        customerDetailsJson["BookingDeposit"] = 0;
        customerDetailsJson["UniqueOrderID"] = 0;//TODO
        $("#reservation-outer > div").hide();
        $("#reservation-outer").show();
        if (parseInt($(".myguest").val()) > maxNonBookingPersonCount) {
            $("#reservation-inner-dopayment").show();
            fillFields();
        }
        else {
            //$("#pay-back").hide();
            $("#reservation-inner-askpayment").show();
        }
    }
}
function fillFields() {
    $("#BodyContent_lblRestaurant").html($("#book-table .location option[value='" + customerDetailsJson["BookingRestaurantID"] + "']").text());
    $("#BodyContent_lblDate").html($('#datepicker').val());
    $("#BodyContent_lblTime").html(SLOTSELECTED);
    $("#BodyContent_lblNoOfGuests").html(customerDetailsJson["BookingNoOfGuests"]);
    $("#BodyContent_lblFirstName").html(customerDetailsJson["BookingFirstName"]);
    $("#BodyContent_lblSurname").html(customerDetailsJson["BookingSurName"]);
    $("#BodyContent_lblDeposit").html("<span class='pound'></span>"+customerDetailsJson["BookingNoOfGuests"] * 5);

    $("#BodyContent_lblNoOfHighChairs").html(customerDetailsJson["BookingNoOfHighChairs"]);
    $("#BodyContent_lblNoOfWheelChairs").html(customerDetailsJson["BookingNoOfWheelChairs"]);
    $("#BodyContent_lblNoOfPrams").html(customerDetailsJson["BookingNoOfPramsSeat"]);
    $("#BodyContent_lblConfirmationSeats").html(customerDetailsJson["BookingNoOfGuests"]);
    $("#BodyContent_lblAddressLine1").html(customerDetailsJson["BookingAddressLine1"]);
    $("#BodyContent_lblAddressLine2").html(customerDetailsJson["BookingAddressLine2"]);
    $("#BodyContent_lblCity").html(customerDetailsJson["BookingCity"]);
    $("#BodyContent_lblPostCode").html(customerDetailsJson["BookingPostalCode"]);
    $("#BodyContent_lblMobile").html(customerDetailsJson["BookingMobile"]);
    $("#BodyContent_lblEmail").html(customerDetailsJson["BookingEmail"]);
    $("#BodyContent_lblNotes").html(customerDetailsJson["BookingNotes"]);
    $(".pound").html("&pound;");
    
}
function paydeposittosagepay() {
    customerDetailsJson["BookingDeposit"] = parseInt($(".myguest").val()) * depositPerPerson;
    services.get("paydeposit", JSON.stringify(customerDetailsJson), paydeposittosagepaySuccess, paydeposittosagepayFail);
}
function paydeposittosagepaySuccess(data) {
    customerDetailsJsonWithDeposit = customerDetailsJson;
    if (data[0].status == "0") {
        showmessage(data[0].statusstr + " , " + data[0].statusdetail);
        return;
    }
    if (data[0].status == "1")
    {
        //irs["SecurityKey"] + "\",\"nexturl\":\"" + keyValuePairs["NextURL"] + "\"}";
        var statusstr = data[0].statusstr;
        var statusdetail = data[0].statusdetail;
        var vpstxid = data[0].vpstxid;
        var securitykey = data[0].securitykey;
        var nexturl = data[0].nexturl;
        window.location = nexturl;
    }
}
function paydeposittosagepayFail(data) { }
function validateCustomerDetails()
{
    var toreturn = true;
    var firstname = $("#txt-firstname").val();
    var surname = $("#txt-surname").val();
    var address1 = $("#txt-addressline1").val();
    var address2 = $("#txt-addressline2").val();
    var city = $("#txt-city").val();
    var zipcode = $("#txt-zipcode").val();
    var mobile = $("#txt-mobile").val();
    var email = $("#txt-email").val();
    var notes = $("#txt-notes").val();
    if (firstname == "") {
        showmessage("Please enter your First Name", $("#txt-firstname"));
        return;
    }
    if (surname == "") {
        showmessage("Please enter your Surname", $("#txt-surname"));
        return;
    }
    if (address1 == "") {
        showmessage("Please enter your Address Line 1", $("#txt-addressline1"));
        return;
    }
    //if (address2 == "") {
    //    alert("Enter Address Line 2");
    //    return;
    //}
    if (city == "") {
        showmessage("Please enter your city", $("#txt-city"));
        return;
    }
    if (!checkPostCode(zipcode))
    {
        showmessage("Postcode has invalid format", $("#txt-zipcode"));
        return;
    }
    if (mobile == "") {
        showmessage("Pleae enter your mobile number", $("#txt-mobile"));
        return;
    }
    if (!checkUKTelephone(mobile)) {
        showmessage(telNumberErrors[telNumberErrorNo], $("#txt-mobile"));
        return;
    }
    if (email == "") {
        showmessage("Please enter your email", $("#txt-email"));
        return;
    }
    if (!isEmail(email)) {
        showmessage("The email address entered was invalid", $("#txt-email"));
        return;
    }
    //if (notes == "") {
    //    alert("Enter Notes");
    //    return;
    //}
    return toreturn
}
function reserveTimeSlot(e)
{
    if ($(e).hasClass('red'))
    {
        showmessage("This slot is full",null);
        return;
    }
    SLOTSELECTED = $(e).val();
    $('.myslot').val(SLOTSELECTED);
    gotocustomerdetails();
}
function closeReservationBox() {
    $("#reservation-outer").hide();
    if ($('#reservation-inner-confirmpayment').css('display') != "none")
        $('.book-table-outer').css('bottom', -460);

}
function populateRestaurantDropdown(e,f)
{
    toProceedwithotherservices = e;
    toProceedwithotherrestaurant = f;
    services.get("getAllRestaurants", null, populateRestaurantDropdownSuccess, populateRestaurantDropdownFail);
}
function populateRestaurantDropdownSuccess(data)
{
    var recs = data[0].records;
    var opt = "";
    var firstRest = 0;
    for (var i = 0; i <= recs.length - 1; i++) {
        if (i == 0)
            firstRest = recs[i].id;
        opt += "<option value='" + recs[i].id + "'>" + recs[i].name + "</option>";
    }
    $('.pop-location').html(opt);
    $('.location').html(opt);
    $('.pop-location').val(firstRest);
    $('.location').val(firstRest);
    $('.getlocation').html(opt);
    $('.getlocation').val(firstRest);
    $('.gallery-location').html(opt);
    $('.gallery-location').val(firstRest);

    $('.rest-gallery-location').html(opt);
    $('.rest-gallery-location').val(firstRest);
    var curLoc = firstRest;
    $('#locId').attr('value', curLoc);
    SELECTEDLOCID = curLoc;
    makeDdAlphabetical($('.pop-location'));
    makeDdAlphabetical($('.location'));
    makeDdAlphabetical($('.getlocation'));
    makeDdAlphabetical($('.gallery-location'));
    makeDdAlphabetical($('.rest-gallery-location'));
    if (toProceedwithotherservices && toProceedwithotherrestaurant) {
        getAddress($('.getlocation').val());
        getGallery($('.gallery-location').val());
    }
    //getContentForRestaurants(SELECTEDLOCID);
}
function makeDdAlphabetical(e) {
    var options = $(e).find("option");
    var arr = options.map(function (_, o) { return { t: $(o).text(), v: o.value }; }).get();
    arr.sort(function (o1, o2) { return o1.t > o2.t ? 1 : o1.t < o2.t ? -1 : 0; });
    options.each(function (i, o) {
        o.value = arr[i].v;
        $(o).text(arr[i].t);
    });
}
function getAllNews() {
    services.get("getAllNews", null, getAllNewsSuccess, getAllNewsFail);
}
function getAllNewsSuccess(data) {
    var divhtml = "";
    var wholehtml = '<ul>';
    var recs = data[0].records;
    for (var i = 0; i <= recs.length - 1 ; i++) {

        var html = '<li><a class="fancybox" href="#inline'+i+'">';
        html += '<div class="default" style="text-align:center;">';
        html += '<img class="newsImg"  src="../services/getImage.aspx?id=' + recs[i].imageID + '" />';
        html += '<div class="pink-hover"><div class="pink-hover-inner-media"><div style="text-align:left;">';
        html += '<div style="margin:5px;font-size: 12px;color: black;font-weight: bold;color:white;">'+recs[i].Date+'</span><br/><span class="day"></span></div></div></div>';
        html += '<article style="text-align:center;"><h3>'+recs[i].Title+'</h3></article></div></a></li>';
        wholehtml += html;

        divhtml += '<div id="inline'+i+'" class="media-details">';
        divhtml += '<h3>'+recs[i].Title+' <span style="color:red;">'+recs[i].Date+'</span></h3>';
        divhtml +='<img class="newsImgInside"  src="../services/getImage.aspx?id=' + recs[i].imageID + '" />';
        divhtml += '<p>' + recs[i].Content + '</p></div>';
    }
    wholehtml += "</ul>";
    $(".image-grid-media").html(wholehtml + divhtml);
    //$(".newsImgInside").load(function () {
    //    var maxWidth = 750; // Max width for the image
    //    var maxHeight = 500;    // Max height for the image
    //    var ratio = 0;  // Used for aspect ratio
    //    var width = $(this).width();    // Current image width
    //    var height = $(this).height();  // Current image height

    //    // Check if the current width is larger than the max
    //    if (width > maxWidth) {
    //        ratio = maxWidth / width;   // get ratio for scaling image
    //        $(this).css("width", maxWidth); // Set new width
    //        $(this).css("height", height * ratio);  // Scale height based on ratio
    //        height = height * ratio;    // Reset height to match scaled image
    //        width = width * ratio;    // Reset width to match scaled image
    //    }

    //    // Check if current height is larger than max
    //    if (height > maxHeight) {
    //        ratio = maxHeight / height; // get ratio for scaling image
    //        $(this).css("height", maxHeight);   // Set new height
    //        $(this).css("width", width * ratio);    // Scale width based on ratio
    //        width = width * ratio;    // Reset width to match scaled image
    //        height = height * ratio;    // Reset height to match scaled image
    //    }
    //    var containerwidth = $(this).parent().width();
    //    if (containerwidth > width) {
    //        var left = (containerwidth - width) / 2;
    //        $(this).css("margin-left", left + "px");
    //    }
    //});
    $(".newsImg").load(function () {
        var maxWidth = 300; // Max width for the image
        var maxHeight = 174;    // Max height for the image
        if (window.mobileAndTabletcheck())
        {
            maxHeight /= 2;
            maxWidth /= 2;
        }
        var ratio = 0;  // Used for aspect ratio
        var width = $(this).width();    // Current image width
        var height = $(this).height();  // Current image height

        // Check if the current width is larger than the max
        if (width > maxWidth) {
            ratio = maxWidth / width;   // get ratio for scaling image
            $(this).css("width", maxWidth); // Set new width
            $(this).css("height", height * ratio);  // Scale height based on ratio
            height = height * ratio;    // Reset height to match scaled image
            width = width * ratio;    // Reset width to match scaled image
        }

        // Check if current height is larger than max
        if (height > maxHeight) {
            ratio = maxHeight / height; // get ratio for scaling image
            $(this).css("height", maxHeight);   // Set new height
            $(this).css("width", width * ratio);    // Scale width based on ratio
            width = width * ratio;    // Reset width to match scaled image
            height = height * ratio;    // Reset height to match scaled image
        }
        var containerwidth = $(this).closest("div").width();
        if (containerwidth > width)
        {
            var left = (containerwidth - width) / 2;
            $(this).css("margin-left", left + "px");
        }
    });
    
}
function getAllNewsFail(data) { }
function getSpecialOffers() {
    services.get("getSpecialOffers", null, getSpecialOffersSuccess, getSpecialOffersFail);
}
function getSpecialOffersSuccess(data) {
    var wholehtml = '<ul>';
    var recs = data[0].records;
    for (var i = 0; i <= recs.length - 1 ; i++) {
        var html = '<li class="specialofferLI">';
        html += '<div class="default" style="text-align:center;">';
        html += '<img class="specialofferIMG" src="../services/getImage.aspx?id=' + recs[i].imageID + '" />';
        html += '<a class="fancybox" href="#inline'+i+'" title="">';
        html += '<div class="pink-hover">';
        html += '<div class="pink-hover-inner">';
        html += '<div class="caption">' + recs[i].Title;
        html += '</div></div></div></a></div>';
        html += '<div id="inline'+i+'" class="inline-content">';
        html += '<img src="../services/getImage.aspx?id=' + recs[i].imageID + '" id="specialoffer-img-' + i + '" class="specialofferImgInside" />';
        html += '<h3>'+recs[i].Title+'</h3><em>' + recs[i].Date + '</em>';
        html += '<p>' + recs[i].Content + '</p></div></li>';
        wholehtml += html;
    }
    wholehtml += "</ul>";
    $(".image-grid-special").html(wholehtml);
    $(".specialofferIMG").load(function () {
        var maxWidth = 300; // Max width for the image
        var maxHeight = 300;    // Max height for the image
        //if (window.mobileAndTabletcheck()) {
        //    maxHeight /= 2;
        //    maxWidth /= 2;
        //}
        var ratio = 0;  // Used for aspect ratio
        var width = $(this).width();    // Current image width
        var height = $(this).height();  // Current image height

        // Check if the current width is larger than the max
        if (width > maxWidth) {
            ratio = maxWidth / width;   // get ratio for scaling image
            $(this).css("width", maxWidth); // Set new width
            $(this).css("height", height * ratio);  // Scale height based on ratio
            height = height * ratio;    // Reset height to match scaled image
            width = width * ratio;    // Reset width to match scaled image
        }

        // Check if current height is larger than max
        if (height > maxHeight) {
            ratio = maxHeight / height; // get ratio for scaling image
            $(this).css("height", maxHeight);   // Set new height
            $(this).css("width", width * ratio);    // Scale width based on ratio
            width = width * ratio;    // Reset width to match scaled image
            height = height * ratio;    // Reset height to match scaled image
        }
        var containerwidth = $(this).closest("li").width();
        if (containerwidth > width) {
            var left = (containerwidth - width) / 2;
            $(this).css("margin-left", left + "px");
        }
        var containerheight = $(this).closest("li").height();
        if (containerheight > height) {
            var left = (containerheight - height) / 2;
            $(this).css("margin-top", left + "px");
        }
    });
}
function getSpecialOffersFail(data) { }
function getGallery(id) {
    if (id == "undefined" || id == undefined || id == null)
        return;
    if (parseInt(id) == 0)
        return;
    var data = "{'RestID':'" + id + "'}";
    services.get("getGallery",data, getGallerySuccess, getGalleryFail);
}
function getGallerySuccess(data)
{
    var recs = data[0].records;
    var html = "No images";
    for (var i = 0; i <= recs.length - 1; i++)
    {
        if (i == 0) html = "";
        html += '<div class="item" style="height:250px !important;">'+"\n";
        html += '<a class="fancybox" href="images/gallery/'+recs[i].cImageBigURL+'" data-fancybox-group="gallery1">'+"\n";
        html += '<img class="galleryImg" src="images/gallery/'+recs[i].cImageURL+'" />'+"\n";
        html += '</a>'+"\n";
        html += '</div><!--/.item-->'+"\n";
    }
    $("#item-grid").html(html);
    $('#item-grid').show();
    $(".galleryImg").load(function () {
        var maxWidth = 340; // Max width for the image
        var maxHeight = 250;    // Max height for the image
        var ratio = 0;  // Used for aspect ratio
        var width = $(this).width();    // Current image width
        var height = $(this).height();  // Current image height

        // Check if the current width is larger than the max
        if (width > maxWidth) {
            ratio = maxWidth / width;   // get ratio for scaling image
            $(this).css("width", maxWidth); // Set new width
            $(this).css("height", height * ratio);  // Scale height based on ratio
            height = height * ratio;    // Reset height to match scaled image
            width = width * ratio;    // Reset width to match scaled image
        }

        // Check if current height is larger than max
        if (height > maxHeight) {
            ratio = maxHeight / height; // get ratio for scaling image
            $(this).css("height", maxHeight);   // Set new height
            $(this).css("width", width * ratio);    // Scale width based on ratio
            width = width * ratio;    // Reset width to match scaled image
            height = height * ratio;    // Reset height to match scaled image
        }
        var containerwidth = $(this).closest("div").width();
        if (containerwidth > width) {
            var left = (containerwidth - width) / 2;
            $(this).css("margin-left", left + "px");
        }
        var containerheight = $(this).closest("li").height();
        if (containerheight > height) {
            var left = (containerheight - height) / 2;
            $(this).css("margin-top", left + "px");
        }
    });
}
function getGalleryFail(data) { }
function populateRestaurantDropdownFail(data)
{
}
function getAddress(id) {
    if (id == "undefined" || id == undefined || id == null)
        return;
    if (parseInt(id) == 0)
        return;
    var data = "{'locid':'" + id + "'}";
    services.get("getAddress", data, getAddressSuccess, getAddressFail);
}
function getAddressSuccess(data) {
    debugger;
    var phoneatag = '<a href="tel:' + data[0].cContact + '">' + data[0].cContact + '</a>';
    $('.myaddress').html(data[0].cAddress + '<br><br>Phone: ' + phoneatag);
    $('#inline').html(data[0].cMap);
    var arr = data[0].OpeningTimesString.split(",");
    var timings = "";
    for (var i = 0; i <= arr.length - 1; i++)
    {
        timings += arr[i] + "<br/>"

    }
    $('.mydays').html(timings);
    //$('.mytime').html(data[0].cTimings);

    //This is for contact us page
    $('.address').html(data[0].cAddress + '<br><br>Phone: ' + phoneatag);
    $('#map-view').html(data[0].cMap);

    //This is for restaurants page;
    var html =  '<p>'+data[0].cContent+'</p>';
    html += '<p class="phone"><i class="fa fa-phone fa-lg" aria-hidden="true"></i> ' + phoneatag + '</p>';
    $('.restaurant-short').html(html);
    
}
function getAddressFail(data) {
    //alert(JSON.stringify(data));
}
function payDeposit(topay)
{
    if (topay) {
        $("#reservation-outer > div").hide();
        $("#reservation-outer").show();
        $("#reservation-inner-dopayment").show();
        custPaidDeposit = true;//this will be true if confirmed deposit
        customerDetailsJson["BookingDeposit"] = parseInt($(".myguest").val()) * depositPerPerson;
        fillFields();
    }
    else {
        if (parseInt($(".myguest").val()) > maxNonBookingPersonCount) {
            showmessage("It is mandatory to pay deposit if number of guest are more than 4",null);
            $("#reservation-outer > div").hide();
            $("#reservation-outer").show();
            $("#reservation-inner-dopayment").show();
            return;
        }
        customerDetailsJson["BookingDeposit"] = parseInt($(".myguest").val()) * depositPerPerson;
        proceedToBooking();
    }
}
function proceedToBooking() {
    services.get("BookingConfirm", JSON.stringify(customerDetailsJson), proceedToBookingSuccess, proceedToBookingFail);
}
function proceedToBookingSuccess(data) {
    $("#reservation-outer > div").hide();
    $("#reservation-outer").show();
    $("#reservation-inner-confirmpayment").show();
    $("#reservation-inner-confirmpayment > div").hide();
    if (custPaidDeposit) {
    }
    else
        $("#no-deposit-paid").show();
    $("#booked-id").html(data[0].Output);
    $("#booked-restNumber").html(data[0].ContactNumber);
    $("#booked-restName").html(data[0].RestaurantName);
    $("#nodepositsuccessmsg").html(data[0].Content);
    $(".pound").html("&pound;");
    clearBookTable();

}
function proceedToBookingFail(data) { }
function goto(e)
{
    if (e == 'reservation-inner-askpayment')
    {
        if (parseInt($(".myguest").val()) <= maxNonBookingPersonCount) {
            e = 'reservation-inner-details';
        }
    }
    $("#reservation-outer > div").hide();
    $("#reservation-outer").show();
    $("#"+e).show();
}
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function hideBookaTable() {
    $('.book-table-outer').css('bottom', -460);
}
function couldnotbook() {
    var location = $('#couldnotbook .pop-location').val();
    var name = $('#couldnotbook #couldnotbook-name').val();
    var mobilenumber = $('#couldnotbook #couldnotbook-mobile').val();
    var restname = $('#couldnotbook .pop-location option:selected').html();
    if (location == "0")
    {
        showmessage("Please select a restaurant.", $('#couldnotbook .pop-location'));
        return;
    }
    if (name == "") {
        showmessage("Please enter your name", $('#couldnotbook #couldnotbook-name'));
        return;
    }
    if (mobilenumber == "") {
        showmessage("Please enter your mobile number", $('#couldnotbook #couldnotbook-mobile'));
        return;
    }
    if (mobilenumber < 0) {
        showmessage("Please enter proper mobile number", $('#couldnotbook #couldnotbook-mobile'));
        return;
    }
    var data = "{'RestID':'"+location+"','name':'"+name+"','mobilenumber':'"+mobilenumber+"','restName':'"+restname+"'}";
    services.get("couldnotbook", data, couldnotbookSuccess, couldnotbookFail);
}
function couldnotbookSuccess(data) {
    showmessage("The booking team will contact you soon",null);
    $('#couldnotbook .pop-location').val('');
    $('#couldnotbook #couldnotbook-name').val('');
    $('#couldnotbook #couldnotbook-mobile').val('');
    $('.fancybox-opened').hide();
    $('.fancybox-overlay').hide();
};
function couldnotbookFail(data) { };
function showmessage(msg,e)
{
    OpenSmartBox({ title: "", content: msg, buttons: "[OK]" },
                    function (result) {
                        if (result === "OK") {
                            
                        }
                        if (e != null)
                            $(e).focus();
                        CloseSmartBox();
                    });
}
function clearBookTable()
{
    $(".myguest").val(0);
    $(".myguesthighchairs").val(0);
    $(".myguestwheelchairs").val(0);
    $(".myguestpramsseats").val(0);
    $('.location').val(0);
    $('#datepicker').val('');
    $("#txt-firstname").val('');
    $("#txt-surname").val('');
    $("#txt-addressline1").val('');
    $("#txt-addressline2").val('');
    $("#txt-city").val('');
    $("#txt-zipcode").val('');
    $("#txt-mobile").val('');
    $("#txt-email").val('');
    $("#txt-notes").val('');
}
window.mobileAndTabletcheck = function () {
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino|android|ipad|playbook|silk/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
}
