function changepassword() {
    $.post("/Changepwd/ChangePassword",
        {
            Password: $("#pwd").val(),
            Password_New: $("#pwdnew").val(),
            Password_Confirm: $("#pwdconfirm").val()
        }, function (data) {
                if (data.Result == 0) {
                    $.messager.alert("错误", data.Message, "error");
                } else if (data.Result == 1) {
                    $.messager.alert("提示", data.Message, "info");
                }
            }, "json");
}