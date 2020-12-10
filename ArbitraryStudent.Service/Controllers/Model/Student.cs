using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Controllers.Model
{
    public class PostStudent
    {
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class PutStudent : PostStudent
    {
        public int? GradeId { get; set; }
    }
}
