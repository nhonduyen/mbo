$(document).ready(function () {
    $('#lreg').addClass('active');
    $('#tbMainDefault').tableHeadFixer({

        left: 3,
        'z-index': 50

    });
    //$('#tbMainDefault').tableHeadFixer({});
    function updateVal2(currentEle, value) {
        if ($(currentEle).find('.thVal').length <= 0) {
            $(currentEle).html('<select class="thVal" ><option value="Low">Low</option><option value="Medium">Medium</option><option value="High">High</option></select>');
            $(".thVal").val($.trim(value));
          
        }
        $(".thVal").focus();
        $(".thVal").change(function () {
            var t = $(".thVal").val();
            if (t != "") $(currentEle).html($(".thVal").val());
            else $(currentEle).html("");
        });
        $(".thVal").unbind('focusout').focusout(function () {
            var t = $(".thVal").val();
            if (t != "") $(currentEle).html($(".thVal").val());
            else $(currentEle).html("");
        });
    }
    function updateVal1(currentEle, value) {
        if ($(currentEle).find('.thVal').length <= 0) {
            $(currentEle).html('<textarea class="thVal" rows="5" style="width: 100%;">' + value.replace(/<br\s*[\/]?>/gi, "\n") + '</textarea>');
        }
        $(".thVal").focus();

        $(".thVal").unbind('focusout').focusout(function () {
            var t = $(".thVal").val();
            if (t) $(currentEle).html($(".thVal").val().replace(/\n/g, "<br>"));
            else $(currentEle).html("");
        });
    }
   
    $('#ckAll').change(function () {
        var checkboxes = $(this).closest('table').find(':checkbox');
        if ($(this).is(':checked')) {
            checkboxes.prop('checked', true);
        } else {
            checkboxes.prop('checked', false);
        }
    });

    $('#btnAdd').click(function () {
        $('.ap').css("backgroundColor", "#d9edf7");
        $('.ap').first().removeClass('edit');
        $('.ap').first().attr('data-click', 0);
        $('.tr-content').removeClass('success');
        var rp = parseInt( $('.rp').attr('rowspan'));
        $('.rp').attr('rowspan', rp + 1);
        $('#tbMainDefault tbody').append('<tr class="tr-content success" data-edit="new"><td data-id="0"></td><td></td><td></td><td></td><td class="lvl"></td></tr>');
        return false;
    });

    $('#tbMainDefault tbody').on('click', '.tr-content td', function () {
        $('.ap').css("backgroundColor", "#d9edf7");
        $('.ap').first().attr('data-click', 0);
        $('.ap').first().removeClass('edit');
        $('.tr-content').removeClass('success').addClass('info');
        var CURR_ST = parseInt($(this).parent().attr('data-status'));
        if (CURR_ST > 0) return false;
        if ($(this).hasClass('lvl'))
            updateVal2(this, $(this).html());
        else
            updateVal1(this, $(this).html());
       
        if ($(this).parent().hasClass('success')) {
            $(this).parent().removeClass('success').addClass('info');
            $(this).parent().children('td').eq(0).removeClass('edit');
        }
        else {
            $(this).parent().removeClass('info').addClass('success');
            $(this).parent().children('td').eq(0).addClass('edit');
        }
        return false;
    });

    $('#tbMainDefault tbody').on("click", ".ap", function () {
       
        $('.tr-content').removeClass('success');
        $('.tr-content').addClass('info');
        var CURR_ST = parseInt($(this).siblings(":last").attr('data-status'));
        if (CURR_ST > 0) return false;
        if ($(this).hasClass('lvl'))
            updateVal2(this, $(this).html());
        else
            updateVal1(this, $(this).html());
        var dataClick = $(this).attr('data-click');
      
        if (dataClick == 0 && !$(this).hasClass('edit') ) {
            $('.ap').first().addClass('edit');
            $('.ap').first().attr('data-click', 1);
            $('.ap').css("backgroundColor", "#dff0d8");
        }
       
        return false;
    });



    $('#btnDel').click(function () {
        var t = confirm('Are you sure you want to delete these rows?');
        if (t) {
            var ids = [];
            var dataClick = $('.ap').first().attr('data-click');

            if (dataClick == 1) {
                var id = $('.ap').eq(0).attr('data-id');
                if (id != 0 && ids.indexOf(id) < 0)
                    ids.push(id);
                $('.ap').text('');
                $('.ap').css("backgroundColor", "#ffffff");
            }
            $('.tr-content').each(function (i, obj) {
                if ($(this).hasClass('success')) {
                    var pid = $(this).children("td:nth-child(1)").attr("data-id");
                    if (pid != 0 && ids.indexOf(pid) < 0) ids.push(pid);
                    $(this).remove();
                }
            });
            $('#btnDel').attr('data-delete', ids.join());
            if ($('.ap').eq(0).text().length == 0 && $('.ap').eq(1).text().length == 0 && $('.tr-content').length > 0) {
                $('.ap').eq(0).text($('.tr-content').eq(0).children("td:nth-child(1)").text());
                $('.ap').eq(0).attr('data-id', $('.tr-content').eq(0).children("td:nth-child(1)").attr('data-id'));
                $('.ap').eq(1).text($('.tr-content').eq(0).children("td:nth-child(2)").text());
                $('.ap').eq(2).text($('.tr-content').eq(0).children("td:nth-child(3)").text());
                $('.ap').eq(3).text($('.tr-content').eq(0).children("td:nth-child(4)").text());
                $('.ap').eq(4).text($('.tr-content').eq(0).children("td:nth-child(5)").text());
                $('.tr-content').each(function (i, obj) {
                    $(this).children("td:nth-child(1)").attr('data-id', $(this).next('tr').children("td:nth-child(1)").attr('data-id'));
                    $(this).children("td:nth-child(1)").text($(this).next('tr').children("td:nth-child(1)").text());
                    $(this).children("td:nth-child(2)").text($(this).next('tr').children("td:nth-child(2)").text());
                    $(this).children("td:nth-child(3)").text($(this).next('tr').children("td:nth-child(3)").text());
                    $(this).children("td:nth-child(4)").text($(this).next('tr').children("td:nth-child(4)").text());
                    $(this).children("td:nth-child(5)").text($(this).next('tr').children("td:nth-child(5)").text());
                });
                $('.tr-content').last().remove();
            }
            $('#btnSave').click();
        }
        return false;
    });

    $('#btnSave').click(function () {
        $(':focus').blur();
        var del = $('#btnDel').attr('data-delete');
        var checkPercentage = parseInt($('.ap').eq(3).text());
        $('.tr-content').each(function (i, obj) {
            var percent = parseInt($(this).children("td:nth-child(4)").text());
            checkPercentage += percent;
        });
        if (checkPercentage <= 100 || del.length > 0) {
            var plans = [];
            var editPlans = [];
            var ID = parseInt($('.cover').attr('data-id'));
            var EMP_ID = $('.cover').eq(0).attr('data-emid');
            var EVA_TIME = $('#ddlPeriod').val();

            var plan = {
                ID: isNaN(parseInt($('.ap').eq(0).attr('data-id'))) ? 0 : parseInt($('.ap').eq(0).attr('data-id')), CONT: $('.ap').eq(0).html(),
                ACTION_PLAN: $('.ap').eq(1).html(), TARGET: $('.ap').eq(2).html(),
                WEIGHT: isNaN($('.ap').eq(3).text()) ? 0 : parseInt($('.ap').eq(3).text()), LVL: $('.ap').eq(4).text()
            };
            if (del.indexOf(plan.ID) < 0) {
                if (plan.ID == 0) plans.push(plan);
                else {
                    editPlans.push(plan);
                }
            }
            $('.tr-content').each(function (i, obj) {
                var p1 = {
                    ID: isNaN(parseInt($(this).children("td:nth-child(1)").attr('data-id'))) ? 0 : parseInt($(this).children("td:nth-child(1)").attr('data-id')),
                    CONT: $(this).children("td:nth-child(1)").html(), ACTION_PLAN: $(this).children("td:nth-child(2)").html(),
                    TARGET: $(this).children("td:nth-child(3)").html(), WEIGHT: isNaN($(this).children("td:nth-child(4)").text()) ? 0 : parseInt($(this).children("td:nth-child(4)").text()),
                    LVL: $(this).children("td:nth-child(5)").text()
                };
                if (del.indexOf(p1.ID) < 0) {
                    if (p1.ID == 0) {
                        plans.push(p1);
                    }
                    else {
                        editPlans.push(p1);
                    }
                }
            });

            $.ajax({
                url: 'Services/DefaultService.asmx/SavePlans',
                data: JSON.stringify(
                   { EVA_TIME: EVA_TIME, EMP_ID: EMP_ID, RESULT_ID: ID, PLANS: plans, DEL: del, EDIT_PLANS: editPlans }
                ),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    var rs = data.d;
                    alert('Save ' + status);

                    location.reload();
                },
                error: function (xhr, status, error) {
                    alert("Error! " + xhr.status);
                },
            });
        }
        else {
            alert('Total weight must be 100%');
        }
        return false;
    });
    function disableButtons() {
        $("#btnSave,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", true);
        return false;
    }
    function enableButtons() {
        $("#btnSave,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", false);
        return false;
    }
    if ($('#ddlPeriod').attr('data-status') == 1) disableButtons(); else enableButtons();

    $('#ddlPeriod').change(function () {
        var EVA_TIME = $(this).val();
        $.ajax({
            url: 'Services/DefaultService.asmx/GetPeriod',
            data: JSON.stringify(
               { EVA_TIME: EVA_TIME }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                $('#ddlPeriod').attr('data-status', rs.STATUS);

                if (rs.STATUS == 1)
                    $("#btnSave,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", true);
                else
                    $("#btnSave,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", false);
                return false;
            },
            error: function (xhr, status, error) {
                alert("Error!" + xhr.status);
            },
        });
        return false;
    });
  

    $('#btnUnconfirm,#btnConfirm').click(function () {
        if ($("input:checkbox:checked").length > 0) {
            var action = 1;
            if (this.id == 'btnConfirm') {
                if ($("input:checkbox:checked").length == 1) {
                    var emp_id = $.trim($('#btnConfirm').attr('data-empid'));
                    var this_emp = $.trim($("input:checkbox:checked").closest('tr').attr('data-emid'));
                    if (emp_id === this_emp) {
                        var count = 0;
                        $('.weight').each(function () {
                            if ($.trim($(this).closest('tr').attr('data-emid')) == emp_id) {
                                var weight = $(this).text();
                                if (isNaN(weight)) {
                                    weight = 0;
                                }
                                else {
                                    weight = parseInt(weight);
                                }
                                count += weight;
                            }
                        });
                        if (count < 100) {
                            alert('Cannot confirm. Total weight < 100');
                            return false;
                        }
                    }
                }
                action = 1;
            }
            else if (this.id == 'btnUnconfirm') {
                action = -1;
            }
            var CONFIRM_DATA = [];
            $('tr.cover').each(function (i, obj) {

                var checkbox = $(this).find("input[type='checkbox']:checked");

                if (checkbox.length > 0) {
                    var RESULT_ID = parseInt($(this).attr('data-id'));
                    var CURR_ST = $(this).children('td').last().attr('data-status');
                    var ID = $('#btnConfirm').attr('data-empid');
                    var EMP_ID = $(this).attr('data-emid');
                    var DATA = { ID: RESULT_ID, APPROVER: ID, EMP_ID: EMP_ID, ACTION: action, CURR_ST: CURR_ST };
                    CONFIRM_DATA.push(DATA);
                }
            });
            if (CONFIRM_DATA.length > 0) {
                $.ajax({
                    url: 'Services/DefaultService.asmx/ConfirmPlan',
                    data: JSON.stringify(
                        { CONFIRM_DATA: CONFIRM_DATA }
                    ),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        var rs = data.d;
                        if (rs > 0) {
                            alert(status);
                            location.reload();
                        }

                    },
                    error: function (xhr, status, error) {
                        alert("Error! " + xhr.status);
                    },
                });
            }

        }
        else {
            alert('Please select an employee');
        }
        return false;
    });
    $('#btnSearch').click(function () {
        var approve = $('#selMbo').val();
        if (approve == "approve") {
            var role = $('#btnConfirm').attr('data-role');
            if (role == 0)
                location.reload();
            var time = $('#ddlPeriod').val();
            var emp_id = $('#btnConfirm').attr('data-empid');

            var url = window.location;

            window.location.replace("Default.aspx?action=2&eva=" + time +  "&act=" + approve+"&id=" + emp_id );
        }
        else {
            var url = window.location;
            var time = $('#ddlPeriod').val();
            window.location.replace("Default.aspx?eva=" + time + "&act=" + approve);
        }
        return false;
    });
    function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };
    var paramId = getUrlParameter('eva');
    if (paramId) {
        var action = getUrlParameter('act');
        $('#selMbo').val(action);
        $('#ddlPeriod').val(paramId);
        var flag = (action == "approve") ? true : false;
        $("#btnAdd").prop("disabled", flag);
        $("#btnDel").prop("disabled", flag);
        $("#btnSave").prop("disabled", flag);
    }


});

