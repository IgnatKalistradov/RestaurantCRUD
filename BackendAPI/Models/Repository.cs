
using Microsoft.EntityFrameworkCore;
using BackendAPI.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BackendAPI.Models
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        private ApplicationDbContext _context;
        private DbSet<TModel> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
        }
        public async Task AddAsync(TModel model)
        {
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            TModel entity = await _dbSet.FindAsync(id) ?? throw new Exception();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> SelectAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TModel>> SelectAsync(QueryOptions<TModel> options)
        {
            IQueryable<TModel> query = _dbSet;

            if(options.Where != null)
            {
                query = query.Where(options.Where);
            }
            if(options.OrderBy != null)
            {
                query = query.OrderBy(options.OrderBy);
            }
            foreach (var include in options.Includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TModel> SelectByIdAsync(int id)
        {
            var key = _context.Model.FindEntityType(typeof(TModel)).FindPrimaryKey().Properties.FirstOrDefault();
            string primaryKeyName = key?.Name;

            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<int>(e, primaryKeyName) == id);
        }

        public async Task<TModel> SelectByIdAsync(int id, QueryOptions<TModel> options)
        {
            IQueryable<TModel> query = _dbSet;

            if(options.Where != null)
            {
                query = query.Where(options.Where);
            }
            if(options.OrderBy != null)
            {
                query = query.OrderBy(options.OrderBy);
            }
            
            foreach(var include in options.Includes)
            {
                query = query.Include(include);
            }

            var key = _context.Model.FindEntityType(typeof(TModel)).FindPrimaryKey().Properties.FirstOrDefault();
            string primaryKeyName = key?.Name;

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, primaryKeyName) == id);
        }

        public async Task UpdateAsync(TModel model)
        {
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
