using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //tự động tăng id
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string URL { set; get; }

        public int? DisplayOrder { set; get; }

        [Required]
        public int GroupID { set; get; }

        [ForeignKey("GroupID")]
        //khoá ngoại
        public virtual MenuGroup MenuGroup { set; get; }

        [MaxLength(10)]
        public string Target { set; get; }

        public bool Status { set; get; }
    }
}