const addCategoryModal = new bootstrap.Modal($("#addCategoryModal"), { keyboard: false });
const editCategoryModal = new bootstrap.Modal($("#editCategoryModal"), { keyboard: false });
const deleteCategoryModal = new bootstrap.Modal($("#deleteCategoryModal"), { keyboard: false });

$("#add").on("click", function () {
    const newCategory = {
        name: $("#categoryName").val(),
        categoryType: $("input[name=categoryType]:checked").val()
    }

    if (isEmpty(newCategory.name)) {
        $("#validation-message").text("Дайте название новой категории");
    } else {
        addCategory(newCategory);
    }
});

$("#categories").on("click", "#editCategory", function () {
    let id = $(this).parent().parent().parent().parent().attr("id");
    let category = getCategory(id);

    if (category != null) {
        $("#editCategoryName").val(category.name);
        if (category.categoryType == "0") {
            $("input[name=editCategoryType]").eq(0).prop("checked", true);
        } else {
            $("input[name=editCategoryType]").eq(1).prop("checked", true);
        }
        editCategoryModal.show();

        $("#saveChanges").on("click", function () {
            const editedCategory = {
                id: id,
                name: $("#editCategoryName").val(),
                categoryType: $("input[name=editCategoryType]:checked").val()
            }

            if (isEmpty(editedCategory.name) || isEmpty(editedCategory.categoryType)) {
                $("#edit-validation-message").text("Заполните поля правильно");
            } else {
                editCategory(editedCategory);
            }
        });

    } else {
        alert("Ошибка")
    }
});

$("#categories").on("click", "#deleteCategory", function () {
    deleteCategoryModal.show();
    let id = $(this).parent().parent().parent().parent().attr("id");

    $("#confirmDelete").on("click", function () {
        deleteCategory(id);
    });
});

const getCategory = (id) => {
    let category;

    $.ajax({
        type: "get",
        url: `/admin/categories/get?id=${id}`,
        async: false
    }).done((data) => {
        if (data == null) {
            category = null;
        } else {
            category = {
                id: data.id,
                name: data.name,
                categoryType: data.categoryType
            }
        }
    }).fail((e) => {
        category = null;
    });

    return category;
}

const addCategory = (category) => {
    $.ajax({
        type: "post",
        url: "/admin/categories/create",
        async: false,
        data: { name: category.name, categoryType: category.categoryType }
    }).done(() => {
        window.location.replace($(location).attr("href"));
    }).fail((e) => {
        $("#validation-message").text(e.responseText);
    });
}

const editCategory = (category) => {
    $.ajax({
        type: "post",
        url: "/admin/categories/update",
        async: false,
        data: { id: category.id, name: category.name, categoryType: category.categoryType }
    }).done(() => {
        window.location.replace($(location).attr("href"));
    }).fail((e) => {
        isEmpty(e.responseText) ? alert("Произогла ошибка при редактировании категории") : $("#edit-validation-message").text(e.responseText);
    });
}

const deleteCategory = (id) => {
    $.ajax({
        type: "post",
        url: "/admin/categories/delete",
        async: false,
        data: { id: id }
    }).done(() => {
        window.location.replace($(location).attr("href"));
    }).fail((e) => {
        isEmpty(e.responseText) ? alert("Произошла ошибка при удалении") : alert(e.responseText);
    });
}

$("#addCategoryModal [data-bs-dismiss=modal]").on("click", function () {
    $("#validation-message").text("");
    $("#categoryName").val("");
    $("input[name=сategoryType]").eq(0).prop("checked", true);
    $("input[name=сategoryType]").eq(1).prop("checked", false);
});

$("#editCategoryModal [data-bs-dismiss=modal]").on("click", function () {
    $("#edit-validation-message").text("");
    $("input[name=editCategoryType]").eq(0).prop("checked", false);
    $("input[name=editCategoryType]").eq(1).prop("checked", false);
});

const isEmpty = (str) => str.trim() == '';