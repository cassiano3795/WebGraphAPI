﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtSecurity.Classes
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(string secretKey)
        {
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
            Key = new SymmetricSecurityKey(secretKeyBytes);


            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
