function check() {
    $('#loader').show();
    $('#checkResult').html('');
    $.ajax({
        url: '/Home/GetCheckInfo',
        success: function (html) {
            $('#loader').hide();
            $('#checkResult').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
    
check();
setTimeout(check, checkTime);

$('#refreshBtn').click(check);