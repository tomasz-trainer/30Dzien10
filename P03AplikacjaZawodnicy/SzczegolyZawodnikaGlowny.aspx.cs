using P06Zawodnicy.Shared.Domain;
using P06Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P03AplikacjaZawodnicy
{
    public partial class SzczegolyZawodnikaGlowny : System.Web.UI.Page
    {
        enum TrybOperacji
        {
            Tworzenie,
            Edycja
        }

        private TrybOperacji trybOperacji => string.IsNullOrEmpty(Request["id"]) ? TrybOperacji.Tworzenie : TrybOperacji.Edycja;


        protected void Page_Load(object sender, EventArgs e)
        {
            string idStr = Request["id"];


            //IsPostback - czy strona jest ładowana po raz pierwszy, czy w wyniku postbacku (np. kliknięcia przycisku)
            if (!string.IsNullOrEmpty(idStr) && !Page.IsPostBack)
            {
                int id = Convert.ToInt32(idStr);
                IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
                var zawodnik = mz.PodajZawodnika(id);

                txtId.Text = Convert.ToString(zawodnik.Id_zawodnika);
                txtImie.Text = zawodnik.Imie;
                txtNazwisko.Text = zawodnik.Nazwisko;
                txtKraj.Text = zawodnik.Kraj;
                txtWaga.Text = zawodnik.Waga.ToString();
                txtWzrost.Text = Convert.ToString(zawodnik.Wzrost);
                txtDataUr.Text = zawodnik.DataUrodzenia.ToString("dd-MM-yyyy");

            }

        }

        protected void btnZapisz_Click(object sender, EventArgs e)
        {
            Zawodnik zawodnik = new Zawodnik();
           
            zawodnik.Imie = txtImie.Text;
            zawodnik.Nazwisko = txtNazwisko.Text;
            zawodnik.Kraj = txtKraj.Text;
            zawodnik.DataUrodzenia = Convert.ToDateTime(txtDataUr.Text);
            zawodnik.Waga = Convert.ToInt32(txtWaga.Text);
            zawodnik.Wzrost = Convert.ToInt32(txtWzrost.Text);

            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();

            if (trybOperacji == TrybOperacji.Tworzenie)
            {
                mz.Dodaj(zawodnik);
            }
            else if (trybOperacji == TrybOperacji.Edycja)
            {
                zawodnik.Id_zawodnika = Convert.ToInt32(txtId.Text);
                mz.Edytuj(zawodnik);
            }

            Response.Redirect($"TabelaZawodnikowGlowny.aspx?podswietlonyId={zawodnik.Id_zawodnika}");

        }
    }
}