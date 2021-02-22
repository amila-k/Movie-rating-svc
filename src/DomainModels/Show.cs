using System;
using System.Collections.Generic;

namespace DomainModels
{
    public class Show
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double? AverageRate { get; set; }

        public ICollection<ShowRate> ShowRates { get; set; }

        public ShowType ShowType { get; set; }
    }
}
