
var app = {
    apiRoute: "http://localhost:58193/api/",
    hide: function (el) {
        el.style.display = 'none';
    },
    show: function (el) {
        el.style.display = 'block';
    },
    fadeIn: function (el) {
        el.style.opacity = 0;

        var last = +new Date();
        var tick = function () {
            el.style.opacity = +el.style.opacity + (new Date() - last) / 400;
            last = +new Date();

            if (+el.style.opacity < 1) {
                (window.requestAnimationFrame && requestAnimationFrame(tick)) || setTimeout(tick, 16);
            }
        };

        tick();
    },
    getDate: function () {

        var request = new XMLHttpRequest();
        request.open('GET', app.apiRoute + 'values', true);

        request.onload = function () {
            if (request.status >= 200 && request.status < 400) {
                // Success!
                var data = JSON.parse(request.responseText);
                console.log(data);
                var element = document.getElementById('atual-date');
                element.innerHTML = data.date;
            } else {
                // We reached our target server, but it returned an error
                console.log('error');
                alert('API respondeu corretamente')
            }
            console.log(request.status)
        };

        request.onerror = function () {
            // There was a connection error of some sort
            alert('API Não está ativa.')
        };

        request.send();
    },

    postDate: function () {
        var request = new XMLHttpRequest();
        request.open('PUT', app.apiRoute + 'values', true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');

        var json = {
            date: document.getElementById("hour").value,
            op: document.getElementById("type").value,
            value: document.getElementById("minutes").value
        }

        request.onreadystatechange = function () {//Call a function when the state changes.

            if (request.readyState == 4 && request.status == 200) {


                var json = JSON.parse(request.responseText);
                console.log(json);

                document.getElementById('update-date').innerHTML = json.date;

                app.show(document.getElementById('success'));

            }else{
                alert('API Não está ativa ou não respondeu corretamente')
            }
        }
        var data = JSON.stringify(json)

        request.onerror = function () {
            // There was a connection error of some sort
            alert('API Não está ativa.')
        };
        
        request.send(data);


    },
    init: function () {
        app.getDate();
    }
}

window.addEventListener('load', function () {
    app.init();
}, true);
document.getElementById('button').onclick = function () {
    app.postDate();
}

