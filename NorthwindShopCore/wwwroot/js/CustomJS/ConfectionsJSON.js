setTimeout(ShowConfections, 1);

function ShowConfections() {

    $.getJSON({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/ConfectionsJsonResult',
        success: function (data) {

            var results = $("#ConfectionsList");


            //alert(data[0].ProductName);

            //var newJsonKK = JSON.stringify(data);

            //var yourval = jQuery.parseJSON(JSON.stringify(data));

            //alert(yourval[0]);

            var confectionsObject = jQuery.parseJSON(data);

            //alert(confectionsObject[0].ProductName);

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
                columns: [{ title: "Product Name", field: "ProductName", template: '<li><a href' + '=' + "'" + "Confection.html?ConfectionIdVal=#:ProductId#" + "'" + '>' + '#: ProductName# ' + '</a></li>' }],
                dataSource: {
                    data: confectionsData,
                    pageSize:10,
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