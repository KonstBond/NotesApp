// Получаем все кнопки для скрытия/показа текста
var toggleButtons = document.querySelectorAll(".note-hide-button");

// Добавляем обработчик события клика для каждой кнопки
toggleButtons.forEach(function (button) {
    button.addEventListener("click", function () {
        
        // Находим элемент с текстом записи
        var record = button.closest(".note");
        var textBlock = record.querySelector(".note-text");

        // Переключаем видимость текста при нажатии на кнопку
        if (textBlock.style.display === "none" || textBlock.style.display === "") {
            textBlock.style.display = "block";
            button.textContent = "Hide note";
        } else {
            textBlock.style.display = "none";
            button.textContent = "View nite";
        }
    });
});