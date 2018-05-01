$.getJSON({
    type: 'POST',
    dataType: 'json',
    contentType: 'application/JSON',
    url: '/api/Product/BeveragesJsonResult',
    success: function (data) {

        var results = $("#BeveragesList");

        var beveragesObject = jQuery.parseJSON(data);

        var beveragesData = [];

        for (var i = 0; i < data.length; i++) {

            try {

                var ProductNameVal = beveragesObject[i].ProductName;
                var ProductId = beveragesObject[i].ProductId;

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
            columns: [{ title: "Product Name", field: "ProductName", template: '<li><a href' + '=' + "'" + "/api/Product/Beverage/#:ProductId#" + "'" + '>' + '#: ProductName# ' + '</a></li>' }],
            dataSource: {
                data: beveragesData,
                pagesize: 6,
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

   