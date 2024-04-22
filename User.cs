using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashing_salting
{
    public class User
    {
        [Key]
        public int id { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string Salt { set; get; }
    }
}
