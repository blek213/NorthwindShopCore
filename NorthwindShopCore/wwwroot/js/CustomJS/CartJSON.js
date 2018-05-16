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

                var ProductsObject = jQuery.parseJSON(data);

                var discountOrNot;

                if (ProductsObject.Obj.Discontinued == true) {
                    discountOrNot = "It's discontinued";

                }
                else {
                    discountOrNot = "It's not discontinued";
                }
                 
                resuts.append("<p style='text-align:center;'>" + ProductsObject.Obj.ProductName +"</p>"+ "<p>"+ "<b> QuantityPerUnit: </b> " + ProductsObject.Obj.QuantityPerUnit + "<b> UnitPrice: </b>" + ProductsObject.Obj.UnitPrice + "<b> Discount: </b>" + discountOrNot +"<b><i> | Count:" + ProductsObject.CountProducts +" |</b></i>" +  "</p>");
            }

        }
    });
}

function BuyProduct() {
    var name = localStorage.getItem("UserName");

    $.ajax({
         type: 'GET',
         dataType: 'json',
         contentType: 'application/JSON',
        url: '/user/User/IsAuth',
        async: false,
        beforeSend: function (xhr) {

            var token = localStorage.getItem("accessUser_token");

            xhr.setRequestHeader("Authorization", "Bearer "+ token);
        },
         success: function (data) {

             alert(data);

         },
         error: function () {
             alert("Error. Please sign in again");
             window.location.href = "../Values/Greeting.html";
         }

     });

}
