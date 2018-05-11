

 function SignInFunc(){

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
                    /*
                    // сохраняем в хранилище sessionStorage токен доступа
                    sessionStorage.setItem(tokenKey, data.access_token);
                    */
                    localStorage.setItem('UserName', name);

                    swal("Success", "You are in system", "success");

                    setTimeout(RedirectToMain, 3000);

                }
                else if (data == 400) {

                    swal("Error", "Typed incorrected data", "error");

                    setTimeout(RedirecttoLogin, 3000);
                }
            },
            error: function (x, y, z) {

                swal({
                    icon: "error",
                    text: "Bad request",
                });
                window.location.href = "Login.html";
            }
        });
    }

   
}

function RedirecttoLogin() {
    window.location.href = "Login.html";

}

function RedirectToMain() {
    window.location.href = "../Values/Greeting.html";

}
