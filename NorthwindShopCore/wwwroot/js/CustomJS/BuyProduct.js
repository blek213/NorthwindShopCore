setTimeout(DeleteCookies, 5000);

function DeleteCookies() {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/JSON',
        url: '/api/Product/BuyProduct',
        success: function (data) {

            if (data == 202) {
                window.location.href = "../Values/Greeting.html";
            }

            if (data == 400) {
                window.location.href = "../Product/Product/Cart.html";
            }
        },
        error: function (x, y, z) {
            alert("Sorry your request has trouble. Try in another time. ");
        }

    });
}