// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.btnSave').on('click', function () {
    let $self = $(this);
    let $tr = $self.parents('tr');
    let address = $tr.find("td:eq(0)").text();
    let yearBuilt = $tr.find("td:eq(1)").text();
    let listPrice = $tr.find("td:eq(2)").text().replace('$', '');;
    let monthlyRent = $tr.find("td:eq(3)").text().replace('$', '');
    let grossYield = $tr.find("td:eq(4)").text().replace('%','');

    $.post('Properties/Save', {
        address,
        yearBuilt,
        listPrice,
        monthlyRent,
        grossYield
    }, function (response) { });

});
