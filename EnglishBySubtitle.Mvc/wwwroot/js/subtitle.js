function toggleExpandSubtitleField() {
    var subtitlesField = document.getElementById("subtitles_field");
    var subtitlesField2 = document.getElementById("subtitles_field2");
    var wordsField2 = document.getElementById("words_field2");
    var wordsField = document.getElementById("words_field");

    if (subtitlesField2.style.display === "none") {
        // Проверяем открыт ли другой блок
        if (wordsField2.style.display === "block") {
            wordsField2.style.display = "none";
            wordsField.textContent = "Show Unique Words";
            // Удаляем доп класс для стиля линии противоположного diva
            wordsField.classList.remove("move_border");
            // Добавляем доп класс для стиля линии
            subtitlesField.classList.add("move_border");
        }
        // Перемещаем блок с субтитрами вниз       
        document.getElementById("container").insertBefore(wordsField, subtitlesField);
        document.getElementById("container").appendChild(subtitlesField2);
        // Открываем блок с субтитрами
        subtitlesField2.style.display = "block";
        subtitlesField.textContent = "Hide Subtitle Text"; 
        // Добавляем доп класс для стиля линии
        subtitlesField.classList.add("move_border"); 
    }
    else {
        subtitlesField2.style.display = "none";
        subtitlesField.textContent = "Show Subtitles Text";       
        // Перемещаем блок с субтитрами вверх
        document.getElementById("container").insertBefore(subtitlesField, wordsField);
        document.getElementById("container").appendChild(subtitlesField2)

        // Удаляем доп класс для стиля линии противоположного diva
        subtitlesField.classList.remove("move_border");
    }
}
function toggleExpandWordsField() {
    var subtitlesField = document.getElementById("subtitles_field");
    var subtitlesField2 = document.getElementById("subtitles_field2");
    var wordsField = document.getElementById("words_field");
    var wordsField2 = document.getElementById("words_field2");    

    if (wordsField2.style.display === "none") {
        if (subtitlesField2.style.display === "block") {
            subtitlesField2.style.display = "none";
            subtitlesField.textContent = "Show Subtitles Text"; 
            // Удаляем доп класс для стиля линии противоположного diva
            subtitlesField.classList.remove("move_border");
        }
        // Добавляем доп класс для стиля линии
        wordsField.classList.add("move_border");
        // Перемещаем блок с уникальными словами вниз        
        document.getElementById("container").insertBefore(subtitlesField, wordsField);
        // Перемещаем блок вывода уникальных слов вниз
        document.getElementById("container").appendChild(wordsField2);
        wordsField2.style.display = "block";
        wordsField.textContent = "Hide Unique Words";               
    }
    else {
        wordsField2.style.display = "none";
        wordsField.textContent = "Show Unique Words";
        document.getElementById("container").appendChild(wordsField2);
        wordsField.classList.remove("move_border");
    }
}