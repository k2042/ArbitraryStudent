using ArbitraryStudent.Service.Db;
using ArbitraryStudent.Service.Db.DataObjects;
using ArbitraryStudent.Service.Services.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Services
{
    public class StudentService
    {
        private readonly ArbitraryDbContext _db;
        public StudentService(ArbitraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            var query = _db.Students.AsQueryable();

            return await query
                .OrderBy(o => o.FullName)
                .Select(o => o.MapToEntity())
                .ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            var dataObject = await _db.Students.FindAsync(id);

            if (dataObject == null)
                return null;
            else
                return dataObject.MapToEntity();
        }

        public async Task<Student> NewStudentAsync(Student student)
        {
            var dataObject = student.MapToDo();
            _db.Students.Add(dataObject);
            await _db.SaveChangesAsync();
            return dataObject.MapToEntity();
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var dataObject = await _db.Students.FindAsync(student.Id);

            if (dataObject == null)
                return null;
            else
            {
                dataObject.Update(student);
                await _db.SaveChangesAsync();
                return dataObject.MapToEntity();
            }
        }

        public async Task<Student> DeleteStudentAsync(int id)
        {
            var dataObject = await _db.Students.FindAsync(id);

            if (dataObject == null)
                return null;
            else
            {
                _db.Remove(dataObject);
                await _db.SaveChangesAsync();
                return dataObject.MapToEntity();
            }
        }
    }

    static class OrderExtensions
    {
        public static StudentDo Update(this StudentDo dataObject, Student student)
        {
            dataObject.FullName = student.FullName;
            dataObject.Birthdate = student.Birthdate;
            dataObject.GradeId = student.GradeId;

            return dataObject;
        }

        public static Student MapToEntity(this StudentDo student)
        {
            return new Student
            {
                Id = student.Id,
                FullName = student.FullName,
                Birthdate = student.Birthdate,
                GradeId = student.GradeId
            };
        }

        public static StudentDo MapToDo(this Student student)
        {
            return new StudentDo
            {
                Id = student.Id,
                FullName = student.FullName,
                Birthdate = student.Birthdate,
                GradeId = student.GradeId
            };
        }

    }
}
