using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Db.DataObjects
{
    public class StudentDo
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public int? GradeId { get; set; }
    }
}
