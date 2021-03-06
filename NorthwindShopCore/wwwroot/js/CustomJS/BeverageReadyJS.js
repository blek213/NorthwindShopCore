﻿$(document).ready(function () {

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

function ContinueButtonFunc() {

    var hiddenProductIdForModel = $("#hiddenProductId").val(); //Get from hidden input

    hiddenProductIdForModel = encodeURIComponent(hiddenProductIdForModel);

    var InputText = $("#InputText").val();

    var button = "Continue";


    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/Cart?IdProductSet=' + hiddenProductIdForModel + "&InputText=" + InputText + "&button=" + button,
        async: false,
        success: function (data) {

            if (data == "Beverage") {

                window.location.href = "../Product/Beverage.html?BeverageIdVal=" + hiddenProductIdForModel;
            }

            if (data == "Buy") {

                var localValue = localStorage.getItem('UserName');

                if (localValue == null) {
                    window.location.href = "../User/Login.html";
                }
                else {
                    window.location.href = "../Product/BuyProduct.html";
                }

            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });
};

function BuyButtonFunc() {
    var hiddenProductIdForModel = $("#hiddenProductId").val(); //Get from hidden input

    hiddenProductIdForModel = encodeURIComponent(hiddenProductIdForModel);

    var InputText = $("#InputText").val();

    var button = "Buy";

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/Cart?IdProductSet=' + hiddenProductIdForModel + "&InputText=" + InputText + "&button=" + button,
        async: false,
        success: function (data) {

            if (data == "Confection") {

                window.location.href = "../Product/Beverage.html?BeverageIdVal=" + hiddenProductIdForModel;
            }

            if (data == "Buy") {

                var localValue = localStorage.getItem('UserName');

                if (localValue == null) {
                    window.location.href = "../User/Login.html";
                }

                else {
                    window.location.href = "../Product/BuyProduct.html";
                }

            }
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });
}