using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Services.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public int? GradeId { get; set; }
    }
}
