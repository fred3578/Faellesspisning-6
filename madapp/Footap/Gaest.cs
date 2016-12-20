using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footap
{
    class Gaest : Hus
    {
        //public Gaest(int husNr) : base(husNr)
       // {
       // }

        public int Barn { get; set; }
        public int Ung { get; set; }
        public int Voksen { get; set; }

        public Gaest(int barn, int ung, int voksen, int husNr)
        {
            Barn = barn;
            Ung = ung;
            Voksen = voksen;
            HusNr = husNr;
        }
        
    }
    
}
