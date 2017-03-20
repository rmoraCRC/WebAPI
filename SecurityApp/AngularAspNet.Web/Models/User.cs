using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SecurityApp.Web.Utilities;


namespace SecurityApp.Web.Models
{
    public partial class User
    {
        [HiddenInput, DataType("Hidden")]
        public int idUser { get; set; }
        [Required, Display(Name = "Full Name", Prompt = "Full Name (ex: John Doe)..."), FontAwesome(Name = "fa fa-male")]
        public string name { get; set; }
        [Required, Display(Name = "Last Name"), FontAwesome(Name = "fa fa-user")]
        public string lastName { get; set; }
        [Required, FontAwesome(Name = "fa fa-location-arrow")]
        public string address { get; set; }
        [Required, DataType(DataType.EmailAddress), FontAwesome(Name = "fa fa-location-arrow")]
        public string email { get; set; }
        [Required, DataType(DataType.PhoneNumber), FontAwesome(Name = "fa fa-phone")]
        public string phone { get; set; }
        public bool status { get; set; }
        [Required, FontAwesome(Name = "fa fa-male")]
        public string userName { get; set; }
        [Required, DataType(DataType.Password), FontAwesome(Name = "fa fa-key")]
        public string password { get; set; }

    }
}
