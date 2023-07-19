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
                        // ��������� ��������� ������ �� �������
                        var response = xhr.responseText;
                        console.log(response);
                        // ��������� ��������������� �� ����� ��������
                        window.location.href = 'DataProcessing/GetSubtitle?InputTitle=' + encodeURIComponent(inputValue);
                    } else {
                        // ��������� ������
                        console.error('������ �������: ' + xhr.status);
                    }
                }
            };
            xhr.send();
        }
    });
});