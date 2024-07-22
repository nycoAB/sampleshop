using Microsoft.IdentityModel.Tokens;
using MyShopApi.App;
using MyShopApi.App.Providers;
using MyShopApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyShopApi.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private AppDbContext _db;
        private UserManager _userManager;
        public AuthRepo(
            AppDbContext context,
            UserManager userManager)
        {
            _db = context;
            _userManager = userManager;
        }
        public KeyValuePair<bool, string> LoginUser(string userName, string password)
        {
            try
            {
                // Check User Exist
                var user = _db.Users
                .FirstOrDefault(u => u.Username == userName);
                if (user == null)
                    return new KeyValuePair<bool, string>(false, "User not found!");

                // Check Password
                if (!_userManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return new KeyValuePair<bool, string>(false, "Username or password is incorrect!");

                // Create Token
                var token = _userManager.CreateToken(user);
                return new KeyValuePair<bool, string>(token.Key, token.Value);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.ToString());
            }
        }

        public KeyValuePair<bool, string> RegisterNewUser(string username, string password)
        {
            try
            {
                // Check User Exist
                var currentUser = _db.Users
                .FirstOrDefault(u => u.Username == username);
                if (currentUser != null)
                    return new KeyValuePair<bool, string>
                        (false, "This username has already been selected!");

                // Check Password Length
                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                    return new KeyValuePair<bool, string>
                        (false, "Password is required and must be equal or more than 6 characters!");

                // Encode Passwords
                var passwords = _userManager.CreatePasswordHash(password);
                var newUser = new User
                {
                    Username = username,
                    PasswordHash = passwords.Item1,
                    PasswordSalt = passwords.Item2,
                };

                _db.Users.Add(newUser);
                var saveResult = _db.SaveChanges();
                if(saveResult > 0)
                {
                    var token = _userManager.CreateToken(newUser);
                    return new KeyValuePair<bool, string>(token.Key, token.Value);
                }
                return new KeyValuePair<bool, string>(false, "Register user failed!");

            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.ToString());
            }
            

        }
    }
}
