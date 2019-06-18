$(document).ready(function () {
    $('#rule').addClass('active');
    var tbPreiod = $('#tbMainDefault').DataTable();
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
    
    $('#btnNew').click(function () {
        $("#mdPeriod").modal({
            backdrop: 'static',
            keyboard: false
        });
        return false;
    });

    $('#frmRule').submit(function (evt) {
        evt.preventDefault();
        var nums = $('#txtNum').val();
        var s = $('#txtS').val();
        var a = $('#txtA').val();
        var bcd = $('#txtB').val();
      
        if ($('#action').val() != 1) {
            $.ajax({
                url: 'Services/RuleService.asmx/Insert',
                data: JSON.stringify(
                   { NUM_EMPS: nums, S: s, A: a, BCD: bcd }
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
                        location.reload();
                    }

                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error!" + xhr.status);
                },
            });
        }
        else {
            $.ajax({
                url: 'Services/RuleService.asmx/Update',
                data: JSON.stringify(
                   { NUM_EMPS: nums, S: s, A: a, BCD: bcd }
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
                        location.reload();
                    }

                    return false;
                },
                error: function (xhr, status, error) {
                    alert("Error!" + xhr.status);
                },
            });
        }
        return false;
    });
   
    
    $('#btnEdit').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {
            $('#frmRule')[0].reset();
            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var tr = checkbox.parents().parent();

            var NUM_EMPS = checkbox.attr('data-num');

            var S = $.trim(tr.children("td:nth-child(3)").text());
            var A = $.trim(tr.children("td:nth-child(4)").text());
            var BCD = $.trim(tr.children("td:nth-child(5)").text());
           
            $('#action').val(1);
            $('#txtNum').val(NUM_EMPS);
            $('#txtS').val(S);
            $('#txtA').val(A);
            $('#txtB').val(BCD);
         
            $('#mdPeriod').modal();
        }
        else {
            alert('Please select a row.');
        }
        return false;
    });

   
    $('#btnDelete').click(function () {
        if ($("#tbMainDefault input:checkbox:checked").length > 0) {

            var checkbox = $("#tbMainDefault tr").find("input[type='checkbox']:checked");
            var NUM_EMPS = checkbox.attr('data-num');
            var t = confirm('Are you sure you want to delete this rule?');
            if (t) {
                $.ajax({
                    url: 'Services/RuleService.asmx/Delete',
                    data: JSON.stringify({
                        NUM_EMPS: NUM_EMPS
                    }),
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    crossBrowser: true,
                    success: function (data, status) {
                        var rs = data.d;
                        if (rs) {
                            alert(status);
                            location.reload();
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

    
   
});