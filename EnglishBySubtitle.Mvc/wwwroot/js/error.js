// ������� ��� ��������������� ��������������� ����� �������� �����
function redirectTo(url, delay) {
    setTimeout(function () {
        window.location.href = url;
    }, delay);
}
// ������� ��� ��������� ������� �������
function startCountdown() {
    var counter = 5; // �������� ��� ��������, ���� ������ ������ �����
    var countdownElement = document.getElementById('countdown');

    var countdownInterval = setInterval(function () {
        counter--;
        countdownElement.innerText = counter;

        if (counter <= 0) {
            clearInterval(countdownInterval);
        }
    }, 1000);
}