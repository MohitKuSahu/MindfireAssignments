
$(document).ready(function () {
    var prevParkingZoneTitle = null;

    var dataTable = $('#parking-table').DataTable({
        columns: [
            {
                data: 'parkingZoneTitle',
                render: function (data, type, row) {
                    if (type === 'display') {
                        if (row.parkingZoneTitle !== prevParkingZoneTitle) {
                            prevParkingZoneTitle = row.parkingZoneTitle;
                            return data;
                        } else {
                            return '';
                        }
                    } else {
                        return data;
                    }
                }
            },
            { data: 'parkingSpaceTitle' },
            {
                data: null,
                render: function (data, type, row) {
                    if (type === 'display') {
                        return '<button class="btn btn-sm btn-danger " id="delete-btn" data-title="' + row.parkingSpaceTitle + '">DELETE SPACE</button>';
                    } else {
                        return '';
                    }
                }
            },
        ],
        ordering: false
    });

    const url = 'https://localhost:7084/api/ReportAPI/';
    ajaxRequest(url, 'GET', null)
        .done(function (data) {
            dataTable.clear().rows.add(data).draw();
            prevParkingZoneTitle = null;
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error('Error fetching report data:', textStatus, errorThrown);
        });

        $(document).on('click', '#delete-btn', function() {
            var title = $(this).data('title');
            var clickedButton = $(this);
            if (confirm('Are you sure you want to delete this parking space?')) {
                ajaxRequest(`https://localhost:7084/api/ParkingSpaceAPI/${title}`, 'DELETE', null)
                .done(function(response) {
                    if (response.success) {
                        alert('Parking space deleted successfully!');
                        dataTable.row(clickedButton.parents('tr')).remove().draw();
                    } else {
                        alert('Failed to delete parking space. It does not exist.');
                    }
                })
                .fail(function(xhr, status, error) {
                    alert('It is restricted only to Booking Agent ' + error);
                });
            }
        });
        
});


