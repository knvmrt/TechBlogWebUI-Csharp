using Microsoft.AspNetCore.Identity;

namespace WebUI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }
        public string AboutAuther { get; internal set; }
    }
}
