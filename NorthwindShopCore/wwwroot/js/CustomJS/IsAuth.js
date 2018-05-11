$(document).ready(function () {
  
    setTimeout(ShowLink, 1);
});

function ShowLink() {

    var localValue = localStorage.getItem('UserName');

    if (localValue == null) {
        $('#userLayout').prepend('<span id="Register"><a href="/html/User/Register.html">Register</a></span>');
        $('#userLayout').prepend('<span id="SignIn"><a href="/html/User/Login.html">SignIn</a></span>');

    }
    else {
        $('#userLayout').prepend('<input id="logOff" type="button" value="logOff" onclick="return LogOffFunc();" />');

    }

}

function LogOffFunc() {

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/user/User/LogOff',
        async: false,
        success: function (data) {

            delete localStorage["UserName"];

            window.location.href = "../Values/Greeting.html";

        },
        error: function (x, y, z) {
            alert("Sorry, troubles in server.");
        }

    });
};