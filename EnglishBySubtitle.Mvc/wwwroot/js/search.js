document.addEventListener('DOMContentLoaded', function () {
    var inputTitle = document.getElementById('InputTitle');
    inputTitle.addEventListener('keypress', function (event) {
        if (event.key === 'Enter') {
            event.preventDefault();
            var inputValue = inputTitle.value;
            var xhr = new XMLHttpRequest();
            xhr.open('GET', 'DataProcessing/GetSubtitle?InputTitle=' + encodeURIComponent(inputValue));
            xhr.onreadystatechange = function requestStatusGetSubtitle () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        // Handling a successful response from the server.
                        var response = xhr.responseText;
                        console.log(response);
                        // Perform a redirect to a new page.
                        window.location.href = 'DataProcessing/GetSubtitle?InputTitle=' + encodeURIComponent(inputValue);
                    } else {
                        // Error handling.
                        console.error('Ошибка запроса: ' + xhr.status);
                    }
                }
            };
            xhr.send();
        }
    });
});