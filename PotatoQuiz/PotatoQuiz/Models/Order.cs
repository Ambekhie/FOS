using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;

namespace OrderSystem.Models
{
    public class Order
    {
        [Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Meal { get; set; }

        [Required]
        public string Side { get; set; }

        [Required]
        public string Dessert { get; set; }

        [Required]
        public string Drink { get; set; }

        public string UserID { get; set; }

        public int CallID { get; set; }

        [Column(Order = 1), ForeignKey("UserID")]
        public virtual User FOSUser { get; set; }

        [Column(Order = 2), ForeignKey("CallID")]
        public virtual Call Call { get; set; }
    }
}
