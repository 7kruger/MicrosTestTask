$("input[name=categoryType]").on("click", function () {
	let value = $("input[name=categoryType]:checked").val();
	if (value == "0") {
		$("#expenseList").attr("disabled", true);
		$("#expenseList").hide();
		$("#incomeList").show();
		$("#incomeList").removeAttr("disabled");
	} else {
		$("#incomeList").attr("disabled", true);
		$("#incomeList").hide();
		$("#expenseList").show();
		$("#expenseList").removeAttr("disabled");
	}
});

$("#add").on("click", function () {

	const newOperation = {
		date: $("#date").val(),
		sum: $("#sum").val(),
		comment: $("#comment").val(),
		categoryId: $("select[name=categoryId]:visible").val()
	}

	if (isEmpty(newOperation.date) || isEmpty(newOperation.sum)) {
		$("#validation-message").text("Заполните все поля!");
	} else {
		create(newOperation);
	}
});

const isEmpty = (str) => str.trim() == '';

const clear = () => {
	$("#date").val('');
	$("#sum").val('');
	$("#comment").val('');
	$("#validation-message").text('');
}

const create = (operation) => {
	$.ajax({
		method: "post",
		async: false,
		url: "/manage/create",
		data: operation
	}).done((data) => {
		clear();
	}).fail(() => {
		$("#validation-message").text("Ошибка");
	});
}