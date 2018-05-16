﻿

 function SignInFunc(){

     var validateVal = ValidateForm();


     if (validateVal == true) {
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

                     var ResultToKen = jQuery.parseJSON(data);

                     alert(ResultToKen.JsonResponseRes.access_token);

                     if (ResultToKen.JsonHttpStatusCode == 202) {

                         //sessionStorage.setItem(tokenKey, data.access_token);

                         localStorage.setItem('accessUser_token', ResultToKen.JsonResponseRes.access_token);
                         localStorage.setItem('UserName', ResultToKen.JsonResponseRes.username);

                         swal("Success", "You are in system", "success");

                         setTimeout(RedirectToMain, 1000);

                     }
                     else if (ResultToKen.JsonHttpStatusCode == 400) {

                         swal("Error", "Typed incorrected data", "error");

                         setTimeout(RedirecttoLogin, 1500);
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
     else {
         swal("Error", "Typed incorrected data", "error");

     }

}

function ValidateForm() {

    var nameVal = $("#name").val();
    var passwordVal = $("#password").val();

    if (nameVal == "" || passwordVal == "") {

        return false;
    }

    if (nameVal.length <= 3)
    {
      
        return false;
    }

    return true;
}

function RedirecttoLogin() {
    window.location.href = "Login.html";

}

function RedirectToMain() {
    window.location.href = "../Values/Greeting.html";

}
