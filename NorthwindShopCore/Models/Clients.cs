using System;
using System.Collections.Generic;

namespace NorthwindShopCore.Models
{
    public partial class Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DatimeRegister { get; set; }
    }
}
