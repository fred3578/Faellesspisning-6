using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footap.BestilMad
{
    class BestilMad : Hus
    {                                            
        public int VoksenAntal { get; set; }
        public int UngAntal { get; set; }
        public int BarnAntal { get; set; }
        public int SpaedAntal { get; set; }

        public BestilMad(int husNr, int voksenAntal, int ungAntal, int barnAntal, int spaedAntal)
        {
            HusNr = husNr;
            VoksenAntal = voksenAntal;
            UngAntal = ungAntal;
            BarnAntal = barnAntal;
            SpaedAntal = spaedAntal;
        }

        public double GetKuverter()
        {
            return VoksenAntal*1 + UngAntal*0.5 + BarnAntal*0.25;
        }

    }
}
