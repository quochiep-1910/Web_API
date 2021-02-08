using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model.Models
{
    [Table("ContactDetails")]
    public class ContactDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(50)]
        public string Phone { set; get; }

        [StringLength(250)]
        public string Email { set; get; }

        [StringLength(500)]
        public string Website { set; get; }

        [StringLength(500)]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; } //kinh độ

        public double? Lng { set; get; }  //vĩ độ

        public bool Status { set; get; }
    }
}