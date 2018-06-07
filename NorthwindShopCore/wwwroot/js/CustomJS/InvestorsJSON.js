$.getJSON({
    type: 'POST',
    dataType: 'json',
    contentType: 'application/JSON',
    url: '/api/Company/ShowContributors',
    success: function (data) {

        var results = $("#resultList");

        for (var i = 0; i < data.length; i++) {

            results.append(data[i].Info);

        }

    },
    error: function (x, y, z) {
        alert(x + '\n' + y + '\n' + z);
    }
});

