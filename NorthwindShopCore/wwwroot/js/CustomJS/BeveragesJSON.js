$.getJSON({
    type: 'POST',
    dataType: 'json',
    contentType: 'application/JSON',
    url: '/api/Product/BeveragesJsonResult',
    success: function (data) {

        var results = $("#BeveragesList");

        var beveragesData = [];

        for (var i = 0; i < data.length; i++) {

            try {

                var ProductNameVal = data[i].productName;
                var ProductId = data[i].productId;

                beveragesData.push({
                    "ProductId": ProductId,
                    "ProductName": ProductNameVal

                });
            }
            catch (error) {

                break;
            }
        }

        $("#grid").kendoGrid({
            columns: [{
                title: "Product Name", field: "ProductName", template: '<li><a href' + '=' + "'"
                    + "Beverage.html?BeverageIdVal=#:ProductId#" + "'" + '>'
                    + '#: ProductName# ' + '</a></li>'
            }],
            dataSource: {
                data: beveragesData,
                pageSize: 10,
                serverPaging: false
            },
            height: 500,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            }
        });

    },
    error: function (x, y, z) {
        alert(x + '\n' + y + '\n' + z);
    }
});

   