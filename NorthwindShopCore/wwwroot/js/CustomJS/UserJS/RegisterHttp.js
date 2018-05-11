
function RegisterFunc() {

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

                if (data == 202) {

                    localStorage.setItem('UserName', name);

                    swal("Success", "Welcome to our shop!", "success");

                    setTimeout(RedirecttoRegister,1500);

                }
                else if (data == 400) {

                    swal("Error", "Typed incorrected data or IIS 500 error", "error");
                    setTimeout(RedirectToMain, 1000);

                }

            },
            error: function (message) {
                swal("Error", "Typed incorrected data or IIS 500 error", "error");

                setTimeout(RedirecttoRegister, 1500);
            }

        });
    }
 
}

function RedirecttoRegister() {
    window.location.href = "Register.html";

}

function RedirectToMain() {
    window.location.href = "../Values/Greeting.html";

}