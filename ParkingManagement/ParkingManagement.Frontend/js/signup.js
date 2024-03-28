$(document).ready(function () {
    $('#signupForm').submit(function (event) {
        event.preventDefault(); // Prevent form submission
         
        var email = $('#email').val();
        var password = $('#password').val();
        var confirmPassword = $('#confirmPassword').val();
        var name = $('#name').val();
        var type = $('#type').val();

        if (!email || !password || !confirmPassword || !name || !type) {
            alert('Please fill in all fields');
            return;
        }

        if (password !== confirmPassword) {
            alert('Passwords do not match');
            return;
        }

        var emailID = encodeURIComponent(email);
        
        ajaxRequest(`https://localhost:7084/api/UserAPI/${emailID}`, 'POST', null)
            .done(function (response) {
                if (response.success) {
                    alert('Email already exists. Please use a different email address.');
                } else {
                    registerUser();
                }
            })
            .fail(function (xhr, status, error) {
                console.log('Error checking email:', error);
            });
    });

    function registerUser() {
        var user = {
            email: $('#email').val(),
            password: $('#password').val(),
            name: $('#name').val(),
            type: $('#type').val()
        };

        ajaxRequest('https://localhost:7084/api/UserAPI', 'POST', JSON.stringify(user))
            .done(function (response) {
                if (response) {
                    alert("Account Created. You are going to redirect to LoginPage.")
                    window.location.href = "login.html";
                }
            })
            .fail(function (xhr, status, error) {
                alert("Registration failed");
                console.log('Registration failed:', error);
            });
    }
});
