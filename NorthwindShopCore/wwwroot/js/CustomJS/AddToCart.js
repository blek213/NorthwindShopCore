
var currentUrl = window.location.href;

var ProductIdVal = ParseIdFromUrl(currentUrl);

setTimeout(ShowProduct, 1);

function ShowProduct() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/AddToCart?IdProductFromView=' + ProductIdVal,
        success: function (data) {

            var ProductObject = jQuery.parseJSON(data);

            $("#ResultFromAddCart").append(ProductObject.ProductName);

        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });
}

function ParseIdFromUrl(CurrentUrl) {

    var GetIndexBeforeId = CurrentUrl.indexOf("al=");

    for (var i = 0; i < 3; i++) {
        GetIndexBeforeId++;
    }

    var ProductId = CurrentUrl.slice(GetIndexBeforeId, 200);

    return ProductId;

}
