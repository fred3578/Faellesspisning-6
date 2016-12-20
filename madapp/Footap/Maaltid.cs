using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footap
{
    class Maaltid
    {
        public string Dag { get; set; }
        public string Ret { get; set; }
        public double MadUdgift { get; set; }

        public Maaltid (string dag , string ret , double madudgift)
        {
            Dag = dag;
            Ret = ret;
            MadUdgift = madudgift;
        }

        public override string ToString ()
        {
            return $"{nameof(Dag)}: {Dag}, {nameof(Ret)}: {Ret}, {nameof(MadUdgift)}: {MadUdgift}";
        }


}


}
