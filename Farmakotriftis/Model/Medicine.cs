using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Farmakotriftis.Model
{
    [Table("Medicine")]
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Packing { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
