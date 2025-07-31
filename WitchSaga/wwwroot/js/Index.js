(function ($, window, document) {
    "use strict";

	function initButton() {
		var self = this;

		$("#addVillager").on("click", function () {
			var currentIndex = $(".villagerGroup").length;

			var villagerGroup = `
			<div class="villagerGroup mb-3">
				<div>
					<h5>Name</h5>
					<input type="text" name="Villagers[${currentIndex}].Name" value="" />
				</div>
				<div>
					<h5>Age of Death</h5>
					<input type="number" name="Villagers[${currentIndex}].AgeOfDeath" value="0"/>
				</div>
				<div>
					<h5>Year of Death</h5>
					<input type="number" name="Villagers[${currentIndex}].YearOfDeath" value="0"/>
				</div>
			</div>
		`;
			$("#villagerContainer").append(villagerGroup);
		});

		// Handle calculate (submit)
		$("#calculate").on("click", function () {
			var formData = $("#villagerForm").serialize();
			
			$.ajax({
				type: "POST",
				url: self.FormDiv.data("calculate-url"),
				data: formData,
				success: function (result) {
					if (result.isSuccess) {
						$("#result").html(result.value);
					}
					else {
						$("#result").html(result.errorMessage);
					}
				},
				error: function (err) {
					$("#result").html("An error occurred.");
				}
			});
		});
    }

    var Index = function (element) {
		this.Element = element;
		this.FormDiv = $("#villagerForm");
    };

    Index.prototype = {
        constructor: Index,
        Register: function () {
			var self = this;

			initButton.call(self);
        }
    };

    // export javascript class into window environment
    window.Index = Index;
})(jQuery, window, document);