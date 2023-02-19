console.log("Verify")

eventListener()

async function eventListener() {
    console.log("EventListener")

    // Listener for the verify input fields
    // inputs vefiy-${id} [0-5]
    // inputs can only be 0-9. cut of anything when pasted in
    // when input has number typed in, focus next input
    // when input has number pasted in that is longer than 1, focus next input and paste in the rest
    // when pressing backspace, focus previous input
    // when last input filled in, submit form
    for (let i = 0; i < 6; i++) {
        let input = document.getElementById(`verify-${i}`);
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
                    document.getElementById(`verify-${i + 1}`).focus();
                } else {
                    document.getElementById("verify_btn").click();
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
                        document.getElementById(`verify-${i + j + 1}`).focus();
                        document.getElementById(`verify-${i + j + 1}`).value = paste[j + 1];
                        if (i + j == 4) {
                            document.getElementById("verify_btn").click();
                        }
                    }
                }
            }
        }

        async function handleKeyDown(event) {
            if (event.key == "Backspace") {
                event.target.value = "";
                if (i > 0) {
                    document.getElementById(`verify-${i - 1}`).focus();
                }
            }
        }
    }
    //Focus first input
    document.getElementById(`verify-0`).focus();

    // Listener for the verify button
    let verifyButton = document.getElementById("verify_btn");
    verifyButton.addEventListener("click", verify);
}

async function verify() {
    console.log("Verify")

    //Arrange UI
    let verifyButton = document.getElementById("verify_btn");
    verifyButton.textContent = "Loading...";
    verifyButton.disabled = true;
    let verifyErr = document.getElementById("verify_err");
    verifyErr.classList.add("hidden");

    for (let i = 0; i < 6; i++) {
        let input = document.getElementById(`verify-${i}`);
        input.disabled = true;
    }

    //Get Values
    let code = "";
    for (let i = 0; i < 6; i++) {
        code += document.getElementById(`verify-${i}`).value;
    }

    //Create User
    await fetch("https://localhost:3001/api/Auth/Verify", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            uuid: verifyUUID,
            verificationCode: code,
        }),
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
        window.location.href = "/Account";
    }).catch(error => {
        console.error('There was an error!', error);

        //Show error
        verifyErr.textContent = error;
        verifyErr.classList.remove("hidden");

        //Set to finished
        verifyButton.textContent = "Verify";
        verifyButton.disabled = false;

        for (let i = 0; i < 6; i++) {
            let input = document.getElementById(`verify-${i}`);
            input.disabled = false;
        }
    });
}