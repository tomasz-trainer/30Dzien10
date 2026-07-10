using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public class ManagerZawodnikowLocal : IManagerZawodnikow
    {
        private List<Zawodnik> zawodnicyCache;
        private string sciezka = @"C:\dane\Zawodnicy.txt";

 
        public Zawodnik[] WczytajZawodnikow()
        {
           // this.sciezka = sciezka;
            //string[] wiersze = File.ReadAllText(sciezka).Split(new string[1] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
            string[] wiersze =  File.ReadAllLines(sciezka);

            Zawodnik[] zawodnicy = new Zawodnik[wiersze.Length-1];

            for (int i = 1; i < wiersze.Length; i++)
            {
                string[] komorki =  wiersze[i].Split(',');

                Zawodnik z = new Zawodnik();
                z.Id_zawodnika = int.Parse(komorki[0]);
                if (!string.IsNullOrEmpty(komorki[1]))
                    z.Id_trenera = int.Parse(komorki[1]);

                z.Imie = komorki[2];
                z.Nazwisko = komorki[3];
                z.Kraj = komorki[4];
                z.DataUrodzenia = DateTime.Parse(komorki[5]);
                z.Wzrost = int.Parse(komorki[6]);
                z.Waga = int.Parse(komorki[7]);

                zawodnicy[i - 1] = z;

            }
            zawodnicyCache = zawodnicy.ToList();
            return zawodnicy;
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            if (zawodnicyCache == null)
                throw new Exception("Najpierw wczytaj zawodnikow");

            List<Zawodnik> zawodnicy = new List<Zawodnik>();
            foreach (var z in zawodnicyCache)
                if (z.Kraj == kraj)
                    zawodnicy.Add(z);

            return zawodnicy.ToArray();
        }


        public string[] PodajKraje()
        {
            // unikam ponownego wczytania danych dzieki zastosowaniu cache'u
            // Zawodnik[] zawodnicy = WczytajZawodnikow();

            if (zawodnicyCache == null)
                throw new Exception("Najpierw wczytaj zawodnikow");

            HashSet<string> kraje = new HashSet<string>();
            foreach (var z in zawodnicyCache)
                kraje.Add(z.Kraj);

            // konwersja HashSet do list aby móc sortować
            List<string> posortowaneKraje = kraje.ToList();
            posortowaneKraje.Sort(); // sortowanie alfabetycznie 
                                     //posortowaneKraje.Reverse(); // ewentualnie mozna odwrócić kolekność

            return posortowaneKraje.ToArray();
        }


        public double PodajSredniWzrost(string kraj)
        {
            Zawodnik[] zawodnicy = PodajZawodnikow(kraj);
            double suma = 0;
            foreach (var z in zawodnicy)
                suma += z.Wzrost;

            return suma / zawodnicy.Length;
        }

        //sortowanie bąbelkowe (ang. Bubble Sort).
        public void PosorotujZawodnikowPoNazwisku(Zawodnik[] posortowaniZawodnicy)
        {
            for (int i = 0; i < posortowaniZawodnicy.Length - 1; i++)
            {
                for (int j = 0; j < posortowaniZawodnicy.Length - i - 1; j++)
                {
                    if (string.Compare(posortowaniZawodnicy[j].Nazwisko, posortowaniZawodnicy[j + 1].Nazwisko) > 0)
                    {
                        // zamiana miejscami 
                        Zawodnik temp = posortowaniZawodnicy[j];
                        posortowaniZawodnicy[j] = posortowaniZawodnicy[j + 1];
                        posortowaniZawodnicy[j + 1] = temp;
                    }
                }
            }
        }

        public void Dodaj(Zawodnik zawodnik)
        {
            int maksId = 0;
            foreach (var z in zawodnicyCache)
                if (z.Id_zawodnika > maksId)
                    maksId = z.Id_zawodnika;

            zawodnik.Id_zawodnika = maksId+1;
            zawodnicyCache.Add(zawodnik);

            zapisz();
        }

        public void Usun(Zawodnik zawodnik)
        {
            Zawodnik zawodnikDoUsuniecia = wyszukajZawodnika(zawodnik);
            zawodnicyCache.Remove(zawodnikDoUsuniecia);
            zapisz();
        }

        public void Edytuj(Zawodnik zawodnik)
        {
            Zawodnik zawodnikDoEdycji = wyszukajZawodnika(zawodnik);

            zawodnikDoEdycji.Id_trenera = zawodnik.Id_trenera;
            zawodnikDoEdycji.Imie = zawodnik.Imie;
            zawodnikDoEdycji.Nazwisko = zawodnik.Nazwisko;
            zawodnikDoEdycji.Kraj = zawodnik.Kraj;
            zawodnikDoEdycji.DataUrodzenia = zawodnik.DataUrodzenia;
            zawodnikDoEdycji.Wzrost = zawodnik.Wzrost;
            zawodnikDoEdycji.Waga = zawodnik.Waga;

            zapisz();
        }

        private Zawodnik wyszukajZawodnika(Zawodnik zawodnik)
        {
            int idEdytowanego = zawodnik.Id_zawodnika;
            Zawodnik zawodnikDoEdycji = null;
            foreach (var z in zawodnicyCache)
                if (z.Id_zawodnika == idEdytowanego)
                {
                    zawodnikDoEdycji = z;
                    break;
                }

            return zawodnikDoEdycji;
        }


        private void zapisz()
        {
            string naglowek = "id_zawodnika,id_trenera,imie,nazwisko,kraj,data_ur,wzrost,waga";
            string szablon = "{0},{1},{2},{3},{4},{5},{6},{7}";

            StringBuilder sb = new StringBuilder(naglowek + Environment.NewLine);
            foreach (var z in zawodnicyCache)
            {
                string wiersz = string.Format(szablon,
                    z.Id_zawodnika,z.Id_trenera, z.Imie, z.Nazwisko, z.Kraj,
                    z.DataUrodzenia.ToString("yyyy-MM-dd"), z.Wzrost, z.Waga);

                sb.AppendLine(wiersz);
            }
            File.WriteAllText(sciezka, sb.ToString(),Encoding.UTF8);
        }



        public int PodajSredniWiekZawodnikow(string kraj)
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

        public Zawodnik[] PodajZawodnikowFiltr(string szukanaFraza)
        {
            throw new NotImplementedException();
        }
    }
}
