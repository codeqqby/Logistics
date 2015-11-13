function changepassword() {
    $.post("/Changepwd/ChangePassword",
        {
            Password: $("#pwd").val(),
            Password_New: $("#pwdnew").val(),
            Password_Confirm: $("#pwdconfirm").val()
        }, function (data) {
                if (data.Result == 1) {
                    $.messager.alert("提示", data.Message, "info");
                } else {
                    $.messager.alert("错误", data.Message, "error");
                }
            }, "json");
}

function addengineering() {
    $.post("/Engineering/Add",
        {
            EngineeringName: $("#proname").val(),
            Uses: $("#protype").val(),
            Area1: $("#area").val(),
            Area2: $("#area2").val(),
            Area3: $("#area3").val(),
            Area4: $("#area4").val(),
            Area5: $("#area5").val(),
            Area6: $("#area6").val(),
            CustomerName: $("#name").val(),
            CustomerTel: $("#tel").val(),
            Price: $("#price").val()
        }, function (data) {
            if (data.Result == 1) {
                $.messager.alert("提示", data.Message, "info");
            } else {
                $.messager.alert("错误", data.Message, "error");
            }
        }, "json");
}