$(document).on('submit', 'form', function () {

    var name = $("#name").val();
    var password = $("#password").val();

    password = encodeURIComponent(password);

    setTimeout(GetHttpResponse, 1);

    function GetHttpResponse() {
        $.getJSON({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/JSON',
            url: '/user/User/Login?name=' + name + "&password=" + password,
            async: false,
            success: function (data) {

                if (data == 202) {

                    localStorage.setItem('UserName', name);

                    alert("You are in system");

                    window.location.href = "../Values/Greeting.html";

                }
                else if (data == 400) {

                    alert("You typed incorrect data ");

                    window.location.href = "Login.html";
                }
       
            },
            error: function (x, y, z) {
                alert("Bad request")
                window.location.href = "Login.html";
            }
        });
    }

   
});