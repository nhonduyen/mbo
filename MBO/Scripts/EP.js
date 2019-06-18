$(document).ready(function () {
    $('#ep').addClass('active');
    var tbPreiod = $('#tbPeriod').DataTable();
    var table = $('#tbMainDefault').DataTable({
        responsive: true,
        sort: false,
        "processing": true,
        "serverSide": true,
        "searching": true,
        //"iDisplayLength": 25,
        ajax: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Paging/EPPAGE.aspx/Data",
            data: function (d) {
                return JSON.stringify({ parameters: d});
            }
        }
        //
    });
    $("#tbMainDefault").on('click', 'tr', function (e) {
        if (!$(this).parent("thead").is('thead')) {
            $("tr").removeClass("success");
            $(this).addClass("success");
            if (!$(e.target).is('#tbMainDefault td input:checkbox')) {
                $(this).find('input:checkbox').trigger('click');
            }
        }
    });
    $("#tbMainDefault").on('change', '.ckb', function () {
        var group = ":checkbox[class='" + $(this).attr("class") + "']";
        if ($(this).is(':checked')) {
            $(group).not($(this)).attr("checked", false);

        }

    });
    var table1 = $('#tbUser').DataTable({
        responsive: true,
        sort: false,
        "processing": true,
        "serverSide": true,
        "searching": true,
        //"iDisplayLength": 25,
        ajax: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Paging/EPPAGE.aspx/Data",
            data: function (d) {
                return JSON.stringify({ parameters: d });
            }
        }
        //
    });
    $("#tbUser").on('click', 'tr', function (e) {
        if (!$(this).parent("thead").is('thead')) {
            $("tr").removeClass("success");
            $(this).addClass("success");
            if (!$(e.target).is('#tbMainDefault td input:checkbox')) {
                $(this).find('input:checkbox').trigger('click');
            }
        }
    });
    $("#tbUser").on('change', '.ckb', function () {
        var group = ":checkbox[class='" + $(this).attr("class") + "']";
        if ($(this).is(':checked')) {
            $(group).not($(this)).attr("checked", false);

        }

    });
    $('#btnNew').click(function () {
        $("#mdPeriod").modal({
            backdrop: 'static',
            keyboard: false
        });
        return false;
    });

    $('#frmPeriod').submit(function (evt) {
        evt.preventDefault();
        var time = $('#txtEvaTime').val();
        var from = $('#txtFrom').val();
        var to = $('#txtTo').val();
        var enable = "Enable";
        var setMbo = "NO";
        $.ajax({
            url: 'Services/PeriodServices.asmx/Insert',
            data: JSON.stringify(
               { EVA_TIME: time, EVA_START: from, EVA_END: to, STATUS:0 }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                if (rs) {
                    $('#mdPeriod').modal('hide');
                    alert(status);
                    tbPreiod.row.add([
                        "<input type='checkbox' class='ckb' data-eva='" + time + "' />",
                        time,
                        from,
                        to,
                        enable,
                        setMbo
                    ]).draw(false);
                }
                $('#frmPeriod')[0].reset();
                return false;
            },
            error: function (xhr, status, error) {
                alert("Error!" + xhr.status);
            },
        });
        return false;
    });
   
    $('#btnDel').on("click",function () {
        if ($("#tbPeriod input:checkbox:checked").length > 0) {
            var t = confirm('Are you sure you want to delete this period?');
            if (t) {
                $('#tbPeriod tbody input:checked').each(function (item) {
                    var row = tbPreiod.row($(this).parents().parent('tr'));

                    $.ajax({
                        url: 'Services/PeriodServices.asmx/Delete',
                        data: JSON.stringify({
                            EVA_TIME: $(this).attr('data-eva')
                        }),
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        crossBrowser: true,
                        success: function (data, status) {
                            row.remove().draw();
                        },
                        error: function (xhr, status, error) {
                            alert("Error! " + xhr.status);
                        },
                    });

                });
            }
        }
        else {
            alert("Please select a row.");
        }
        return false;
    });
    $('#btnSet').on("click", function () {
        if ($("#tbPeriod input:checkbox:checked").length > 0) {

            var EVA_TIME = $('#tbPeriod tbody input:checked').parent().next().text();
            var set = $('#tbPeriod tbody input:checked').attr('data-set');
            if (set != 1) {
                $.ajax({
                    url: 'Services/PeriodServices.asmx/SetMbo',
                    data: JSON.stringify({
                        EVA_TIME: EVA_TIME
                    }),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        alert(status);
                        location.reload();
                    },
                    error: function (xhr, status, error) {
                        alert("Error! " + xhr.status);
                    },
                });
            }
            else {
                alert('Already set');
            }
        }

        else {
            alert("Please select a row.");
        }
        return false;
    });
    $('#btnDisable').on("click", function () {
        if ($("#tbPeriod input:checkbox:checked").length > 0) {
           
            var EVA_TIME = $('#tbPeriod tbody input:checked').parent().next().text();
            var STATUS = 1;
                    $.ajax({
                        url: 'Services/PeriodServices.asmx/Disable',
                        data: JSON.stringify({
                            EVA_TIME: EVA_TIME, STATUS: STATUS
                        }),
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        crossBrowser: true,
                        success: function (data, status) {
                            alert(status);
                            location.reload();
                        },
                        error: function (xhr, status, error) {
                            alert("Error! " + xhr.status);
                        },
                    });

               
            }
       
        else {
            alert("Please select a row.");
        }
        return false;
    });
    $('#btnEnable').on("click", function () {
        if ($("#tbPeriod input:checkbox:checked").length > 0) {

            var EVA_TIME = $('#tbPeriod tbody input:checked').parent().next().text();
            var STATUS = 0;
            $.ajax({
                url: 'Services/PeriodServices.asmx/Disable',
                data: JSON.stringify({
                    EVA_TIME: EVA_TIME, STATUS: STATUS
                }),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    alert(status);
                    location.reload();
                },
                error: function (xhr, status, error) {
                    alert("Error! " + xhr.status);
                },
            });


        }

        else {
            alert("Please select a row.");
        }
        return false;
    });
    // employee
    $('#btnAdd').click(function () {
        $('#frmModify')[0].reset();
        $('#action').val(0);
        $('#title').html('<span class="glyphicon glyphicon-file"></span>Add Employee');
        $('#mdEmp').modal();
        return false;
    });

    $('#btnEdit').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {
            $('#frmModify').find("input[type=text], textarea").val("");
            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var tr = checkbox.parents().parent();

            var EMP_ID = checkbox.attr('data-emp-id');
           
            var NAME = $.trim(tr.children("td:nth-child(4)").text());
            var WORKGROUP = $.trim(tr.children("td:nth-child(5)").text());
            var ENTER_DATE = $.trim(tr.children("td:nth-child(6)").text()); 
            var EVA_GROUP = checkbox.attr("data-group"); 
            $('#action').val(1);
            $('#ddlGroup').val(WORKGROUP);
            $('#ddlEva').val(EVA_GROUP);
            $('#txtName').val(NAME);
            $('#txtDate').val(ENTER_DATE);
            $('#txtId').val(EMP_ID);
           
            $('#mdEmp').modal();
        }
        else {
            alert('Please select a row.');
        }
        return false;
    });

    $('#frmModify').submit(function (e) {
        e.preventDefault();
        var EMP_ID = $('#txtId').val();
        var NAME = $('#txtName').val();
        var WORKGROUP = $('#ddlGroup').val();
        var ENTER_DATE = $('#txtDate').val();
        var EVA_GROUP = $('#ddlEva').val();
        var action = $('#action').val();
        if (action == 1) {
            console.log('update');
            $.ajax({
                url: 'Services/EPService.asmx/UpdateEmployee',
                data: JSON.stringify({
                    EMP_ID: EMP_ID, NAME: NAME, WORKGROUP: WORKGROUP, ENTER_DATE: ENTER_DATE, EVA_GROUP: EVA_GROUP
                }),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    var rs = data.d;
                    if (rs > 0) {
                        alert(status);
                        table.ajax.reload(null, false);
                    }
                    $("#mdEmp").modal('hide');
                    $('#frmModify').find("input[type=text], textarea").val("");
                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error!" + xhr.status);
                },
            });
        }
        else {
        
            $.ajax({
                url: 'Services/EPService.asmx/InsertEmployee',
                data: JSON.stringify({
                    EMP_ID: EMP_ID, NAME: NAME, WORKGROUP: WORKGROUP, ENTER_DATE: ENTER_DATE, EVA_GROUP: EVA_GROUP
                }),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    var rs = data.d;
                    if (rs > 0) {
                        alert(status);
                        table.ajax.reload(null, false);
                    }
                    $("#mdEmp").modal('hide');
                    $('#frmModify').find("input[type=text], textarea").val("");
                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error!" + xhr.status);
                },
            });
        }
    });

    $('#btnDelete').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {
     
            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var EMP_ID = checkbox.attr('data-emp-id');
           
            var t = confirm('Are you sure you want to delete this employee?');
            if (t) {
                $.ajax({
                    url: 'Services/EPService.asmx/DeleteEmployee',
                    data: JSON.stringify({
                        EMP_ID: EMP_ID
                    }),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        var rs = data.d;
                        if (rs) {
                            alert(status);
                            table.ajax.reload(null, false);
                        }
                        return false;
                    },
                    error: function (xhr, status, error) {
                        alert("Error! " + xhr.status);
                    },
                });
            }
        }
        else {
            alert("Please select a row.");
        }
        return false;
    });
    $('#btnUnAssign').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {

            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var EMP_ID = checkbox.attr('data-emp-id');

            var t = confirm('Are you sure you want to unassign this employee?');
            if (t) {
                $.ajax({
                    url: 'Services/EPService.asmx/Unassign',
                    data: JSON.stringify({
                        EMP_ID: EMP_ID
                    }),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        var rs = data.d;
                        if (rs) {
                            alert(status);
                            table.ajax.reload(null, false);
                        }
                        return false;
                    },
                    error: function (xhr, status, error) {
                        alert("Error! " + xhr.status);
                    },
                });
            }
        }
        else {
            alert("Please select a row.");
        }
        return false;
    });
    $('#btnResetPass').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {

            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var EMP_ID = checkbox.attr('data-emp-id');

            var t = confirm('Are you sure you want to reset this employee password?');
            if (t) {
                $.ajax({
                    url: 'Services/EPService.asmx/ResetPass',
                    data: JSON.stringify({
                        EMP_ID: EMP_ID
                    }),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        var rs = data.d;
                        if (rs) {
                            alert("Reset "+status+" New password is 123456");
                            table.ajax.reload(null, false);
                        }
                        return false;
                    },
                    error: function (xhr, status, error) {
                        alert("Error! " + xhr.status);
                    },
                });
            }
        }
        else {
            alert("Please select a row.");
        }
        return false;
    });
    $('#btnApprover1,#btnApprover2').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {
            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var tr = checkbox.parents().parent();

            var EMP_ID = checkbox.attr('data-emp-id');
            $('#btnAssign').attr('data-empid', EMP_ID);
            $('#btnChange').attr('data-empid', EMP_ID);
            if ($(this).attr('id') == 'btnApprover1') {
                $('#btnChange').attr('data-assign', 1);
                $('#btnAssign').attr('data-assign', 1);
            }
            else {
                $('#btnChange').attr('data-assign', 2);
                $('#btnAssign').attr('data-assign', 2);
            }
            $('#mdUser').modal();
        }
        else {
            alert('Please select an employee');
        }
        return false;
    });
    $('#btnAssign').click(function () {
        if ($("#tbUser input:checkbox:checked").length > 0) {
            var checkbox = $("#tbUser tr").find("input[type='checkbox']:checked");
            var tr = checkbox.parents().parent();
            var m = $(this).attr('data-assign');
            var emp_id = $(this).attr('data-empid');
            var APPROVER = tr.children("td:nth-child(3)").text();
            $.ajax({
                url: 'Services/EPService.asmx/Assign',
                data: JSON.stringify({
                    EMP_ID: emp_id, APPROVER:APPROVER, ROLE: m
                }),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    var rs = data.d;
                    if (rs > 0) {
                        alert(status);
                        table.ajax.reload(null, false);
                        $('#mdUser').modal('hide');
                    }
                    if (rs == -1) {
                        alert('Error. Duplicate approver');
                    }
                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error! " + xhr.status);
                },
            });
        }
        else {
            alert('Please select an employee');
        }
        return false;
    });
    $('#btnChange').click(function () {
        if ($("#tbUser input:checkbox:checked").length > 0) {
            var checkbox = $("#tbUser tr").find("input[type='checkbox']:checked");
            var tr = checkbox.parents().parent();
            var m = $(this).attr('data-assign');
            var emp_id = $(this).attr('data-empid');
            var APPROVER = tr.children("td:nth-child(3)").text();
            $.ajax({
                url: 'Services/EPService.asmx/ChangeApprover',
                data: JSON.stringify({
                    EMP_ID: emp_id, APPROVER: APPROVER, ROLE: m
                }),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    var rs = data.d;
                    if (rs) {
                        alert(status);
                        table.ajax.reload(null, false);
                        $('#mdUser').modal('hide');
                    }
                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error! " + xhr.status);
                },
            });
        }
        else {
            alert('Please select an employee');
        }
        return false;
    });
    $('#btnUpload').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {
            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var tr = checkbox.parents().parent();
            var EMP_ID = tr.children("td:nth-child(3)").text();
            window.location.replace("UploadImage.aspx?emp_id=" + EMP_ID);
        }
        else {
            alert('Please select an employee');
        }
       
        return false;
    });
});