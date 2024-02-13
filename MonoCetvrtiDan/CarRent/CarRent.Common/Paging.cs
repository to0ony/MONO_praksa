using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Common
{
    public class Paging
    {
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 10;
    }
}
