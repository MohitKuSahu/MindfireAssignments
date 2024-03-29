$(document).ready(function() {
    $('#parking-form').submit(function(event) {
        event.preventDefault(); 
        var parkingZoneTitle = $('#parking-zone').val();
        var parkingSpaceNumber = $('#parking-space').val();

        if (!parkingSpaceNumber || !parkingZoneTitle) {
            alert('Please fill up the details');
            return;
        }

        if (parkingSpaceNumber < 10) {
            parkingSpaceNumber = '0' + parkingSpaceNumber;
        }
        var parkingSpaceTitle = parkingZoneTitle + parkingSpaceNumber; 

        ajaxRequest('https://localhost:7084/api/ParkingZoneAPI', 'POST', JSON.stringify({ parkingZoneTitle: parkingZoneTitle }))
        .done(function(response) {
            if (response) {
                var parkingZoneId = response.parkingZoneId;
                var requestData = {
                    parkingZoneId: parkingZoneId,
                    parkingSpaceTitle: parkingSpaceTitle   
                };
                ajaxRequest('https://localhost:7084/api/ParkingSpaceAPI', 'POST', JSON.stringify(requestData))
                .done(function(response) {
                    if (response.success) {
                        alert('Parking space added successfully!');
                    } else {
                        alert('Failed to add parking space. It already exists.');
                    }
                })
                .fail(function(xhr, status, error) {
                    alert('It is restricted only to Booking Agent. ' + error);
                });
            } else {
                alert('Failed to add parking zone. ' + response.message);
            }
        })
        .fail(function(xhr, status, error) {
            alert('It is restricted only to Booking Agent. ' + error);
        });
    });
});