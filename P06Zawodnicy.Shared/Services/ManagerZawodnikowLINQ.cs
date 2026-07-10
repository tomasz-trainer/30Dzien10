using AutoMapper;
using P06Zawodnicy.Shared.Data;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public class ManagerZawodnikowLINQ : IManagerZawodnikow
    {
          private void mapujNaZawodnikaDb(Zawodnik z, ZawodnikDb zdb)
          {
            zdb.Id_zawodnika = z.Id_zawodnika;
            zdb.Imie = z.Imie;
            zdb.Nazwisko = z.Nazwisko;
            zdb.Kraj = z.Kraj;
            zdb.Data_ur = z.DataUrodzenia;
            zdb.Wzrost = z.Wzrost;
            zdb.Waga = z.Waga;
            zdb.Id_trenera = z.Id_trenera;
          }

        public Zawodnik[] mapujZawodnikow(params ZawodnikDb[] dane)
        {
            Zawodnik[] tab = new Zawodnik[dane.Length];
            for (int i = 0; i < dane.Length; i++)
            {
                tab[i] = new Zawodnik()
                {
                    Id_zawodnika = dane[i].Id_zawodnika,
                    Id_trenera = dane[i].Id_trenera,
                    Imie = dane[i].Imie,
                    Nazwisko = dane[i].Nazwisko,
                    Kraj = dane[i].Kraj,
                    DataUrodzenia = dane[i].Data_ur,
                    Wzrost = dane[i].Wzrost,
                    Waga = dane[i].Waga,
                };
            }
            return tab;
        }

        public void Dodaj(Zawodnik z) // zawodnik domain uzyawny na UI
        {
            var zdb = new ZawodnikDb(); // tworzymy nowego zawodnika bazodanowego 
            mapujNaZawodnikaDb(z, zdb);
            ModelBazyDataContext db = new ModelBazyDataContext();
            db.ZawodnikDb.InsertOnSubmit(zdb); //zapisujemy w bazie danych
            db.SubmitChanges();
        }



        public void Edytuj(Zawodnik edytowany)
        {
            ModelBazyDataContext db = new ModelBazyDataContext();
            ZawodnikDb zd = db.ZawodnikDb.FirstOrDefault(x => x.Id_zawodnika == edytowany.Id_zawodnika);
            mapujNaZawodnikaDb(edytowany, zd);
            db.SubmitChanges();
        }

        public string[] PodajKraje()
        {
            return new ModelBazyDataContext()
             .ZawodnikDb
             .GroupBy(x => x.Kraj)
             .Select(x => x.Key)
             .ToArray();
        }

        public int PodajSredniWiekZawodnikow(string kraj)
        {
            ModelBazyDataContext db = new ModelBazyDataContext();

            double sredniWiek = db.ZawodnikDb
                .Where(x => x.Kraj.Equals(kraj))
                .Select(x => DateTime.Now.Year - x.Data_ur.Year)
                .Average();

            return Convert.ToInt32(sredniWiek);
        }

        public double PodajSredniWzrost(string kraj)
        {
            return new ModelBazyDataContext()
                .ZawodnikDb
                .Where(x => x.Kraj == kraj)
                .Average(x => x.Wzrost);
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            var zawodnicyDb = new ModelBazyDataContext()
             .ZawodnikDb
             .Where(x => x.Kraj == kraj)
             .ToArray();

             return mapujZawodnikow(zawodnicyDb);      
        }

        public void Usun(Zawodnik zawodnik)
        {
            ModelBazyDataContext db = new ModelBazyDataContext();
            var usuwany = db.ZawodnikDb.FirstOrDefault(x => x.Id_zawodnika == zawodnik.Id_zawodnika);
            db.ZawodnikDb.DeleteOnSubmit(usuwany);
            db.SubmitChanges();
        }

        public Zawodnik[] WczytajZawodnikow()
        {
            return mapujZawodnikow(new ModelBazyDataContext().ZawodnikDb.ToArray());
        }
        //public Zawodnik[] WczytajZawodnikow()
        //{
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        //    IMapper mapper = config.CreateMapper();

        //    var zawodnicyDb = new ModelBazyDataContext().ZawodnikDb.ToArray();
        //    return mapper.Map<Zawodnik[]>(zawodnicyDb);
        //}


        public GrupaKraju[] PodajSredniWzrostDlaKazdegoKraju()
        {
             return 
                new ModelBazyDataContext()
                .ZawodnikDb
                .GroupBy(x=>x.Kraj)
                .Select(x=>new GrupaKraju
                {
                    Kraj = x.Key,
                    SredniWzrost = x.Average(y=>y.Wzrost)
                }).ToArray();
        }

        public Zawodnik PodajZawodnika(int id)
        {
            var z = new ModelBazyDataContext()
                .ZawodnikDb.FirstOrDefault(x => x.Id_zawodnika == id);
            return mapujZawodnikow(z).FirstOrDefault();
        }

        public Zawodnik[] PodajZawodnikowFiltr(string szukanaFraza)
        {
            szukanaFraza = szukanaFraza.ToLower();
            var db = new ModelBazyDataContext();

             var zawodnicy = db.ZawodnikDb
                .Where(x=>
                   x.Imie.ToLower().Contains(szukanaFraza) ||
                   x.Nazwisko.ToLower().Contains(szukanaFraza) ||
                   x.Kraj.ToLower().Contains(szukanaFraza) ||
                   x.Data_ur.Year.ToString().Contains(szukanaFraza) ||
                   x.Data_ur.Month.ToString().Contains(szukanaFraza) ||
                   x.Data_ur.Day.ToString().Contains(szukanaFraza) ||
                   x.Wzrost.ToString().Contains(szukanaFraza) ||
                   x.Waga.ToString().Contains(szukanaFraza))
                .ToArray();

            return mapujZawodnikow(zawodnicy);


        }
    }
}
