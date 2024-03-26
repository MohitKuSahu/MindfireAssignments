$(document).ready(function() {
    var dataTable = $('#report-table').DataTable({
        columns: [
            { data: 'parkingZoneTitle' }, 
            { data: 'parkingSpaceTitle' }, 
            { data: 'totalBookings' }, 
            { data: 'vehicleParked' }, 
        ],
        paging: false 
    });

    $('#generate-report-btn').on('click', function() {
        var startDate = $('#start-date').val();
        var endDate = $('#end-date').val();
        const baseUrl = 'https://localhost:7084/api/ReportAPI/';
        const url = `${baseUrl}${startDate}/${endDate}`;
        $.ajax({
            url: url,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            },
            dataType: 'json',
            success: function(data) {
                dataTable.clear().draw();
                dataTable.rows.add(data).draw();
            },
            error: function(jqXHR, textStatus, errorThrown) {
                alert('It is Restricted Only to Booking Agent!');
                console.error('Error fetching report data:', textStatus, errorThrown);
            }
        });
    });

    document.getElementById('exportPdf').addEventListener('click', function() {
        var doc = new jsPDF();
        doc.setFontSize(18);
        doc.text('ParkingCarReport', 10, 10); // Add heading to the PDF
        doc.autoTable({html: '#report-table'});
        const currentDate = new Date();
        const formattedDate = currentDate.toDateString();
        const formattedTime = currentDate.toLocaleTimeString();
        const formattedDateTime = formattedDate +" " +formattedTime;
        const fileName = `Parking-Report-${formattedDateTime}.pdf`;
        doc.save(fileName);
    });
});
