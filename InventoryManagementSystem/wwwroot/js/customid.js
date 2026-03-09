$(document).ready(function () {

    $("#customIdAddBtn").click(function () {
        addRow();
    });

    function addRow() {
        var number = $('#elementsContainer div.elementRow').length + 1;

        var optionsText = "";
        $.each(customIdOptions, function (index, item) {
            optionsText += `<option value="${item.id}">${item.title}</option>`;
        });

        var row = `
        <div class="row align-items-center mb-2 elementRow" data-customrow="${number}">
			<div class="col-md-6">
				<div class="input-group">
					<span class="input-group-text" id="basic-addon1">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-down-up" viewBox="0 0 16 16">
							<path fill-rule="evenodd" d="M11.5 15a.5.5 0 0 0 .5-.5V2.707l3.146 3.147a.5.5 0 0 0 .708-.708l-4-4a.5.5 0 0 0-.708 0l-4 4a.5.5 0 1 0 .708.708L11 2.707V14.5a.5.5 0 0 0 .5.5m-7-14a.5.5 0 0 1 .5.5v11.793l3.146-3.147a.5.5 0 0 1 .708.708l-4 4a.5.5 0 0 1-.708 0l-4-4a.5.5 0 0 1 .708-.708L4 13.293V1.5a.5.5 0 0 1 .5-.5"></path>
						</svg>
					</span>
					<select class="form-select typeSelect">
						`+ optionsText +`
					</select>
				</div>
			</div>
			<div class="col-md-6">
				<div class="input-group">
					<input type="text" class="form-control valueInput" placeholder="Format or value">
					<span class="input-group-text" id="basic-addon1">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-question-circle" viewBox="0 0 16 16">
							<path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"></path>
							<path d="M5.255 5.786a.237.237 0 0 0 .241.247h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286m1.557 5.763c0 .533.425.927 1.01.927.609 0 1.028-.394 1.028-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94"></path>
						</svg>
					</span>
				</div>
			</div>
	    </div>
        `;

        $("#elementsContainer").append(row);
    }

    $(document).on("keyup change", ".valueInput, .typeSelect", function () {
        updateExample();
    });

    function updateExample() {

        var result = "";

        $(".elementRow").each(function () {

            var type = $(this).find(".typeSelect").val();
            var value = $(this).find(".valueInput").val();

            if (type === 1) {
                result += value;
            }

            if (type === 2) {
                var rand = Math.floor(Math.random() * 99999);
                result += value + rand;
            }

            if (type === 3) {
                result += value + "001";
            }

            if (type === 4) {
                var year = new Date().getFullYear();
                result += year;
            }

        });

        $("#examplePreview").text(result);

    }

});
