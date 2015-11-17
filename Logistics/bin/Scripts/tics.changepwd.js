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

function addproengineering() {
    $.post("/ProEngineering/Add",
        {
            EngineeringName: $("#proname").textbox("getValue"),
            Uses: $("#protype").find("option:selected").text(),
            Area1: $("#area1").find("option:selected").text(),
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

function resetproengineering() {
    $("#proname").textbox("setValue", "");
    $("#protype").val("0");
    $("#area1").val("0");
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

function prohousetypechange() {
    if ($("#housetype").val() == "0") {
        hiddenaddress();
    }
    else if ($("#housetype").val() == "1") {
        $("#address1").next().show();
        $("#address2").next().show();
        $("#address3").next().show();
        $("#address4").next().show();
        $("#address5").next().hide();
        $("#address6").next().hide();
        $("#lb1").html($("#housetype").find("option:selected").text());
        $("#lb2").html("期");
        $("#lb3").html("幢");
        $("#lb4").html("室");
        $("#lb1").show();
        $("#lb2").show();
        $("#lb3").show();
        $("#lb4").show();
        $("#build").show();
        $("#address1").next().children().attr("readonly", "readonly");
        $("#frmbuild").attr("src", "/Build/Index");
    } else if ($("#housetype").val() == "2") {
        $("#address1").next().show();
        $("#address2").next().show();
        $("#address3").next().show();
        $("#address4").next().hide();
        $("#address5").next().hide();
        $("#address6").next().hide();
        $("#lb1").html("小区");
        $("#lb2").html("期");
        $("#lb3").html("幢");
        $("#lb1").show();
        $("#lb2").show();
        $("#lb3").show();
        $("#lb4").hide();
        $("#build").show();
        $("#address1").next().children().attr("readonly", "readonly");
        $("#frmbuild").attr("src", "/Build/Index");
    } else if ($("#housetype").val() == "3") {
        $("#build").hide();
        $("#address1").next().show();
        $("#address2").next().hide();
        $("#address3").next().hide();
        $("#address4").next().hide();
        $("#address5").next().show();
        $("#address6").next().show();
        $("#address1").next().children().removeAttr("readonly");
        $("#lb1").html("镇");
        $("#lb2").html("村");
        $("#lb1").show();
        $("#lb2").show();
        $("#lb3").hide();
        $("#lb4").hide();
    }
}

function setaddress(address) {
    $("#address1").textbox("setValue", address);
}

function bindbuildradio() {
    $(":radio").click(function () {
        if ($(this).attr("name").indexOf("rdbfirstletter") >= 0) {
            location.href = "/Build/Index?firstLetter=" + $(this).val();
        }
        if ($(this).attr("name").indexOf("rdbbuild") >= 0) {
            parent.setaddress($(this).val());
        }
    });
}

function addprohourse() {
    $.post("/ProHourse/Add",
        {
            MachineType: $('input:radio[name="drpType"]:checked').val(),
            ProHouseType: $("#housetype").val(),
            Address0: $("#area").find("option:selected").text(),
            Address1: $("#address1").textbox("getValue"),
            Address2: $("#address2").textbox("getValue"),
            Address3: $("#address3").textbox("getValue"),
            Address4: $("#address4").textbox("getValue"),
            Address5: $("#address5").textbox("getValue"),
            Address6: $("#address6").textbox("getValue"),
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

function resetprohourse() {
    $("input:radio:checked").each(function (i, val) {
        if (val.name == "drpType") {
            $("input:radio[name='" + val.name + "']:checked").attr("checked", false);
        }
    });
    $("#housetype").val("0");
    $("#address1").textbox("setValue", "");
    $("#address2").textbox("setValue", "");
    $("#address3").textbox("setValue", "");
    $("#address4").textbox("setValue", "");
    $("#address5").textbox("setValue", "");
    $("#address6").textbox("setValue", "");
    $("#name").textbox("setValue", "");
    $("#tel").textbox("setValue", "");
    $("#price").textbox("setValue", "");
}

function getyearoption() {
    var date = new Date();
    var year = date.getFullYear();
    for (var i = 0; i < 5; i++)
    {
        if (i == 0) {
            year = year;
        }
        else {
            year -= 1;
        }
        $("#year").append("<option value='" + year + "'>" + year + "</option>");
    }
}

function yearchanged() {
    var startdate = $("#startdate").datebox("getValue");
    var enddate = $("#enddate").datebox("getValue");
    alert(startdate);
}