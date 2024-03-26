$(document).ready(function() {
    $('#parking-form').submit(function(event) {
        event.preventDefault(); 
        var parkingZoneTitle = $('#parking-zone').val();
        var parkingSpaceNumber = $('#parking-space').val();

        if (parkingSpaceNumber < 10) {
            parkingSpaceNumber = '0' + parkingSpaceNumber;
        }
        var parkingSpaceTitle = parkingZoneTitle + parkingSpaceNumber; 
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7084/api/ParkingZoneAPI', 
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            },
            data: JSON.stringify({ parkingZoneTitle: parkingZoneTitle }),
            contentType: 'application/json',
            dataType: 'json'
        })
        .done(function(response) {
            if (response) {
                var parkingZoneId = response.parkingZoneId;
                var requestData = {
                    parkingZoneId: parkingZoneId,
                    parkingSpaceTitle: parkingSpaceTitle   
                };
                $.ajax({
                    type: 'POST',
                    url: 'https://localhost:7084/api/ParkingSpaceAPI',
                    headers: {
                        'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
                    },
                    data: JSON.stringify(requestData),
                    contentType: 'application/json',
                    dataType: 'json'
                })
                .done(function(response) {
                    if (response.success) {
                        alert('Parking space added successfully!');
                    } else {
                        alert('Failed to add parking space. It already exists.');
                    }
                })
                .fail(function(xhr, status, error) {
                    alert('It is Restricted only to Booking Agent. ' + error);
                });
            } else {
                alert('Failed to add parking zone. ' + response.message);
            }
        })
    });
});

$('#delete-btn').click(function(event) {
    event.preventDefault(); 
    var parkingZoneTitle = $('#parking-zone').val();
    var parkingSpaceNumber = $('#parking-space').val();

    if (parkingSpaceNumber < 10) {
        parkingSpaceNumber = '0' + parkingSpaceNumber;
    }
    var parkingSpaceTitle = parkingZoneTitle + parkingSpaceNumber; 

    $.ajax({
        type: 'DELETE',
        url: `https://localhost:7084/api/ParkingSpaceAPI/${parkingSpaceTitle}`,
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
    })
    .done(function(response) {
        if (response.success) {
            alert('Parking space deleted successfully!');
        } else {
            alert('Failed to delete parking space. It does not exists.');
        }
    })
    .fail(function(xhr, status, error) {
        alert('It is Restricted only to Booking Agent ' + error);
    });

     
});