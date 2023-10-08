document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('inputSearch');
    const blocks = document.querySelectorAll('.note');

    searchInput.addEventListener('input', function () {
        const searchText = searchInput.value.toLowerCase();

        blocks.forEach(function (block) {
            const blockTitle = block.querySelector('.note-title').textContent.toLowerCase();
            if (blockTitle.includes(searchText)) {
                block.style.display = 'block';
            } else {
                block.style.display = 'none';
            }
        });
    });
});