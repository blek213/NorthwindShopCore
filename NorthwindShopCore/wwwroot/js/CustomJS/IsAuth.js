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
        $('#userLayout').prepend('<input type="submit" value="logOff" />');

    }
    
}