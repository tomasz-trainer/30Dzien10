
$(document).ready(function () {

    $("#btnSzukaj").on("click", function () {

        var wartoscZInputa = $('#txtSzukaj').val();

        $.ajax({
            method: "POST",
            url: "./services/Wyszukiwarka.aspx",
            data: { fraza: wartoscZInputa }
        })
            .done(function (msg) {
                // alert("Data Saved: " + msg);

                $('#main-panel > div.content > div > div:nth-child(1)').html(msg);
            });
    });


});
