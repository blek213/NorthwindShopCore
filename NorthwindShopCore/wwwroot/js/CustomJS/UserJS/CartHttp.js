$(document).on('submit', 'form', function () {
    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/user/User/Login',
        success: function (data) {

            var UserNameForm = $("#name").val();

            alert(data);

            if (data == 202) {

                localStorage.setItem('UserName', UserNameForm);

                window.location.href = "../Values/Greeting.html";

            }
            else if (data == 400) {
                window.location.href = "Login.html";
            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });

});