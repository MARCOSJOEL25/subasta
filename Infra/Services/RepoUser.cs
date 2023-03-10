using core.Interface;
using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using subasta.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public class RepoUser : IRepoUser
    {
        private ApplicationContext _db;
        private readonly IConfiguration _configuration;

        public RepoUser(ApplicationContext db, IConfiguration configuration) 
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<string> login(string userName, string password)
        {
            var user = await _db.user.FirstOrDefaultAsync(x => x.userName.ToLower().Equals(userName.ToLower()));

            if (user == null)
            {
                return "nouser";
            } else if (!CheckPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }else
            {
                return CrearToken(user);
            }
        }

        public async Task<int> Register(user user, string password)
        {
            try
            {
                if (await UserExiste(user.userName))
                {
                    return -1;
                }

                crearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;

                await _db.user.AddAsync(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception)
            {
                return -500;
            }
            
        }

        public async Task<bool> UserExiste(string username)
        {
            return await _db.user.AnyAsync(x => x.userName.ToLower().Equals(username.ToLower()));
        }

        private void crearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512() )
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++) 
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }

                return true;
            }
        }

        private string CrearToken(user user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                                        GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
