$(document).on('submit', 'form', function () {

    var name = $("#name").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var repeatpassword = $("#repeatpassword").val();

    email = encodeURIComponent(email);
    password = encodeURIComponent(password);
    repeatpassword = encodeURIComponent(repeatpassword);

    setTimeout(GetHttpResponse, 1);

    function GetHttpResponse() {
        $.getJSON({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/JSON',
            url: '/user/User/Register?name=' + name + "&email=" + email + "&password=" + password + "&repeatpassword=" + repeatpassword,
            async: false,
            success: function (data) {

                alert(data);

                if (data == 202) {

                    localStorage.setItem('UserName', name);

                    alert("Welcome to our shop!");

                    window.location.href = "../Values/Greeting.html";

                }
                else if (data == 400) {

                    alert("You typed incorrect data ");

                    window.location.href = "Register.html";
                }

            },
            error: function (x, y, z) {
                alert("Bad request")
                window.location.href = "Register.html";
            }

        });
    }
 

    //$.ajax({
    //    statusCode: {
    //        202: function () {
    //            window.location.href = "../Values/Greeting.html";

    //        },
    //        400: function () {
    //            alert("Bad Request. Try to again");
    //            window.location.href = "../User/Register.html";
    //        }
    //    }
    //});

});