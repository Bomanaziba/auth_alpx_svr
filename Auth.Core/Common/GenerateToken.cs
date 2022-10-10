

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Core.Common
{

    public class GenerateToken
    {
        public static string GenerateAccessToken(Claim[] claims, string jwtToken = "")
        {
            try
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(3),
                    Issuer = "alpx.svr",
                    Audience = "alpx.svr",
                    TokenType = Constant.TokenType,
                    SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var access_token = tokenHandler.WriteToken(token);

                return access_token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string GenerateRefreshToken(string jwtToken = "")
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}