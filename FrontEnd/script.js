const container = document.querySelector(".container");
const btnSignIn = document.getElementById("btn-sign-in");
const btnSignUp=document.getElementById("btn-sign-up");

btnSignIn.addEventListener("click",()=>{
    container.classList.remove("toggle");
});
btnSignUp.addEventListener("click",()=>{
    container.classList.add("toggle");
})


class LoginClass {
    static async Login(event) {
        event.preventDefault();

        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;

        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Accept", "application/json");

        const raw = JSON.stringify({
            "usuario": email,
            "password": password
        });

        const requestOptions = {
            method: "POST",
            headers: myHeaders,
            body: raw
        };

        try {
            const response = await fetch("http://localhost:5101/Login/login", requestOptions);
            const result = await response.text();
            console.log(result);

            if (result === "true") {
                window.location.href = "principal/principal.html";
            } else {
                alert("Usuario o contraseña incorrectos.");
            }
        } catch (error) {
            console.error("Error en el login:", error);
        }
    }
}

window.LoginClass = LoginClass;

