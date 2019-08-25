using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LinkTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //the server key used to sign the JWT token is here, use more than 16 chars
        private string ServerKeyUsedSign = "FOCUSONE_LINK_COMPANY_LOGON";
        
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Post(userData enParam)
        {
            string oldToken = "";//GET CHECK OLD TOKEN
            string CreateOn = "20190825191300";
            var claims = new Claim[] { new Claim(ClaimTypes.Sid, "555"), new Claim(ClaimTypes.Name, enParam.user), new Claim(ClaimTypes.SerialNumber, enParam.pass), new Claim("CreatedOn", CreateOn) };
            string newJwtToken = GenerateToken(claims);
            removeToken(oldToken);//Clear Old Token 
            return new ObjectResult(new
            {
                status = StatusCodes.Status200OK,
                token = newJwtToken,
                expires = ""
            });
        }


        [HttpPost]
        [Route("api/[controller]/GetDetail")]
        public IActionResult GetDetail(postData enParam)
        {
            var principal = GetPrincipalFromExpiredToken(enParam.token);
            var username = principal.Identity.Name;
            var xxx = principal.Claims;
            string sid = xxx.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            string password = xxx.Where(c => c.Type == ClaimTypes.SerialNumber).Select(c => c.Value).SingleOrDefault();
            return new ObjectResult(new
            {
                name = principal.Identity.Name
            });
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ServerKeyUsedSign));

            var jwt = new JwtSecurityToken(issuer: "Blinkingcaret",
                audience: "Everyone",
                claims: claims, //the user's claims, for example new Claim[] { new Claim(ClaimTypes.Name, "The username"), //... 
                notBefore: DateTime.UtcNow,
                expires: DateTime.MaxValue,//DateTime.UtcNow.AddMinutes(5)
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt); //the method is called WriteToken but returns a string
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ServerKeyUsedSign)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private void removeToken(string oldToken)
        {
            //string username,string oldToken
        }
        
        //[HttpGet]
        //public ActionResult<string> Get()
        //{
        //    return "Value";
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        public class userData
        {
            public string user { get; set; }
            public string pass { get; set; }
        }

        public class postData {
           public string token { get; set; }
        }
    }
}
