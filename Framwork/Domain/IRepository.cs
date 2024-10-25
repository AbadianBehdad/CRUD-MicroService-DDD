using System.Linq.Expressions;

namespace Framwork.Domain
{
    public interface IRepository<TKEY,T> where T : class
    {
        Task Add(T Entity);
        void Delete(T Entity);
        Task<List<T>> GetAll();
        Task<T> Get(TKEY id);
        Task<bool> Exists(Expression<Func<T, bool>> expression);
        Task Save();

    }
}
