
$(document).ready(function () {
    //wyszukaj();

    $("#txtSzukaj").on("keypress", function (e) {

        if (e.which == 13) {
            e.preventDefault();
            wyszukaj();
        }

    });

    $("#btnSzukaj").on("click", function () {
        wyszukaj();
    });


    function wyszukaj() {
        var wartoscZInputa = $('#txtSzukaj').val();

        var obrazek = $("#dvLadowanieContainer").html();
        $('#main-panel > div.content > div > div:nth-child(1)').html(obrazek);

        $.ajax({
            method: "POST",
            url: "./services/Wyszukiwarka.aspx",
            data: { fraza: wartoscZInputa }
        })
            .done(function (msg) {
                // alert("Data Saved: " + msg);

                $('#main-panel > div.content > div > div:nth-child(1)').html(msg);
            });
    }


});
