using Microsoft.IdentityModel.Tokens;
using MyShopApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyShopApi.App.Providers
{
    public class UserManager
    {
        private IConfiguration _configs;
        public UserManager(IConfiguration configs)
        {
            _configs = configs;
        }

        public KeyValuePair<bool,string> CreateToken(User user)
        {
            try
            {
                // Generate Claims
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                };
                // Generate Token
                var securityKey = Encoding.UTF8.GetBytes(
                    _configs.GetValue<string>("Token:SecurityKey"));

                var accessExpiryMinute = _configs.GetValue<int>("Token:AccessExpiryMinutes");

                var cred = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256Signature);

                var tokenObject = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(accessExpiryMinute),
                    signingCredentials: cred);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
                return new KeyValuePair<bool, string>(true, token);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.ToString());
            }
        }
        public Tuple<string, string> CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var passHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var passSalt = hmac.Key;

                return new Tuple<string, string>(
                    Convert.ToBase64String(passHash),
                    Convert.ToBase64String(passSalt));
            }
        }
        public bool VerifyPasswordHash(string password, string passHash, string passSalt)
        {
            if (string.IsNullOrWhiteSpace(passHash) ||
                string.IsNullOrWhiteSpace(passSalt))
                return false;

            var passHashBytes = Convert.FromBase64String(passHash);
            var passSaltBytes = Convert.FromBase64String(passSalt);

            using (var hmac = new HMACSHA256(passSaltBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passHashBytes);
            }
        }
    }
}
