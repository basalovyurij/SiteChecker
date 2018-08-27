function check() {
    $('#loader').show();
    $.ajax({
        url: '/Home/GetCheckInfo',
        success: function (html) {
            $('#loader').hide();
            $('#checkResult').html(html);
        }
    });
}
    
check();
setTimeout(check, checkTime);

$('#refreshBtn').click(check);