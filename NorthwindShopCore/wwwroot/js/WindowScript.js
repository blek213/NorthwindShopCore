$(document).ready(function () {

    $('.popup .close_window, .overlay').click(function () {
        $('.popup, .overlay').css({ 'opacity': '0', 'visibility': 'hidden' });
    });

    $('input.open_window').click(function (e) {
        $('.popup, .overlay').css({ 'opacity': '1', 'visibility': 'visible' });

        e.preventDefault();

        var hiddenId = $('#hiddenProductId').val();

        hiddenId = encodeURIComponent(hiddenId);

        $('#ProductNameInWindow').load("http://localhost:56567/Product/AddToCart?IdProductFromView=" + hiddenId);
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



