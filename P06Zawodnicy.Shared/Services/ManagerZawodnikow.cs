
using P06Zawodnicy.Shared.Domain;
using P08PolaczenieZBaza;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public class ManagerZawodnikow : IManagerZawodnikow
    {
        //PolaczenieZBaza pzb;
        //public ManagerZawodnikow()
        //{
        //    pzb = new PolaczenieZBaza();
        //}
        PolaczenieZBaza pzb = new PolaczenieZBaza();



        public Zawodnik[] WczytajZawodnikow()
        {
           object[][] dane =  pzb.WyslijPolecenieSQL("select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost,waga from zawodnicy");

           mapujZawodnikow(dane, out Zawodnik[] zawodnicy);
           return zawodnicy;
        }

        private void mapujZawodnikow(object[][] dane, out Zawodnik[] zawodnicy)
        {
            zawodnicy = new Zawodnik[dane.Length];

            for (int i = 0; i < dane.Length; i++)
            {
                var w = dane[i]; // i-ty wiersz 
                Zawodnik z = new Zawodnik();
                z.Id_zawodnika = (int)w[0];

                if (w[1] != DBNull.Value)
                    z.Id_trenera = (int)w[1];
 
                z.Imie = (string)w[2];
                z.Nazwisko = (string)w[3];
                z.Kraj = (string)w[4];

                if (w[5] != DBNull.Value)
                    z.DataUrodzenia = (DateTime)w[5];

                if (w[6] != DBNull.Value)
                    z.Wzrost = (int)w[6];

                if (w[7] != DBNull.Value)
                    z.Waga = (int)w[7];

                zawodnicy[i] = z;
            }

        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            object[][] dane = pzb.WyslijPolecenieSQL($"select id_zawodnika, id_trenera, imie, nazwisko, kraj, data_ur, wzrost,waga from zawodnicy where kraj='{kraj}'");

            mapujZawodnikow(dane, out Zawodnik[] zawodnicy);
            return zawodnicy;
        }


        public string[] PodajKraje()
        {
           object[][] dane = pzb.WyslijPolecenieSQL("select distinct kraj from zawodnicy");

            string[] kraje = new string[dane.Length];
            for (int i = 0; i < dane.Length; i++)
                kraje[i] = (string)dane[i][0];

            return kraje;
        }


        public double PodajSredniWzrost(string kraj)
        {
            object[][] dane = pzb.WyslijPolecenieSQL($"select avg(wzrost*1.0) from zawodnicy where kraj = '{kraj}'");
            return (double)dane[0][0];
        }

         public int PodajSredniWiekZawodnikow(string kraj)
        {
            SqlConnection connection = new SqlConnection(pzb.ConnectionString);
            SqlCommand command = new SqlCommand("SredniWiekZawodnikow", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Kraj", kraj));

            SqlParameter sredniWiekParam = new SqlParameter("@SredniWiek", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output,
            };
            command.Parameters.Add(sredniWiekParam);

            connection.Open();
            command.ExecuteNonQuery();

            if (sredniWiekParam.Value != DBNull.Value)
            {
                return (int)sredniWiekParam.Value;
            }
            else
            {
                throw new Exception("Nie udało sie policzyć średniego wieku ");
            }
        }




        ////sortowanie bąbelkowe (ang. Bubble Sort).
        //public void PosorotujZawodnikowPoNazwisku(Zawodnik[] posortowaniZawodnicy)
        //{
         
        //}

         // podataność na atak SQL injection
         // np: podczas podawania kraju podać:
         // POL','20240101',1,1); drop table zawodnicy--
        public void Dodaj(Zawodnik zawodnik)
        {
             
            string szablon = "insert into zawodnicy (id_trenera,imie, nazwisko,kraj,data_ur,wzrost,waga) values ({0},'{1}','{2}','{3}','{4}',{5},{6})";
            string sql = string.Format(szablon,
                zawodnik.Id_trenera == null ? "null" : zawodnik.Id_trenera.ToString(),
                zawodnik.Imie, zawodnik.Nazwisko, zawodnik.Kraj,
                zawodnik.DataUrodzenia.ToString("yyyyMMdd"),
                zawodnik.Wzrost, zawodnik.Waga
                );

            pzb.WyslijPolecenieSQL(sql);

        }

        public void Usun(Zawodnik zawodnik)
        {
            pzb.WyslijPolecenieSQL($"delete zawodnicy where id_zawodnika = {zawodnik.Id_zawodnika}");
        }

        public void Edytuj(Zawodnik edytowany)
        {
            string sql = $@"update zawodnicy set 
                id_trenera = {(edytowany.Id_trenera == null ? "null" : edytowany.Id_trenera.ToString())},
                imie = '{edytowany.Imie}', 
                nazwisko= '{edytowany.Nazwisko}',
                kraj='{edytowany.Kraj}',
                data_ur='{edytowany.DataUrodzenia.ToString("yyyyMMdd")}',
                wzrost ={edytowany.Wzrost}, 
                waga ={edytowany.Waga}
                where id_zawodnika ={edytowany.Id_zawodnika}";

            pzb.WyslijPolecenieSQL(sql);
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

// komunikacja z bazą danych może przebiegac na 3 sposoby :
//1) Polecenia SQL , parametryzacja zapytań
//2) procedury wbudowane 
//3) ORM (object-relation-mapping) 