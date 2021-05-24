using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class Register
    {
        [Required, StringLength(50,MinimumLength=4,ErrorMessage="the user name must at least 4 character and  max 50")]
        public string UserName { get; set; }
        [Required, StringLength(50,MinimumLength=4,ErrorMessage="the password must at least 4 character and  max 50")]
        public string Password { get; set; }

    }
}