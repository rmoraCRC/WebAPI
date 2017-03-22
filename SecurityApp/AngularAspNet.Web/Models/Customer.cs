
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SecurityApp.Web.Models
{
    public class Customer
    {
        [HiddenInput, DataType("Hidden")]
        public int idCompany { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string status { get; set; }
    }
}