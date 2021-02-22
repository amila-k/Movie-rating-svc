using System;
using System.Collections.Generic;
using System.Text;

namespace ContractModels
{
    public class ShowParameters
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string FilterText { get; set; }
    }
}
