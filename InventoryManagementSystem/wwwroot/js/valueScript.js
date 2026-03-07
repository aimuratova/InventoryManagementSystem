$(document).ready(function () { 
    $('#addValueButton').click(function () {
        let inputs = $('.addValue');
        const inventoryId = $(this).data('inventoryId');

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
            error: function (error) {
                $('#errorModalValue').prepend('Failed to add value. Please try again.').show();

                if (error.message) {
                    $('#errorModalValue').prepend(error.message);
                }

                $.each(error.errors, function (index, item) {
                    console.log(item);
                    $('#errorModalValue').prepend(item);
                });
            }
        });
    });

    $('#valueDataTable tbody tr').click(function () {
        let rowId = $(this).data('rowid');

        window.location.href = `/InventoryValue/Index?id=${rowId}`;
    });

});