using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unam.CoHu.Libreria.Model.Web
{
    [Serializable]
    public class ControlItem
    {
        public ControlItem() { }

        public int Id { get; set; }
        public string Label { get; set; }
        public object ItemSource { get; set; }
    }
}
