using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WebClient
{
    public static class StaticDetails
    {
        public static readonly string ApiBaseUrl = "https://localhost:5001/";
        public static readonly string StudentsUrl = ApiBaseUrl + "Api/Studentet";
        public static readonly string SubjectsUrl = ApiBaseUrl + "Api/Lendet";
        public static readonly string ProfessorsUrl = ApiBaseUrl + "Api/Profesoret";
        public static readonly string ExamsUrl = ApiBaseUrl + "Api/Provimet";
        public static readonly string StudentExamsUrl = ApiBaseUrl + "Api/ProvimetStudenteve";
        public static readonly string BranchesUrl = ApiBaseUrl + "Api/Drejtimet";
        public static readonly string StatusesUrl = ApiBaseUrl + "Api/Statuset";
        public static readonly string AuthenticateUrl = ApiBaseUrl + "Authenticate";
        public static readonly string UsersUrl = ApiBaseUrl + "Api/Users";

        public static string EncodeToBase64String(string username, string password)
        {
            var credentialBytes = Encoding.UTF8.GetBytes(username + ":" + password);
            return Convert.ToBase64String(credentialBytes);
        }

        public static string[] DecodeToken(string token)
        {

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var result = new string[2]
            {
                tokenS.Claims.FirstOrDefault(claim=>claim.Type == "password")?.Value,
                tokenS.Claims.FirstOrDefault(claim=> claim.Type=="username")?.Value
            } ;
            return result;
        }
    }
}
