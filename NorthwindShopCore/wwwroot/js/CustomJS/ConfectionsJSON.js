setTimeout(ShowConfections, 1);

function ShowConfections() {

    //$.getJSON({
    //    type: 'POST',
    //    contentType: 'application/JSON',
    //    dataType: 'json',
    //    url: '/api/Product/ConfectionsJsonResult',
    //    async:false,
    //    success: function (data) {

    //        var results = $("#ConfectionsList");

    //        alert(data); //[object Object],[object Object],[object Object],[object Object],[object Object],...
    //        alert(data[0].ProductName); //undefined

    //        //var confectionsObject = jQuery.parseJSON(data);

    //        var confectionsData = [];

    //        for (var i = 0; i < data.length; i++) {

    //            try {

    //                var ProductNameVal = data[i].ProductName;
    //                var ProductId = data[i].ProductId;

    //                confectionsData.push({
    //                    "ProductId": ProductId,
    //                    "ProductName": ProductNameVal

    //                });
    //            }
    //            catch (error) {

    //                break;
    //            }
    //        }

    //        $("#grid").kendoGrid({
    //            columns: [{ title: "Product Name", field: "ProductName", template: '<li><a href' + '=' + "'" + "Confection.html?ConfectionIdVal=#:ProductId#" + "'" + '>' + '#: ProductName# ' + '</a></li>' }],
    //            dataSource: {
    //                data: confectionsData,
    //                pageSize:10,
    //                serverPaging: false
    //            },
    //            height: 500,
    //            pageable: {
    //                refresh: true,
    //                pageSizes: true,
    //                buttonCount: 5
    //            }
    //        });

    //    },
    //    error: function (x, y, z) {
    //        alert(x + '\n' + y + '\n' + z);
    //    }
    //});

    $.post("/api/Product/ConfectionsJsonResult").success(function (data) {


    })
}