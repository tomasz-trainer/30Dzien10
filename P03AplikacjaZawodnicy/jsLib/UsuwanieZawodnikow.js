

$(document).ready(function () {


    $(".btn-danger").on("click", function () {

        var idUsuwanego = $(this).data("id");


        $.ajax({
            method: "POST",
            url: "./services/UsunZawodnika.aspx",
            data: { idUsuwanego: idUsuwanego }
        })
            .done(function (msg) {

                if (msg.success == true) {
                    $("button[data-id='" + idUsuwanego + "']").closest("tr").hide(1500, function () {
                        $(this).remove();
                    });
                } else {
                    alert(msg.errorMessage);
                }      

            });

    });

});