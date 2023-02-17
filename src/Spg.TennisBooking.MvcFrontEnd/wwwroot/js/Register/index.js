console.log("Register")

eventListener()

async function eventListener() {
    console.log("EventListener")

    let registerButton = document.getElementById("register_btn");
    registerButton.addEventListener("click", register);

    let loginButton = document.getElementById("login_btn");
    loginButton.addEventListener("click", login);
}

async function register() {
    console.log("Register")

    //Get Values
    let email = document.getElementById("InputEmail").value;
    let password = document.getElementById("InputPassword").value;
    let confirmPassword = document.getElementById("InputPasswordConfirm").value;

    //Check if passwords match
    if (password != confirmPassword) {
        alert("Passwords do not match");
        return;
    }

    //Create User
    let repsonse = await fetch("https://localhost:3001/api/Auth/Register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
        noCors: true,
        body: {
            email: email,
            password: password
        }
    }).catch((err) => {
        console.log(err);
    });
    console.log(repsonse.json());
}

async function login() {
    console.log("Login")

    //Get Values
    let email = document.getElementById("InputEmailLogin").value;
    let password = document.getElementById("InputPasswordLogin").value;

    //Create User
    let repsonse = await fetch("https://localhost:3001/api/Auth/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
        },
        noCors: true,
        body: {
            email: email,
            password: password
        }
    }).catch((err) => {
        console.log(err);
    });
    console.log(repsonse.json());
}