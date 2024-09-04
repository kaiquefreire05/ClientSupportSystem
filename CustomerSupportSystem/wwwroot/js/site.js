// Alerts config
$('.close-alert').click(function () {
    $('.alert').hide('hide');
});

// Function to initialize the DataTables
function getDatatable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "language": {
            "sEmptyTable": "No records found",
            "sInfo": "Showing from _START_ to _END_ of _TOTAL_ records",
            "sInfoEmpty": "Showing 0 to 0 of 0 records",
            "sInfoFiltered": "(Filtered from _MAX_ total records)",
            "sLengthMenu": "Show _MENU_ records per page",
            "sLoadingRecords": "Loading...",
            "sProcessing": "Processing...",
            "sZeroRecords": "No matching records found",
            "sSearch": "Search",
            "oPaginate": {
                "sNext": "Next",
                "sPrevious": "Previous",
                "sFirst": "First",
                "sLast": "Last"
            },
            "oAria": {
                "sSortAscending": ": Sort columns in ascending order",
                "sSortDescending": ": Sort columns in descending order"
            }
        }
    });
}


// Initialize DataTables
getDatatable("#tickets-table");
getDatatable("#users-table");
getDatatable("#tickets-comments-table");
getDatatable("#closed-tickets-table");