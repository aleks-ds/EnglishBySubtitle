// Функция для автоматического перенаправления через заданное время
function redirectTo(url, delay) {
    setTimeout(function () {
        window.location.href = url;
    }, delay);
}
// Функция для обратного отсчета времени
function startCountdown() {
    var counter = 5; // Измените это значение, если хотите другое время
    var countdownElement = document.getElementById('countdown');

    var countdownInterval = setInterval(function () {
        counter--;
        countdownElement.innerText = counter;

        if (counter <= 0) {
            clearInterval(countdownInterval);
        }
    }, 1000);
}