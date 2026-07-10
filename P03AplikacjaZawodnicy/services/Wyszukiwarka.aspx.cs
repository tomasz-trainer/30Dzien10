using P06Zawodnicy.Shared.Domain;
using P06Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P03AplikacjaZawodnicy.services
{
    public partial class Wyszukiwarka : System.Web.UI.Page
    {
        public Zawodnik[] Zawodnicy { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            string szukanaFraza = Request["fraza"];

            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();

            if (!string.IsNullOrEmpty(szukanaFraza))
                Zawodnicy = mz.PodajZawodnikowFiltr(szukanaFraza);
            else
                Zawodnicy = mz.WczytajZawodnikow();

        }
    }
}