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
    public partial class TabelaZawodnikowGlowny : System.Web.UI.Page
    {
         public int? IdPodswietlanego { get; set; }
        public Zawodnik[] Zawodnicy { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
            // usuwanie
            string idUsuwanegoStr = Request["idUsuwanego"];
            if (!string.IsNullOrEmpty(idUsuwanegoStr))
            {
                int idUsuwanego = Convert.ToInt32(idUsuwanegoStr);
                mz.Usun(new Zawodnik() { Id_zawodnika = idUsuwanego});
            }
           
            Zawodnicy = mz.WczytajZawodnikow();

            //podswietlenie edytowanego zawodnika
            string idPodswietlonegoStr = Request["podswietlonyId"];
            if (!string.IsNullOrEmpty(idPodswietlonegoStr))
            {
                IdPodswietlanego = Convert.ToInt32(idPodswietlonegoStr);
            }
        }
    }
}