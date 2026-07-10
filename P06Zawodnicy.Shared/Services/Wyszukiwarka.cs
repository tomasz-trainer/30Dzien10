using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public class Wyszukiwarka
    {
        private readonly IManagerZawodnikow mz;
        private readonly ManagerTrenerow mt;

        public Wyszukiwarka(IManagerZawodnikow mz, ManagerTrenerow mt)
        {
            this.mz = mz;
            this.mt = mt;
        }
        
        public List<Osoba> WyszukajOsoby(string fragmentTekstu) 
        {
            List<Osoba> osoby = new List<Osoba>();
            osoby.AddRange(mz.WczytajZawodnikow());
            osoby.AddRange(mt.WczytajTrenerow());

            List<Osoba> wyniki = new List<Osoba>();

            foreach (var o in osoby)
            {
                if (o.Imie.ToLower().Contains(fragmentTekstu.ToLower()) || o.Nazwisko.ToLower().Contains(fragmentTekstu.ToLower()))
                    wyniki.Add(o);
                     
            }
            return wyniki;
        }
    }
}
