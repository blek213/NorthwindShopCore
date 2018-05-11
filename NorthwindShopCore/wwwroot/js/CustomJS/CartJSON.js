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
    var tokenKey = "accessToken";

    $.ajax({
         type: 'GET',
         dataType: 'json',
         contentType: 'application/JSON',
         url: '/user/User/IsAuth?NameFromLocalStorage=',
        async: false,
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
         success: function (data) {

             alert(data);

         },
         error: function () {
             alert("You are not in system. Please sign in");
             window.location.href = "../Values/Greeting.html";
         }

     });

    var localValue = localStorage.getItem('UserName');

    if (localValue == null) {
        window.location.href = "../User/Login.html";
    }

    else {
        window.location.href = "../Product/BuyProduct.html";
    }

}
