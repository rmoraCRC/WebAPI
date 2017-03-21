using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SecurityApp.Web.Utilities;

namespace SecurityApp.Web.Models
{
    public class Application
    {
        [HiddenInput, DataType("Hidden")]
        public int idApplication { get; set; }
        [Required, Display(Name = "Name"), FontAwesome(Name = "fa fa-align-justify")]
        public string description { get; set; }
        [Required, Display(Name = "Creation Date"), FontAwesome(Name = "fa fa-calendar")]
        public DateTime createDateTime { get; set; }
        public bool status { get; set; }
    }
}