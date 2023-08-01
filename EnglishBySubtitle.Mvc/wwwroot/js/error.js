// Function for automatic redirection after a specified time.
function redirectTo(url, delay) {
    setTimeout(function () {
        window.location.href = url;
    }, delay);
}
// Countdown timer function.
function startCountdown() {
    var counter = 5;
    var countdownElement = document.getElementById('countdown');

    var countdownInterval = setInterval(function () {
        counter--;
        countdownElement.innerText = counter;

        if (counter <= 0) {
            clearInterval(countdownInterval);
        }
    }, 1000);
}