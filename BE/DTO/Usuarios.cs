using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTO
{
    public class Usuarios
    {
        public int? id { get; set; }
        public string username { get; set; }
        public string? email { get; set; }
        public string? passwordHash { get; set; }
        public DateTime? createdAt { get; set; }
        public bool? isActive { get; set; }
        public string? token { get; set; }

    }


}
