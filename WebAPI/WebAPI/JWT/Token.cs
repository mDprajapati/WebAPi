using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using WebAPI.Models;
using WebAPI.ViewModel;

namespace WebAPI.JWT
{
    public class Token
    {
        public string GenerateToken(Employee employee)
        {
            string key = "my_secret_key_12345";
            var issuer = "https://localhost:44363";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim(JwtRegisteredClaimNames.NameId, employee.ID.ToString()));
            permClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, employee.Name));
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Email, employee.Email));

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(issuer, issuer, permClaims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            
            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}