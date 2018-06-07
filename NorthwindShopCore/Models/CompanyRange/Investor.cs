using System;
using System.Collections.Generic;

namespace NorthwindShopCore
{
    public partial class Investor
    {
        public Investor()
        {
            Company = new HashSet<Company>();
        }

        public int Investorid { get; set; }
        public string Name { get; set; }
        public int? Companyid { get; set; }
        public int? Capital { get; set; }

        public ICollection<Company> Company { get; set; }
    }
}
