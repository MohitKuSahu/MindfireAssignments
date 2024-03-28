function ajaxRequest(url, type, data) {
    return $.ajax({
        url: url,
        type: type,
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        data: data
    });
}