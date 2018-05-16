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

                console.log(data);

                var discountOrNot;

                if (data.obj.discontinued == true) {
                    discountOrNot = "It's discontinued";

                }
                else {
                    discountOrNot = "It's not discontinued";
                }
                 
                resuts.append("<p style='text-align:center;'>" + data.obj.productName +"</p>"+ "<p>"+ "<b> QuantityPerUnit: </b> " + data.obj.quantityPerUnit + "<b> UnitPrice: </b>" + data.obj.UnitPrice + "<b> Discount: </b>" + discountOrNot +"<b><i> | Count:" + data.countProducts +" |</b></i>" +  "</p>");
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
