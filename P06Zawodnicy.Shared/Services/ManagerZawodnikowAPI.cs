using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    internal class ManagerZawodnikowAPI : IManagerZawodnikow
    {
        public void Dodaj(Zawodnik zawodnik)
        {
            throw new NotImplementedException();
        }

        public void Edytuj(Zawodnik edytowany)
        {
            throw new NotImplementedException();
        }

        public string[] PodajKraje()
        {
            throw new NotImplementedException();
        }

        public int PodajSredniWiekZawodnikow(string kraj)
        {
            throw new NotImplementedException();
        }

        public double PodajSredniWzrost(string kraj)
        {
            throw new NotImplementedException();
        }

        public GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju()
        {
            throw new NotImplementedException();
        }

        public Zawodnik PodajZawodnika(int v)
        {
            throw new NotImplementedException();
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            throw new NotImplementedException();
        }

        public void Usun(Zawodnik zawodnik)
        {
            throw new NotImplementedException();
        }

        public Zawodnik[] WczytajZawodnikow()
        {
            throw new NotImplementedException();
        }
    }
}
