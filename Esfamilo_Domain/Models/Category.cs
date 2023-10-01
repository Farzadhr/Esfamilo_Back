using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string CategroyName { get; set; }
    }
}
