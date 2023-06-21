using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using STGeneticsService.Domain.Repository;
public class OauthService : IOauthService{
    private readonly IUserRepository _usersRepository;

    private readonly IConfiguration _configuration;

    public OauthService(IUserRepository usersRepository, IConfiguration configuration){
        _usersRepository = usersRepository;
        _configuration = configuration;
    }
    public OauthResponse GetToken(string userName, string password){
       
        Users user = _usersRepository.GetUser(userName, password);
        if(user == null){
            OauthResponse oauthResponse = new OauthResponse();
            oauthResponse.message = "Invalid user name or password";
            oauthResponse.code = "401"; 
            return oauthResponse;
        }
  
       return GenerateToken(user);
    }

    private OauthResponse GenerateToken(Users user ){
        OauthResponse oauthResponse = new OauthResponse();

        if(user.userName == null || user.password == null){
            oauthResponse.message = "Invalid user name or password";
            oauthResponse.code = "401"; 
            return oauthResponse;
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
            );
        var subject = new ClaimsIdentity(new Claim[]{
            new Claim(JwtRegisteredClaimNames.Sub, user.userName),
            new Claim(JwtRegisteredClaimNames.Email, user.userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

        var expires = DateTime.UtcNow.AddMinutes(10);
        oauthResponse.expires_in = expires; 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            oauthResponse.access_token = jwtToken;
            oauthResponse.code = "200";
            oauthResponse.message = "Success";

            return oauthResponse; 
    }
}