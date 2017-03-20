using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityAppDataAccess.Model
{
    [Table("securityapp.Roles")]
    public partial class Role
    {

        #region Properties

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdApplication { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGroup { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUser { get; set; }

        public bool Status { get; set; }

        public bool IsNew { get; set; }

        public virtual Application application { get; set; }

        public virtual Group group { get; set; }

        public virtual User user { get; set; }

        #endregion


    }
}