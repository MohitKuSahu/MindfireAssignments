
function checkAuthentication() {
    const token = localStorage.getItem('jwtToken');

    if (!token) {
        window.location.href = 'login.html';
    }
}


document.addEventListener('DOMContentLoaded', function() {
    checkAuthentication();
});
