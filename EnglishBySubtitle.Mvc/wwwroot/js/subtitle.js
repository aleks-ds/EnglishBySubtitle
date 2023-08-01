function toggleExpandSubtitleField() {
    var subtitlesField = document.getElementById("subtitles_field");
    var subtitlesField2 = document.getElementById("subtitles_field2");
    var wordsField2 = document.getElementById("words_field2");
    var wordsField = document.getElementById("words_field");

    if (subtitlesField2.style.display === "none") {
        if (wordsField2.style.display === "block") {
            wordsField2.style.display = "none";
            wordsField.textContent = "Show Unique Words";
            // Removing the additional class for the style of the line of the opposite div
            wordsField.classList.remove("move_border");
            // Adding an additional class for the style of the line.
            subtitlesField.classList.add("move_border");
        }
        // Moving the subtitle block down.
        document.getElementById("container").insertBefore(wordsField, subtitlesField);
        document.getElementById("container").appendChild(subtitlesField2);
        // Opening the subtitle block.
        subtitlesField2.style.display = "block";
        subtitlesField.textContent = "Hide Subtitle Text"; 
        // Adding an additional class for the style of the line.
        subtitlesField.classList.add("move_border"); 
    }
    else {
        subtitlesField2.style.display = "none";
        subtitlesField.textContent = "Show Subtitles Text";       
        // Moving the subtitle block up.
        document.getElementById("container").insertBefore(subtitlesField, wordsField);
        document.getElementById("container").appendChild(subtitlesField2)

        // Removing the additional class for the style of the line of the opposite div
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
            // Removing the additional class for the style of the line of the opposite div
            subtitlesField.classList.remove("move_border");
        }
        // Adding an additional class for the style of the line.
        wordsField.classList.add("move_border");
        // Moving the block with unique words down.
        document.getElementById("container").insertBefore(subtitlesField, wordsField);
        // Moving the block displaying unique words down.
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