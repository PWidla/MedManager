using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "kj2JMl2N44kjeL992N9zZ1lwedaNZ1aqL";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private List<Admin> _admins = new List<Admin>();
        private List<Doctor> _doctors = new List<Doctor>();
        private List<Patient> _patients = new List<Patient>();
        private List<IUser> _userAccountList = new List<IUser>();

        private readonly ApplicationDbContext _context;
        public JwtTokenHandler(ApplicationDbContext context)
        {
            _context = context;
            _admins?.AddRange(_context.Admins.ToList());
            _doctors?.AddRange(_context.Doctors.ToList());
            _patients?.AddRange(_context.Patients.ToList());
            _userAccountList?.AddRange(_admins);
            _userAccountList?.AddRange(_doctors);
            _userAccountList?.AddRange(_patients);
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.EmailAddress) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;

            //Validation
            var userAccount = _userAccountList.Where(x => x.EmailAddress == authenticationRequest.EmailAddress && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, authenticationRequest.EmailAddress), //toconsider
                new Claim(ClaimTypes.Role, userAccount.Role.ToString()),
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                EmailAddress = userAccount.EmailAddress,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }
    }
}
