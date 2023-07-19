document.addEventListener('DOMContentLoaded', function () {
    var inputTitle = document.getElementById('InputTitle');
    inputTitle.addEventListener('keypress', function (event) {
        if (event.key === 'Enter') {
            event.preventDefault();
            var inputValue = inputTitle.value;
            var xhr = new XMLHttpRequest();
            xhr.open('GET', 'DataProcessing/GetSubtitle?InputTitle=' + encodeURIComponent(inputValue));
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        // Обработка успешного ответа от сервера
                        var response = xhr.responseText;
                        console.log(response);
                        // Выполнить перенаправление на новую страницу
                        window.location.href = 'DataProcessing/GetSubtitle?InputTitle=' + encodeURIComponent(inputValue);
                    } else {
                        // Обработка ошибки
                        console.error('Ошибка запроса: ' + xhr.status);
                    }
                }
            };
            xhr.send();
        }
    });
});