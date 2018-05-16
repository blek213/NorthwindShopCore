var currentUrl = window.location.href;

var BeverageIdVal = ParseIdFromUrl(currentUrl);

setTimeout(ShowBeverage, 1);

function ShowBeverage() {
    $.getJSON({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/BeverageJsonResult/' + BeverageIdVal,
        success: function (data) {

            var hiddenProductIdSend = $("#hiddenProductId");

            var OurHiddenIdProductSet = $("#IdProductSet");

            var ProductNameResult = $("#ProductName");

            var ProductQuantityResult = $("#QuantityPerUnit");

            var ProductUnitPrice = $("#UnitPrice");

            var ProductDiscountBool = $("#DiscountBool");

            ProductNameResult.append("<p>" + data[0].productName + "</p>");
            ProductQuantityResult.append("<p>" + data[0].quantityPerUnit + "</p>");
            ProductUnitPrice.append("<p>" + data[0].unitPrice + "</p>");

            if (data[0].discontinued == true) {
                ProductDiscountBool.append("<p>" + "it's discounted" + "</p>")
            }
            else {
                ProductDiscountBool.append("<p>" + "it's not discounted" + "</p>")
            }

            var checkAA = $("#hiddenProductId").attr("value", String(data[0].productId));
            var checkBB = $("#IdProductSet").attr("value", String(data[0].productId));
           
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