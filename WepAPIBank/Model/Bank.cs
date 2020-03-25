using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WepAPIBank.Model
{
    public class Bank
    {
        [Key]
        public int BankID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }

    }
}
