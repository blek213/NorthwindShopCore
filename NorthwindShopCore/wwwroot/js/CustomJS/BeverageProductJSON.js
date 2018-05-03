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

            var beverageObject = jQuery.parseJSON(data);

            ProductNameResult.append("<p>" + beverageObject[0].ProductName + "</p>");
            ProductQuantityResult.append("<p>" + beverageObject[0].QuantityPerUnit + "</p>");
            ProductUnitPrice.append("<p>" + beverageObject[0].UnitPrice + "</p>");

            if (beverageObject[0].Discontinued == true) {
                ProductDiscountBool.append("<p>" + "it's discounted" + "</p>")
            }
            else {
                ProductDiscountBool.append("<p>" + "it's not discounted" + "</p>")
            }

            var checkAA = $("#hiddenProductId").attr("value", String(beverageObject[0].ProductId));
            var checkBB = $("#IdProductSet").attr("value", String(beverageObject[0].ProductId));
           
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