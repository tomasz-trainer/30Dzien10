using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Domain
{
    public class Zawodnik : Osoba
    {
        public int Id_zawodnika { get; set; }
        public int? Id_trenera { get; set; }
  
        public string Kraj { get; set; }
        public int Wzrost { get; set; }
        public int Waga { get; set; }


        //public string ImieNazwisko
        //{
        //    get { return $"{Imie} {Nazwisko}"; }
        //}
        public string ImieNazwisko => $"{Imie} {Nazwisko}";
    }
}
