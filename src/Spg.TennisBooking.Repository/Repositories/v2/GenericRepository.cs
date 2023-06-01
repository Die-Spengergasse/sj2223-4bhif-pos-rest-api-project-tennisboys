using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure.v2;

namespace Spg.TennisBooking.Repository.Repositories.v2
{
    public class GenericRepository : IRepositoryRead, IRepositoryWrite
    {
        private readonly TennisBookingContext _context;
        public GenericRepository(TennisBookingContext Db)
        {
            _context = Db;
        }

        public T? FindById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update<T>(T ent) where T : class
        {
            _context.Set<T>().Update(ent);
            _context.SaveChanges();
        }
    }

    public interface IRepositoryRead
    {
        T? FindById<T>(int id) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
    }

    public interface IRepositoryWrite
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T ent) where T : class;
    }
}