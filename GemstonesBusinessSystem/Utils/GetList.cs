using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Utils
{
    class GetList<T>
    {
        public static IEnumerable<T> getList(ObservableCollection<T> list)
        {
            foreach (var val in list)
            {
                yield return val;
            }
        }
    }
}
