namespace Restaurant.Data
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<bool> ExistsAsync(int id);
        Task AddAsync(TModel model);
        Task<IEnumerable<TModel>> SelectAllAsync();
        Task<IEnumerable<TModel>> SelectAsync(QueryOptions<TModel> options);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(int id);
        Task<TModel> SelectByIdAsync(int id);
        Task<TModel> SelectByIdAsync(int id, QueryOptions<TModel> options);
        Task<IEnumerable<TModel>> SelectByIdsAsync(IEnumerable<int> ids);
    }
}
