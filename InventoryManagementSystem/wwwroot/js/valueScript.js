$(document).ready(function () { 
    $('#addValueButton').click(function () {
        let inputs = $('.addValue');
        const inventoryId = $(this).data('inventoryid');
        const token = localStorage.getItem('authToken');

        const formData = new FormData(); 

        let listObject = []

        const rowNum = $('#valueDataTable tbody tr').length + 1;

        $.each(inputs, function (index, item) {
            let fieldId = $(item).data('field');
            let typeId = $(item).data('type');
            let value;

            if ($(item).attr('type') === 'file') {
                // Append file(s) to FormData
                if (item.files.length > 0) {
                    for (let i = 0; i < item.files.length; i++) {
                        formData.append('files', item.files[i]);
                    }
                }
                value = null; // or you can store filename
            } else if ($(item).is(':checkbox')) {
                value = $(item).prop('checked');
            } else {
                value = $(item).val();
            }

            listObject.push({
                inventoryId: inventoryId,
                fieldId: fieldId,
                typeId: typeId,
                value: value,
                rowNum: rowNum
            });
        });

        // Append JSON values as a string
        formData.append('values', JSON.stringify(listObject));

        $.ajax({
            url: '/InventoryValue/Add', // Uses the form's action URL
            type: 'POST',
            processData: false, // important for FormData
            contentType: false, // important for FormData
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: formData,
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
                    $('#errorModalValue').prepend('<br/>' + response.message);

                    $.each(response.errors, function (index, item) {
                        console.log(item);
                        $('#errorModalValue').prepend('<p>' + item + '</p>');
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