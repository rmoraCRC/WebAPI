
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SecurityApp.Web.Models
{
    public class Token
    {
        [HiddenInput, DataType("Hidden")]
        public int IdToken { get; set; }
        public int IdUser { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool Status { get; set; }
    }
}