console.log("Reset");

console.log(resetUUID);

eventListener()

async function eventListener() {
    console.log("EventListener")

    let resetForm = document.getElementById("reset_form");
    async function handleResetForm(event) {
        event.preventDefault();
        await reset();
    }
    resetForm.addEventListener('submit', handleResetForm);

    // Listener for the verify input fields
    // inputs vefiy-${id} [0-5]
    // inputs can only be 0-9. cut of anything when pasted in
    // when input has number typed in, focus next input
    // when input has number pasted in that is longer than 1, focus next input and paste in the rest
    // when pressing backspace, focus previous input
    // when last input filled in, submit form
    for (let i = 0; i < 6; i++) {
        let input = document.getElementById(`reset-${i}`);
        input.addEventListener("input", handleInput);
        input.addEventListener("paste", handlePaste);
        input.addEventListener("keydown", handleKeyDown);

        async function handleInput(event) {
            let value = event.target.value;
            if (value.length > 1) {
                event.target.value = value[0];
            }
            if (value.length == 1) {
                if (i < 5) {
                    document.getElementById(`reset-${i + 1}`).focus();
                }
            }
        }

        async function handlePaste(event) {
            console.log("Paste")
            //https://developer.mozilla.org/en-US/docs/Web/API/Element/paste_event
            let paste = (event.clipboardData || window.clipboardData).getData('text');
            console.log("Pasting ", paste);
            if (paste.length > 0) {
                event.target.value = paste[0];
                for (let j = 0; j < paste.length; j++) {
                    if (i + j < 5) {
                        document.getElementById(`reset-${i + j + 1}`).focus();
                        document.getElementById(`reset-${i + j + 1}`).value = paste[j + 1];
                    }
                }
            }
        }

        async function handleKeyDown(event) {
            if (event.key == "Backspace") {
                event.target.value = "";
                if (i > 0) {
                    document.getElementById(`reset-${i - 1}`).focus();
                }
            }
        }
    }
    // Listener for the verify button
    let resetButton = document.getElementById("reset_btn");
    resetButton.addEventListener("click", reset);
}

async function reset() {
    console.log("Reset");

    //Arrange UI
    let resetButton = document.getElementById("reset_btn");
    resetButton.textContent = "Loading...";
    resetButton.disabled = true;
    let resetErr = document.getElementById("reset_err");
    resetErr.classList.add("hidden");

    //Validate
    //New password
    let newPassword = document.getElementById("InputNewPassword").value;
    //New password confirm
    let newPasswordConfirm = document.getElementById("InputNewPasswordConfirm").value;

    if (newPassword != newPasswordConfirm) {
        resetErr.classList.remove("hidden");
        resetErr.textContent = "Passwords do not match";
        resetButton.textContent = "Reset";
        resetButton.disabled = false;
        return;
    }

    let code = "";
    for (let i = 0; i < 6; i++) {
        code += document.getElementById(`reset-${i}`).value;
    }

    await fetch("https://localhost:3001/api/Auth/ResetPassword", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            "uuid": resetUUID,
            "resetCode": code,
            "password": newPassword
        })
    }).then(async response => {
        const isJson = response.headers.get('content-type')?.includes('application/json');
        const data = isJson ? await response.json() : null;

        // check for error response
        if (!response.ok) {
            // get error message from body or default to response status
            const error = (data && data.message) || response.status;
            return Promise.reject(error);
        }
        console.log(data);

        //Set to finished
        window.location.href = "/Register";
    }).catch(error => {
        console.error('There was an error!', error);

        //Show error
        resetErr.textContent = error;
        resetErr.classList.remove("hidden");

        //Set to finished
        resetButton.textContent = "Register";
        resetButton.disabled = false;
    });
}