
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SecurityApp.Web.Models
{
    public class Group
    {
        [HiddenInput, DataType("Hidden")]
        public int IdGroup { get; set; }
        public string Description { get; set; }

    }
}