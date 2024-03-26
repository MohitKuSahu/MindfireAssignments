$(document).ready(function () {
    function fetchParkingZones() {
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            }
        });

        $.get("https://localhost:7084/api/ParkingZoneAPI", function (data) {
            var dropdown = $('#parking-zone-select');
            dropdown.append($('<option disabled selected>').text('Select Here'));
            $.each(data, function (index, zone) {
                dropdown.append($('<option>').val(zone.parkingZoneId).text(zone.parkingZoneTitle));
            });
        });
    }
    fetchParkingZones();



    function fetchParkingSpaces(zoneId) {
        const apiUrl = `https://localhost:7084/api/ParkingSpaceAPI/${zoneId}`;

        return $.ajax({
            url: apiUrl,
            type: 'GET',
            dataType: 'json',
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            },
        });
    }
    


    function fetchBookingsForToday() {
        return $.ajax({
            url: 'https://localhost:7084/api/VehicleParkingAPI', 
            type: 'GET',
            dataType: 'json',
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            }
        });
    }

    var spaceId;
    var occupied=0;

    function renderParkingSpaces(zoneId) {
        var currentTime = new Date();
        fetchParkingSpaces(zoneId).done(function (parkingSpaces) {
            fetchBookingsForToday().done(function (bookings) {
                var html = '';
                var row = '<div class="row">';
                if (parkingSpaces.length === 0) {
                    html += '<p>No spaces available.</p>';
                    $('#parking-spaces').html(html); 
                } 
                else{
                    $.each(parkingSpaces, function(index, space) {
                        var bookingsForSpace = bookings.filter(function(booking) {
                            if (booking.parkingSpaceId === space.parkingSpaceId) {
                                if (!booking.bookingDateTime || (booking.releaseDateTime && new Date(booking.releaseDateTime) <= currentTime)) {
                                    return false;
                                } else {
                                    return true;
                                }
                            } else {
                                return false;
                            }
                        });
                    
                        var bookingColor = bookingsForSpace.length > 0 ? 'lightgray' : 'lightgreen';
                    
                        row += '<div class="col-6 col-sm-4 col-md-3 col-lg-2 parking-space ' + bookingColor + '" data-space-title="' + space.parkingSpaceTitle + '" data-space-id="' + space.parkingSpaceId + '">';
                        row += '<h4>' + space.parkingSpaceTitle + '</h4>';
                        row += '</div>';
                    
                        if (index === parkingSpaces.length - 1) {
                            row += '</div>';
                            html += row;
                            row = '<div class="row">';
                        }
                    });
                    
                    html += '<div class="row mt-2"></div>';
                    html += '<div id="color-indication" class="col-12">';
                    html += '<div class="row align-items-center">';
                    html += '<div class="col-auto d-flex ">';
                    html += '<div class="color-block lightgreen mr-2 align-items-center"></div>';
                    html += '<p class="status-text">Vacant</p>';
                    html += '</div>';
                    html += '<div class="w-100"></div>';
                    html += '<div class="col-auto d-flex ">';
                    html += '<div class="color-block lightgray mr-2 align-items-center"></div>';
                    html += '<p class="status-text">Occupied</p>';
                    html += '</div>';
                    html += '</div>'; 
                    html += '</div>'; 
    
                    $('#parking-spaces').html(html); 
    
                    $('.parking-space.lightgreen').click(function () {
                        var spaceTitle = $(this).data('space-title');
                        spaceId = $(this).attr('data-space-id');
                        occupied = 0;
                    
                        $.ajax({
                            url: `https://localhost:7084/api/VehicleParkingAPI/${spaceId}`,
                            type: 'GET',
                            headers: {
                                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
                            },
                            success: function (response) {
                                openModal(spaceTitle);
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                $(this).off('click');
                                console.error('Authorization check failed:', textStatus, errorThrown);
                            }
                        });
                    });
                    
                    $('.parking-space.lightgray').click(function () {
                         spaceId = $(this).attr('data-space-id');
                        var spaceTitle = $(this).data('space-title');
                        occupied = 1;
                    
                        $.ajax({
                            url: `https://localhost:7084/api/VehicleParkingAPI/${spaceId}`,
                            type: 'GET',
                            headers: {
                                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
                            },
                            success: function (response) {
                                if (Array.isArray(response) && response.length > 0) {
                                    var vehicleDetail = response[0];
                                    $('#vehicle-registration').val(vehicleDetail.vehicleRegistration);
                                    $('#booking-date-time').val(vehicleDetail.bookingDateTime);
                                    $('#release-date-time').val(vehicleDetail.releaseDateTime);
                                    openModal(spaceTitle);
                                } else {
                                    console.error("No vehicle details found for space ID:", spaceId);
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                $(this).off('click');
                                console.error("Error fetching vehicle details:", textStatus, errorThrown);
                            }
                        });
                        
                    });
                }
               

            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching bookings for today:", textStatus, errorThrown);
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching parking spaces:", textStatus, errorThrown);
        });
    }


    
    function openModal(spaceTitle) {
        $('#vehicle-modal').css('display', 'block');
        if (occupied==1) {
            $('#release-Btn').show();
        }
        else {
            $('#vehicle-registration').val('');
            $('#booking-date-time').val('');
            $('#release-date-time').val('');
            $('#release-Btn').hide(); 
        }
        $('#spaceTitle').text(spaceTitle);
    }

    $('.close').click(function () {
        $('#vehicle-modal').css('display', 'none');
    });
    $(window).click(function (event) {
        if (event.target == $('#vehicle-modal')[0]) {
            $('#vehicle-modal').css('display', 'none');
        }
    });
    



    $('#refresh-button').click(function () {
        var selectedZoneId = $('#parking-zone-select').val(); 
        $('#parking-spaces').empty(); 
        refreshParkingSpaces(selectedZoneId); 
    });


    function refreshParkingSpaces(zoneId) {
        renderParkingSpaces(zoneId);    
        setTimeout(function() {
            $('#auto-refresh-indicator').hide();
        }, 2000); 
    }

    setInterval(function() {
        $('#auto-refresh-indicator').show();
     var selectedZoneId = $('#parking-zone-select').val(); 
      refreshParkingSpaces(selectedZoneId);
    }, 1 * 30 * 1000);



    $('#submit-Btn').click(function (event) {
        // Prevent default form submission
        event.preventDefault();
    
        var vehicleRegistration = $('#vehicle-registration').val();
        var bookingDateTime = $('#booking-date-time').val();
        var releaseDateTime = $('#release-date-time').val();
        var parkingZoneId = $('#parking-zone-select').val();
        var parkingSpaceId = spaceId;
    

            if (!vehicleRegistration|| !bookingDateTime || !releaseDateTime) {
                alert('Please fill in all required fields.');
                return; 
            }   
       
        var requestData = {
            vehicleRegistration: vehicleRegistration,
            bookingDateTime: bookingDateTime,
            releaseDateTime: releaseDateTime,
            parkingZoneId: parkingZoneId,
            parkingSpaceId: parkingSpaceId
        };

        console.log("Submitting data:", requestData); // Debugging log
    
        $.ajax({
            url: 'https://localhost:7084/api/VehicleParkingAPI',
            type: 'PUT', // Assuming you're updating existing data
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            },
            success: function (response) {
                alert('Booking Successful');
                $('#vehicle-modal').css('display', 'none'); // Close the modal
                
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error('Error submitting booking:', textStatus, errorThrown);
            }
        });
    });

    $('#release-Btn').click(function () { 

        if (confirm("Are you sure you want to release this vehicle?")) {
            $.ajax({
                url: `https://localhost:7084/api/VehicleParkingAPI/${spaceId}`,
                type: 'DELETE',
                headers: {
                    'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
                },
                success: function (response) {
                    alert('Space is now vacant successfully');
                    $('#vehicle-modal').css('display', 'none');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error releasing space:', textStatus, errorThrown);
                    alert('Failed to release space. Please try again.');
                }
            });
        }
    });
    
    
});


