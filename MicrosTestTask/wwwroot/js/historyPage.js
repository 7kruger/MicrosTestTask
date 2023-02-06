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