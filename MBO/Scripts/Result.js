$(document).ready(function () {
    $('#lrequest').addClass('active');
    var grp = getUrlParameter('group');
    if (grp) {
        $('#btnReview').show();
    }
    else {
        $('#btnReview').hide();
    }
    $('#tbMainDefault').tableHeadFixer({

        left: 3,
        'z-index': 50

    });
    $('#ckAll').change(function () {
        var checkboxes = $(this).closest('table').find(':checkbox');
        if ($(this).is(':checked')) {
            checkboxes.prop('checked', true);
        } else {
            checkboxes.prop('checked', false);
        }
    });
    $("#tbMainDefault tbody").on('change', '.ckb', function () {
        var group = ":checkbox[class='" + $(this).attr("class") + "']";
        if ($(this).is(':checked')) {
            $(group).not($(this)).attr("checked", false);

        }

    });

    disableButtons();
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
                    $("#btnSaveResult,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", true);
                else
                    $("#btnSaveResult,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", false);
                return false;
            },
            error: function (xhr, status, error) {
                alert("Error!" + xhr.status);
            },
        });
        return false;
    });

    $('#tbMainDefault tbody').on('click', '.tr-content', function () {
        if ($(this).hasClass('success')) {
            $(this).removeClass('success');
        }
        else {
            $(this).addClass('success');
        }
    });

    $('.ap').click(function () {
        var dataClick = $(this).attr('data-click');

        if (dataClick == 0) {
            $('.ap').attr('data-click', 1);
            $('.ap').css("backgroundColor", "#dff0d8");
        }
        else {
            $('.ap').attr('data-click', 0);
            $('.ap').css("backgroundColor", "#ffffff");
        }
    });


    $('#btnUnconfirm,#btnConfirm').click(function () {
        if ($("input:checkbox:checked").length > 0) {
            var action = 1;
            if (this.id == 'btnConfirm') {
                $('#btnSaveResult').click();
                action = 1;
            }
            else if (this.id == 'btnUnconfirm') {
                action = -1;
            }
            var CONFIRM_DATA = [];
            $('tr.cover').each(function (i, obj) {

                var checkbox = $(this).find("input[type='checkbox']:checked");

                if (checkbox.length > 0) {
                    var RESULT_ID = $(this).attr('id');
                    var CURR_ST = $(this).children('td').last().attr('data-status');
                    var ID = $('#btnConfirm').attr('data-empid');
                    var EMP_ID = $(this).attr('data-emid');
                    var DATA = { ID: RESULT_ID, APPROVER: ID, EMP_ID: EMP_ID, ACTION: action, CURR_ST: CURR_ST };
                    CONFIRM_DATA.push(DATA);
                }
            });
            if (CONFIRM_DATA.length > 0) {
                $.ajax({
                    url: 'Services/DefaultService.asmx/Confirm',
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

                            $('#alert').find('strong').remove();
                            $('#alert').append("<strong>" + status + "</strong>");
                            $('#alert').show();
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
        var time = $('#ddlPeriod').val();
        var group = $('#ddlEva').val();
        if (approve == "approve") {
            if (group.length > 0)
                window.location.replace("Result.aspx?action=2&eva=" + time + "&group=" + group);
            else
                window.location.replace("Result.aspx?action=2&eva=" + time);
        }
        else {
            window.location.replace("Result.aspx?eva=" + time);
        }
        return false;
    });
    function disableButtons() {
        if ($('#ddlPeriod').attr('data-status') == 1)
            $("#btnSaveResult,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", true);
        else
            $("#btnSaveResult,#btnAdd,#btnDel,#btnConfirm,#btnUnconfirm").prop("disabled", false);
    }
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
    var act = getUrlParameter('action');
    var group = getUrlParameter('group');
    if (paramId) {
        if (group)
            $('#ddlEva').val(group);
        if (act == 2) {
            $('#selMbo').val('approve');
        } else {
            $('#selMbo').val('register');
        }
        $('#ddlPeriod').val(paramId);

        $("#btnSave").prop("disabled", true);
    }
    $('.edit,.self_rate,.m1rate, .m2rate,.result').click(function (e) {
        var ID = $('#btnConfirm').attr('data-empid');

        if ($("input:checkbox:checked").length > 0) {
            e.stopPropagation();
            if ($(this).find('.thVal').length != 0) {
                return false;
            }
            var tdClass = $(this).attr('class');
            var element = this;
            var value = $(this).text();
            var value1 = $(this).html();
            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var parentTr = checkbox.parent().parent();
            var EMP_ID = parentTr.attr('data-emid');
            var ROLE = parentTr.attr('data-role');
            var STATUS = parentTr.children(":last").attr('data-status');
            if (tdClass == 'result') {
                if (ROLE == 0) {
                    updateVal1(this, value1);
                }
            }
            else if (tdClass == 'action' || tdClass == 'self_rate') {
                if (ROLE == 0) updateVal(this, value);
            }
            else {
                if (ROLE == 1) {
                    if (tdClass == 'm1rate')
                        updateVal(element, value);
                }
                if (ROLE == 2) {
                    if (tdClass == 'm2rate')
                        updateVal(element, value);
                }
            }

        }
        return false;
    });
    function getTotalScore(group, mbo, capacity) {
        if (group == 1 || group == 3) {
            var result = (mbo * 60) / 100 + (capacity * 40) / 100;
            var tail = result - parseInt(result);
            if (tail >= 0.5)
                return Math.ceil(result);
            return Math.floor(result);

        }
        return capacity;
    }
    function getGrade(group, score) {
        if (group == 1 || group == 3) {
            if (score >= 0 && score <= 64)
                return "D";
            if (score >= 65 && score <= 74)
                return "C";
            if (score >= 75 && score <= 84)
                return "B";
            if (score >= 85 && score <= 89)
                return "A";
            if (score >= 90 && score <= 100)
                return "S";
        }
        else {
            if (score >= 0 && score <= 59)
                return "D";
            if (score >= 60 && score <= 69)
                return "C";
            if (score >= 70 && score <= 79)
                return "B";
            if (score >= 80 && score <= 89)
                return "A";
            if (score >= 90 && score <= 100)
                return "S";
        }
    }
    function updateAll() {
        var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
        var numRow = parseInt(checkbox.parent('td').attr('rowspan'));
        var parentTr = checkbox.parent().parent();
        var id = parentTr.attr('id');
        // selfscore
        var rate = isNaN(parseInt(parentTr.find(".self_rate").text())) ? 0 : parseInt(parentTr.find(".self_rate").text());
        if (rate > 100) rate = 100;
        var weight = isNaN(parseInt(parentTr.find(".weight").text())) ? 0 : parseInt(parentTr.find(".weight").text());
        var selfScore = parseInt(weight * (rate / 100));
        //m1score
        var m1rate = isNaN(parseInt(parentTr.find(".m1rate").text())) ? 0 : parseInt(parentTr.find(".m1rate").text());
        if (m1rate > 100) m1rate = 100;
        var m1Score = parseInt(weight * (m1rate / 100));
        var m2rate = isNaN(parseInt(parentTr.find(".m2rate").text())) ? 0 : parseInt(parentTr.find(".m2rate").text());
        if (m2rate > 100) m2rate = 100;
        var m2Score = parseInt(weight * (m2rate / 100));
        for (var i = 1; i < numRow; i++) {
            var tr = parentTr.next();
            //selfscore
            var w = isNaN(parseInt(tr.find(".weight").text())) ? 0 : parseInt(tr.find(".weight").text());
            var r = isNaN(parseInt(tr.find(".self_rate").text())) ? 0 : parseInt(tr.find(".self_rate").text());
            var m1r = isNaN(parseInt(tr.find(".m1rate").text())) ? 0 : parseInt(tr.find(".m1rate").text());
            var m2r = isNaN(parseInt(tr.find(".m2rate").text())) ? 0 : parseInt(tr.find(".m2rate").text());
            if (m1r > 100) m1r = 100;
            if (m2r > 100) m2r = 100;
            if (r > 100) r = 100;
            selfScore += parseInt(w * (r / 100));

            m1Score += parseInt(w * (m1r / 100));
            m2Score += parseInt(w * (m2r / 100));
            parentTr = tr;
        }
        if (isNaN(selfScore)) selfScore = 0;
        if (isNaN(m1Score)) m1Score = 0;
        if (isNaN(m2Score)) m2Score = 0;
        $("#" + id).find('.self_score').text(selfScore);
        $("#" + id).find('.m1score').text(m1Score);
        $("#" + id).find('.m2score').text(m2Score);
        //$("#" + id).find('.mbo_final').text(m2Score);
        var sum1 = 0;
        $('.m1edit').each(function () {
            var t = isNaN(parseInt($(this).text())) ? 0 : parseInt($(this).text());
            sum1 += t;
        });
        var sum2 = 0;
        $('.m2edit').each(function () {
            var t = isNaN(parseInt($(this).text())) ? 0 : parseInt($(this).text());
            sum2 += t;
        });

        $('#sumM1').text(sum1);
        $('#sumM2').text(sum2);
        // $('#btnSaveResult').click();
        return false;
    }
    $('#tbElement').on('click', '.m1edit, .m2edit', function (e) {
        e.stopPropagation();
        var hrPart = $(this).closest('tr').children(":first").text();
        if (hrPart == 'Other certificate' || hrPart == 'Chứng chỉ khác') {
            return false;
        }
        var oriValue = parseInt($(this).parent().find('.weight').text());
        var role = $('#tbElement').attr('data-role');
        var value = $(this).text();

        if (role == 2 && $(this).attr('class') == 'm2edit')
            updateVal2(this, value);
        if (role == 1 && $(this).attr('class') == 'm1edit')
            updateVal2(this, value);

        return false;
    });
    function updateVal1(currentEle, value) {
        if ($(currentEle).find('.thVal').length <= 0) {
            $(currentEle).html('<textarea class="thVal" rows="5" style="width: 100%;">' + value.replace(/<br\s*[\/]?>/gi, "\n") + '</textarea>');
        }
        $(".thVal").focus();
        $('.thVal').blur(function () {
            var t = $(".thVal").val();
            if ($(".thVal").val() && $(".thVal").val().length > 0) {
                $(currentEle).html($(".thVal").val().replace(/\n/g, "<br>"));
            }
            else {
                $(currentEle).html("");
            }
            updateAll();
        });

        return false;
    }
    function updateVal(currentEle, value) {
        $(currentEle).html('<input class="thVal" type="text" value="' + value + '" size="4" />');
        $(".thVal").focus();
        $(".thVal").keyup(function (event) {
            if (event.keyCode == 13) {
                $(currentEle).html($(".thVal").val());
                updateAll();
            }
        });

        $(".thVal").unbind('focusout').focusout(function () { // you can use $('html')
            $(currentEle).html($(".thVal").val());
            updateAll();
        });
    }
    function updateVal2(currentEle, value) {
        $(currentEle).html('<input class="thVal" type="text" value="' + value + '" size="4" />');
        $(".thVal").focus();
        $(".thVal").keyup(function (event) {
            if (event.keyCode == 13) {
                $(currentEle).html($(".thVal").val());
                updateAll();
            }
        });

        $(".thVal").unbind('focusout').focusout(function () { // you can use $('html')
            $(currentEle).html($(".thVal").val());
            updateAll();
        });
    }
    //$("#tbElement").on('keydown', 'input', function (e) {
    //    var keyCode = e.keyCode || e.which;
    //    var idx = $(this).closest('td').index();
    //    var td = $(this).closest('tr').next().children().eq(idx);
    //    var value = td.text();
    //    var hrPart = $(td).closest('tr').children(":first").text();
    //    if (hrPart == 'Other certificate' || hrPart == 'Chứng chỉ khác') {
    //        return false;
    //    }
    //    if (keyCode == 9) {
    //        e.preventDefault();
    //        td.trigger("click");
    //    }
    //    return false;
    //});
    $('.cap1, .cap2').click(function () {
        if ($("input:checkbox:checked").length > 0) {
            var ROLE = $(this).parent().attr('data-role');
            var STATUS = $(this).siblings(":last").attr('data-status');
            var RESULT_ID = $(this).parent().attr('id');
            var GROUP_ID = $(this).parent().attr('data-group');
            var APPROVER = $('#btnConfirm').attr('data-empid');

            if (ROLE == 0) return false;
            if (parseInt(GROUP_ID) == 5) GROUP_ID = 3;
            $('#btnSaveScore').attr('data-id', RESULT_ID);
            $('#tbElement').attr('data-role', ROLE);

            if (STATUS > ROLE) {
                alert('This record has been confirmed');
                return false;
            }
            if (ROLE != 1 && $(this).attr('class') == 'cap1') return false;
            if (ROLE != 2 && $(this).attr('class') == 'cap2') return false;
            $.ajax({
                url: 'Services/ResultService.asmx/GetElementTable',
                data: JSON.stringify(
                   { RESULT_ID: RESULT_ID, GROUP_ID: GROUP_ID, APPROVER: APPROVER }
                ),
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                crossBrowser: true,
                success: function (data, status) {
                    var rs = data.d;
                    if (rs) {
                        var sumM1 = 0;
                        var sumM2 = 0;
                        var sum = 0;
                        $('#tbElement tr').not(':last').not(':first').remove();
                        for (var i = 0; i < rs.length; i++) {
                            sumM1 += rs[i].M1_Score;
                            sumM2 += rs[i].M2_Score;
                            sum += rs[i].Weight;
                            $('#tbElement tbody').append('<tr id="' + rs[i].Factor_ID + '"><td>' + rs[i].Element + '</td><td>' + rs[i].Factor + '</td><td class="weight">' + rs[i].Weight + '</td><td class="m1edit">' + rs[i].M1_Score + '</td><td class="m2edit">' + rs[i].M2_Score + '</td></tr>');
                        }
                        $('#sum').text(sum);
                        $('#sumM1').text(sumM1);
                        $('#sumM2').text(sumM2);
                    }
                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error! " + xhr.status);
                },
            });

            $('#mdElement').modal();
        }
        else {
            alert('Please select a row');
        }
    });

    $('#btnSaveScore').click(function () {
        if ($('#tbElement').find('.thVal').length > 0) {
            $(':focus').blur();
        }
        var resId = $(this).attr('data-id');
        var group = $('#' + resId).attr('data-group');
        var sum1 = $('#sumM1').text();
        var sum2 = $('#sumM2').text();

        if (sum1 > 100 || sum2 > 100) {
            alert('score > 100. cannot save'); return false;
        }
        var exit1 = 0;
        var exit2 = 0;
        if (sum1 > 0) {
            $('.m1edit').each(function () {
                var weight = parseInt($(this).prev().text());
                var score1 = isNaN($(this).text()) ? 0 : parseInt($(this).text());
                if (score1 > weight) {
                    exit1 = score1;

                }
            });
        }
        if (sum2 > 0) {
            $('.m2edit').each(function () {
                var weight = $(this).prev().prev().text();
                var score2 = isNaN($(this).text()) ? 0 : parseInt($(this).text());
                if (score2 > weight) {
                    exit2 = score2;

                }
            });
        }
        if (exit1 != 0) {

            alert('Score m1 ' + exit1 + ' is bigger than weight'); return false;
        }
        if (exit2 != 0) {
            alert('Score m2 ' + exit2 + ' is bigger than weight'); return false;
        }
        $('#' + resId).find('.cap1').text(sum1);
        $('#' + resId).find('.cap2').text(sum2);
        $('#' + resId).find('.cap_final').text(sum2);
        //var mbo_final = $('#' + resId).find('.mbo_final').text();
        var mbo_final = $('#' + resId).find('.m2score').text();
        var m1_final = $('#' + resId).find('.m1score').text();
        var total = getTotalScore(group, mbo_final, sum2);

        var m1_final_score = getTotalScore(group, m1_final, sum1);
        var grade = getGrade(group, total);
        var m1_grade = getGrade(group, m1_final_score);

        $('#' + resId).find('.total').text(total);
        $('#' + resId).find('.grade').text(grade);
        if ($('#' + resId).find('.final_grade').attr('data-value').length <= 0) {
            $('#' + resId).find('.final_grade').text(grade);
        }
        $('#' + resId).find('.m1_score').text(parseInt(m1_final_score));
        $('#' + resId).find('.m1_score').attr('data-grade', m1_grade);
        $('#' + resId).find('.m1_grade').text(m1_grade);

        var role = $('#tbElement').attr('data-role');
        var scores = [];
        $('#tbElement tbody tr').each(function () {
            var factor_id = $(this).attr('id');
            var m1 = isNaN(parseInt($(this).find('.m1edit').text())) ? 0 : parseInt($(this).find('.m1edit').text());
            var m2 = isNaN(parseInt($(this).find('.m1edit').text())) ? 0 : parseInt($(this).find('.m2edit').text());
            if (isNaN(m1)) m1 = 0;
            if (isNaN(m2)) m2 = 0;
            var score = { RESULT_ID: resId, FACTOR_ID: factor_id, M1_SCORE: m1, M2_SCORE: m2 };
            scores.push(score);
        });
        $.ajax({
            url: 'Services/ResultService.asmx/SaveScore',
            data: JSON.stringify(
               { SCORES: scores, ROLE: role }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                $('#btnSaveResult').click();
            },
            error: function (xhr, status, error) {
                alert("Error! " + xhr.status);
                console.log(error);
            },
        });
        $('#mdElement').modal('hide');
        return false;
    });

    $('#btnSaveResult').click(function () {
        $('.thVal').blur();
        //if ($('#tbMainDefault').find('.thVal').length > 0) {
        //    $(':focus').blur();
        //}
        if ($("input:checkbox:checked").length > 0) {
            var plans = [];
            var results = [];
            $("input:checked").each(function () {
                if ($(this).attr('class') == 'ckb') {
                    var numRow = $(this).parent('td').attr('rowspan');
                    var parentTr = $(this).parent().parent();
                    var STATUS = parentTr.children(":last").attr('data-status');
                    var role = parentTr.attr('data-role');
                    if (role == 0 && STATUS > 0) {
                        alert('Cannot save. This record has been confirmed');
                        return false;
                    }
                    if (role == 1 && STATUS > 1) {
                        alert('Cannot save. This record has been confirmed');
                        return false;
                    }
                    if (role == 2 && STATUS > 2) {
                        alert('Cannot save. This record has been confirmed');
                        return false;
                    }
                    var id = parseInt(parentTr.attr('id'));
                    var mbo_self_score = isNaN(parseFloat(parentTr.find('.self_score').text())) ? 0 : parseFloat(parentTr.find('.self_score').text());
                    var mbo_m1_score = isNaN(parseFloat(parentTr.find('.m1score').text())) ? 0 : parseFloat(parentTr.find('.m1score').text());
                    var mbo_m2_score = isNaN(parseFloat(parentTr.find('.m2score').text())) ? 0 : parseFloat(parentTr.find('.m2score').text());
                    var mbo_final_score = mbo_m2_score;
                    var cap_m1 = isNaN(parseFloat(parentTr.find('.cap1').text())) ? 0 : parseFloat(parentTr.find('.cap1').text());
                    var cap_m2 = isNaN(parseFloat(parentTr.find('.cap2').text())) ? 0 : parseFloat(parentTr.find('.cap2').text());
                    var cap_final = cap_m2;
                    var total = isNaN(parseFloat(parentTr.find('.total').text())) ? 0 : parseFloat(parentTr.find('.total').text());
                    var grade = parentTr.find('.grade').last().text();

                    var m1_final_score = parentTr.find('.m1_score').text().length == 0 ? 0 : parseFloat(parentTr.find('.m1_score').text());
                    //var m1_grade = parentTr.find('.m1_grade').text();
                    var m1_grade = parentTr.find('.m1_score').attr('data-grade');
                    var result = {
                        ID: id, MBO_SELF_SCORE: mbo_self_score, MBO_M1_SCORE: mbo_m1_score, MBO_M2_SCORE: mbo_m2_score, MBO_FINAL_SCORE: mbo_final_score,
                        CAP_M1: cap_m1, CAP_M2: cap_m2, CAP_FINAL_SCORE: cap_final, TOTAL_SCORE: total, GRADE: grade,
                        M1_FINAL_SCORE: m1_final_score, M1_GRADE: m1_grade
                    };

                    if (role == 0 && STATUS == 0)
                        results.push(result);
                    if (role == 1)
                        results.push(result);
                    if (role == 2)
                        results.push(result);

                   
                    var plan_id = isNaN(parentTr.find('.pid').attr('data-id')) ? 0 : parseInt(parentTr.find('.pid').attr('data-id'));
                    if (isNaN(plan_id)) plan_id = 0;
                    var action = '';
                    var result1 = parentTr.find('.result').html();
                    var mbo_self_rate = isNaN(parentTr.find('.self_rate').text()) ? 0 : parseInt(parentTr.find('.self_rate').text());
                    var mbo_m1_rate = isNaN(parentTr.find('.m1rate').text()) ? 0 : parseInt(parentTr.find('.m1rate').text());
                    var mbo_m2_rate = isNaN(parentTr.find('.m2rate').text()) ? 0 : parseInt(parentTr.find('.m2rate').text());
                    if (isNaN(mbo_self_rate)) mbo_self_rate = 0;
                    if (isNaN(mbo_m1_rate)) mbo_m1_rate = 0;
                    if (isNaN(mbo_m2_rate)) mbo_m2_rate = 0;
                   
                    var plan = { RESULT_ID: id, ID: plan_id, ACTION: action, RESULT: result1, MBO_SELF_RATE: mbo_self_rate, MBO_M1_RATE: mbo_m1_rate, MBO_M2_RATE: mbo_m2_rate };

                    if (role == 0 && STATUS == 0)
                        plans.push(plan);
                    if (role == 1)
                        plans.push(plan);
                    if (role == 2)
                        plans.push(plan);

                    for (var i = 1; i < numRow; i++) {
                        var tr = parentTr.next();
                        var plan_id = isNaN(tr.find('.pid').attr('data-id')) ? 0 : parseInt(tr.find('.pid').attr('data-id'));
                        if (isNaN(plan_id)) plan_id = 0;
                        action = '';
                        result2 = tr.find('.result').html();
                        mbo_self_rate = isNaN(tr.find('.self_rate').text()) ? 0 : parseInt(tr.find('.self_rate').text());
                        mbo_m1_rate = isNaN(tr.find('.m1rate').text()) ? 0 : parseInt(tr.find('.m1rate').text());
                        mbo_m2_rate = isNaN(tr.find('.m2rate').text()) ? 0 : parseInt(tr.find('.m2rate').text());

                        if (isNaN(mbo_self_rate)) mbo_self_rate = 0;
                        if (isNaN(mbo_m1_rate)) mbo_m1_rate = 0;
                        if (isNaN(mbo_m2_rate)) mbo_m2_rate = 0;
                        console.log([mbo_m1_rate, mbo_m2_rate]);
                        var plan1 = {
                            ID: plan_id, ACTION: action, RESULT: result2, MBO_SELF_RATE: mbo_self_rate, MBO_M1_RATE: mbo_m1_rate,
                            MBO_M2_RATE: mbo_m2_rate, RESULT_ID: id
                        };
                        if (role == 0 && STATUS == 0)
                            plans.push(plan1);
                        if (role == 1)
                            plans.push(plan1);
                        if (role == 2)
                            plans.push(plan1);
                        parentTr = tr;
                    }
                    console.log([results, plans, APPROVER]);
                    var APPROVER = $('#btnConfirm').attr('data-empid');
                    $.ajax({
                        url: 'Services/ResultService.asmx/UpdateResult',
                        data: JSON.stringify(
                           { RESULTS: results, PLANS: plans, APPROVER: APPROVER }
                        ),
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        crossBrowser: true,
                        success: function (data, status) {
                            var rs = data.d;
                            console.log(rs);
                            $('#alert').find('strong').remove();
                            $('#alert').append("<strong>Save " + status + "</strong>");
                            $('#alert').show();
                            return false;
                        },
                        error: function (xhr, status, error) {
                            alert("Error! " + xhr.status);
                            console.log(error);
                        },
                    });
                }
            });
        }
        else {
            alert('Please select a row');
        }
        return false;
    });

    $('.cont').click(function () {
        $("#plan_detail").html('');
        var id = parseInt($(this).attr('data-id'));
        $.ajax({
            url: 'Services/ResultService.asmx/GetActionPlanById',
            data: JSON.stringify(
               { ID: id }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                for (var i = 0; i < rs.length; i++) {
                    $("#plan_detail").append("<span>" + rs[i].ACTION_PLAN + "</span>");
                }
                $('#mdShowActionPlan').modal();
                return false;
            },
            error: function (xhr, status, error) {
                alert("Error! " + xhr.status);
            },
        });
        return false;
    });
    $('#btnReview').click(function () {
        $('#tbReview > tbody').empty();
        var emp_id = $('#btnConfirm').attr('data-empid');
        var period_id = $('#ddlPeriod').val();
        var group = $('#ddlEva').val();
        $.ajax({
            url: 'Services/ResultService.asmx/GetResultReview',
            data: JSON.stringify(
               { EMP_ID: emp_id, GROUP: group, PERIOD_ID: period_id }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                if (rs) {

                    for (var i = 0; i < rs.length; i++) {

                        $('#tbReview tbody').append('<tr><td>' + (i + 1) + '</td><td>' + rs[i].NAME + '</td><td>' + rs[i].GROUP + '</td><td>' + rs[i].SCORE + '</td><td>' + rs[i].GRADE + '</td></tr>');
                    }

                }
                return false;
            },
            error: function (xhr, status, error) {
                alert("Error! " + xhr.status);
            },
        });

        $('#mdReview').modal();
        return false;
    });
    $('#btnExportReview').click(function () {
        fnExcelReport("tbReview");
        return false;
    });

    function fnExcelReport(id) {
        var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        tab = document.getElementById(id); // id of table

        for (j = 0 ; j < tab.rows.length ; j++) {
            tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            //tab_text=tab_text+"</tr>";
        }

        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            txtArea1.document.open("txt/html", "replace");
            txtArea1.document.write(tab_text);
            txtArea1.document.close();
            txtArea1.focus();
            sa = txtArea1.document.execCommand("SaveAs", true, "Export.xls");
        }
        else                 //other browser not tested on IE 11
            sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

        return (sa);
    }
});