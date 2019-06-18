$(document).ready(function () {
    $('#linfo').addClass('active');
    var table = $('#tbMainDefault').DataTable({
        responsive: true,
        sort: false,
        "processing": true,
        "serverSide": true,
        "searching": false,
        //"iDisplayLength": 25,
        ajax: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Paging/InfoPage.aspx/Data",
            data: function (d) {
                return JSON.stringify({
                    parameters: d, PERIOD_ID: $('#ddlPeriod').val(), GROUP: $('#ddlEva').val(), QUERY: $('#txtQuery').val()
                });
            }
        }
        //
    });
    $('#btnSearch').click(function () {
        table.draw();
    });
    $('#tbMainDefault tbody').on('change', 'input[type="text"]', function () {
        var score = $(this).val() ? parseInt($(this).val()) : 0;
        var SCORE = {
            RESULT_ID: $(this).attr('data-id'),
            FACTOR_ID: $(this).attr('data-fid'),
            M1_SCORE: score,
            M2_SCORE: score
        };

        $.ajax({
            url: 'Services/InfoService.asmx/SaveScore',
            data: JSON.stringify(
               { SCORE: SCORE, ROLE: 2 }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                if (rs > 0)
                    alert(status);
            },
            error: function (xhr, status, error) {
                alert("Error! " + xhr.status);
            },
        });

        return false;
    });
    $('#tbMainDefault tbody').on('change', 'input[type="checkbox"]', function () {
        var ID = $(this).attr('data-id');
        var FINAL_GRADE = $(this).attr('data-value');
        var th = $(this).closest('table').find('th').eq($(this).parent().index());
        var REASON = th.text();
        if ($(this).is(":checked")) {
            // it is checked

        }
        else {
            REASON = "";
            FINAL_GRADE = "";
        }
        $.ajax({
            url: 'Services/InfoService.asmx/SaveFinal',
            data: JSON.stringify(
               { ID: ID, FINAL_GRADE: FINAL_GRADE, REASON: REASON }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                if (rs > 0) {
                    alert(status);
                    table.ajax.reload();
                }
            },
            error: function (xhr, status, error) {
                alert("Error! " + xhr.status);
            },
        });
        return false;
    });
    $('#tbMainDefault tbody').on('change', 'textarea', function () {
        $.ajax({
            url: 'Services/InfoService.asmx/SaveRemark',
            data: JSON.stringify(
               { ID: $(this).attr('data-id'), REMARK: $(this).val() }
            ),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            crossBrowser: true,
            success: function (data, status) {
                var rs = data.d;
                if (rs > 0)
                    alert(status);
            },
            error: function (xhr, status, error) {
                alert("Error! " + xhr.status);
            },
        });
        return false;
    });
});