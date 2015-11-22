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

function getcurrentuser() {
    $.post("/User/Current",
       { }, function (data) {
           $("#current").html(data.current);
           $("#admin").html(data.admin);
       }, "json");
}

function addproengineering() {
    $.post("/ProEngineering/Add",
        {
            ProjectName: $("#proname").textbox("getValue"),
            ProjectUses: $("#protype").find("option:selected").text(),
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

function datechanged() {
    var y = $("#year").find("option:selected").val();
    var m = $("#month").find("option:selected").val();
    if (y != "0" && m != "0") {
        $("#startdate").datebox("setValue", formatdate(getdate(y, m, 1)));
        if (m == "01" || m == "03" || m == "05" || m == "07" || m == "08" || m == "10" || m == "12") {
            $("#enddate").datebox("setValue", formatdate(getdate(y, m, 31)));
        }
        if (m == "04" || m == "06" || m == "09" || m == "11") {
            $("#enddate").datebox("setValue", formatdate(getdate(y, m, 30)));
        }
        if (m == "02") {
            if (y % 4 == 0) {
                $("#enddate").datebox("setValue", formatdate(getdate(y, m, 29)));
            }
            else {
                $("#enddate").datebox("setValue", formatdate(getdate(y, m, 28)));
            }
        }
    } else if (y != "0") {
        $("#startdate").datebox("setValue", formatdate(getdate(y, 1, 1)));
        $("#enddate").datebox("setValue", formatdate(getdate(y, 12, 31)));
    }
    else {
        $("#startdate").datebox("setValue", "");
        $("#enddate").datebox("setValue", "");
    }
}

function getdate(y, m, d) {
    var date = y + "-" + m + "-" + d;
    date = new Date(Date.parse(date.replace(/-/g, "/")));
    return date;
}

function formatdate(date) {
    var y = date.getFullYear();
    var m = date.getMonth()+1;
    m = m < 10 ? '0' + m : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;
    return y + '-' + m + '-' + d;
}

function getproject() {
    $("#dg").datagrid("load", {
        ProjectStatus: $('input:checkbox[name="drpstate"]:checked').val(),
        CustomerName: $("#name").textbox("getValue"),
        CustomerTel: $("#tel").textbox("getValue"),
        ProjectAddress: $("#addr").textbox("getValue"),
        ProjectType: $('input:radio[name="drptype"]:checked').val(),
        MachineType: $('input:radio[name="drpmachinetype"]:checked').val(),
        StartDate: $("#startdate").datebox("getValue"),
        EndDate: $("#enddate").datebox("getValue")
    });
} 

function getprojectbydate(date) {
    $("#year").val("0");
    $("#month").val("0");
    var d = new Date();
    $("#startdate").datebox("setValue", formatdate(adddate(d, -1 * date)));
    $("#enddate").datebox("setValue", formatdate(getdate(d.getFullYear(), d.getMonth() + 1, d.getDate())));
    $("#dg").datagrid("load", {
        StartDate: $("#startdate").datebox("getValue"),
        EndDate: $("#enddate").datebox("getValue")
    });
}

function adddate(date, t) {
    var d = date.valueOf() + t * 24 * 60 * 60 * 1000;
    d = new Date(d);
    return d;
}

function getprojectbyptype(data) {
    $("#dg").datagrid("load", {
        ProjectType: data
    });
}

function getprojectbypaddress(data) {
    $("#dg").datagrid("load", {
        ProjectAddress: data
    });
}

function getprojectbycname(data) {
    $("#dg").datagrid("load", {
        CustomerName: data
    });
}

function getprojectbyctel(data) {
    $("#dg").datagrid("load", {
        CustomerTel: data
    });
}

function updateprojectstatus(pid,status) {
    $.post("/Query/ModifyProjectStatus",
        {
            ProjectID: pid,
            ProjectStatus: status
        }, function (data) {
            if (data.Result == 1) {
                $.messager.alert("提示", data.Message, "info");
            } else {
                $.messager.alert("错误", data.Message, "error");
            }
        }, "json");
}

var method;
function newuser() {
    method = "new";
    $("#dlg").dialog("open").dialog("setTitle", "添加用户");
    $("#fm").form("clear");
    $("#id").textbox("setValue", "0");
}

function edituser() {
    method = "edit";
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $("#id").textbox("setValue", row.id);
        $("#uname").textbox("setValue", row.uname);
        $("#rname").textbox("setValue", row.rname);
        $("#phone").textbox("setText", row.phone);
        $("#isadmin").combobox("setValue", row.isadmin);
        $("#dlg").dialog("open").dialog("setTitle", "修改用户");
    } else {
        $.messager.alert("提示", "请选择一条要修改的记录", "info");
    }
}
function saveuser() {
    if (method == "new") {
        $.post("/User/Add",
            {
                UserName: $("#uname").val(),
                RealName: $("#rname").val(),
                Phone: $("#phone").val(),
                IsAdmin: $("#isadmin").combobox("getValue")
            }, function (data) {
                if (data.Result == 1) {
                    refresh();
                    $.messager.alert("提示", data.Message, "info");
                } else {
                    $.messager.alert("错误", data.Message, "error");
                }
            }, "json");
    }
    else if (method == "edit") {
        $.post("/User/Modify",
           {
               UserID: $("#id").val(),
               UserName: $("#uname").val(),
               RealName: $("#rname").val(),
               Phone: $("#phone").val(),
               IsAdmin: $("#isadmin").combobox("getValue")
           }, function (data) {
               if (data.Result == 1) {
                   refresh();
                   $.messager.alert("提示", data.Message, "info");
               } else {
                   $.messager.alert("错误", data.Message, "error");
               }
           }, "json");
    }
}

function deleteuser() {
    var row = $("#dg").datagrid("getSelected");
    if (row) {
        $.messager.confirm("确认", "你确定要删除选中的用户吗?", function (r) {
            if (r) {
                $.post("/User/Delete", { UserID: row.id }, function (data) {
                    if (data.Result == 1) {
                        refresh();
                        $.messager.alert("提示", data.Message, "info");
                    } else {
                        $.messager.alert("错误", data.Message, "error");
                    }
                }, "json");
            }
        });
    } else {
        $.messager.alert("提示", "请选择一条要删除的记录", "info");
    }
}

function searchusers() {
    $("#dg").datagrid("load", {
        UserName: $("#Name-search").val()
    });
}

function refresh() {
    $("#dg").datagrid("reload");
}