using System;

namespace DomainModels
{
    public class ShowRate
    {
        public int ShowId { get; set; }

        public Show Show { get; set; }

        public Guid UserId { get; set; }

        public int Rate { get; set; }
    }
}
