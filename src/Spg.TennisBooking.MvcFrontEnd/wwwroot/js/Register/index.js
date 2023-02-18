console.log("Register")

eventListener()

async function eventListener() {
    console.log("EventListener")

    let regsiterForm = document.getElementById("register_form");
    async function handleRegisterForm(event) {
        event.preventDefault();
        await register();
    }
    regsiterForm.addEventListener('submit', handleRegisterForm);

    let loginForm = document.getElementById("login_form");
    async function handleLoginForm(event) {
        event.preventDefault();
        await login();
    }
    loginForm.addEventListener('submit', handleLoginForm);

    let registerViewButton = document.getElementById("registerView_btn");
    registerViewButton.addEventListener("click", registerView);

    let loginViewButton = document.getElementById("loginView_btn");
    loginViewButton.addEventListener("click", loginView);
}

async function register() {
    console.log("Register")

    //Arrange UI
    let registerButton = document.getElementById("register_btn");
    registerButton.innerHTML = "Loading...";
    registerButton.disabled = true;
    let registerErr = document.getElementById("register_err");
    registerErr.classList.add("hidden");

    //Get Values
    let email = document.getElementById("InputEmail").value;
    let password = document.getElementById("InputPassword").value;
    let confirmPassword = document.getElementById("InputPasswordConfirm").value;

    //Check if passwords match
    if (password != confirmPassword) {
        registerErr.innerHTML = "Passwords do not match";
        registerErr.classList.remove("hidden");

        //Set to finished
        registerButton.innerHTML = "Register";
        registerButton.disabled = false;
        return;
    }

    //Create User
    await fetch("https://localhost:3001/api/Auth/Register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            email: email,
            password: password
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
        window.location.href = "/Verify?uuid=" + data.uuid;
    }).catch(error => {
        console.error('There was an error!', error);

        //Show error
        registerErr.innerHTML = error;
        registerErr.classList.remove("hidden");

        //Set to finished
        registerButton.innerHTML = "Register";
        registerButton.disabled = false;
    });
}

async function login() {
    console.log("Login")

    //Arrange UI
    let loginButton = document.getElementById("login_btn");
    loginButton.innerHTML = "Loading...";
    loginButton.disabled = true;
    let loginErr = document.getElementById("login_err");
    loginErr.classList.add("hidden");

    //Get Values
    let email = document.getElementById("InputEmailLogin").value;
    let password = document.getElementById("InputPasswordLogin").value;

    //Login User
    await fetch("https://localhost:3001/api/Auth/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            email: email,
            password: password
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

        //Set auth cookie and redirect
        document.cookie = "Authorization=Bearer " + data.token;

        window.location.href = "/Account";
    }).catch(error => {
        console.error('There was an error!', error);

        //Show error
        loginErr.innerHTML = error;
        loginErr.classList.remove("hidden");

        //Set to finished
        loginButton.innerHTML = "Login";
        loginButton.disabled = false;
    });
}

async function registerView() {
    console.log("RegisterView")

    let loginView = document.getElementById("loginView");
    loginView.classList.add("hidden");

    let registerView = document.getElementById("registerView");
    registerView.classList.remove("hidden");
}

async function loginView() {
    console.log("LoginView")

    let loginView = document.getElementById("loginView");
    loginView.classList.remove("hidden");

    let registerView = document.getElementById("registerView");
    registerView.classList.add("hidden");
}