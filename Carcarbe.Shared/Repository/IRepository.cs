using Carcarbe.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Carcarbe.Shared.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
