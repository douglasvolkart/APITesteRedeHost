
$(function () {


    $.get("http://localhost:58193/api/values",function (response) {
        console.log(response);
        $('.atual-date').html(response.date);
    });


    $('.btn-primary').on('click', function(e){

        $('.alert-success').hide();

        var json = {
            date: $('#hour').val(),
            op: $('#type').val(),
            value: $('#minutes').val()
        }

        var data = JSON.stringify(json);

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": "http://localhost:58193/api/values",
            "method": "PUT",
            "headers": {
              "content-type": "application/json",
              "cache-control": "no-cache",
              "postman-token": "ff0efb3d-822d-5fb7-9bdb-6415ef61fc61"
            },
            "processData": false,
            "data": data
          }
          
          $.ajax(settings).done(function (response) {
            console.log(response);


            $('.update-date').html(response.date);

            $('.alert-success').fadeIn();


          });

    });

});