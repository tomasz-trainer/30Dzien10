


$(document).ready(function () {



    $("#btnAjaxZapisz").on("click", function (e) {
        e.preventDefault();

        var id = $('#glownyObszar_txtId').val();
        var imie = $('#glownyObszar_txtImie').val();
        var nazwisko = $('#glownyObszar_txtNazwisko').val();
        var kraj = $('#glownyObszar_txtKraj').val();
        var dataUr = $('#glownyObszar_txtDataUr').val();
        var wzrost = $('#glownyObszar_txtWzrost').val();
        var waga = $('#glownyObszar_txtWaga').val();

        $.ajax({
            method: "POST",
            url: "./services/ZapiszZawodnika.aspx",
            data: {
                id: id,
                imie: imie,
                nazwisko: nazwisko,
                kraj: kraj,
                dataUr: dataUr,
                wzrost: wzrost,
                waga: waga
            }
        })
            .done(function (msg) {
                if (msg.success == true) {
                    alert('Zapisano zmiany!');
                } else {
                    alert(msg.errorMessage);
                }
               
            });
    });


});