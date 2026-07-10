using P06Zawodnicy.Shared.Domain;
using P06Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P03AplikacjaZawodnicy.services
{
    public partial class ZapiszZawodnika : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idZawodnikaStr = Request["id"];
             if (!string.IsNullOrEmpty(idZawodnikaStr))
            {
                Zawodnik z = new Zawodnik();
                string errorMessage = string.Empty;
                try
                {                
                    z.Id_zawodnika = Convert.ToInt32(idZawodnikaStr);
                    z.Imie = Request["imie"];
                    z.Nazwisko = Request["nazwisko"];
                    z.Kraj = Request["kraj"];
                    z.DataUrodzenia = Convert.ToDateTime(Request["dataUr"]);
                    z.Wzrost = Convert.ToInt32(Request["wzrost"]);
                    z.Waga = Convert.ToInt32(Request["waga"]);


                    IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
                    mz.Edytuj(z);
                }
                catch (Exception)
                {
                    errorMessage = "Nie udało się zapisać zawodnika";
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(new
                {
                    data = z,
                    success = errorMessage == string.Empty ? true : false,
                    errorMessage
                });
            }

           

        }
    }
}