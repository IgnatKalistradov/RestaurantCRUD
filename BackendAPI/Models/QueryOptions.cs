using System.Linq.Expressions;

namespace BackendAPI.Models
{
    public class QueryOptions<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>>? Where { get; set; }
        private List<string> _includes = new();
        public Expression<Func<TEntity, TEntity>>? OrderBy { get; set; }
        public List<string> Includes { get => _includes; }
        
        public void AddInclude(string include)
        {
            if (!string.IsNullOrWhiteSpace(include) && !_includes.Contains(include))
            {
                _includes.Add(include);
            }
        }
    }
}