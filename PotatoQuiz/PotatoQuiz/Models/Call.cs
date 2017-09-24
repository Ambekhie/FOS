using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace OrderSystem.Models
{
    public class Call
    {
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Restaurant { get; set; }

        public DateTime Time { get; set; }

        public string UserID { get; set; }

        [Column(Order = 1), ForeignKey("UserID")]
        public virtual User FOSUser { get; set; }
    }
}
