using System;
using System.Collections.Generic;

namespace NorthwindShopCore
{
    public partial class Owner
    {
        public Owner()
        {
            Company = new HashSet<Company>();
        }

        public int Ownerid { get; set; }
        public string Name { get; set; }
        public int? Salary { get; set; }

        public ICollection<Company> Company { get; set; }
    }
}
