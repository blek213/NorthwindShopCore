
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

            $("#ResultFromAddCart").append(data.productName);

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
