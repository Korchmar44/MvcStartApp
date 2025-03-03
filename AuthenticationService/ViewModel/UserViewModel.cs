using AuthenticationService.Models;
using System.Net.Mail;

namespace AuthenticationService.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public bool FromRussia { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            FullName = GetFullName(user.FirstName, user.LastName);
            FromRussia = GetFromRussiaValue(user.Email);
        }

        private string? GetFullName(string? firstName, string? lastName)
        {
            return String.Concat(firstName, " ", lastName);
        }

        private bool GetFromRussiaValue(string email)
        {
            MailAddress mailAddress = new MailAddress(email);

            if (mailAddress.Host.Contains(".ru"))
                return true;
            return false;
        }
    }
}
