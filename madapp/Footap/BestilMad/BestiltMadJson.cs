using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footap.BestilMad
{
    class BestiltMadJson : IEnumerable
    {
        public List<BestilMad> MandagsMad { get; set; }
        public List<BestilMad> TirsdagsMad { get; set; }
        public List<BestilMad> OnsdagsMad { get; set; }
        public List<BestilMad> TorsdagsMad { get; set; }

        public BestiltMadJson(List<BestilMad> mandagsMad, List<BestilMad> tirsdagsMad, List<BestilMad> onsdagsMad, List<BestilMad> torsdagsMad)
        {
            MandagsMad = mandagsMad;
            TirsdagsMad = tirsdagsMad;
            OnsdagsMad = onsdagsMad;
            TorsdagsMad = torsdagsMad;
        }


        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
