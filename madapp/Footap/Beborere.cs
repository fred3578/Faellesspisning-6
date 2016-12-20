using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footap
{
    class Beborere : Hus
    {

        public ObservableCollection<Beborere> Beboreres { get; set; }
        public ObservableCollection<Hus> Huses { get; set; }


        public string Navn { get; set; }
        public int Alder { get; set; }
        public int HusNr { get; set; }

        public Beborere(string navn, int alder, int husNr)
        {
           
            Navn = navn;
            Alder = alder;
            HusNr = husNr;

        }


        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Navn)}: {Navn}, {nameof(Alder)}: {Alder}, {nameof(HusNr)}: {HusNr}";
        }
    }
}
