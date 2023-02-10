const operationsToShow = 5;
const operationsCount = $("#categories").length;
const btn = $("#showMore");

$("#showMore").on("click", function () {
	let list = $("#operations").children();
	let nowShowing = list.filter(":visible").length;
	list.slice(nowShowing - 1, nowShowing + operationsToShow).fadeIn();
	if (nowShowing + 5 >= operationsCount) {
		$("#showMore").hide();
	}
});

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

$("#operations").on("click", "#deleteOperation", function () {
	let elem = $(this).parent().parent().parent();

	$.ajax({
		method: "post",
		url: "/manage/delete",
		async: false,
		data: { id: elem.attr("id") }
	}).done(() => {
		elem.remove();
		deleteOperationModal.hide();
	}).fail(e => {
		deleteOperationModal.hide();
		alert(e.responseText != null ? e.responseText : "Не удалось удалить операцию")
	});
});