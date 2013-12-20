using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sela.TalkBack.Common.Entities
{
    public class ConsolidatedResponse<T>
    {
        public List<T> Items { get; set; }
    }
}
