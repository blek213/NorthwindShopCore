using System;
using System.Collections.Generic;

namespace NorthwindShopCore
{
    public partial class Company
    {
        public int Companyid { get; set; }
        public string Name { get; set; }
        public int Ownerid { get; set; }
        public int Typeid { get; set; }
        public int? Profitperyear { get; set; }
        public int? Investorid { get; set; }

        public Investor Investor { get; set; }
        public Owner Owner { get; set; }
        public Companytype Type { get; set; }
    }
}
