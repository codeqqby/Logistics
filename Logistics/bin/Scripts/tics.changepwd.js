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
            EngineeringName: $("#proname").textbox("getValue"),
            Uses: $("#protype").find("option:selected").text(),
            Area1: $("#area").find("option:selected").text(),
            Area2: $("#area2").find("option:selected").text(),
            Area3: $("#area3").textbox("getValue"),
            Area4: $("#area4").textbox("getValue"),
            Area5: $("#area5").textbox("getValue"),
            Area6: $("#area6").textbox("getValue"),
            CustomerName: $("#name").textbox("getValue"),
            CustomerTel: $("#tel").textbox("getValue"),
            Price: $("#price").textbox("getValue")
        }, function (data) {
            if (data.Result == 1) {
                $.messager.alert("提示", data.Message, "info");
            } else {
                $.messager.alert("错误", data.Message, "error");
            }
        }, "json");
}

function resetengineering() {
    $("#proname").textbox("setValue", "");
    $("#protype").val("0");
    $("#area").val("0");
    $("#area2").val("0");
    $("#area3").textbox("setValue", "");
    $("#area4").textbox("setValue", "");
    $("#area5").textbox("setValue", "");
    $("#area6").textbox("setValue", "");
    $("#name").textbox("setValue", "");
    $("#tel").textbox("setValue", "");
    $("#price").textbox("setValue", "");
}

function hiddenaddress() {
    $("#build").hide();
    $("#address1").next().hide();
    $("#address2").next().hide();
    $("#address3").next().hide();
    $("#address4").next().hide();
    $("#address5").next().hide();
    $("#address6").next().hide();
    $("#lb1").hide();
    $("#lb2").hide();
    $("#lb3").hide();
    $("#lb4").hide();
}

function housetypechange() {
    if ($("#housetype").val() == "0") {
        hiddenaddress();
    }
    else if ($("#housetype").val() == "1") {
        $("#address1").next().show();
        $("#address2").next().show();
        $("#address3").next().show();
        $("#address4").next().show();
        $("#lb1").html($("#housetype").find("option:selected").text());
        $("#lb2").html("期");
        $("#lb3").html("幢");
        $("#lb4").html("室");
        $("#lb1").show();
        $("#lb2").show();
        $("#lb3").show();
        $("#lb4").show();
        $("#build").show();
        $("#frmbuild").attr("src", "/Build/Index");
    } else if ($("#housetype").val() == "2") {
        $("#lb1").html("小区");
        $("#lb2").html("期");
        $("#lb3").html("幢");
        $("#address4").next().hide();
        $("#lb4").hide();
        $("#build").show();
    } else if ($("#housetype").val() == "3") {
        $("#build").hide();
        $("#address2").next().hide();
        $("#address3").next().hide();
        $("#lb3").hide();
        $("#address1").removeAttr("readonly");
        $("#lb1").html("镇");
        $("#lb2").html("村");
        $("#address5").next().show();
        $("#address6").next().show();
    }
}

function getfirstletter() {
    $.post("/Build/GetFirstLetter",
         { }, function (data) {
             if (data != null) {
                 alert(data);
             }
         }, "json");
}
