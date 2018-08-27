$(document).ready(function () {
    loadData();

    $('#btnShowAddModal').click(ShowAddModel);
    $('#btnAdd').click(Add);
});

//Load Data function
function loadData() {
    $.ajax({
        url: "/Manage/List",
        success: function (html) {
            $('#list').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ShowAddModel() {
    $('#myModal').modal('show');
}

function Add() {    
    var data = {
        url: $('#Url').val()
    };

    if (!data.url) {
        alert('Введите url');
        return;
    }

    $.ajax({
        url: "/Manage/Add",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.result) {
                alert('Такой url уже добавлен');
            }

            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delele(url) {
    var ans = confirm("Вы уверены, что хотите удалить адрес " + url + "?");
    if (ans) {
        $.ajax({
            url: "/Manage/Delete/",
            type: "POST",
            data: JSON.stringify({ url }),
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}