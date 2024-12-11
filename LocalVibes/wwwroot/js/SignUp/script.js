const wrapper = document.querySelector('.wrapper');
const registerLink = document.querySelector('.register-link');
const loginLink = document.querySelector('.login-link');

registerLink.onclick = () => {
    wrapper.classList.add('active');
}

loginLink.onclick = () => {
    wrapper.classList.remove('active');
}

document.addEventListener("DOMContentLoaded", function () {
    const inputs = document.querySelectorAll(".input-box input");
    const fileInputs = document.querySelectorAll(".input-box-image input[type='file']");
    const selects = document.querySelectorAll(".input-box select");
    const textAreas = document.querySelectorAll(".input-box textarea");

    inputs.forEach(input => {
        updateInputState(input);
        input.addEventListener("change", () => updateInputState(input));
    });

    fileInputs.forEach(input => {
        updateFileInputState(input);
        input.addEventListener("change", () => updateFileInputState(input));
    });

    selects.forEach(select => {
        updateSelectState(select);
        select.addEventListener("change", () => updateSelectState(select));
    });

    textAreas.forEach(textArea => {
        updateInputState(textArea);
        textArea.addEventListener("input", () => updateInputState(textArea));
    });

    function updateInputState(input) {
        var value = input.value.trim();

        if (value !== "" && value !== 'Birthdate' && value !== 'FormationDate') {
            input.setAttribute("data-filled", "true");
        } else {
            input.removeAttribute("data-filled");
        }
    }

    function updateFileInputState(input) {
        if (input.files && input.files.length > 0) {
            input.setAttribute("data-filled", "true");
        } else {
            input.removeAttribute("data-filled");
        }
    }

    function updateSelectState(select) {
        if (select.value.trim() !== "") {
            select.setAttribute("data-filled", "true");
        } else {
            select.removeAttribute("data-filled");
        }
    }
});