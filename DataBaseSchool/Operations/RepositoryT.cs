using DataBaseSchool.Model;
using StudentCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseSchool.Operations
{
    public class StudentCore<T> : IStudentCore<T> where T : class
    {
        protected readonly SchoolContext _context;

        protected StudentCore(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        public async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task  Remove(Guid id)
        {
            var result = await Get(id);
            _context.Set<T>().Remove(result);
            await _context.SaveChangesAsync();

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        public async Task Update(Guid id, T newEntity)
        {

            var result = await Get(id);
            _context.Entry(result).CurrentValues.SetValues(newEntity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
    }
}
