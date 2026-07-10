using P06Zawodnicy.Shared.Domain;
using P08PolaczenieZBaza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared.Services
{
    public class ManagerTrenerow
    {
        PolaczenieZBaza pzb = new PolaczenieZBaza();
        public Trener[] WczytajTrenerow()
        {
            object[][] dane = pzb.WyslijPolecenieSQL("select id_trenera, imie, nazwisko from trenerzy");
            Trener[] trenerzy = new Trener[dane.Length];
            for (int i = 0; i < dane.Length; i++)
            {
                trenerzy[i] = new Trener()
                {
                    Id_trenera = (int)dane[i][0],
                    Imie = (string)dane[i][1],
                    Nazwisko = (string)dane[i][2],
                };
            }
            return trenerzy;

        }
    }
}
