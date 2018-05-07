setTimeout(ShowProducts, 1);

function ShowProducts() {
    $.getJSON({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/CartJsonResult',
        async: false,
        success: function (data) {

            if (data != null) {
                var resuts = $("#ProductList");

                var ProductsObject = jQuery.parseJSON(data);

                resuts.append("<p>" + ProductsObject.Obj.ProductName + "  Count:" + ProductsObject.CountProducts + "</p>");
            }

        }
    });
}

$(document).click('#BuyButton', function () {

    var localValue = localStorage.getItem('UserName');

    if (localValue == null) {
       
        window.location.href = "../User/Login.html";

    }

    else {
        window.location.href = "../Product/BuyProduct.html";
    }

});
