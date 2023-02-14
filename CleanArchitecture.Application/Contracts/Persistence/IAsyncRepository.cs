using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        //Func<T> - IEnumerable - in memory - solo aceptan funciones
        //Expression<Func<T>> - IQueryable - SQL Server - solo aceptan expression functions
       //La implementacion es la misma Where(x => x.property == "value")
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);
    }
}
