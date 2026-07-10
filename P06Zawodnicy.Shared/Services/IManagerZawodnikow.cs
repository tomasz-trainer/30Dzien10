using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public interface IManagerZawodnikow
    {
        Zawodnik[] WczytajZawodnikow();
        string[] PodajKraje();
        double PodajSredniWzrost(string kraj);
        int PodajSredniWiekZawodnikow(string kraj);
        void Dodaj(Zawodnik zawodnik);
        void Usun(Zawodnik zawodnik);
        void Edytuj(Zawodnik edytowany);
        Zawodnik[] PodajZawodnikow(string kraj);

        GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju();
        Zawodnik PodajZawodnika(int v);
    }
}
