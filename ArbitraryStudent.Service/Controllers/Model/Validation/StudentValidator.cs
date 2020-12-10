using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Controllers.Model.Validation
{
    public class PostStudentValidator<T> : AbstractValidator<T> where T : PostStudent
    {
        public PostStudentValidator()
        {
            RuleFor(o => o.FullName)
                .NotEmpty()
                .WithMessage("Student name should not be empty");

            RuleFor(o => o.FullName)
                .MaximumLength(250)
                .WithMessage("Student name should not exceed 250 symbols");
        }
    }

    public class PutStudentValidator<T> : PostStudentValidator<T> where T : PutStudent
    {
        public PutStudentValidator()
        {
            // This will validate all the base validator is configured for
            // and could validate some other values supplied with PUT method.
            // Since there is no new values to validate when updating a student,
            // let it lie here empty, merely demonstrating a possibility.
        }
    }
}
