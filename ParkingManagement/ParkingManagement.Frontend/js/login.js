document.getElementById("loginForm").addEventListener("submit", function(event) {
    event.preventDefault(); 

    const name = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const type = document.getElementById("type").value;

    fetch("https://localhost:7084/api/LoginAPI", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: name,
            email: email,
            password: password,
            type: type
        })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Authentication failed');
        }
        return response.json();
    })
    .then(data => {
        // Check if authentication was successful
        if (data && data.token) {
            localStorage.setItem("jwtToken", data.token);
            alert("Successfully Login.. Welcome To Site.");
            window.location.href = "ParkingManagement.html";
        } else {
            alert("Authentication failed. Please try again.");
        }
    })
    .catch(error => {
        console.error("Error:", error);
        alert("Invalid UserName or Password");
    });
});


