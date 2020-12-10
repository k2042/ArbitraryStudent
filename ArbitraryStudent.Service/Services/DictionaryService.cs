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
    public class DictionaryService
    {
        private readonly ArbitraryDbContext _db;
        public DictionaryService(ArbitraryDbContext db)
        {
            _db = db;
        }

        public async Task<IDictionary<int, string>> GetGradesAsync()
        {
            var query = _db.GradeDictionary.AsQueryable();

            return await query
                .OrderBy(o => o.Name)
                .ToDictionaryAsync(g => g.Id, g => g.Name);
        }

        public async Task<int> NewGradeAsync(string name)
        {
            var dataObject = new GradeDo()
            {
                Name = name
            };

            _db.GradeDictionary.Add(dataObject);
            await _db.SaveChangesAsync();
            return dataObject.Id;
        }

        public async Task DeleteGradeAsync(int id)
        {
            var dataObject = _db.GradeDictionary.First(g => g.Id == id);
            _db.GradeDictionary.Remove(dataObject);
            await _db.SaveChangesAsync();
        }
    }
}
