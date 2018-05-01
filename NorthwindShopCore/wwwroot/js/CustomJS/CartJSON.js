﻿setTimeout(ShowProducts, 1);

function ShowProducts() {
    $.getJSON({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/CartJsonResult',
        success: function (data) {
            var resuts = $("#ProductList");

            var ProductsObject = jQuery.parseJSON(data);

            resuts.append("<p>" + ProductsObject.Obj.ProductName + "  Count:" + ProductsObject.CountProducts + "</p>");

        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });

}