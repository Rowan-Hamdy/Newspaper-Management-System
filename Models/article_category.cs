using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NewspaperCMS.Models
{
    [Table("article_category")]
    public partial class article_category
    {
        [Key]
        public int article_id { get; set; }
        [Key]
        [StringLength(20)]
        [Unicode(false)]
        public string category_name { get; set; }
        [StringLength(20)]
        [Unicode(false)]


        [ForeignKey("article_id")]
        [InverseProperty("article_categories")]
        public virtual article article { get; set; }
        [ForeignKey("category_name")]
        [InverseProperty("article_categories")]
        public virtual category category_nameNavigation { get; set; }
    }
}