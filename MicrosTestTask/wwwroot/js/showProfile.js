const showUserInfoModal = new bootstrap.Modal($("#showUserInfoModal"), { keyboard: false });

const showProfile = (name) => {
	$.ajax({
		type: "get",
		url: "/profile/showprofile",
		data: { username: name },
	}).done((data) => {
		$("#username").text(data.username);
		$("#allTimeIncome").text(`Приходы: +${data.allTimeIncome}`);
		$("#difference").text(`Разница: ${data.difference >= 0 ? "+" : "-"}${data.difference}`);
		$("#allTimeExpense").text(`Расходы: -${data.allTimeExpense}`);
		showUserInfoModal.show();
	}).fail(e => {
		alert("something went wrong")
	});
};