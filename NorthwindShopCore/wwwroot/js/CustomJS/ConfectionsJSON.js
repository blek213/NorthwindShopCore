setTimeout(ShowConfections, 1);

function ShowConfections() {

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/ConfectionsJsonResult',
        success: function (data) {

            var results = $("#ConfectionsList");

            var confectionsObject = jQuery.parseJSON(data);

            var confectionsData = [];

            for (var i = 0; i < data.length; i++) {

                try {

                    var ProductNameVal = confectionsObject[i].ProductName;
                    var ProductId = confectionsObject[i].ProductId;

                    confectionsData.push({
                        "ProductId": ProductId,
                        "ProductName": ProductNameVal

                    });
                }
                catch (error) {

                    break;
                }

            }

            $("#grid").kendoGrid({
                columns: [{ title: "Product Name", field: "ProductName", template: '<li><a href' + '=' + "'" + "/api/Product/Confection/#:ProductId#" + "'" + '>' + '#: ProductName# ' + '</a></li>' }],
                dataSource: {
                    data: confectionsData,
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

}