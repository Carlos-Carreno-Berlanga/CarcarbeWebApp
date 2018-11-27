using Carcarbe.Shared.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarbe.Shared.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T item);
        Task AddAsync(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
