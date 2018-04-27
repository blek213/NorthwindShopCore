using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindShopCore.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "http://localhost:50915/";
        public const string AUDIENCE = "http://localhost:50915/"; 
        const string KEY = "mysupersecret_secretkey!123";   // key for decryption
        public const int LIFETIME = 45; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
