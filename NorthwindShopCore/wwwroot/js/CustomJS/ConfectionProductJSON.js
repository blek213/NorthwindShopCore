
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

            var hiddenProductIdSend = $("#hiddenProductId");

            var OurHiddenIdProductSet = $("#IdProductSet");

            var ProductNameResult = $("#ProductName");

            var ProductQuantityResult = $("#QuantityPerUnit");

            var ProductUnitPrice = $("#UnitPrice");

            var ProductDiscountBool = $("#DiscountBool");

            var confectionObject = jQuery.parseJSON(data);

            ProductNameResult.append("<p>" + confectionObject[0].ProductName + "</p>");
            ProductQuantityResult.append("<p>" + confectionObject[0].QuantityPerUnit + "</p>");
            ProductUnitPrice.append("<p>" + confectionObject[0].UnitPrice + "</p>");

            if (confectionObject[0].Discontinued == true) {
                ProductDiscountBool.append("<p>" + "it's discounted" + "</p>")
            }
            else {
                ProductDiscountBool.append("<p>" + "it's not discounted" + "</p>")
            }

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
