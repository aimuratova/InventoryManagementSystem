$(document).ready(function () { 
    $('#addValueButton').click(function () {
        let inputs = $('.addValue');
        const inventoryId = $(this).data('inventoryid');
        const token = localStorage.getItem('authToken');

        let listObject = []

        const rowNum = $('#valueDataTable tbody tr').length + 1;

        $.each(inputs, function (index, item) {
            let fieldId = $(item).data('field');
            let typeId = $(item).data('type');
            let value = $(item).val();

            if ($(item).is(':checkbox')) {
                value = $(item).prop('checked');
            }

            listObject.push({
                inventoryId: inventoryId,
                fieldId: fieldId,
                typeId: typeId,
                value: value,
                rowNum: rowNum
            });
        });

        $.ajax({
            url: 'InventoryValue/Add', // Uses the form's action URL
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(listObject),
            success: function (response) {
                window.location.reload();
            },            
            error: function (xhr, status, error) {

                $('#errorModalValue')
                    .prepend('Failed to add value. Please try again.')
                    .show();

                console.log(xhr);

                var response = xhr.responseJSON;

                if (response && !response.success) {
                    $('#errorModalValue').prepend(response.message);

                    $.each(response.errors, function (index, item) {
                        console.log(item);
                        $('#errorModalValue').prepend(item);
                    });
                }
            }
        });
    });

    $('#valueDataTable tbody tr').click(function () {
        let rowId = $(this).data('rowid');
        let inventoryId = $(this).data('inventoryid');

        window.location.href = `/InventoryValue/Index?id=${rowId}&inventoryId=${inventoryId}`;
    });

    $(".bigtext").each(function () {

        var fullText = $(this).text();
        var maxLength = 100;

        if (fullText.length > maxLength) {

            var shortText = fullText.substr(0, maxLength) + "...";

            $(this).html(`
            <span class="short-text">${shortText}</span>
            <span class="full-text" style="display:none">${fullText}</span>
            <a href="#" class="read-more-btn">Read more</a>
        `);
        }

    });
});