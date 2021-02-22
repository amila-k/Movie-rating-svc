using System;

namespace ContractModels
{
    public class Show
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double? AverageRate { get; set; }

        public byte[] Image { get; set; }
    }
}
