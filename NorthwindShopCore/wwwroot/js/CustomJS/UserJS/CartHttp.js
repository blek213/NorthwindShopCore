$(document).on('submit', 'form', function () {

    var IdProductSet = $("#IdProductSet").val();
    var InputText = $("#InputText").val();
    var button = $("name='button'").val();

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/user/User/Register?IdProductSet=' + IdProductSet + "&InputText=" + InputText + "&button=" + button,
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