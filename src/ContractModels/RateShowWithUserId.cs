using System;

namespace ContractModels
{
    public class RateShowWithUserId : RateShow
    {
        public Guid UserId { get; set; }
    }
}
