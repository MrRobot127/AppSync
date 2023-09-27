using System.Security.Claims;

namespace ERPConnect.Web.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("CreateRole", "False"),
            new Claim("EditRole","False"),
            new Claim("DeleteRole","False")
        };
    }
}
