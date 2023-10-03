using WebUI.Models;

namespace WebUI.Areas.Admin.ViewModel
{
    public class UserRoleVM
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
