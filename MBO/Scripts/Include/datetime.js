$(document).ready(function () {
    var nowTemp = new Date();
    var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);
    $('#txtFrom')
       .datepicker({
           format: 'yyyy-mm-dd'
       }).on('changeDate', function (ev) {
           $(this).datepicker('hide');
       });
    $('#txtTo')
      .datepicker({
          format: 'yyyy-mm-dd'
      }).on('changeDate', function (ev) {
          $(this).datepicker('hide');
         
      });
    $('#txtYear')
          .datepicker({
              format: 'yyyy-mm-dd',
              viewMode: 'years'
          }).on('changeDate', function (ev) {
              $(this).datepicker('hide');
              var date = new Date($(this).val());
              $(this).val(date.getFullYear());
          });
    $('#txtDate')
         .datepicker({
             format: 'yyyy-mm-dd'
         }).on('changeDate', function (ev) {
             $(this).datepicker('hide');
         });
});
