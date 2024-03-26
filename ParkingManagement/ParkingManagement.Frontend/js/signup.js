$(document).ready(function () {
    $('#signupForm').submit(function (event) {
        event.preventDefault(); // Prevent form submission

        var email = encodeURIComponent($('#email').val());
        $.ajax({
            url: `https://localhost:7084/api/UserAPI/${email}`,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    alert('Email already exists. Please use a different email address.');
                } else {
                    registerUser();
                }
            },
            error: function (xhr, status, error) {
                console.log('Error checking email:', error);
            }
        });
    });

  
    function registerUser() {
        var user = {
            email: $('#email').val(),
            password: $('#password').val(),
            name: $('#name').val(),
            type: $('#type').val()
        };

        $.ajax({
            url: 'https://localhost:7084/api/UserAPI',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function (response) {
                if (response) {
                    window.location.href = "login.html";
                    alert("Account Created. You are going to redirect to LoginPage.")
                }
            },
            error: function (xhr, status, error) {
                alert("Success");
                console.log('Registration failed:', error);
            }
        });
    }
});
