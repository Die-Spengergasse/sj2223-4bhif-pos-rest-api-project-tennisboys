console.log("Forgot");

eventListener();

async function eventListener() {
    console.log("EventListener");

    let forgotForm = document.getElementById("forgot_form");
    async function handleForgotForm(event) {
        event.preventDefault();
        await forgot();
    }
    forgotForm.addEventListener('submit', handleForgotForm);
}

async function forgot() {
    console.log("Forgot");

    //Arrange UI
    let forgotButton = document.getElementById("forgot_btn");
    forgotButton.textContent = "Loading...";
    forgotButton.disabled = true;
    let forgotErr = document.getElementById("forgot_err");
    forgotErr.classList.add("hidden");

    //Get Values
    let email = document.getElementById("InputEmail").value;

    //Create User
    await fetch("https://localhost:3001/api/v2/Auth/ForgotPassword", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            Email: email
        })
    })
        .then(async response => {
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
            window.location.href = "/Reset?uuid=" + data.uuid;
        }).catch(error => {
            console.error('There was an error!', error);

            //Show error
            forgotErr.textContent = error;
            forgotErr.classList.remove("hidden");

            //Set to finished
            forgotButton.textContent = "Register";
            forgotButton.disabled = false;
        });

    //Set to finished
    forgotButton.textContent = "Forgot";
    forgotButton.disabled = false;
}