using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Domain
{
    public class Trener : Osoba
    {    
        public int Id_trenera { get; set; }
       
        public string Kraj { get; set; }
          
        public string ImieNazwisko => $"{Imie} {Nazwisko}";
    }
}
