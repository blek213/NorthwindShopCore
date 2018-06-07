using System;
using System.Collections.Generic;

namespace NorthwindShopCore
{
    public partial class Companytype
    {
        public Companytype()
        {
            Company = new HashSet<Company>();
        }

        public int Companytypeid { get; set; }
        public string Nametype { get; set; }

        public ICollection<Company> Company { get; set; }
    }
}
