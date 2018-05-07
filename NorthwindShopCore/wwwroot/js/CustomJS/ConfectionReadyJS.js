$(document).ready(function () {

    $('.popup .close_window, .overlay').click(function () {
        $('.popup, .overlay').css({ 'opacity': '0', 'visibility': 'hidden' });
    });

    $('input.open_window').click(function (e) {
        $('.popup, .overlay').css({ 'opacity': '1', 'visibility': 'visible' });

        e.preventDefault();

        var hiddenId = $("#hiddenProductId").val(); //Get from hidden input

        hiddenId = encodeURIComponent(hiddenId);

        $('#ProductNameInWindow').load("http://localhost:50915/html/Product/AddToCart.html?IdProductFromView=" + hiddenId);
       
    });

    $('#plusButton').click(function (e) {

        var getVal = $('#InputText').val();

        getVal++;

        $('#InputText').val(String(getVal));

    });

    $('#minusButton').click(function (e) {

        var getVal = $('#InputText').val();

        if (getVal != 1) {

            getVal--;

            $('#InputText').val(String(getVal));
        }

    });

   

});



$(document).on('submit', 'form', function () {

    var hiddenProductIdForModel = $("#hiddenProductId").val(); //Get from hidden input

    hiddenProductIdForModel = encodeURIComponent(hiddenProductIdForModel);

    var InputText = $("#InputText").val();

    var button = $("input[name='button']").val();

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/Cart?IdProductSet=' + hiddenProductIdForModel + "&InputText=" + InputText +"&button="+ button,
        success: function (data) {

            alert(data);

            if (data == "Buy") {

                window.location.href = "../Values/Greeting.html";

            }

            if (data == 202) {

                localStorage.setItem('UserName', UserNameForm);

                window.location.href = "../Values/Greeting.html";

            }
           
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });

});

