using Esfamilo_Domain.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Domain.Models
{
    public class Friend : BaseEntity
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
