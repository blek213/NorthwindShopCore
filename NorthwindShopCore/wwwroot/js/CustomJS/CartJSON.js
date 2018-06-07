setTimeout(ShowProducts, 1);

function ShowProducts() {
    $.getJSON({
        type: 'GET',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/CartJsonResult',
        async: false,
        success: function (data) {

            if (data != null) {
                var resuts = $("#ProductList");

                var discountOrNot;

                if (data.obj.discontinued == true) {
                    discountOrNot = "It's discontinued";

                }
                else {
                    discountOrNot = "It's not discontinued";
                }
                 
                resuts.append("<p style='text-align:center;'>" + data.obj.productName +"</p>"+ "<p>"+ "<b> QuantityPerUnit: </b> " + data.obj.quantityPerUnit + "<b> UnitPrice: </b>" + data.obj.UnitPrice + "<b> Discount: </b>" + discountOrNot +"<b><i> | Count:" + data.countProducts +" |</b></i>" +  "</p>");
                resuts.append("<div style='text-align:right;' >" +"<input type='button' value='Delete products' onclick='return DeleteProducts();' /> "+ "</div>");
            }

        }
    });
}

function BuyProduct() {
    var name = localStorage.getItem("UserName");

    //var isCartEmpy = false; //IsCartEmpyFunc();

        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/JSON',
            url: '/user/User/IsAuth',
            async: false,
            beforeSend: function (xhr) {

                var token = localStorage.getItem("accessUser_token");

                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function (data) {


            },
            error: function () {
                alert("Error. Please sign in again");
                window.location.href = "../Values/Greeting.html";
            }

        });
    
}
/*
function IsCartEmpyFunc() {
    var resultBool;

    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/isNullCookie',
        async: false,
        success: function (data) {

            if (data == true) {

                resultBool = true;

            }
            if (data == false) {

                resultBool=false;
            }
        },
        error: function () {
            alert("Error occured. Try later.");
            window.location.href = "../Values/Greeting.html";
        }

    });

    return resultBool;
}
*/
function DeleteProducts() {

    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/DeleteProducts',
        async: false,
        success: function (data) {

            window.location.href = "Cart.html";

        },
        error: function () {
            alert("Error occured. Try later.");
            window.location.href = "../Values/Greeting.html";
        }

    });
}
