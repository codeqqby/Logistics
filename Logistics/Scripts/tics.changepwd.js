function changepassword() {
    $.post("/Changepwd/ChangePassword",
        {
            UserName: "aaa",
            Password: $("#pwd").val(),
            Password_New: $("#pwdNew").val(),
            Password_Confirm: $("#pwdConfirm").val()
        }, function (data) {
                if (data.Result == 0) {
                    $.messager.alert("错误", data.Message, "error");
                } else if (data.Result == 1) {
                    $.messager.alert("提示", data.Message, "info");
                }
            }, "json");
}