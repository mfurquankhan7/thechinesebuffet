var services = {
    callback: null,
    get: function (method, info, onSuccess, onError) {
        var loadingmethods = ["bookTable", "BookingConfirm", "contactUs", "registernewuser", "applyjob", "couldnotbook", "paydeposit"];
        if(loadingmethods.indexOf(method) > -1)
            $("#loading-screen").show();
        $.ajax({
            url: "/services/data.ashx?method=" + method,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            data: info,
            responseType: "json",
            success: function (result) {
                $("#loading-screen").hide();
                if (typeof (onSuccess) == "function") {
                    result = result.replace(/\n/g, ' ').replace(/\r/g, ' ');
                    data = $.parseJSON('[' + result + ']');
                    onSuccess(data);
                } else
                    services.OnComplete(result);
            },
            error: function (result) {
                $("#loading-screen").hide();
                if (typeof (onError) == "function") {
                    onError(result)
                } else
                    services.OnFail(result);
            }
        });
    },
    save: function (method, info) {
        $.ajax({
            url: "/services/data.ashx?method=" + method,
            data: info,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: services.OnComplete,
            error: services.OnFail
        });
        return false;
    },
    OnComplete: function (result) {
        console.log($.parseJSON('{' + result + '}'));
        //alert('Success');
    },
    OnFail: function (result) {
        console.log($.parseJSON('{' + result + '}'));
        //alert('Request Failed');
    }
}