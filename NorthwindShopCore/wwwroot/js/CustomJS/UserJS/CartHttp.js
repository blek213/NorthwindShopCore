$(document).on('submit', 'form', function () {

    var IdProductSet = $("#IdProductSet").val();
    var InputText = $("#InputText").val();
    var button = $("name='button'").val();


    alert(button);

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/user/User/Register?IdProductSet=' + IdProductSet + "&InputText=" + InputText + "&button=" + button,
        //beforeSend: function (xhr) {
        //    xhr.setRequestHeader('Authorization', 'Bearer t-7614f875-8423-4f20-a674-d7cf3096290e');
        //},
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