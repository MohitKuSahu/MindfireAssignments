$(document).ready(function () {
    var prevParkingZoneTitle = null;

    var dataTable = $('#report-table').DataTable({
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
            { data: 'totalBookings' },
            {
                data: 'vehicleParked',
                render: function (data, type, row) {
                    return data === 1 ? '1' : '0';
                }
            },
        ],
        paging: false,
        ordering: false
    });

    $('#generate-report-btn').on('click', function () {
        var startDate = $('#start-date').val();
        var endDate = $('#end-date').val();


        if (!startDate || !endDate) {
            alert('Please fill up Date');
            return;
        }

        var startingDate = new Date(startDate);
        var endingDate = new Date(endDate);

        if (endingDate <= startingDate) {
            alert('End date must be greater than Start date.');
            return;
        }

        const baseUrl = 'https://localhost:7084/api/ReportAPI/';
        const url = `${baseUrl}${startDate}/${endDate}`;
        ajaxRequest(url, 'GET', null)
            .done(function (data) {
                dataTable.clear().rows.add(data).draw();
                prevParkingZoneTitle = null;
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert('It is Restricted Only to Booking Agent!');
                console.error('Error fetching report data:', textStatus, errorThrown);
            });
    });

    document.getElementById('exportPdf').addEventListener('click', function () {
        var doc = new jsPDF();
        doc.setFontSize(18);
        doc.text('ParkingCarReport', 10, 10);
        doc.autoTable({ html: '#report-table' });
        const currentDate = new Date();
        const formattedDate = currentDate.toDateString();
        const formattedTime = currentDate.toLocaleTimeString();
        const formattedDateTime = formattedDate + " " + formattedTime;
        const fileName = `Parking-Report-${formattedDateTime}.pdf`;
        doc.save(fileName);
    });
});
