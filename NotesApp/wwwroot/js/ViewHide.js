var toggleButtons = document.querySelectorAll(".note-hide-button");
toggleButtons.forEach(function (button) {
    button.addEventListener("click", function () {
        
        var record = button.closest(".note");
        var textBlock = record.querySelector(".note-text");

        if (textBlock.style.display === "none" || textBlock.style.display === "") {
            textBlock.style.display = "block";
            button.textContent = "Hide note";
        } else {
            textBlock.style.display = "none";
            button.textContent = "View nite";
        }
    });
});

