using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BE.Request
{
    public class StudentsRequest
    {
        public int? StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

    }

    public class StudentsInsertRequest
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
