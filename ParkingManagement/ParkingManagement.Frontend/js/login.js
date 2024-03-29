document.getElementById("loginForm").addEventListener("submit", function(event) {
    event.preventDefault(); 

    var name = $('#name').val();
    var email = $('#email').val();
    var password = $('#password').val();
    
 
    var user = {
        name: name,
        email: email,
        password: password,
        type:"default"
       
    };

    fetch("https://localhost:7084/api/LoginAPI", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    })
    .then(async response => {
        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage);
        }
        return response.json();
    })
    .then(data => {
        if (data && data.token) {
            localStorage.setItem("jwtToken", data.token);
            alert("Successfully Login.. Welcome To Site.");
            window.location.href = "DashBoard.html";
        } else {
            alert("Authentication failed. Please try again.");
        }
    })
    .catch(error => {
        console.error("Error:", error);
        alert("Invalid UserName or Password");
    });
});
