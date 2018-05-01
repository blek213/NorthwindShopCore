$(document).ready(function () {
    ShowIndex();
});

function ShowIndex() {
    $("#fromIndex").load("http://localhost:50915/html/Index.html");
    $("#fromIndexFooter").load("http://localhost:50915/html/IndexFooter.html");
}