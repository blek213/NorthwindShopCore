var currentUrl = window.location.href;

var ConfectionIdVal = ParseIdFromUrl(currentUrl);

setTimeout(ShowConfection, 1);

function ShowConfection() {
    $.getJSON({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/ConfectionJsonResult/' + ConfectionIdVal,
        success: function (data) {

            var ProductNameResult = $("#ProductName");

            var ProductQuantityResult = $("#QuantityPerUnit");

            var ProductUnitPrice = $("#UnitPrice");

            var confectionObject = jQuery.parseJSON(data);

            ProductNameResult.append("<p>" + confectionObject[0].ProductName + "</p>");
            ProductQuantityResult.append("<p>" + confectionObject[0].QuantityPerUnit + "</p>");
            ProductUnitPrice.append("<p>" + confectionObject[0].UnitPrice + "</p>");
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }

    });
}

function ParseIdFromUrl(CurrentUrl) {

    var GetIndexBeforeId = CurrentUrl.indexOf("on/");

    for (var i = 0; i < 3; i++) {
        GetIndexBeforeId++;
    }

    var ProductId = CurrentUrl.slice(GetIndexBeforeId, 200);

    return ProductId;

}