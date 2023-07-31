function getSubtitleById(element) {
    var idSubtitle = element.getAttribute("data-idsubtitle");
    var titleofMovie = element.querySelector(".td_left").innerText;

    // Sending an asynchronous request to the server using AJAX
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/DataProcessing/ReadSubtitle?idSubtitle=' + encodeURIComponent(idSubtitle)) + '&titleofMovie=' + encodeURIComponent(titleofMovie);
    console.log('Request URL: ' + '/DataProcessing/ReadSubtitle?idSubtitle=' + encodeURIComponent(idSubtitle)) + '&titleofMovie=' + encodeURIComponent(titleofMovie);
    xhr.onreadystatechange = function requestStatusReadSubtitle() {
        console.log('Request status: ' + xhr.readyState);
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                // Handling a successful response from the server
                var response = xhr.responseText;
                console.log(response);
                // Perform a redirect to a new page
                window.location.href = '/DataProcessing/ReadSubtitle?idSubtitle=' + encodeURIComponent(idSubtitle) + '&titleofMovie=' + encodeURIComponent(titleofMovie);
            } else {
                // Handling an error
                console.error('Request error: ' + xhr.status);
            }
        }
    };
    xhr.send();
}


